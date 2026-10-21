import type { PagedResultDto } from "#/abp-core";

import type { EmailMessageDto, EmailMessageGetListInput } from "#/platform/messages";

import requestClient from "../request";

/**
 * Get the paging list of email messages
 * @param {EmailMessageGetListInput} input parameter
 * @returns {Promise<PagedResultDto<EmailMessageDto>>} email message list
 */
export function getPagedListApi(input?: EmailMessageGetListInput): Promise<PagedResultDto<EmailMessageDto>> {
	return requestClient.get<PagedResultDto<EmailMessageDto>>("/api/platform/messages/email", {
		params: input,
	});
}

/**
 * Get email messages
 * @param id Id
 * @returns {EmailMessageDto} email message
 */
export function getApi(id: string): Promise<EmailMessageDto> {
	return requestClient.get<EmailMessageDto>(`/api/platform/messages/email/${id}`);
}

/**
 * Delete email message
 * @param id Id
 * @returns {void}
 */
export function deleteApi(id: string): Promise<void> {
	return requestClient.delete(`/api/platform/messages/email/${id}`);
}

/**
 * Send email message
 * @param id Id
 * @returns {void}
 */
export function sendApi(id: string): Promise<void> {
	return requestClient.post(`/api/platform/messages/email/${id}/send`);
}
