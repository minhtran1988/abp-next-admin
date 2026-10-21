import type { ListResultDto } from '@abp/core';

import type {
  WebhookDefinitionCreateDto,
  WebhookDefinitionDto,
  WebhookDefinitionGetListInput,
  WebhookDefinitionUpdateDto,
} from '../types/definitions';

import { useRequest } from '@abp/request';

export function useWebhookDefinitionsApi() {
  const { cancel, request } = useRequest();

  /**
   * Delete Webhook definition
   * @param name Webhook name
   */
  function deleteApi(name: string): Promise<void> {
    return request(`/api/webhooks/definitions/${name}`, {
      method: 'DELETE',
    });
  }

  /**
   * Query Webhook definition
   * @param name Webhook name
   * @returns Webhook defines data transfer object
   */
  function getApi(name: string): Promise<WebhookDefinitionDto> {
    return request<WebhookDefinitionDto>(`/api/webhooks/definitions/${name}`, {
      method: 'GET',
    });
  }

  /**
   * Query Webhook definition list
   * @param input Webhook filter conditions
   * @returns Webhook defines a list of data transfer objects
   */
  function getListApi(
    input?: WebhookDefinitionGetListInput,
  ): Promise<ListResultDto<WebhookDefinitionDto>> {
    return request<ListResultDto<WebhookDefinitionDto>>(
      `/api/webhooks/definitions`,
      {
        method: 'GET',
        params: input,
      },
    );
  }

  /**
   * Create Webhook definition
   * @param input Webhook definition parameters
   * @returns Webhook defines data transfer object
   */
  function createApi(
    input: WebhookDefinitionCreateDto,
  ): Promise<WebhookDefinitionDto> {
    return request<WebhookDefinitionDto>('/api/webhooks/definitions', {
      data: input,
      method: 'POST',
    });
  }

  /**
   * Update Webhook definition
   * @param name Webhook name
   * @param input Webhook definition parameters
   * @returns Webhook defines data transfer object
   */
  function updateApi(
    name: string,
    input: WebhookDefinitionUpdateDto,
  ): Promise<WebhookDefinitionDto> {
    return request<WebhookDefinitionDto>(`/api/webhooks/definitions/${name}`, {
      data: input,
      method: 'PUT',
    });
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
