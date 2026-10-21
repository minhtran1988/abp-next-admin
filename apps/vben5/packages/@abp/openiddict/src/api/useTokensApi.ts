import type { PagedResultDto } from '@abp/core';

import type {
  OpenIddictTokenDto,
  OpenIddictTokenGetListInput,
} from '../types/tokens';

import { useRequest } from '@abp/request';

export function useTokensApi() {
  const { cancel, request } = useRequest();

  /**
   * Delete token
   * @param id token id
   */
  function deleteApi(id: string): Promise<void> {
    return request(`/api/openiddict/tokens/${id}`, {
      method: 'DELETE',
    });
  }

  /**
   * Query token
   * @param id token id
   * @returns token entity data transfer object
   */
  function getApi(id: string): Promise<OpenIddictTokenDto> {
    return request<OpenIddictTokenDto>(`/api/openiddict/tokens/${id}`, {
      method: 'GET',
    });
  }

  /**
   * Query token paging list
   * @param input filter parameter
   * @returns Token entity data transfer object paginated list
   */
  function getPagedListApi(
    input?: OpenIddictTokenGetListInput,
  ): Promise<PagedResultDto<OpenIddictTokenDto>> {
    return request<PagedResultDto<OpenIddictTokenDto>>(
      `/api/openiddict/tokens`,
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
