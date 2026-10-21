import type { PagedResultDto, ListResultDto } from "#/abp-core";

import type {
	IdentityClaimCreateDto,
	IdentityClaimDeleteDto,
	IdentityClaimDto,
	IdentityClaimUpdateDto,
} from "#/management/identity/claims";

import type {
	GetRolePagedListInput,
	IdentityRoleCreateDto,
	IdentityRoleDto,
	IdentityRoleUpdateDto,
} from "#/management/identity";

import requestClient from "../../request";

/**
 * Added new characters
 * @param input parameter
 * @returns role entity data transfer object
 */
export function createApi(input: IdentityRoleCreateDto): Promise<IdentityRoleDto> {
	return requestClient.post<IdentityRoleDto>("/api/identity/roles", input);
}

/**
 * Delete role
 * @param id role id
 */
export function deleteApi(id: string): Promise<void> {
	return requestClient.delete(`/api/identity/roles/${id}`);
}

/**
 * Query role
 * @param id role id
 * @returns role entity data transfer object
 */
export function getApi(id: string): Promise<IdentityRoleDto> {
	return requestClient.get<IdentityRoleDto>(`/api/identity/roles/${id}`);
}

/**
 * Update roles
 * @param id role id
 * @returns role entity data transfer object
 */
export function updateApi(id: string, input: IdentityRoleUpdateDto): Promise<IdentityRoleDto> {
	return requestClient.put<IdentityRoleDto>(`/api/identity/roles/${id}`, input);
}

/**
 * Query the role paging list
 * @param input filter parameter
 * @returns paging list of role entity data transfer objects
 */
export function getPagedListApi(input?: GetRolePagedListInput): Promise<PagedResultDto<IdentityRoleDto>> {
	return requestClient.get<PagedResultDto<IdentityRoleDto>>("/api/identity/roles", {
		params: input,
	});
}

/**
 * Remove the role from the organization
 * @param id role id
 * @param ouId organization id
 */
export function removeOrganizationUnitApi(id: string, ouId: string): Promise<void> {
	return requestClient.delete(`/api/identity/roles/${id}/organization-units/${ouId}`);
}

/**
 * Get the role declaration list
 * @param id role id
 */
export function getClaimsApi(id: string): Promise<ListResultDto<IdentityClaimDto>> {
	return requestClient.get<ListResultDto<IdentityClaimDto>>(`/api/identity/roles/${id}/claims`);
}

/**
 * Remove role declaration
 * @param id role id
 * @param input role declaration dto
 */
export function deleteClaimApi(id: string, input: IdentityClaimDeleteDto): Promise<void> {
	return requestClient.delete(`/api/identity/roles/${id}/claims`, {
		params: input,
	});
}

/**
 * Create role statement
 * @param id role id
 * @param input role declaration dto
 */
export function createClaimApi(id: string, input: IdentityClaimCreateDto): Promise<void> {
	return requestClient.post(`/api/identity/roles/${id}/claims`, input);
}

/**
 * Updated role statement
 * @param id role id
 * @param input user role dto
 */
export function updateClaimApi(id: string, input: IdentityClaimUpdateDto): Promise<void> {
	return requestClient.put(`/api/identity/roles/${id}/claims`, input);
}
