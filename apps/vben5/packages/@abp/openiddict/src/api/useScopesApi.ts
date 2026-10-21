import type { ListResultDto, PagedResultDto } from '@abp/core';

import type {
  OpenIddictScopeCreateDto,
  OpenIddictScopeDto,
  OpenIddictScopeGetListInput,
  OpenIddictScopeUpdateDto,
} from '../types/scopes';

import { useRequest } from '@abp/request';

export function useScopesApi() {
  const { cancel, request } = useRequest();
  /**
   * New scope
   * @param input parameter
   * @returns scope entity data transfer object
   */
  function createApi(
    input: OpenIddictScopeCreateDto,
  ): Promise<OpenIddictScopeDto> {
    return request<OpenIddictScopeDto>('/api/openiddict/scopes', {
      data: input,
      method: 'POST',
    });
  }

  /**
   * Delete range
   * @param id range id
   */
  function deleteApi(id: string): Promise<void> {
    return request(`/api/openiddict/scopes/${id}`, {
      method: 'DELETE',
    });
  }

  /**
   * Query scope
   * @param id range id
   * @returns scope entity data transfer object
   */
  function getApi(id: string): Promise<OpenIddictScopeDto> {
    return request<OpenIddictScopeDto>(`/api/openiddict/scopes/${id}`, {
      method: 'GET',
    });
  }

  /**
   *Update range
   * @param id range id
   * @returns scope entity data transfer object
   */
  function updateApi(
    id: string,
    input: OpenIddictScopeUpdateDto,
  ): Promise<OpenIddictScopeDto> {
    return request<OpenIddictScopeDto>(`/api/openiddict/scopes/${id}`, {
      data: input,
      method: 'PUT',
    });
  }

  /**
   * Query range paging list
   * @param input filter parameter
   * @returns range entity data transfer object paginated list
   */
  function getPagedListApi(
    input?: OpenIddictScopeGetListInput,
  ): Promise<PagedResultDto<OpenIddictScopeDto>> {
    return request<PagedResultDto<OpenIddictScopeDto>>(
      `/api/openiddict/scopes`,
      {
        method: 'GET',
        params: input,
      },
    );
  }

  /**
   * Query the list of available ranges
   * @returns List of available range entity data transfer objects
   */
  function getAssignableScopesApi(): Promise<
    ListResultDto<OpenIddictScopeDto>
  > {
    return request<ListResultDto<OpenIddictScopeDto>>(
      `/api/openiddict/scopes/assignable-scopes`,
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
    getAssignableScopesApi,
    getPagedListApi,
    updateApi,
  };
}
