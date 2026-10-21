import type { ListResultDto } from "#/abp-core";

import type {
	NotificationGroupDto,
	NotificationProviderDto,
	NotificationTemplateDto,
} from "#/notifications/definitions";
import type { NotificationSendInput, NotificationTemplateSendInput } from "#/notifications/notifications";

import requestClient from "@/api/request";

/**
 * Get the list of available notification providers
 * @returns {Promise<ListResultDto<NotificationProviderDto>>} List of available notification providers
 */
export function getAssignableProvidersApi(): Promise<ListResultDto<NotificationProviderDto>> {
	return requestClient.get<ListResultDto<NotificationProviderDto>>("/api/notifications/assignable-providers");
}

/**
 * Get the list of available notifications
 * @returns {Promise<ListResultDto<NotificationGroupDto>>} available notification list
 */
export function getAssignableNotifiersApi(): Promise<ListResultDto<NotificationGroupDto>> {
	return requestClient.get<ListResultDto<NotificationGroupDto>>("/api/notifications/assignables");
}
/**
 * Get the list of available notification templates
 * @returns {Promise<ListResultDto<NotificationTemplateDto>>} list of available notification templates
 */
export function getAssignableTemplatesApi(): Promise<ListResultDto<NotificationTemplateDto>> {
	return requestClient.get<ListResultDto<NotificationTemplateDto>>("/api/notifications/assignable-templates", {
		method: "GET",
	});
}
/**
 * Send notification
 * @param input parameter
 * @returns {Promise<void>}
 */
export function sendNotiferApi(input: NotificationSendInput): Promise<void> {
	return requestClient.post("/api/notifications/send", input);
}
/**
 * Send template notification
 * @param input parameter
 * @returns {Promise<void>}
 */
export function sendTemplateNotiferApi(input: NotificationTemplateSendInput): Promise<void> {
	return requestClient.post("/api/notifications/send/template", input);
}
