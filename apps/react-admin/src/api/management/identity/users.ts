import type { ListResultDto, PagedResultDto } from "#/abp-core";
import type { IdentityRoleDto, OrganizationUnitDto } from "#/management/identity";
import type {
	IdentityClaimCreateDto,
	IdentityClaimDeleteDto,
	IdentityClaimDto,
	IdentityClaimUpdateDto,
} from "#/management/identity/claims";
import type {
	ChangeUserPasswordInput,
	GetUserPagedListInput,
	IdentityUserCreateDto,
	IdentityUserDto,
	IdentityUserUpdateDto,
} from "#/management/identity";
import requestClient from "../../request";

/**
 * Add new user
 * @param input parameter
 * @returns User entity data transfer object
 */
export function createApi(input: IdentityUserCreateDto): Promise<IdentityUserDto> {
	return requestClient.post<IdentityUserDto>("/api/identity/users", input);
}

/**
 * Delete user
 * @param id user id
 */
export function deleteApi(id: string): Promise<void> {
	return requestClient.delete(`/api/identity/users/${id}`);
}

/**
 * Query user
 * @param id user id
 * @returns User entity data transfer object
 */
export function getApi(id: string): Promise<IdentityUserDto> {
	return requestClient.get<IdentityUserDto>(`/api/identity/users/${id}`);
}

/**
 * Update user
 * @param id user id
 * @returns User entity data transfer object
 */
export function updateApi(id: string, input: IdentityUserUpdateDto): Promise<IdentityUserDto> {
	return requestClient.put<IdentityUserDto>(`/api/identity/users/${id}`, input);
}

/**
 * Query user paging list
 * @param input filter parameter
 * @returns User entity data transfer object paginated list
 */
export function getPagedListApi(input?: GetUserPagedListInput): Promise<PagedResultDto<IdentityUserDto>> {
	return requestClient.get<PagedResultDto<IdentityUserDto>>("/api/identity/users", {
		params: input,
	});
}

/**
 * Remove users from the organization
 * @param id user id
 * @param ouId organization id
 */
export function removeOrganizationUnitApi(id: string, ouId: string): Promise<void> {
	return requestClient.delete(`/api/identity/users/${id}/organization-units/${ouId}`);
}

/**
 * Get the list of user organizations
 * @param id user id
 */
export function getOrganizationUnitsApi(id: string): Promise<ListResultDto<OrganizationUnitDto>> {
	return requestClient.get<ListResultDto<OrganizationUnitDto>>(`/api/identity/users/${id}/organization-units`);
}

/**
 * Lock user
 * @param id user id
 * @param seconds lock duration (seconds)
 */
export function lockApi(id: string, seconds: number): Promise<void> {
	return requestClient.put(`/api/identity/users/${id}/lock/${seconds}`);
}

/**
 * Unlock user
 * @param id user id
 */
export function unLockApi(id: string): Promise<void> {
	return requestClient.put(`/api/identity/users/${id}/unlock`);
}

/**
 * Change user password
 * @param id user id
 * @param input password change dto
 */
export function changePasswordApi(id: string, input: ChangeUserPasswordInput): Promise<void> {
	return requestClient.put(`/api/identity/users/change-password?id=${id}`, input);
}

/**
 * Get the list of available roles
 */
export function getAssignableRolesApi(): Promise<ListResultDto<IdentityRoleDto>> {
	return requestClient.get<ListResultDto<IdentityRoleDto>>("/api/identity/users/assignable-roles");
}

/**
 * Get user role list
 * @param id user id
 */
export function getRolesApi(id: string): Promise<ListResultDto<IdentityRoleDto>> {
	return requestClient.get<ListResultDto<IdentityRoleDto>>(`/api/identity/users/${id}/roles`);
}

/**
 * Get user statement list
 * @param id user id
 */
export function getClaimsApi(id: string): Promise<ListResultDto<IdentityClaimDto>> {
	return requestClient.get<ListResultDto<IdentityClaimDto>>(`/api/identity/users/${id}/claims`);
}

/**
 * Delete user statement
 * @param id user id
 * @param input user declaration dto
 */
export function deleteClaimApi(id: string, input: IdentityClaimDeleteDto): Promise<void> {
	return requestClient.delete(`/api/identity/users/${id}/claims`, { params: input });
}

/**
 * Create user statement
 * @param id user id
 * @param input user declaration dto
 */
export function createClaimApi(id: string, input: IdentityClaimCreateDto): Promise<void> {
	return requestClient.post(`/api/identity/users/${id}/claims`, input);
}

/**
 * Update user statement
 * @param id user id
 * @param input user declaration dto
 */
export function updateClaimApi(id: string, input: IdentityClaimUpdateDto): Promise<void> {
	return requestClient.put(`/api/identity/users/${id}/claims`, input);
}
