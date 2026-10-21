import type { ListResultDto } from "#/abp-core";

import type {
	FeatureDefinitionCreateDto,
	FeatureDefinitionDto,
	FeatureDefinitionGetListInput,
	FeatureDefinitionUpdateDto,
} from "#/management/features/definitions";

import requestClient from "../../request";

/**
 * Delete function definition
 * @param name function name
 */
export function deleteApi(name: string): Promise<void> {
	return requestClient.delete(`/api/feature-management/definitions/${name}`);
}

/**
 * Query function definition
 * @param name function name
 * @returns function definition data transfer object
 */
export function getApi(name: string): Promise<FeatureDefinitionDto> {
	return requestClient.get<FeatureDefinitionDto>(`/api/feature-management/definitions/${name}`);
}

/**
 * Query function definition list
 * @param input function filter conditions
 * @returns function definition data transfer object list
 */
export function getListApi(input?: FeatureDefinitionGetListInput): Promise<ListResultDto<FeatureDefinitionDto>> {
	return requestClient.get<ListResultDto<FeatureDefinitionDto>>("/api/feature-management/definitions", {
		params: input,
	});
}

/**
 * Create function definition
 * @param input function definition parameters
 * @returns function definition data transfer object
 */
export function createApi(input: FeatureDefinitionCreateDto): Promise<FeatureDefinitionDto> {
	return requestClient.post<FeatureDefinitionDto>("/api/feature-management/definitions", input);
}

/**
 * Update function definition
 * @param name function name
 * @param input function definition parameters
 * @returns function definition data transfer object
 */
export function updateApi(name: string, input: FeatureDefinitionUpdateDto): Promise<FeatureDefinitionDto> {
	return requestClient.put<FeatureDefinitionDto>(`/api/feature-management/definitions/${name}`, input);
}
