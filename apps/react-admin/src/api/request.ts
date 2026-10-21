import { authenticateResponseInterceptor, errorMessageResponseInterceptor, RequestClient } from "@/request-client";
import useLocaleStore from "@/store/localeI18nStore";
import useUserStore from "@/store/userStore";
import userStore from "@/store/userStore";
import { mapLocaleToAbpLanguageFormat } from "@/utils";
import { toast } from "sonner";
import { refreshToken } from "./account/token";
import { wrapperResult } from "@/utils/abp/request";
import { handleOAuthError } from "@/utils/abp/handleOAuthError";
import useAbpStore from "@/store/abpCoreStore";

const requestClient = new RequestClient({
	baseURL: import.meta.env.VITE_APP_BASE_API,
});

/**
 * Re-authentication logic
 */
async function doReAuthenticate() {
	console.warn("Access token or refresh token is invalid or expired. ");
	// Log out directly
	userStore.getState().actions.clearUserInfoAndToken();
}

/**
 * Refresh token logic
 */
async function doRefreshToken() {
	console.debug("try -> Refresh token");

	const { userToken } = useUserStore.getState();
	if (!userToken.refreshToken) {
		console.warn("No refresh token available.");
		return "";
	}

	try {
		const res = await refreshToken({ refreshToken: userToken.refreshToken });

		const { tokenType, accessToken, refreshToken: newRefreshToken } = res;

		if (accessToken) {
			// Update userStore and save new token
			useUserStore.getState().actions.setUserToken({
				accessToken: `${tokenType} ${accessToken}`,
				refreshToken: newRefreshToken,
			});
			console.debug("Token refreshed successfully.");
			return `${tokenType} ${accessToken}`; // Return new token for interceptor to use
		}

		console.error("Failed to refresh token: No access token returned.");
		return "";
	} catch (error) {
		console.error("Error refreshing token:", error);
		return ""; // Return an empty string to trigger re-login logic
	}
}

function formatToken(token: null | string) {
	return token ? token : null; //There is a tokenType acquisition value
}

// Request header processing
requestClient.addRequestInterceptor({
	fulfilled: async (config) => {
		const { userToken } = useUserStore.getState();
		const { tenantId, xsrfToken } = useAbpStore.getState();
		if (userToken.accessToken) {
			config.headers.Authorization = `${userToken.accessToken}`;
		}
		const { locale } = useLocaleStore.getState();
		config.headers["Accept-Language"] = mapLocaleToAbpLanguageFormat(locale);
		// config.headers["X-Request-From"] = "slash-admin";
		config.headers["X-Request-From"] = "vben";

		config.headers.__tenant = tenantId;
		config.headers.RequestVerificationToken = xsrfToken;

		return config;
	},
});

// response data deconstruction
requestClient.addResponseInterceptor<any>({
	fulfilled: (response) => {
		const { data, status } = response;
		const { hasWrapResult, getData } = wrapperResult(response);

		if (hasWrapResult()) {
			return getData();
		}

		if (status >= 200 && status < 400) {
			return data;
		}

		throw Object.assign({}, response, { response });
	},
});

// Token expiration processing
requestClient.addResponseInterceptor(
	authenticateResponseInterceptor({
		client: requestClient,
		doReAuthenticate,
		doRefreshToken,
		enableRefreshToken: true,
		formatToken,
	}),
);

// General error handling, if you do not enter the above error handling logic, you will enter here
requestClient.addResponseInterceptor(
	errorMessageResponseInterceptor((msg: string, error) => {
		// This can be customized according to the business. You can get the information in the error for customized processing and make different prompts according to different codes instead of directly using message.error to prompt msg.
		// The error field returned by the current mock interface is error or message
		const responseData = error?.response?.data ?? {};
		if (responseData?.error_description) {
			const { formatError } = handleOAuthError();
			toast.error(formatError(responseData) || msg, {
				position: "top-center",
			});
			return;
		}
		const errorMessage = responseData?.error ?? responseData?.message ?? "";
		// If there is no error message, a prompt will be given based on the status code.
		toast.error(errorMessage || msg, {
			position: "top-center",
		});
	}),
);

export default requestClient;
