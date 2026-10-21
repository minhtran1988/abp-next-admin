import type { PagedResultDto } from '@abp/core';

import type {
  AuditLogDeleteManyInput,
  AuditLogDto,
  AuditLogGetListInput,
} from '../types/audit-logs';

import { useRequest } from '@abp/request';

export function useAuditLogsApi() {
  const { cancel, request } = useRequest();

  /**
   * Get audit logs
   * @param id log id
   */
  function getApi(id: string): Promise<AuditLogDto> {
    return request<AuditLogDto>(`/api/auditing/audit-log/${id}`, {
      method: 'GET',
    });
  }

  /**
   * Get the audit log paging list
   * @param input parameter
   */
  function getPagedListApi(
    input: AuditLogGetListInput,
  ): Promise<PagedResultDto<AuditLogDto>> {
    return request<PagedResultDto<AuditLogDto>>('/api/auditing/audit-log', {
      method: 'GET',
      params: input,
    });
  }

  /**
   * Delete audit log
   * @param id log id
   */
  function deleteApi(id: string): Promise<void> {
    return request(`/api/auditing/audit-log/${id}`, {
      method: 'DELETE',
    });
  }

  /**
   * Delete audit logs in batches
   * @param input parameter
   */
  function deleteManyApi(input: AuditLogDeleteManyInput): Promise<void> {
    return request(`/api/auditing/audit-log/bulk`, {
      data: input,
      method: 'DELETE',
    });
  }

  return {
    cancel,
    deleteApi,
    deleteManyApi,
    getApi,
    getPagedListApi,
  };
}
