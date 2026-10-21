import type { ListResultDto } from '@abp/core';

import type {
  NotificationGroupDefinitionCreateDto,
  NotificationGroupDefinitionDto,
  NotificationGroupDefinitionGetListInput,
  NotificationGroupDefinitionUpdateDto,
} from '../types';

import { useRequest } from '@abp/request';

export function useNotificationGroupDefinitionsApi() {
  const { cancel, request } = useRequest();

  /**
   * Delete notification group definition
   * @param name notification group name
   */
  function deleteApi(name: string): Promise<void> {
    return request(`/api/notifications/definitions/groups/${name}`, {
      method: 'DELETE',
    });
  }

  /**
   * Query notification group definition
   * @param name notification group name
   * @returns Notification group definition data transfer object
   */
  function getApi(name: string): Promise<NotificationGroupDefinitionDto> {
    return request<NotificationGroupDefinitionDto>(
      `/api/notifications/definitions/groups/${name}`,
      {
        method: 'GET',
      },
    );
  }

  /**
   * Query notification group definition list
   * @param input notification group filter conditions
   * @returns Notification group definition data transfer object list
   */
  function getListApi(
    input?: NotificationGroupDefinitionGetListInput,
  ): Promise<ListResultDto<NotificationGroupDefinitionDto>> {
    return request<ListResultDto<NotificationGroupDefinitionDto>>(
      `/api/notifications/definitions/groups`,
      {
        method: 'GET',
        params: input,
      },
    );
  }

  /**
   * Create notification group definition
   * @param input notification group definition parameters
   * @returns Notification group definition data transfer object
   */
  function createApi(
    input: NotificationGroupDefinitionCreateDto,
  ): Promise<NotificationGroupDefinitionDto> {
    return request<NotificationGroupDefinitionDto>(
      '/api/notifications/definitions/groups',
      {
        data: input,
        method: 'POST',
      },
    );
  }

  /**
   * Update notification group definition
   * @param name notification group name
   * @param input notification group definition parameters
   * @returns Notification group definition data transfer object
   */
  function updateApi(
    name: string,
    input: NotificationGroupDefinitionUpdateDto,
  ): Promise<NotificationGroupDefinitionDto> {
    return request<NotificationGroupDefinitionDto>(
      `/api/notifications/definitions/groups/${name}`,
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
