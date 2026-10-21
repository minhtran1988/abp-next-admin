import type { ListResultDto } from "#/abp-core";

import type { UserSubscreNotification } from "#/notifications/subscribes";

import requestClient from "../../request";

/**
 * Get all my subscription notifications
 * @returns Subscription notification list
 */
export function getMySubscribesApi(): Promise<ListResultDto<UserSubscreNotification>> {
	return requestClient.get<ListResultDto<UserSubscreNotification>>("/api/notifications/my-subscribes/all");
}

/**
 * Subscribe to notifications
 * @param name notification name
 */
export function subscribeApi(name: string): Promise<void> {
	return requestClient.post("/api/notifications/my-subscribes", {
		name,
	});
}

/**
 * Unsubscribe notifications
 * @param name notification name
 */
export function unSubscribeApi(name: string): Promise<void> {
	return requestClient.delete(`/api/notifications/my-subscribes?name=${name}`);
}
