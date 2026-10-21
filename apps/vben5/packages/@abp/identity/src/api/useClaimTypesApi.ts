import type { ListResultDto, PagedResultDto } from '@abp/core';

import type {
  GetIdentityClaimTypePagedListInput,
  IdentityClaimTypeCreateDto,
  IdentityClaimTypeDto,
  IdentityClaimTypeUpdateDto,
} from '../types';

import { useRequest } from '@abp/request';

export function useClaimTypesApi() {
  const { cancel, request } = useRequest();

  /**
   * Added user statement
   * @param input parameter
   * @returns User declared entity data transfer object
   */
  function createApi(
    input: IdentityClaimTypeCreateDto,
  ): Promise<IdentityClaimTypeDto> {
    return request<IdentityClaimTypeDto>('/api/identity/claim-types', {
      data: input,
      method: 'POST',
    });
  }

  /**
   * Delete user statement
   * @param id user statement id
   */
  function deleteApi(id: string): Promise<void> {
    return request(`/api/identity/claim-types/${id}`, {
      method: 'DELETE',
    });
  }

  /**
   * Query user statement
   * @param id user statement id
   * @returns User declared entity data transfer object
   */
  function getApi(id: string): Promise<IdentityClaimTypeDto> {
    return request<IdentityClaimTypeDto>(`/api/identity/claim-types/${id}`, {
      method: 'GET',
    });
  }

  /**
   * Update user statement
   * @param id user statement id
   * @returns User declared entity data transfer object
   */
  function updateApi(
    id: string,
    input: IdentityClaimTypeUpdateDto,
  ): Promise<IdentityClaimTypeDto> {
    return request<IdentityClaimTypeDto>(`/api/identity/claim-types/${id}`, {
      data: input,
      method: 'PUT',
    });
  }

  /**
   * Query user statement paging list
   * @param input filter parameter
   * @returns User declared entity data transfer object paginated list
   */
  function getPagedListApi(
    input?: GetIdentityClaimTypePagedListInput,
  ): Promise<PagedResultDto<IdentityClaimTypeDto>> {
    return request<PagedResultDto<IdentityClaimTypeDto>>(
      `/api/identity/claim-types`,
      {
        method: 'GET',
        params: input,
      },
    );
  }

  /**
   * Get the list of available declaration types
   */
  function getAssignableClaimsApi(): Promise<
    ListResultDto<IdentityClaimTypeDto>
  > {
    return request<ListResultDto<IdentityClaimTypeDto>>(
      `/api/identity/claim-types/actived-list`,
      {
        method: 'GET',
      },
    );
  }

  return {
    cancel,
    createApi,
    deleteApi,
    getApi,
    getAssignableClaimsApi,
    getPagedListApi,
    updateApi,
  };
}
