import type { PagedResultDto } from "#/abp-core";

import type { LogDto, LogGetListInput } from "#/management/auditing";

import requestClient from "../../request";

/**
 * Get system logs
 * @param id log id
 */
export function getApi(id: string): Promise<LogDto> {
	return requestClient.get<LogDto>(`/api/auditing/logging/${id}`, {
		method: "GET",
	});
}

/**
 * Get the system log paging list
 * @param input parameter
 */
export function getPagedListApi(input: LogGetListInput): Promise<PagedResultDto<LogDto>> {
	return requestClient.get<PagedResultDto<LogDto>>("/api/auditing/logging", {
		params: input,
	});
}
