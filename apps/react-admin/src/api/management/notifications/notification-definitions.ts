import type { ListResultDto } from "#/abp-core";

import type {
	NotificationDefinitionCreateDto,
	NotificationDefinitionDto,
	NotificationDefinitionGetListInput,
	NotificationDefinitionUpdateDto,
} from "#/notifications/definitions";

import requestClient from "../../request";

/**
 * Delete notification definition
 * @param name notification name
 */
export function deleteApi(name: string): Promise<void> {
	return requestClient.delete(`/api/notifications/definitions/notifications/${name}`);
}

/**
 * Query notification definition
 * @param name notification name
 * @returns notification definition data transfer object
 */
export function getApi(name: string): Promise<NotificationDefinitionDto> {
	return requestClient.get<NotificationDefinitionDto>(`/api/notifications/definitions/notifications/${name}`);
}

/**
 * Query notification definition list
 * @param input notification filter conditions
 * @returns Notification definition data transfer object list
 */
export function getListApi(
	input?: NotificationDefinitionGetListInput,
): Promise<ListResultDto<NotificationDefinitionDto>> {
	return requestClient.get<ListResultDto<NotificationDefinitionDto>>("/api/notifications/definitions/notifications", {
		params: input,
	});
}

/**
 * Create notification definition
 * @param input notification definition parameters
 * @returns notification definition data transfer object
 */
export function createApi(input: NotificationDefinitionCreateDto): Promise<NotificationDefinitionDto> {
	return requestClient.post<NotificationDefinitionDto>("/api/notifications/definitions/notifications", input);
}

/**
 * Update notification definition
 * @param name notification name
 * @param input notification definition parameters
 * @returns notification definition data transfer object
 */
export function updateApi(name: string, input: NotificationDefinitionUpdateDto): Promise<NotificationDefinitionDto> {
	return requestClient.put<NotificationDefinitionDto>(`/api/notifications/definitions/notifications/${name}`, input);
}
