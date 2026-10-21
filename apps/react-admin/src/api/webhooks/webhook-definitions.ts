import type { ListResultDto } from "#/abp-core";

import type {
	WebhookDefinitionCreateDto,
	WebhookDefinitionDto,
	WebhookDefinitionGetListInput,
	WebhookDefinitionUpdateDto,
} from "#/webhooks/definitions";

import requestClient from "../request";

/**
 * Delete Webhook definition
 * @param name Webhook name
 */
export function deleteApi(name: string): Promise<void> {
	return requestClient.delete(`/api/webhooks/definitions/${name}`);
}

/**
 * Query Webhook definition
 * @param name Webhook name
 * @returns Webhook defines data transfer object
 */
export function getApi(name: string): Promise<WebhookDefinitionDto> {
	return requestClient.get<WebhookDefinitionDto>(`/api/webhooks/definitions/${name}`);
}

/**
 * Query Webhook definition list
 * @param input Webhook filter conditions
 * @returns Webhook defines a list of data transfer objects
 */
export function getListApi(input?: WebhookDefinitionGetListInput): Promise<ListResultDto<WebhookDefinitionDto>> {
	return requestClient.get<ListResultDto<WebhookDefinitionDto>>("/api/webhooks/definitions", {
		params: input,
	});
}

/**
 * Create Webhook definition
 * @param input Webhook definition parameters
 * @returns Webhook defines data transfer object
 */
export function createApi(input: WebhookDefinitionCreateDto): Promise<WebhookDefinitionDto> {
	return requestClient.post<WebhookDefinitionDto>("/api/webhooks/definitions", input);
}

/**
 * Update Webhook definition
 * @param name Webhook name
 * @param input Webhook definition parameters
 * @returns Webhook defines data transfer object
 */
export function updateApi(name: string, input: WebhookDefinitionUpdateDto): Promise<WebhookDefinitionDto> {
	return requestClient.put<WebhookDefinitionDto>(`/api/webhooks/definitions/${name}`, input);
}
