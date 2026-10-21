import type { ListResultDto, PagedResultDto } from '@abp/core';

import type { IdentityRoleDto, OrganizationUnitDto } from '../types';
import type {
  IdentityClaimCreateDto,
  IdentityClaimDeleteDto,
  IdentityClaimDto,
  IdentityClaimUpdateDto,
} from '../types/claims';
import type {
  ChangeUserPasswordInput,
  GetUserPagedListInput,
  IdentityUserCreateDto,
  IdentityUserDto,
  IdentityUserUpdateDto,
} from '../types/users';

import { useRequest } from '@abp/request';

export function useUsersApi() {
  const { cancel, request } = useRequest();

  /**
   * Add new user
   * @param input parameter
   * @returns User entity data transfer object
   */
  function createApi(input: IdentityUserCreateDto): Promise<IdentityUserDto> {
    return request<IdentityUserDto>('/api/identity/users', {
      data: input,
      method: 'POST',
    });
  }

  /**
   * Delete user
   * @param id user id
   */
  function deleteApi(id: string): Promise<void> {
    return request(`/api/identity/users/${id}`, {
      method: 'DELETE',
    });
  }

  /**
   * Query user
   * @param id user id
   * @returns User entity data transfer object
   */
  function getApi(id: string): Promise<IdentityUserDto> {
    return request<IdentityUserDto>(`/api/identity/users/${id}`, {
      method: 'GET',
    });
  }

  /**
   * Update user
   * @param id user id
   * @returns User entity data transfer object
   */
  function updateApi(
    id: string,
    input: IdentityUserUpdateDto,
  ): Promise<IdentityUserDto> {
    return request<IdentityUserDto>(`/api/identity/users/${id}`, {
      data: input,
      method: 'PUT',
    });
  }

  /**
   * Query user paging list
   * @param input filter parameter
   * @returns User entity data transfer object paginated list
   */
  function getPagedListApi(
    input?: GetUserPagedListInput,
  ): Promise<PagedResultDto<IdentityUserDto>> {
    return request<PagedResultDto<IdentityUserDto>>(`/api/identity/users`, {
      method: 'GET',
      params: input,
    });
  }

  /**
   * Remove users from the organization
   * @param id user id
   * @param ouId organization id
   */
  function removeOrganizationUnitApi(id: string, ouId: string): Promise<void> {
    return request(`/api/identity/users/${id}/organization-units/${ouId}`, {
      method: 'DELETE',
    });
  }

  /**
   * Get the list of user organizations
   * @param id user id
   */
  function getOrganizationUnitsApi(
    id: string,
  ): Promise<ListResultDto<OrganizationUnitDto>> {
    return request<ListResultDto<OrganizationUnitDto>>(
      `/api/identity/users/${id}/organization-units`,
      {
        method: 'GET',
      },
    );
  }

  /**
   * Lock user
   * @param id user id
   * @param seconds lock duration (seconds)
   */
  function lockApi(id: string, seconds: number): Promise<void> {
    return request(`/api/identity/users/${id}/lock/${seconds}`, {
      method: 'PUT',
    });
  }

  /**
   * Unlock user
   * @param id user id
   */
  function unLockApi(id: string): Promise<void> {
    return request(`/api/identity/users/${id}/unlock`, {
      method: 'PUT',
    });
  }

  /**
   * Change user password
   * @param id user id
   * @param input password change dto
   */
  function changePasswordApi(
    id: string,
    input: ChangeUserPasswordInput,
  ): Promise<void> {
    return request(`/api/identity/users/change-password?id=${id}`, {
      data: input,
      method: 'PUT',
    });
  }

  /**
   * Get the list of available roles
   */
  function getAssignableRolesApi(): Promise<ListResultDto<IdentityRoleDto>> {
    return request<ListResultDto<IdentityRoleDto>>(
      `/api/identity/users/assignable-roles`,
      {
        method: 'GET',
      },
    );
  }

  /**
   * Get user role list
   * @param id user id
   */
  function getRolesApi(id: string): Promise<ListResultDto<IdentityRoleDto>> {
    return request<ListResultDto<IdentityRoleDto>>(
      `/api/identity/users/${id}/roles`,
      {
        method: 'GET',
      },
    );
  }

  /**
   * Get user statement list
   * @param id user id
   */
  function getClaimsApi(id: string): Promise<ListResultDto<IdentityClaimDto>> {
    return request<ListResultDto<IdentityClaimDto>>(
      `/api/identity/users/${id}/claims`,
      {
        method: 'GET',
      },
    );
  }

  /**
   * Delete user statement
   * @param id user id
   * @param input user declaration dto
   */
  function deleteClaimApi(
    id: string,
    input: IdentityClaimDeleteDto,
  ): Promise<void> {
    return request(`/api/identity/users/${id}/claims`, {
      method: 'DELETE',
      params: input,
    });
  }

  /**
   * Create user statement
   * @param id user id
   * @param input user declaration dto
   */
  function createClaimApi(
    id: string,
    input: IdentityClaimCreateDto,
  ): Promise<void> {
    return request(`/api/identity/users/${id}/claims`, {
      data: input,
      method: 'POST',
    });
  }

  /**
   * Update user statement
   * @param id user id
   * @param input user declaration dto
   */
  function updateClaimApi(
    id: string,
    input: IdentityClaimUpdateDto,
  ): Promise<void> {
    return request(`/api/identity/users/${id}/claims`, {
      data: input,
      method: 'PUT',
    });
  }

  return {
    cancel,
    changePasswordApi,
    createApi,
    createClaimApi,
    deleteApi,
    deleteClaimApi,
    getApi,
    getAssignableRolesApi,
    getClaimsApi,
    getOrganizationUnitsApi,
    getPagedListApi,
    getRolesApi,
    lockApi,
    removeOrganizationUnitApi,
    unLockApi,
    updateApi,
    updateClaimApi,
  };
}
