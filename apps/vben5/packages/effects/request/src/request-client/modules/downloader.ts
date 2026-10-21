import type { RequestClient } from '../request-client';
import type { RequestClientConfig } from '../types';

type DownloadRequestConfig = {
  /**
   * Define the data type expected to be obtained.
   * raw: original AxiosResponse, including headers, status, etc.
   * body: only returns the BODY part (Blob) of the response data
   */
  responseReturn?: 'body' | 'raw';
} & Omit<RequestClientConfig, 'responseReturn'>;

class FileDownloader {
  private client: RequestClient;

  constructor(client: RequestClient) {
    this.client = client;
  }
  /**
   * Download file
   * @param url full link to the file
   * @param config configuration information, optional.
   * @returns If config.responseReturn is 'body', return Blob (default), otherwise return RequestResponse<Blob>
   */
  public async download<T = Blob>(
    url: string,
    config?: DownloadRequestConfig,
  ): Promise<T> {
    const finalConfig: DownloadRequestConfig = {
      responseReturn: 'body',
      method: 'GET',
      ...config,
      responseType: 'blob',
    };

    // Prefer a generic request if available; otherwise, dispatch to method-specific calls.
    const method = (finalConfig.method || 'GET').toUpperCase();
    const clientAny = this.client as any;

    if (typeof clientAny.request === 'function') {
      return await clientAny.request(url, finalConfig);
    }
    const lower = method.toLowerCase();

    if (typeof clientAny[lower] === 'function') {
      if (['POST', 'PUT'].includes(method)) {
        const { data, ...rest } = finalConfig as Record<string, any>;
        return await clientAny[lower](url, data, rest);
      }

      return await clientAny[lower](url, finalConfig);
    }

    throw new Error(
      `RequestClient does not support method "${method}". Please ensure the method is properly implemented in your RequestClient instance.`,
    );
  }
}

export { FileDownloader };
