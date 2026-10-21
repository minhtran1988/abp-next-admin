import type { ListResultDto, PagedResultDto } from "#/abp-core";

import type {
	WebhookAvailableGroupDto,
	WebhookSubscriptionCreateDto,
	WebhookSubscriptionDeleteManyInput,
	WebhookSubscriptionDto,
	WebhookSubscriptionGetListInput,
	WebhookSubscriptionUpdateDto,
} from "#/webhooks/subscriptions";

import requestClient from "../request";

/**
 * Create subscription
 * @param input parameter
 * @returns Subscribe to Dto
 */
export function createApi(input: WebhookSubscriptionCreateDto): Promise<WebhookSubscriptionDto> {
	return requestClient.post<WebhookSubscriptionDto>("/api/webhooks/subscriptions", input);
}

/**
 * Delete subscription
 * @param id subscription ID
 */
export function deleteApi(id: string): Promise<void> {
	return requestClient.delete(`/api/webhooks/subscriptions/${id}`);
}

/**
 * Delete subscriptions in batches
 * @param input parameter
 */
export function bulkDeleteApi(input: WebhookSubscriptionDeleteManyInput): Promise<void> {
	return requestClient.delete("/api/webhooks/subscriptions/delete-many", {
		data: input,
	});
}

/**
 * Query the list of all available Webhook groups
 * @returns Webhook grouping list
 */
export function getAllAvailableWebhooksApi(): Promise<ListResultDto<WebhookAvailableGroupDto>> {
	return requestClient.get<ListResultDto<WebhookAvailableGroupDto>>("/api/webhooks/subscriptions/availables");
}

/**
 * Query subscription
 * @param id subscription ID
 * @returns Subscribe to Dto
 */
export function getApi(id: string): Promise<WebhookSubscriptionDto> {
	return requestClient.get<WebhookSubscriptionDto>(`/api/webhooks/subscriptions/${id}`);
}

/**
 * Query subscription paging list
 * @param input filter parameter
 * @returns Subscribe to Dto list
 */
export function getPagedListApi(
	input: WebhookSubscriptionGetListInput,
): Promise<PagedResultDto<WebhookSubscriptionDto>> {
	return requestClient.get<PagedResultDto<WebhookSubscriptionDto>>("/api/webhooks/subscriptions", {
		params: input,
	});
}

/**
 * Update subscription
 * @param id subscription ID
 * @param input update parameters
 * @returns Subscribe to Dto
 */
export function updateApi(id: string, input: WebhookSubscriptionUpdateDto): Promise<WebhookSubscriptionDto> {
	return requestClient.put<WebhookSubscriptionDto>(`/api/webhooks/subscriptions/${id}`, input);
}
