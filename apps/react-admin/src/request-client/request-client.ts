import type { AxiosInstance, AxiosRequestConfig, AxiosResponse, CreateAxiosDefaults } from "axios";

import axios from "axios";

import { FileDownloader } from "./modules/downloader";
import { InterceptorManager } from "./modules/interceptor";
import { FileUploader } from "./modules/uploader";
import type { RequestClientOptions } from "./types";
import { merge } from "@/utils/merge";
import { bindMethods } from "@/utils";

class RequestClient {
	private readonly instance: AxiosInstance;

	public addRequestInterceptor: InterceptorManager["addRequestInterceptor"];
	public addResponseInterceptor: InterceptorManager["addResponseInterceptor"];

	public download: FileDownloader["download"];
	// Whether the token is being refreshed
	public isRefreshing = false;
	// Refresh the token queue
	public refreshTokenQueue: ((token: string) => void)[] = [];
	public upload: FileUploader["upload"];

	/**
	 * Constructor, used to create Axios instances
	 * @param options - Axios request configuration, optional
	 */
	constructor(options: RequestClientOptions = {}) {
		// Merge default configuration and incoming configuration
		const defaultConfig: CreateAxiosDefaults = {
			headers: {
				"Content-Type": "application/json;charset=utf-8",
			},
			// Default timeout
			timeout: 10_000,
		};
		const { ...axiosConfig } = options;
		const requestConfig = merge(axiosConfig, defaultConfig);
		this.instance = axios.create(requestConfig);

		bindMethods(this);

		// Instantiate the interceptor manager
		const interceptorManager = new InterceptorManager(this.instance);
		this.addRequestInterceptor = interceptorManager.addRequestInterceptor.bind(interceptorManager);
		this.addResponseInterceptor = interceptorManager.addResponseInterceptor.bind(interceptorManager);

		// Instantiate the file uploader
		const fileUploader = new FileUploader(this);
		this.upload = fileUploader.upload.bind(fileUploader);
		// Instantiate the file downloader
		const fileDownloader = new FileDownloader(this);
		this.download = fileDownloader.download.bind(fileDownloader);
	}

	/**
	 *DELETE request method
	 */
	public delete<T = any>(url: string, config?: AxiosRequestConfig): Promise<T> {
		return this.request<T>(url, { ...config, method: "DELETE" });
	}

	/**
	 * GET request method
	 */
	public get<T = any>(url: string, config?: AxiosRequestConfig): Promise<T> {
		return this.request<T>(url, { ...config, method: "GET" });
	}

	/**
	 * POST request method
	 */
	public post<T = any>(url: string, data?: any, config?: AxiosRequestConfig): Promise<T> {
		return this.request<T>(url, { ...config, data, method: "POST" });
	}

	/**
	 * PUT request method
	 */
	public put<T = any>(url: string, data?: any, config?: AxiosRequestConfig): Promise<T> {
		return this.request<T>(url, { ...config, data, method: "PUT" });
	}

	/**
	 * Common request method
	 */
	public async request<T>(url: string, config: AxiosRequestConfig): Promise<T> {
		try {
			const response: AxiosResponse<T> = await this.instance({
				url,
				...config,
			});
			return response as T;
		} catch (error: any) {
			throw error.response ? error.response.data : error;
		}
	}
}

export { RequestClient };
