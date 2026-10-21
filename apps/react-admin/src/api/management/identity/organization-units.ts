import type { ListResultDto, PagedResultDto } from "#/abp-core";

import type { IdentityRoleDto, IdentityUserDto } from "#/management/identity";
import type {
	GetIdentityRolesInput,
	GetIdentityUsersInput,
	GetOrganizationUnitPagedListInput,
	GetUnaddedRoleListInput,
	GetUnaddedUserListInput,
	OrganizationUnitAddRoleDto,
	OrganizationUnitAddUserDto,
	OrganizationUnitCreateDto,
	OrganizationUnitDto,
	OrganizationUnitGetChildrenDto,
	OrganizationUnitUpdateDto,
} from "#/management/identity/organization-units";

import requestClient from "../../request";

/**
 * Add new organizational structure
 * @param input parameter
 * @returns Organization entity data transfer object
 */
export function createApi(input: OrganizationUnitCreateDto): Promise<OrganizationUnitDto> {
	return requestClient.post<OrganizationUnitDto>("/api/identity/organization-units", input);
}

/**
 * Delete organization
 * @param id organization id
 */
export function deleteApi(id: string): Promise<void> {
	return requestClient.delete(`/api/identity/organization-units/${id}`);
}

/**
 * Query organization structure
 * @param id organization id
 * @returns Organization entity data transfer object
 */
export function getApi(id: string): Promise<OrganizationUnitDto> {
	return requestClient.get<OrganizationUnitDto>(`/api/identity/organization-units/${id}`);
}

/**
 * Update organizational structure
 * @param id organization id
 * @returns Organization entity data transfer object
 */

export function updateApi(id: string, input: OrganizationUnitUpdateDto): Promise<OrganizationUnitDto> {
	return requestClient.put<OrganizationUnitDto>(`/api/identity/organization-units/${id}`, input);
}

/**
 * Query the paging list of organizations
 * @param input filter parameter
 * @returns Organization entity data transfer object paginated list
 */
export function getPagedListApi(
	input?: GetOrganizationUnitPagedListInput,
): Promise<PagedResultDto<OrganizationUnitDto>> {
	return requestClient.get<PagedResultDto<OrganizationUnitDto>>("/api/identity/organization-units", {
		params: input,
	});
}

/**
 * Query the list of root organizations
 * @returns Organization entity data transfer object list
 */
export function getRootListApi(): Promise<ListResultDto<OrganizationUnitDto>> {
	return requestClient.get<ListResultDto<OrganizationUnitDto>>("/api/identity/organization-units/root-node");
}

/**
 * Query the list of organizations
 * @returns Organization entity data transfer object list
 */
export function getAllListApi(): Promise<ListResultDto<OrganizationUnitDto>> {
	return requestClient.get<ListResultDto<OrganizationUnitDto>>("/api/identity/organization-units/all");
}

/**
 * Query the list of subordinate organizations
 * @param input query parameters
 * @returns Organization entity data transfer object list
 */
export function getChildrenApi(input: OrganizationUnitGetChildrenDto): Promise<ListResultDto<OrganizationUnitDto>> {
	return requestClient.get<ListResultDto<OrganizationUnitDto>>("/api/identity/organization-units/find-children", {
		params: input,
	});
}

/**
 * Query the organization user list
 * @param id organization id
 * @param input query filter parameters
 * @returns User entity data transfer object paginated list
 */
export function getUserListApi(id: string, input?: GetIdentityUsersInput): Promise<PagedResultDto<IdentityUserDto>> {
	return requestClient.get<PagedResultDto<IdentityUserDto>>(`/api/identity/organization-units/${id}/users`, {
		params: input,
	});
}

/**
 * Query the list of users who have not joined the organization
 * @param input query filter parameters
 * @returns User entity data transfer object paginated list
 */
export function getUnaddedUserListApi(input: GetUnaddedUserListInput): Promise<PagedResultDto<IdentityUserDto>> {
	return requestClient.get<PagedResultDto<IdentityUserDto>>(
		`/api/identity/organization-units/${input.id}/unadded-users`,
		{
			params: input,
		},
	);
}

/**
 * Add user to organization
 * @param id organization id
 * @param input user id list
 */
export function addMembers(id: string, input: OrganizationUnitAddUserDto): Promise<void> {
	return requestClient.post(`/api/identity/organization-units/${id}/users`, input);
}

/**
 * Query the list of organizational roles
 * @param id organization id
 * @param input query filter parameters
 * @returns paging list of role entity data transfer objects
 */
export function getRoleListApi(id: string, input?: GetIdentityRolesInput): Promise<PagedResultDto<IdentityRoleDto>> {
	return requestClient.get<PagedResultDto<IdentityRoleDto>>(`/api/identity/organization-units/${id}/roles`, {
		params: input,
	});
}

/**
 * Query the list of roles that have not joined the organization
 * @param input query filter parameters
 * @returns paging list of role entity data transfer objects
 */
export function getUnaddedRoleListApi(input: GetUnaddedRoleListInput): Promise<PagedResultDto<IdentityRoleDto>> {
	return requestClient.get<PagedResultDto<IdentityRoleDto>>(
		`/api/identity/organization-units/${input.id}/unadded-roles`,
		{
			params: input,
		},
	);
}

/**
 * Add roles to the organization
 * @param id organization id
 * @param input role id list
 */
export function addRoles(id: string, input: OrganizationUnitAddRoleDto): Promise<void> {
	return requestClient.post(`/api/identity/organization-units/${id}/roles`, input);
}

/**
 *Mobile organization
 * @param id organization id
 * @param parentId parent organization id
 */
export function moveTo(id: string, parentId?: string): Promise<void> {
	return requestClient.put(`api/identity/organization-units/${id}/move`, { parentId });
}
