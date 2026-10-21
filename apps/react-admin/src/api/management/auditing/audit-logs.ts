import type { PagedResultDto } from "#/abp-core";

import type { AuditLogDto, AuditLogGetListInput } from "#/management/auditing";
import requestClient from "../../request";

/**
 * Get audit logs
 * @param id log id
 */
export function getApi(id: string): Promise<AuditLogDto> {
	return requestClient.get<AuditLogDto>(`/api/auditing/audit-log/${id}`);
}

/**
 * Get the audit log paging list
 * @param input parameter
 */
export function getPagedListApi(input: AuditLogGetListInput): Promise<PagedResultDto<AuditLogDto>> {
	return requestClient.get<PagedResultDto<AuditLogDto>>("/api/auditing/audit-log", {
		params: input,
	});
}

/**
 * Delete audit log
 * @param id log id
 */
export function deleteApi(id: string): Promise<void> {
	return requestClient.delete(`/api/auditing/audit-log/${id}`);
}
