import type { AxiosResponse } from "axios";

import { errorFormat } from "./useErrorFormat";

export function wrapperResult(response: AxiosResponse) {
	const { hasError, throwIfError: throwIfAbpError } = errorFormat(response);
	const _defaultWrapperHeaderKey: string = "_abpwrapresult";
	const { data, headers } = response;
	/** Whether the result has been packaged */
	function hasWrapResult(): boolean {
		return headers[_defaultWrapperHeaderKey] === "true" || hasError();
	}

	/** Get packaging results */
	function getData(): any {
		throwIfError();
		return data.result;
	}

	/** If the request is wrong, throw an exception */
	function throwIfError(): void {
		throwIfAbpError();
		const { code, details, message } = data;
		const hasSuccess = data && Reflect.has(data, "code") && code === "0";
		if (!hasSuccess) {
			const content = details || message;
			throw Object.assign({}, response, { message: content, response });
		}
	}

	return {
		getData,
		hasWrapResult,
	};
}
