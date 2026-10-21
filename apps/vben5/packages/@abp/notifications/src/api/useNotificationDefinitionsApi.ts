import type { ListResultDto } from '@abp/core';

import type {
  NotificationDefinitionCreateDto,
  NotificationDefinitionDto,
  NotificationDefinitionGetListInput,
  NotificationDefinitionUpdateDto,
} from '../types/definitions';

import { useRequest } from '@abp/request';

export function useNotificationDefinitionsApi() {
  const { cancel, request } = useRequest();

  /**
   * Delete notification definition
   * @param name notification name
   */
  function deleteApi(name: string): Promise<void> {
    return request(`/api/notifications/definitions/notifications/${name}`, {
      method: 'DELETE',
    });
  }

  /**
   * Query notification definition
   * @param name notification name
   * @returns notification definition data transfer object
   */
  function getApi(name: string): Promise<NotificationDefinitionDto> {
    return request<NotificationDefinitionDto>(
      `/api/notifications/definitions/notifications/${name}`,
      {
        method: 'GET',
      },
    );
  }

  /**
   * Query notification definition list
   * @param input notification filter conditions
   * @returns Notification definition data transfer object list
   */
  function getListApi(
    input?: NotificationDefinitionGetListInput,
  ): Promise<ListResultDto<NotificationDefinitionDto>> {
    return request<ListResultDto<NotificationDefinitionDto>>(
      `/api/notifications/definitions/notifications`,
      {
        method: 'GET',
        params: input,
      },
    );
  }

  /**
   * Create notification definition
   * @param input notification definition parameters
   * @returns notification definition data transfer object
   */
  function createApi(
    input: NotificationDefinitionCreateDto,
  ): Promise<NotificationDefinitionDto> {
    return request<NotificationDefinitionDto>(
      '/api/notifications/definitions/notifications',
      {
        data: input,
        method: 'POST',
      },
    );
  }

  /**
   * Update notification definition
   * @param name notification name
   * @param input notification definition parameters
   * @returns notification definition data transfer object
   */
  function updateApi(
    name: string,
    input: NotificationDefinitionUpdateDto,
  ): Promise<NotificationDefinitionDto> {
    return request<NotificationDefinitionDto>(
      `/api/notifications/definitions/notifications/${name}`,
      {
        data: input,
        method: 'PUT',
      },
    );
  }

  return {
    cancel,
    createApi,
    deleteApi,
    getApi,
    getListApi,
    updateApi,
  };
}
