import type { ListResultDto } from "#/abp-core";

import type {
	PermissionGroupDefinitionCreateDto,
	PermissionGroupDefinitionDto,
	PermissionGroupDefinitionGetListInput,
	PermissionGroupDefinitionUpdateDto,
} from "#/management/permissions/groups";

import requestClient from "../../request";

/**
 * Delete permission definition
 * @param name permission name
 */
export function deleteApi(name: string): Promise<void> {
	return requestClient.delete(`/api/permission-management/definitions/groups/${name}`);
}

/**
 * Query permission definition
 * @param name permission name
 * @returns permission definition data transfer object
 */
export function getApi(name: string): Promise<PermissionGroupDefinitionDto> {
	return requestClient.get<PermissionGroupDefinitionDto>(`/api/permission-management/definitions/groups/${name}`);
}

/**
 * Query permission definition list
 * @param input permission filter conditions
 * @returns Permission definition data transfer object list
 */
export function getListApi(
	input?: PermissionGroupDefinitionGetListInput,
): Promise<ListResultDto<PermissionGroupDefinitionDto>> {
	return requestClient.get<ListResultDto<PermissionGroupDefinitionDto>>(
		"/api/permission-management/definitions/groups",
		{
			params: input,
		},
	);
}

/**
 * Create permission definition
 * @param input permission definition parameters
 * @returns permission definition data transfer object
 */
export function createApi(input: PermissionGroupDefinitionCreateDto): Promise<PermissionGroupDefinitionDto> {
	return requestClient.post<PermissionGroupDefinitionDto>("/api/permission-management/definitions/groups", input);
}

/**
 * Update permission definitions
 * @param name permission name
 * @param input permission definition parameters
 * @returns permission definition data transfer object
 */
export function updateApi(
	name: string,
	input: PermissionGroupDefinitionUpdateDto,
): Promise<PermissionGroupDefinitionDto> {
	return requestClient.put<PermissionGroupDefinitionDto>(
		`/api/permission-management/definitions/groups/${name}`,
		input,
	);
}
