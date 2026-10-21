import type { PermissionProvider, PermissionResultDto, PermissionsUpdateDto } from "#/management/permissions";

import requestClient from "../../request";

/**
 * Query permissions
 * @param provider
 * @returns permission entity data transfer object
 */
export function getApi(provider: PermissionProvider): Promise<PermissionResultDto> {
	return requestClient.get<PermissionResultDto>("/api/permission-management/permissions", {
		params: provider,
	});
}

/**
 * Update permissions
 * @param provider
 * @param input
 */
export function updateApi(provider: PermissionProvider, input: PermissionsUpdateDto): Promise<void> {
	return requestClient.put("/api/permission-management/permissions", input, {
		params: provider,
	});
}
