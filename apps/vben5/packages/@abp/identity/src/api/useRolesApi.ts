import type { ListResultDto, PagedResultDto } from '@abp/core';

import type {
  IdentityClaimCreateDto,
  IdentityClaimDeleteDto,
  IdentityClaimDto,
  IdentityClaimUpdateDto,
} from '../types/claims';
import type {
  GetRolePagedListInput,
  IdentityRoleCreateDto,
  IdentityRoleDto,
  IdentityRoleUpdateDto,
} from '../types/roles';

import { useRequest } from '@abp/request';

export function useRolesApi() {
  const { cancel, request } = useRequest();
  /**
   * Added new characters
   * @param input parameter
   * @returns role entity data transfer object
   */
  function createApi(input: IdentityRoleCreateDto): Promise<IdentityRoleDto> {
    return request<IdentityRoleDto>('/api/identity/roles', {
      data: input,
      method: 'POST',
    });
  }

  /**
   * Delete role
   * @param id role id
   */
  function deleteApi(id: string): Promise<void> {
    return request(`/api/identity/roles/${id}`, {
      method: 'DELETE',
    });
  }

  /**
   * Query role
   * @param id role id
   * @returns role entity data transfer object
   */
  function getApi(id: string): Promise<IdentityRoleDto> {
    return request<IdentityRoleDto>(`/api/identity/roles/${id}`, {
      method: 'GET',
    });
  }

  /**
   * Update roles
   * @param id role id
   * @returns role entity data transfer object
   */
  function updateApi(
    id: string,
    input: IdentityRoleUpdateDto,
  ): Promise<IdentityRoleDto> {
    return request<IdentityRoleDto>(`/api/identity/roles/${id}`, {
      data: input,
      method: 'PUT',
    });
  }

  /**
   * Query the role paging list
   * @param input filter parameter
   * @returns paging list of role entity data transfer objects
   */
  function getPagedListApi(
    input?: GetRolePagedListInput,
  ): Promise<PagedResultDto<IdentityRoleDto>> {
    return request<PagedResultDto<IdentityRoleDto>>(`/api/identity/roles`, {
      method: 'GET',
      params: input,
    });
  }

  /**
   * Remove the role from the organization
   * @param id role id
   * @param ouId organization id
   */
  function removeOrganizationUnitApi(id: string, ouId: string): Promise<void> {
    return request(`/api/identity/roles/${id}/organization-units/${ouId}`, {
      method: 'DELETE',
    });
  }

  /**
   * Get the role declaration list
   * @param id role id
   */
  function getClaimsApi(id: string): Promise<ListResultDto<IdentityClaimDto>> {
    return request<ListResultDto<IdentityClaimDto>>(
      `/api/identity/roles/${id}/claims`,
      {
        method: 'GET',
      },
    );
  }

  /**
   * Remove role declaration
   * @param id role id
   * @param input role declaration dto
   */
  function deleteClaimApi(
    id: string,
    input: IdentityClaimDeleteDto,
  ): Promise<void> {
    return request(`/api/identity/roles/${id}/claims`, {
      method: 'DELETE',
      params: input,
    });
  }

  /**
   * Create role statement
   * @param id role id
   * @param input role declaration dto
   */
  function createClaimApi(
    id: string,
    input: IdentityClaimCreateDto,
  ): Promise<void> {
    return request(`/api/identity/roles/${id}/claims`, {
      data: input,
      method: 'POST',
    });
  }

  /**
   * Updated role statement
   * @param id role id
   * @param input user role dto
   */
  function updateClaimApi(
    id: string,
    input: IdentityClaimUpdateDto,
  ): Promise<void> {
    return request(`/api/identity/roles/${id}/claims`, {
      data: input,
      method: 'PUT',
    });
  }

  return {
    cancel,
    createApi,
    createClaimApi,
    deleteApi,
    deleteClaimApi,
    getApi,
    getClaimsApi,
    getPagedListApi,
    removeOrganizationUnitApi,
    updateApi,
    updateClaimApi,
  };
}
