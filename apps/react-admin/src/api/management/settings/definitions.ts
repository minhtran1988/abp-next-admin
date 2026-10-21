import type { ListResultDto } from "#/abp-core";

import type {
	SettingDefinitionCreateDto,
	SettingDefinitionDto,
	SettingDefinitionGetListInput,
	SettingDefinitionUpdateDto,
} from "#/management/settings/definitions";
import requestClient from "@/api/request";

/**
 * Delete setting definition
 * @param name set name
 */
export function deleteApi(name: string): Promise<void> {
	return requestClient.delete(`/api/setting-management/settings/definitions/${name}`);
}

/**
 * Query setting definition
 * @param name set name
 * @returns Set the definition data transfer object
 */
export function getApi(name: string): Promise<SettingDefinitionDto> {
	return requestClient.get<SettingDefinitionDto>(`/api/setting-management/settings/definitions/${name}`);
}

/**
 * Query setting definition list
 * @param input Set filter conditions
 * @returns Set the definition data transfer object list
 */
export function getListApi(input?: SettingDefinitionGetListInput): Promise<ListResultDto<SettingDefinitionDto>> {
	return requestClient.get<ListResultDto<SettingDefinitionDto>>("/api/setting-management/settings/definitions", {
		params: input,
	});
}

/**
 * Create settings definition
 * @param input Set definition parameters
 * @returns Set the definition data transfer object
 */
export function createApi(input: SettingDefinitionCreateDto): Promise<SettingDefinitionDto> {
	return requestClient.post<SettingDefinitionDto>("/api/setting-management/settings/definitions", input);
}

/**
 * Update settings definition
 * @param name set name
 * @param input Set definition parameters
 * @returns Set the definition data transfer object
 */
export function updateApi(name: string, input: SettingDefinitionUpdateDto): Promise<SettingDefinitionDto> {
	return requestClient.put<SettingDefinitionDto>(`/api/setting-management/settings/definitions/${name}`, input);
}
