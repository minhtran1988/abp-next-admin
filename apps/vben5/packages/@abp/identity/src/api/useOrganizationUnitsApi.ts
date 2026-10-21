import type { ListResultDto, PagedResultDto } from '@abp/core';

import type { IdentityRoleDto, IdentityUserDto } from '../types';
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
} from '../types/organization-units';

import { useRequest } from '@abp/request';

export function useOrganizationUnitsApi() {
  const { cancel, request } = useRequest();

  /**
   * Add new organizational structure
   * @param input parameter
   * @returns Organization entity data transfer object
   */
  function createApi(
    input: OrganizationUnitCreateDto,
  ): Promise<OrganizationUnitDto> {
    return request<OrganizationUnitDto>('/api/identity/organization-units', {
      data: input,
      method: 'POST',
    });
  }

  /**
   * Delete organization
   * @param id organization id
   */
  function deleteApi(id: string): Promise<void> {
    return request(`/api/identity/organization-units/${id}`, {
      method: 'DELETE',
    });
  }

  /**
   * Query organization structure
   * @param id organization id
   * @returns Organization entity data transfer object
   */
  function getApi(id: string): Promise<OrganizationUnitDto> {
    return request<OrganizationUnitDto>(
      `/api/identity/organization-units/${id}`,
      {
        method: 'GET',
      },
    );
  }

  /**
   * Update organizational structure
   * @param id organization id
   * @returns Organization entity data transfer object
   */
  function updateApi(
    id: string,
    input: OrganizationUnitUpdateDto,
  ): Promise<OrganizationUnitDto> {
    return request<OrganizationUnitDto>(
      `/api/identity/organization-units/${id}`,
      {
        data: input,
        method: 'PUT',
      },
    );
  }

  /**
   * Query the paging list of organizations
   * @param input filter parameter
   * @returns Organization entity data transfer object paginated list
   */
  function getPagedListApi(
    input?: GetOrganizationUnitPagedListInput,
  ): Promise<PagedResultDto<OrganizationUnitDto>> {
    return request<PagedResultDto<OrganizationUnitDto>>(
      `/api/identity/organization-units`,
      {
        method: 'GET',
        params: input,
      },
    );
  }

  /**
   * Query the list of root organizations
   * @returns Organization entity data transfer object list
   */
  function getRootListApi(): Promise<ListResultDto<OrganizationUnitDto>> {
    return request<ListResultDto<OrganizationUnitDto>>(
      `/api/identity/organization-units/root-node`,
      {
        method: 'GET',
      },
    );
  }

  /**
   * Query the list of organizations
   * @returns Organization entity data transfer object list
   */
  function getAllListApi(): Promise<ListResultDto<OrganizationUnitDto>> {
    return request<ListResultDto<OrganizationUnitDto>>(
      `/api/identity/organization-units/all`,
      {
        method: 'GET',
      },
    );
  }

  /**
   * Query the list of subordinate organizations
   * @param input query parameters
   * @returns Organization entity data transfer object list
   */
  function getChildrenApi(
    input: OrganizationUnitGetChildrenDto,
  ): Promise<ListResultDto<OrganizationUnitDto>> {
    return request<ListResultDto<OrganizationUnitDto>>(
      `/api/identity/organization-units/find-children`,
      {
        method: 'GET',
        params: input,
      },
    );
  }

  /**
   * Query the organization user list
   * @param id organization id
   * @param input query filter parameters
   * @returns User entity data transfer object paginated list
   */
  function getUserListApi(
    id: string,
    input?: GetIdentityUsersInput,
  ): Promise<PagedResultDto<IdentityUserDto>> {
    return request<PagedResultDto<IdentityUserDto>>(
      `/api/identity/organization-units/${id}/users`,
      {
        method: 'GET',
        params: input,
      },
    );
  }

  /**
   * Query the list of users who have not joined the organization
   * @param input query filter parameters
   * @returns User entity data transfer object paginated list
   */
  function getUnaddedUserListApi(
    input: GetUnaddedUserListInput,
  ): Promise<PagedResultDto<IdentityUserDto>> {
    return request<PagedResultDto<IdentityUserDto>>(
      `/api/identity/organization-units/${input.id}/unadded-users`,
      {
        method: 'GET',
        params: input,
      },
    );
  }

  /**
   * Add user to organization
   * @param id organization id
   * @param input user id list
   */
  function addMembers(
    id: string,
    input: OrganizationUnitAddUserDto,
  ): Promise<void> {
    return request(`/api/identity/organization-units/${id}/users`, {
      data: input,
      method: 'POST',
    });
  }

  /**
   * Query the list of organizational roles
   * @param id organization id
   * @param input query filter parameters
   * @returns paging list of role entity data transfer objects
   */
  function getRoleListApi(
    id: string,
    input?: GetIdentityRolesInput,
  ): Promise<PagedResultDto<IdentityRoleDto>> {
    return request<PagedResultDto<IdentityRoleDto>>(
      `/api/identity/organization-units/${id}/roles`,
      {
        method: 'GET',
        params: input,
      },
    );
  }

  /**
   * Query the list of roles that have not joined the organization
   * @param input query filter parameters
   * @returns paging list of role entity data transfer objects
   */
  function getUnaddedRoleListApi(
    input: GetUnaddedRoleListInput,
  ): Promise<PagedResultDto<IdentityRoleDto>> {
    return request<PagedResultDto<IdentityRoleDto>>(
      `/api/identity/organization-units/${input.id}/unadded-roles`,
      {
        method: 'GET',
        params: input,
      },
    );
  }

  /**
   * Add roles to the organization
   * @param id organization id
   * @param input role id list
   */
  function addRoles(
    id: string,
    input: OrganizationUnitAddRoleDto,
  ): Promise<void> {
    return request(`/api/identity/organization-units/${id}/roles`, {
      data: input,
      method: 'POST',
    });
  }

  /**
   *Mobile organization
   * @param id organization id
   * @param parentId parent organization id
   */
  function moveTo(id: string, parentId?: string): Promise<void> {
    return request(`api/identity/organization-units/${id}/move`, {
      data: { parentId },
      method: 'PUT',
    });
  }

  return {
    addMembers,
    addRoles,
    cancel,
    createApi,
    deleteApi,
    getAllListApi,
    getApi,
    getChildrenApi,
    getPagedListApi,
    getRoleListApi,
    getRootListApi,
    getUnaddedRoleListApi,
    getUnaddedUserListApi,
    getUserListApi,
    moveTo,
    updateApi,
  };
}
