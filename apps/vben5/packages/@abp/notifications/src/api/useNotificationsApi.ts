import type { ListResultDto } from '@abp/core';

import type {
  NotificationGroupDto,
  NotificationProviderDto,
  NotificationTemplateDto,
} from '../types/definitions';
import type {
  NotificationSendInput,
  NotificationTemplateSendInput,
} from '../types/notifications';

import { useRequest } from '@abp/request';

export function useNotificationsApi() {
  const { cancel, request } = useRequest();
  /**
   * Get the list of available notification providers
   * @returns {Promise<ListResultDto<NotificationProviderDto>>} List of available notification providers
   */
  function getAssignableProvidersApi(): Promise<
    ListResultDto<NotificationProviderDto>
  > {
    return request<ListResultDto<NotificationProviderDto>>(
      '/api/notifications/assignable-providers',
      {
        method: 'GET',
      },
    );
  }
  /**
   * Get the list of available notifications
   * @returns {Promise<ListResultDto<NotificationGroupDto>>} available notification list
   */
  function getAssignableNotifiersApi(): Promise<
    ListResultDto<NotificationGroupDto>
  > {
    return request<ListResultDto<NotificationGroupDto>>(
      '/api/notifications/assignables',
      {
        method: 'GET',
      },
    );
  }
  /**
   * Get the list of available notification templates
   * @returns {Promise<ListResultDto<NotificationTemplateDto>>} list of available notification templates
   */
  function getAssignableTemplatesApi(): Promise<
    ListResultDto<NotificationTemplateDto>
  > {
    return request<ListResultDto<NotificationTemplateDto>>(
      '/api/notifications/assignable-templates',
      {
        method: 'GET',
      },
    );
  }
  /**
   * Send notification
   * @param input parameter
   * @returns {Promise<void>}
   */
  function sendNotiferApi(input: NotificationSendInput): Promise<void> {
    return request('/api/notifications/send', {
      data: input,
      method: 'POST',
    });
  }
  /**
   * Send template notification
   * @param input parameter
   * @returns {Promise<void>}
   */
  function sendTemplateNotiferApi(
    input: NotificationTemplateSendInput,
  ): Promise<void> {
    return request('/api/notifications/send/template', {
      data: input,
      method: 'POST',
    });
  }

  return {
    cancel,
    getAssignableNotifiersApi,
    getAssignableProvidersApi,
    getAssignableTemplatesApi,
    sendNotiferApi,
    sendTemplateNotiferApi,
  };
}
