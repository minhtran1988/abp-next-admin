import type { PagedResultDto } from "#/abp-core";

import type {
	WebhookSendRecordDeleteManyInput,
	WebhookSendRecordDto,
	WebhookSendRecordGetListInput,
	WebhookSendRecordResendManyInput,
} from "#/webhooks/send-attempts";

import requestClient from "../request";

/**
 * Query sending records
 * @param id record ID
 * @returns Send record Dto
 */
export function getApi(id: string): Promise<WebhookSendRecordDto> {
	return requestClient.get<WebhookSendRecordDto>(`/api/webhooks/send-attempts/${id}`);
}

/**
 * Delete sending record
 * @param id record ID
 */
export function deleteApi(id: string): Promise<void> {
	return requestClient.delete(`/api/webhooks/send-attempts/${id}`);
}

/**
 * Delete sending records in batches
 * @param input parameter
 */
export function bulkDeleteApi(input: WebhookSendRecordDeleteManyInput): Promise<void> {
	return requestClient.delete("/api/webhooks/send-attempts/delete-many", {
		data: input,
	});
}

/**
 * Query the paging list of sending records
 * @param input filter parameter
 * @returns Send record Dto paging list
 */
export function getPagedListApi(input: WebhookSendRecordGetListInput): Promise<PagedResultDto<WebhookSendRecordDto>> {
	return requestClient.get<PagedResultDto<WebhookSendRecordDto>>("/api/webhooks/send-attempts", {
		params: input,
	});
}

/**
 * Resend
 * @param id record ID
 */
export function reSendApi(id: string): Promise<void> {
	return requestClient.post(`/api/webhooks/send-attempts/${id}/resend`);
}

/**
 * Batch Resend
 * @param input parameter
 */
export function bulkReSendApi(input: WebhookSendRecordResendManyInput): Promise<void> {
	return requestClient.post("/api/webhooks/send-attempts/resend-many", input);
}
