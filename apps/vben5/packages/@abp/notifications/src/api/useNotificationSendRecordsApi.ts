import type { PagedResultDto } from '@abp/core';

import type {
  NotificationSendRecordDto,
  NotificationSendRecordGetPagedListInput,
} from '../types/send-records';

import { useRequest } from '@abp/request';

export function useNotificationSendRecordsApi() {
  const { cancel, request } = useRequest();
  /**
   * Get the paging list of notification sending records
   * @param input query filter parameters
   * @returns {Promise<PagedResultDto<NotificationSendRecordDto>>} Notification sending record paging list
   */
  function getPagedListApi(
    input?: NotificationSendRecordGetPagedListInput,
  ): Promise<PagedResultDto<NotificationSendRecordDto>> {
    return request<PagedResultDto<NotificationSendRecordDto>>(
      '/api/notifications/send-records',
      {
        method: 'GET',
        params: input,
      },
    );
  }
  /**
   * Delete sending record
   * @param id Send record ID
   * @returns {Promise<void>}
   */
  function deleteApi(id: string): Promise<void> {
    return request(`/api/notifications/send-records/${id}`, {
      method: 'DELETE',
    });
  }
  /**
   * Resend notification
   * @param id Send record ID
   * @returns {Promise<void>}
   */
  function reSendApi(id: string): Promise<void> {
    return request(`/api/notifications/send-records/${id}/re-send`, {
      method: 'POST',
    });
  }

  return {
    cancel,
    deleteApi,
    getPagedListApi,
    reSendApi,
  };
}
