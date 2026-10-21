import type { ListResultDto } from "#/abp-core";

import type {
	NotificationGroupDefinitionCreateDto,
	NotificationGroupDefinitionDto,
	NotificationGroupDefinitionGetListInput,
	NotificationGroupDefinitionUpdateDto,
} from "#/notifications/groups";

import requestClient from "../../request";

/**
 * Delete notification group definition
 * @param name notification group name
 */
export function deleteApi(name: string): Promise<void> {
	return requestClient.delete(`/api/notifications/definitions/groups/${name}`);
}

/**
 * Query notification group definition
 * @param name notification group name
 * @returns Notification group definition data transfer object
 */
export function getApi(name: string): Promise<NotificationGroupDefinitionDto> {
	return requestClient.get<NotificationGroupDefinitionDto>(`/api/notifications/definitions/groups/${name}`);
}

/**
 * Query notification group definition list
 * @param input notification group filter conditions
 * @returns Notification group definition data transfer object list
 */
export function getListApi(
	input?: NotificationGroupDefinitionGetListInput,
): Promise<ListResultDto<NotificationGroupDefinitionDto>> {
	return requestClient.get<ListResultDto<NotificationGroupDefinitionDto>>("/api/notifications/definitions/groups", {
		params: input,
	});
}

/**
 * Create notification group definition
 * @param input notification group definition parameters
 * @returns Notification group definition data transfer object
 */
export function createApi(input: NotificationGroupDefinitionCreateDto): Promise<NotificationGroupDefinitionDto> {
	return requestClient.post<NotificationGroupDefinitionDto>("/api/notifications/definitions/groups", input);
}

/**
 * Update notification group definition
 * @param name notification group name
 * @param input notification group definition parameters
 * @returns Notification group definition data transfer object
 */
export function updateApi(
	name: string,
	input: NotificationGroupDefinitionUpdateDto,
): Promise<NotificationGroupDefinitionDto> {
	return requestClient.put<NotificationGroupDefinitionDto>(`/api/notifications/definitions/groups/${name}`, input);
}
