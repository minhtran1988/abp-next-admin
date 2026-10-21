import type { PagedResultDto } from '@abp/core';

import type { LogDto, LogGetListInput } from '../types/loggings';

import { useRequest } from '@abp/request';

export function useLoggingsApi() {
  const { cancel, request } = useRequest();

  /**
   * Get system logs
   * @param id log id
   */
  function getApi(id: string): Promise<LogDto> {
    return request<LogDto>(`/api/auditing/logging/${id}`, {
      method: 'GET',
    });
  }

  /**
   * Get the system log paging list
   * @param input parameter
   */
  function getPagedListApi(
    input: LogGetListInput,
  ): Promise<PagedResultDto<LogDto>> {
    return request<PagedResultDto<LogDto>>('/api/auditing/logging', {
      method: 'GET',
      params: input,
    });
  }

  return {
    cancel,
    getApi,
    getPagedListApi,
  };
}
