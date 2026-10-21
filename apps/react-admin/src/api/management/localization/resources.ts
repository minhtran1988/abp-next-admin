import type { ListResultDto } from "#/abp-core";

import type {
	ResourceCreateDto,
	ResourceDto,
	ResourceGetListInput,
	ResourceUpdateDto,
} from "#/management/localization/resources";

import requestClient from "../../request";

/**
 * Query resource list
 * @param input parameter
 * @returns resource list
 */
export function getListApi(input?: ResourceGetListInput): Promise<ListResultDto<ResourceDto>> {
	return requestClient.get<ListResultDto<ResourceDto>>("/api/abp/localization/resources", {
		params: input,
	});
}

/**
 * Query resources
 * @param name resource name
 * @returns queried resources
 */
export function getApi(name: string): Promise<ResourceDto> {
	return requestClient.get<ResourceDto>(`/api/localization/resources/${name}`);
}

/**
 * Delete resources
 * @param name resource name
 */
export function deleteApi(name: string): Promise<void> {
	return requestClient.delete(`/api/localization/resources/${name}`);
}

/**
 * Create resources
 * @param input parameter
 * @returns created resources
 */
export function createApi(input: ResourceCreateDto): Promise<ResourceDto> {
	return requestClient.post<ResourceDto>("/api/localization/resources", input);
}

/**
 * Edit resources
 * @param name resource name
 * @param input parameter
 * @returns edited resources
 */
export function updateApi(name: string, input: ResourceUpdateDto): Promise<ResourceDto> {
	return requestClient.put<ResourceDto>(`/api/localization/resources/${name}`, input);
}
