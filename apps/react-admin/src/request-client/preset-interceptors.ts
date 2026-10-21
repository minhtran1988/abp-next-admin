import type { RequestClient } from "./request-client";
import type { MakeErrorMessageFn, ResponseInterceptorConfig } from "./types";

import { t as $t } from "@/locales/i18n";

import axios from "axios";

export const authenticateResponseInterceptor = ({
	client,
	doReAuthenticate,
	doRefreshToken,
	enableRefreshToken,
	formatToken,
}: {
	client: RequestClient;
	doReAuthenticate: () => Promise<void>;
	enableRefreshToken: boolean;
	doRefreshToken: () => Promise<string>;
	formatToken: (token: string) => null | string;
}): ResponseInterceptorConfig => {
	return {
		rejected: async (error) => {
			const { config, response } = error;
			// If it is not a 401 error, throw an exception directly
			if (response?.status !== 401) {
				throw error;
			}
			// Determine whether the refreshToken function is enabled
			// If it is not enabled or the request has been retried, jump directly to re-login.
			if (!enableRefreshToken || config.__isRetryRequest) {
				await doReAuthenticate();
				throw error;
			}
			// If the token is being refreshed, add the request to the queue and wait for the refresh to complete
			if (client.isRefreshing) {
				return new Promise((resolve) => {
					client.refreshTokenQueue.push((newToken: string) => {
						config.headers.Authorization = formatToken(newToken);
						resolve(client.request(config.url, { ...config }));
					});
				});
			}

			// Mark starts refreshing token
			client.isRefreshing = true;
			// Mark the current request as a retry request to avoid infinite loops
			config.__isRetryRequest = true;

			try {
				const newToken = await doRefreshToken();

				// Handle requests in the queue
				client.refreshTokenQueue.forEach((callback) => callback(newToken));
				// Clear the queue
				client.refreshTokenQueue = [];

				return client.request(error.config.url, { ...error.config });
			} catch (refreshError) {
				// If refreshing the token fails, handle the error (such as forcing logout or jumping to the login page)
				client.refreshTokenQueue.forEach((callback) => callback(""));
				client.refreshTokenQueue = [];
				console.error("Refresh token failed, please login again.");
				await doReAuthenticate();

				throw refreshError;
			} finally {
				client.isRefreshing = false;
			}
		},
	};
};

export const errorMessageResponseInterceptor = (makeErrorMessage?: MakeErrorMessageFn): ResponseInterceptorConfig => {
	return {
		rejected: (error: any) => {
			if (axios.isCancel(error)) {
				return Promise.reject(error);
			}

			const err: string = error?.toString?.() ?? "";
			let errMsg = "";
			if (err?.includes("Network Error")) {
				errMsg = $t("ui.fallback.http.networkError");
			} else if (error?.message?.includes?.("timeout")) {
				errMsg = $t("ui.fallback.http.requestTimeout");
			}
			if (errMsg) {
				makeErrorMessage?.(errMsg, error);
				return Promise.reject(error);
			}

			let errorMessage = "";
			const status = error?.response?.status;

			switch (status) {
				case 400: {
					errorMessage = $t("ui.fallback.http.badRequest");
					break;
				}
				case 401: {
					errorMessage = $t("ui.fallback.http.unauthorized");
					break;
				}
				case 403: {
					errorMessage = $t("ui.fallback.http.forbidden");
					break;
				}
				case 404: {
					errorMessage = $t("ui.fallback.http.notFound");
					break;
				}
				case 408: {
					errorMessage = $t("ui.fallback.http.requestTimeout");
					break;
				}
				default: {
					errorMessage = $t("ui.fallback.http.internalServerError");
				}
			}
			makeErrorMessage?.(errorMessage, error);
			return Promise.reject(error);
		},
	};
};
