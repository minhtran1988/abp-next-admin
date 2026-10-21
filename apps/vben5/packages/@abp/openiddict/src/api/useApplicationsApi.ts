import type { PagedResultDto } from '@abp/core';

import type {
  OpenIddictApplicationCreateDto,
  OpenIddictApplicationDto,
  OpenIddictApplicationGetListInput,
  OpenIddictApplicationUpdateDto,
} from '../types/applications';

import { useRequest } from '@abp/request';

export function useApplicationsApi() {
  const { cancel, request } = useRequest();
  /**
   * New application
   * @param input parameter
   * @returns application entity data transfer object
   */
  function createApi(
    input: OpenIddictApplicationCreateDto,
  ): Promise<OpenIddictApplicationDto> {
    return request<OpenIddictApplicationDto>('/api/openiddict/applications', {
      data: input,
      method: 'POST',
    });
  }

  /**
   * Delete app
   * @param id application id
   */
  function deleteApi(id: string): Promise<void> {
    return request(`/api/openiddict/applications/${id}`, {
      method: 'DELETE',
    });
  }

  /**
   * Query application
   * @param id application id
   * @returns application entity data transfer object
   */
  function getApi(id: string): Promise<OpenIddictApplicationDto> {
    return request<OpenIddictApplicationDto>(
      `/api/openiddict/applications/${id}`,
      {
        method: 'GET',
      },
    );
  }

  /**
   * Update application
   * @param id application id
   * @returns application entity data transfer object
   */
  function updateApi(
    id: string,
    input: OpenIddictApplicationUpdateDto,
  ): Promise<OpenIddictApplicationDto> {
    return request<OpenIddictApplicationDto>(
      `/api/openiddict/applications/${id}`,
      {
        data: input,
        method: 'PUT',
      },
    );
  }

  /**
   * Query application paging list
   * @param input filter parameter
   * @returns paging list of application entity data transfer objects
   */
  function getPagedListApi(
    input?: OpenIddictApplicationGetListInput,
  ): Promise<PagedResultDto<OpenIddictApplicationDto>> {
    return request<PagedResultDto<OpenIddictApplicationDto>>(
      `/api/openiddict/applications`,
      {
        method: 'GET',
        params: input,
      },
    );
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
