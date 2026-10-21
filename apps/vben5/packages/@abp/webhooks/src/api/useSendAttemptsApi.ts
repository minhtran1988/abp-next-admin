import type { PagedResultDto } from '@abp/core';

import type {
  WebhookSendRecordDeleteManyInput,
  WebhookSendRecordDto,
  WebhookSendRecordGetListInput,
  WebhookSendRecordResendManyInput,
} from '../types/sendAttempts';

import { useRequest } from '@abp/request';

export function useSendAttemptsApi() {
  const { cancel, request } = useRequest();

  /**
   * Query sending records
   * @param id record ID
   * @returns Send record Dto
   */
  function getApi(id: string): Promise<WebhookSendRecordDto> {
    return request<WebhookSendRecordDto>(`/api/webhooks/send-attempts/${id}`, {
      method: 'GET',
    });
  }
  /**
   * Delete sending record
   * @param id record ID
   */
  function deleteApi(id: string): Promise<void> {
    return request(`/api/webhooks/send-attempts/${id}`, {
      method: 'DELETE',
    });
  }
  /**
   * Delete sending records in batches
   * @param input parameter
   */
  function bulkDeleteApi(
    input: WebhookSendRecordDeleteManyInput,
  ): Promise<void> {
    return request(`/api/webhooks/send-attempts/delete-many`, {
      data: input,
      method: 'DELETE',
    });
  }
  /**
   * Query the paging list of sending records
   * @param input filter parameter
   * @returns Send record Dto paging list
   */
  function getPagedListApi(
    input: WebhookSendRecordGetListInput,
  ): Promise<PagedResultDto<WebhookSendRecordDto>> {
    return request<PagedResultDto<WebhookSendRecordDto>>(
      `/api/webhooks/send-attempts`,
      {
        method: 'GET',
        params: input,
      },
    );
  }
  /**
   * Resend
   * @param id record ID
   */
  function reSendApi(id: string): Promise<void> {
    return request(`/api/webhooks/send-attempts/${id}/resend`, {
      method: 'POST',
    });
  }
  /**
   * Batch Resend
   * @param input parameter
   */
  function bulkReSendApi(
    input: WebhookSendRecordResendManyInput,
  ): Promise<void> {
    return request(`/api/webhooks/send-attempts/resend-many`, {
      data: input,
      method: 'POST',
    });
  }

  return {
    bulkDeleteApi,
    bulkReSendApi,
    cancel,
    deleteApi,
    getApi,
    getPagedListApi,
    reSendApi,
  };
}
