import type { GetSecurityLogPagedRequest, SecurityLogDto } from "#/management/identity";
import type { PagedResultDto } from "#/abp-core";
import requestClient from "../../request";

/**
 * Delete security log
 * @param id security log id
 */
export function deleteApi(id: string): Promise<void> {
	return requestClient.delete(`/api/auditing/security-log/${id}`);
}

/**
 * Query security logs
 * @param id security log id
 * @returns Security log entity data transfer object
 */
export function getApi(id: string): Promise<SecurityLogDto> {
	return requestClient.get<SecurityLogDto>(`/api/auditing/security-log/${id}`);
}

/**
 * Query the security log paging list
 * @param input filter parameter
 * @returns Security log entity data transfer object paging list
 */
export function getPagedListApi(input?: GetSecurityLogPagedRequest): Promise<PagedResultDto<SecurityLogDto>> {
	return requestClient.get<PagedResultDto<SecurityLogDto>>("/api/auditing/security-log", {
		params: input,
	});
}
