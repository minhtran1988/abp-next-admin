import type { PagedResultDto } from "#/abp-core";

import type {
	GetMyNotifilerPagedListInput,
	MarkReadStateInput,
	UserNotificationDto,
} from "#/notifications/my-notifilers";

import requestClient from "@/api/request";
/**
 * Get my notification list
 * @param {GetMyNotifilerPagedListInput} input parameter
 * @returns {Promise<PagedResultDto<UserNotificationDto>>} notification paging list
 */
export function getMyNotifilersApi(input?: GetMyNotifilerPagedListInput): Promise<PagedResultDto<UserNotificationDto>> {
	return requestClient.get<PagedResultDto<UserNotificationDto>>("/api/notifications/my-notifilers", {
		params: input,
	});
}
/**
 * Delete my notification
 * @param {string} id notification id
 * @returns {void}
 */
export function deleteMyNotifilerApi(id: string): Promise<void> {
	return requestClient.delete(`/api/notifications/my-notifilers/${id}`);
}
/**
 * Set notification read status
 * @param {MarkReadStateInput} input parameter
 * @returns {void}
 */
export function markReadStateApi(input: MarkReadStateInput): Promise<void> {
	return requestClient.put("/api/notifications/my-notifilers/mark-read-state", input);
}
