import type { ListResultDto } from '@abp/core';

import type { UserSubscreNotification } from '../types/subscribes';

import { useRequest } from '@abp/request';

export function useMySubscribesApi() {
  const { cancel, request } = useRequest();

  /**
   * Get all my subscription notifications
   * @returns Subscription notification list
   */
  function getMySubscribesApi(): Promise<
    ListResultDto<UserSubscreNotification>
  > {
    return request<ListResultDto<UserSubscreNotification>>(
      '/api/notifications/my-subscribes/all',
      {
        method: 'GET',
      },
    );
  }

  /**
   * Subscribe to notifications
   * @param name notification name
   */
  function subscribeApi(name: string): Promise<void> {
    return request('/api/notifications/my-subscribes', {
      data: {
        name,
      },
      method: 'POST',
    });
  }

  /**
   * Unsubscribe notifications
   * @param name notification name
   */
  function unSubscribeApi(name: string): Promise<void> {
    return request(`/api/notifications/my-subscribes?name=${name}`, {
      method: 'DELETE',
    });
  }

  return {
    cancel,
    getMySubscribesApi,
    subscribeApi,
    unSubscribeApi,
  };
}
