import type { ListResultDto } from "#/abp-core";

import type {
	PermissionDefinitionCreateDto,
	PermissionDefinitionDto,
	PermissionDefinitionGetListInput,
	PermissionDefinitionUpdateDto,
} from "#/management/permissions/definitions";

import requestClient from "../../request";

/**
 * Delete permission definition
 * @param name permission name
 */
export function deleteApi(name: string): Promise<void> {
	return requestClient.delete(`/api/permission-management/definitions/${name}`);
}

/**
 * Query permission definition
 * @param name permission name
 * @returns permission definition data transfer object
 */
export function getApi(name: string): Promise<PermissionDefinitionDto> {
	return requestClient.get<PermissionDefinitionDto>(`/api/permission-management/definitions/${name}`);
}

/**
 * Query permission definition list
 * @param input permission filter conditions
 * @returns Permission definition data transfer object list
 */
export function getListApi(input?: PermissionDefinitionGetListInput): Promise<ListResultDto<PermissionDefinitionDto>> {
	return requestClient.get<ListResultDto<PermissionDefinitionDto>>("/api/permission-management/definitions", {
		params: input,
	});
}

/**
 * Create permission definition
 * @param input permission definition parameters
 * @returns permission definition data transfer object
 */
export function createApi(input: PermissionDefinitionCreateDto): Promise<PermissionDefinitionDto> {
	return requestClient.post<PermissionDefinitionDto>("/api/permission-management/definitions", input);
}

/**
 * Update permission definitions
 * @param name permission name
 * @param input permission definition parameters
 * @returns permission definition data transfer object
 */
export function updateApi(name: string, input: PermissionDefinitionUpdateDto): Promise<PermissionDefinitionDto> {
	return requestClient.put<PermissionDefinitionDto>(`/api/permission-management/definitions/${name}`, input);
}
