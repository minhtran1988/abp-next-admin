import type { PagedResultDto } from '@abp/core';

import type {
  ResourceCreateDto,
  ResourceDto,
  ResourceGetPagedListInput,
  ResourceUpdateDto,
} from '../types/resources';

import { useRequest } from '@abp/request';

export function useResourcesApi() {
  const { cancel, request } = useRequest();

  /**
   * Query resource paging list
   * @param input parameter
   * @returns resource list
   */
  function getPagedListApi(
    input?: ResourceGetPagedListInput,
  ): Promise<PagedResultDto<ResourceDto>> {
    return request<PagedResultDto<ResourceDto>>('/api/localization/resources', {
      method: 'GET',
      params: input,
    });
  }

  /**
   * Query resources
   * @param name resource name
   * @returns queried resources
   */
  function getApi(name: string): Promise<ResourceDto> {
    return request<ResourceDto>(`/api/localization/resources/${name}`, {
      method: 'GET',
    });
  }

  /**
   * Delete resources
   * @param name resource name
   */
  function deleteApi(name: string): Promise<void> {
    return request(`/api/localization/resources/${name}`, {
      method: 'DELETE',
    });
  }

  /**
   * Create resources
   * @param input parameter
   * @returns created resources
   */
  function createApi(input: ResourceCreateDto): Promise<ResourceDto> {
    return request<ResourceDto>(`/api/localization/resources`, {
      data: input,
      method: 'POST',
    });
  }

  /**
   * Edit resources
   * @param name resource name
   * @param input parameter
   * @returns edited resources
   */
  function updateApi(
    name: string,
    input: ResourceUpdateDto,
  ): Promise<ResourceDto> {
    return request<ResourceDto>(`/api/localization/resources/${name}`, {
      data: input,
      method: 'PUT',
    });
  }

  return {
    cancel,
    createApi,
    deleteApi,
    getApi,
    getPagedListApi,
    updateApi,
  };
}
