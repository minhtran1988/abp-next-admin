import type { PagedResultDto } from '@abp/core';

import type {
  GetSecurityLogPagedRequest,
  SecurityLogDeleteManyInput,
  SecurityLogDto,
} from '../types/security-logs';

import { useRequest } from '@abp/request';

export function useSecurityLogsApi() {
  const { cancel, request } = useRequest();

  /**
   * Delete security log
   * @param id id
   */
  function deleteApi(id: string): Promise<void> {
    return request(`/api/auditing/security-log/${id}`, {
      method: 'DELETE',
    });
  }

  /**
   * Delete security logs in batches
   * @param input parameter
   */
  function deleteManyApi(input: SecurityLogDeleteManyInput): Promise<void> {
    return request(`/api/auditing/security-log/bulk`, {
      data: input,
      method: 'DELETE',
    });
  }

  /**
   * Query security logs
   * @param id id
   * @returns Security log entity data transfer object
   */
  function getApi(id: string): Promise<SecurityLogDto> {
    return request<SecurityLogDto>(`/api/auditing/security-log/${id}`, {
      method: 'GET',
    });
  }

  /**
   * Query the security log paging list
   * @param input filter parameter
   * @returns Security log entity data transfer object paging list
   */
  function getPagedListApi(
    input?: GetSecurityLogPagedRequest,
  ): Promise<PagedResultDto<SecurityLogDto>> {
    return request<PagedResultDto<SecurityLogDto>>(
      `/api/auditing/security-log`,
      {
        method: 'GET',
        params: input,
      },
    );
  }

  return {
    cancel,
    deleteApi,
    deleteManyApi,
    getApi,
    getPagedListApi,
  };
}
