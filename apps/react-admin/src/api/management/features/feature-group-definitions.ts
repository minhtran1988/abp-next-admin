import type { ListResultDto } from "#/abp-core";

import type {
	FeatureGroupDefinitionCreateDto,
	FeatureGroupDefinitionDto,
	FeatureGroupDefinitionGetListInput,
	FeatureGroupDefinitionUpdateDto,
} from "#/management/features/groups";

import requestClient from "../../request";

/**
 * Delete function definition
 * @param name function name
 */
export function deleteApi(name: string): Promise<void> {
	return requestClient.delete(`/api/feature-management/definitions/groups/${name}`);
}

/**
 * Query function definition
 * @param name function name
 * @returns function definition data transfer object
 */
export function getApi(name: string): Promise<FeatureGroupDefinitionDto> {
	return requestClient.get<FeatureGroupDefinitionDto>(`/api/feature-management/definitions/groups/${name}`);
}

/**
 * Query function definition list
 * @param input function filter conditions
 * @returns function definition data transfer object list
 */
export function getListApi(
	input?: FeatureGroupDefinitionGetListInput,
): Promise<ListResultDto<FeatureGroupDefinitionDto>> {
	return requestClient.get<ListResultDto<FeatureGroupDefinitionDto>>("/api/feature-management/definitions/groups", {
		params: input,
	});
}

/**
 * Create function definition
 * @param input function definition parameters
 * @returns function definition data transfer object
 */
export function createApi(input: FeatureGroupDefinitionCreateDto): Promise<FeatureGroupDefinitionDto> {
	return requestClient.post<FeatureGroupDefinitionDto>("/api/feature-management/definitions/groups", input);
}

/**
 * Update function definition
 * @param name function name
 * @param input function definition parameters
 * @returns function definition data transfer object
 */
export function updateApi(name: string, input: FeatureGroupDefinitionUpdateDto): Promise<FeatureGroupDefinitionDto> {
	return requestClient.put<FeatureGroupDefinitionDto>(`/api/feature-management/definitions/groups/${name}`, input);
}
