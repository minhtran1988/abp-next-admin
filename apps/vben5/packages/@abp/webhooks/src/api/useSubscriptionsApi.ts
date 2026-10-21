import type { ListResultDto, PagedResultDto } from '@abp/core';

import type {
  WebhookAvailableGroupDto,
  WebhookSubscriptionCreateDto,
  WebhookSubscriptionDeleteManyInput,
  WebhookSubscriptionDto,
  WebhookSubscriptionGetListInput,
  WebhookSubscriptionUpdateDto,
} from '../types/subscriptions';

import { useRequest } from '@abp/request';

export function useSubscriptionsApi() {
  const { cancel, request } = useRequest();

  /**
   * Create subscription
   * @param input parameter
   * @returns Subscribe to Dto
   */
  function createApi(
    input: WebhookSubscriptionCreateDto,
  ): Promise<WebhookSubscriptionDto> {
    return request<WebhookSubscriptionDto>(`/api/webhooks/subscriptions`, {
      data: input,
      method: 'POST',
    });
  }
  /**
   * Delete subscription
   * @param id subscription ID
   */
  function deleteApi(id: string): Promise<void> {
    return request(`/api/webhooks/subscriptions/${id}`, {
      method: 'DELETE',
    });
  }
  /**
   * Delete subscriptions in batches
   * @param input parameter
   */
  function bulkDeleteApi(
    input: WebhookSubscriptionDeleteManyInput,
  ): Promise<void> {
    return request(`/api/webhooks/subscriptions/delete-many`, {
      data: input,
      method: 'DELETE',
    });
  }
  /**
   * Query the list of all available Webhook groups
   * @returns Webhook grouping list
   */
  function getAllAvailableWebhooksApi(): Promise<
    ListResultDto<WebhookAvailableGroupDto>
  > {
    return request<ListResultDto<WebhookAvailableGroupDto>>(
      `/api/webhooks/subscriptions/availables`,
      {
        method: 'GET',
      },
    );
  }
  /**
   * Query subscription
   * @param id subscription ID
   * @returns Subscribe to Dto
   */
  function getApi(id: string): Promise<WebhookSubscriptionDto> {
    return request<WebhookSubscriptionDto>(
      `/api/webhooks/subscriptions/${id}`,
      {
        method: 'GET',
      },
    );
  }
  /**
   * Query subscription paging list
   * @param input filter parameter
   * @returns Subscribe to Dto list
   */
  function getPagedListApi(
    input: WebhookSubscriptionGetListInput,
  ): Promise<PagedResultDto<WebhookSubscriptionDto>> {
    return request<PagedResultDto<WebhookSubscriptionDto>>(
      `/api/webhooks/subscriptions`,
      {
        method: 'GET',
        params: input,
      },
    );
  }
  /**
   * Update subscription
   * @param id subscription ID
   * @param input update parameters
   * @returns Subscribe to Dto
   */
  function updateApi(
    id: string,
    input: WebhookSubscriptionUpdateDto,
  ): Promise<WebhookSubscriptionDto> {
    return request<WebhookSubscriptionDto>(
      `/api/webhooks/subscriptions/${id}`,
      {
        data: input,
        method: 'PUT',
      },
    );
  }

  return {
    bulkDeleteApi,
    cancel,
    createApi,
    deleteApi,
    getAllAvailableWebhooksApi,
    getApi,
    getPagedListApi,
    updateApi,
  };
}
