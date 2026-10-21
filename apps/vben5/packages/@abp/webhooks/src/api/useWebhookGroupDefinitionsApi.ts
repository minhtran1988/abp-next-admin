import type { ListResultDto } from '@abp/core';

import type {
  WebhookGroupDefinitionCreateDto,
  WebhookGroupDefinitionDto,
  WebhookGroupDefinitionGetListInput,
  WebhookGroupDefinitionUpdateDto,
} from '../types/groups';

import { useRequest } from '@abp/request';

export function useWebhookGroupDefinitionsApi() {
  const { cancel, request } = useRequest();

  /**
   * Delete Webhook group definition
   * @param name Webhook group name
   */
  function deleteApi(name: string): Promise<void> {
    return request(`/api/webhooks/definitions/groups/${name}`, {
      method: 'DELETE',
    });
  }

  /**
   * Query Webhook group definition
   * @param name Webhook group name
   * @returns Webhook grouping defines data transfer object
   */
  function getApi(name: string): Promise<WebhookGroupDefinitionDto> {
    return request<WebhookGroupDefinitionDto>(
      `/api/webhooks/definitions/groups/${name}`,
      {
        method: 'GET',
      },
    );
  }

  /**
   * Query the Webhook group definition list
   * @param input Webhook grouping filter conditions
   * @returns Webhook group definition data transfer object list
   */
  function getListApi(
    input?: WebhookGroupDefinitionGetListInput,
  ): Promise<ListResultDto<WebhookGroupDefinitionDto>> {
    return request<ListResultDto<WebhookGroupDefinitionDto>>(
      `/api/webhooks/definitions/groups`,
      {
        method: 'GET',
        params: input,
      },
    );
  }

  /**
   * Create Webhook group definition
   * @param input Webhook group definition parameters
   * @returns Webhook grouping defines data transfer object
   */
  function createApi(
    input: WebhookGroupDefinitionCreateDto,
  ): Promise<WebhookGroupDefinitionDto> {
    return request<WebhookGroupDefinitionDto>(
      '/api/webhooks/definitions/groups',
      {
        data: input,
        method: 'POST',
      },
    );
  }

  /**
   * Update Webhook group definition
   * @param name Webhook group name
   * @param input Webhook group definition parameters
   * @returns Webhook grouping defines data transfer object
   */
  function updateApi(
    name: string,
    input: WebhookGroupDefinitionUpdateDto,
  ): Promise<WebhookGroupDefinitionDto> {
    return request<WebhookGroupDefinitionDto>(
      `/api/webhooks/definitions/groups/${name}`,
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
