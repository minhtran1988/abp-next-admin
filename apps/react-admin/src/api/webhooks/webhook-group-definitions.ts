import type { ListResultDto } from "#/abp-core";

import type {
	WebhookGroupDefinitionCreateDto,
	WebhookGroupDefinitionDto,
	WebhookGroupDefinitionGetListInput,
	WebhookGroupDefinitionUpdateDto,
} from "#/webhooks/groups";

import requestClient from "../request";

/**
 * Delete Webhook group definition
 * @param name Webhook group name
 */
export function deleteApi(name: string): Promise<void> {
	return requestClient.delete(`/api/webhooks/definitions/groups/${name}`);
}

/**
 * Query Webhook group definition
 * @param name Webhook group name
 * @returns Webhook grouping defines data transfer object
 */
export function getApi(name: string): Promise<WebhookGroupDefinitionDto> {
	return requestClient.get<WebhookGroupDefinitionDto>(`/api/webhooks/definitions/groups/${name}`);
}

/**
 * Query the Webhook group definition list
 * @param input Webhook grouping filter conditions
 * @returns Webhook group definition data transfer object list
 */
export function getListApi(
	input?: WebhookGroupDefinitionGetListInput,
): Promise<ListResultDto<WebhookGroupDefinitionDto>> {
	return requestClient.get<ListResultDto<WebhookGroupDefinitionDto>>("/api/webhooks/definitions/groups", {
		params: input,
	});
}

/**
 * Create Webhook group definition
 * @param input Webhook group definition parameters
 * @returns Webhook grouping defines data transfer object
 */
export function createApi(input: WebhookGroupDefinitionCreateDto): Promise<WebhookGroupDefinitionDto> {
	return requestClient.post<WebhookGroupDefinitionDto>("/api/webhooks/definitions/groups", input);
}

/**
 * Update Webhook group definition
 * @param name Webhook group name
 * @param input Webhook group definition parameters
 * @returns Webhook grouping defines data transfer object
 */
export function updateApi(name: string, input: WebhookGroupDefinitionUpdateDto): Promise<WebhookGroupDefinitionDto> {
	return requestClient.put<WebhookGroupDefinitionDto>(`/api/webhooks/definitions/groups/${name}`, input);
}
