import type { RemoteServiceErrorInfo } from '@abp/core';
import type { AxiosResponse } from 'axios';

export function useErrorFormat(response: AxiosResponse) {
  const _defaultErrorHeaderKey: string = '_abperrorformat';
  const { data, headers } = response;
  /** Whether the request is wrong */
  function hasError(): boolean {
    if (
      typeof headers.has === 'function' &&
      headers.has(_defaultErrorHeaderKey)
    ) {
      return String(headers[_defaultErrorHeaderKey]).includes('true');
    }
    return false;
  }
  /** If the request is wrong, throw an exception */
  function throwIfError(): void {
    if (!hasError()) return;
    throw Object.assign({}, response, { message: getErrorMessage(), response });
  }

  /** Get error message */
  function getErrorMessage(): string | undefined {
    if (!hasError()) return undefined;
    const errorJson = data.error as RemoteServiceErrorInfo;
    let errorMessage = errorJson.message;
    if (errorJson.validationErrors) {
      errorMessage += errorJson.validationErrors
        .map((error) => error.message)
        .join('\n');
    }
    return errorMessage;
  }

  return {
    hasError,
    getErrorMessage,
    throwIfError,
  };
}
