import type { PagedResultDto } from '@abp/core';

import type {
  GetMyNotifilerPagedListInput,
  MarkReadStateInput,
  UserNotificationDto,
} from '../types/my-notifilers';

import { useRequest } from '@abp/request';

export function useMyNotifilersApi() {
  const { cancel, request } = useRequest();
  /**
   * Get my notification list
   * @param {GetMyNotifilerPagedListInput} input parameter
   * @returns {Promise<PagedResultDto<UserNotificationDto>>} notification paging list
   */
  function getMyNotifilersApi(
    input?: GetMyNotifilerPagedListInput,
  ): Promise<PagedResultDto<UserNotificationDto>> {
    return request<PagedResultDto<UserNotificationDto>>(
      '/api/notifications/my-notifilers',
      {
        method: 'GET',
        params: input,
      },
    );
  }
  /**
   * Delete my notification
   * @param {string} id notification id
   * @returns {void}
   */
  function deleteMyNotifilerApi(id: string): Promise<void> {
    return request(`/api/notifications/my-notifilers/${id}`, {
      method: 'DELETE',
    });
  }
  /**
   * Set notification read status
   * @param {MarkReadStateInput} input parameter
   * @returns {void}
   */
  function markReadStateApi(input: MarkReadStateInput): Promise<void> {
    return request(`/api/notifications/my-notifilers/mark-read-state`, {
      data: input,
      method: 'PUT',
    });
  }

  return {
    cancel,
    deleteMyNotifilerApi,
    getMyNotifilersApi,
    markReadStateApi,
  };
}
