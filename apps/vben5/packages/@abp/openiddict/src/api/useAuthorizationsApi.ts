import type { PagedResultDto } from '@abp/core';

import type {
  OpenIddictAuthorizationDto,
  OpenIddictAuthorizationGetListInput,
} from '../types/authorizations';

import { useRequest } from '@abp/request';

export function useAuthorizationsApi() {
  const { cancel, request } = useRequest();

  /**
   * Delete authorization
   * @param id authorization id
   */
  function deleteApi(id: string): Promise<void> {
    return request(`/api/openiddict/authorizations/${id}`, {
      method: 'DELETE',
    });
  }

  /**
   * Query authorization
   * @param id authorization id
   * @returns Authorization entity data transfer object
   */
  function getApi(id: string): Promise<OpenIddictAuthorizationDto> {
    return request<OpenIddictAuthorizationDto>(
      `/api/openiddict/authorizations/${id}`,
      {
        method: 'GET',
      },
    );
  }

  /**
   * Query the authorized paging list
   * @param input filter parameter
   * @returns Authorized entity data transfer object paginated list
   */
  function getPagedListApi(
    input?: OpenIddictAuthorizationGetListInput,
  ): Promise<PagedResultDto<OpenIddictAuthorizationDto>> {
    return request<PagedResultDto<OpenIddictAuthorizationDto>>(
      `/api/openiddict/authorizations`,
      {
        method: 'GET',
        params: input,
      },
    );
  }

  return {
    cancel,
    deleteApi,
    getApi,
    getPagedListApi,
  };
}
