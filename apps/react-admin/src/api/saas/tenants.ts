import type { ListResultDto, PagedResultDto } from "#/abp-core";

import type {
	GetTenantPagedListInput,
	TenantConnectionStringCheckInput,
	TenantConnectionStringDto,
	TenantConnectionStringSetInput,
	TenantCreateDto,
	TenantDto,
	TenantUpdateDto,
} from "#/saas/tenants";

import requestClient from "../request";

/**
 * Create tenant
 * @param {TenantCreateDto} input parameter
 * @returns created tenant
 */
export function createApi(input: TenantCreateDto): Promise<TenantDto> {
	return requestClient.post<TenantDto>("/api/saas/tenants", input);
}

/**
 * Edit tenant
 * @param {string} id parameter
 * @param {TenantUpdateDto} input parameter
 * @returns edited tenant
 */
export function updateApi(id: string, input: TenantUpdateDto): Promise<TenantDto> {
	return requestClient.put<TenantDto>(`/api/saas/tenants/${id}`, input);
}

/**
 * Query tenants
 * @param {string} id Id
 * @returns queried tenant
 */
export function getApi(id: string): Promise<TenantDto> {
	return requestClient.get<TenantDto>(`/api/saas/tenants/${id}`);
}

/**
 * Delete tenant
 * @param {string} id Id
 * @returns {void}
 */
export function deleteApi(id: string): Promise<void> {
	return requestClient.delete(`/api/saas/tenants/${id}`);
}

/**
 * Query tenant paging list
 * @param {GetTenantPagedListInput} input parameter
 * @returns {void}
 */
export function getPagedListApi(input?: GetTenantPagedListInput): Promise<PagedResultDto<TenantDto>> {
	return requestClient.get<PagedResultDto<TenantDto>>("/api/saas/tenants", {
		params: input,
	});
}

/**
 * Set connection string
 * @param {string} id tenant ID
 * @param {TenantConnectionStringSetInput} input parameter
 * @returns connection string
 */
export function setConnectionStringApi(
	id: string,
	input: TenantConnectionStringSetInput,
): Promise<TenantConnectionStringDto> {
	return requestClient.put<TenantConnectionStringDto>(`/api/saas/tenants/${id}/connection-string`, input);
}

/**
 * Query connection string
 * @param {string} id tenant ID
 * @param {string} name connection string name
 * @returns connection string
 */
export function getConnectionStringApi(id: string, name: string): Promise<TenantConnectionStringDto> {
	return requestClient.get<TenantConnectionStringDto>(`/api/saas/tenants/${id}/connection-string/${name}`);
}

/**
 * Query all connection strings
 * @param {string} id tenant ID
 * @returns connection string list
 */
export function getConnectionStringsApi(id: string): Promise<ListResultDto<TenantConnectionStringDto>> {
	return requestClient.get<ListResultDto<TenantConnectionStringDto>>(`/api/saas/tenants/${id}/connection-string`);
}

/**
 * Delete tenant
 * @param {string} id tenant ID
 * @param {string} name connection string name
 * @returns {void}
 */
export function deleteConnectionStringApi(id: string, name: string): Promise<void> {
	return requestClient.delete(`/api/saas/tenants/${id}/connection-string/${name}`);
}

/**
 * Check database connection string
 * @param input parameter
 */
export function checkConnectionString(input: TenantConnectionStringCheckInput): Promise<void> {
	return requestClient.post("/api/saas/tenants/connection-string/check", input);
}
