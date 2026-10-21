import type { PagedResultDto } from "#/abp-core";

import type { SmsMessageDto, SmsMessageGetListInput } from "#/platform/messages";

import requestClient from "../request";

/**
 * Get the paging list of SMS messages
 * @param {EmailMessageGetListInput} input parameter
 * @returns {Promise<PagedResultDto<EmailMessageDto>>} SMS message list
 */
export function getPagedListApi(input?: SmsMessageGetListInput): Promise<PagedResultDto<SmsMessageDto>> {
	return requestClient.get<PagedResultDto<SmsMessageDto>>("/api/platform/messages/sms", {
		params: input,
	});
}

/**
 * Get SMS messages
 * @param id Id
 * @returns {SmsMessageDto} SMS message
 */
export function getApi(id: string): Promise<SmsMessageDto> {
	return requestClient.get<SmsMessageDto>(`/api/platform/messages/sms/${id}`);
}

/**
 * Delete SMS messages
 * @param id Id
 * @returns {void}
 */
export function deleteApi(id: string): Promise<void> {
	return requestClient.delete(`/api/platform/messages/sms/${id}`);
}

/**
 * Send SMS messages
 * @param id Id
 * @returns {void}
 */
export function sendApi(id: string): Promise<void> {
	return requestClient.post(`/api/platform/messages/sms/${id}/send`);
}
