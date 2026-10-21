import type { PagedResultDto } from "#/abp-core";
import type {
	OpenIddictApplicationCreateDto,
	OpenIddictApplicationDto,
	OpenIddictApplicationGetListInput,
	OpenIddictApplicationUpdateDto,
} from "#/openiddict/applications";

import requestClient from "../request";

/**
 * Create application
 * @param input parameter
 * @returns application entity data transfer object
 */
export function createApi(input: OpenIddictApplicationCreateDto): Promise<OpenIddictApplicationDto> {
	return requestClient.post<OpenIddictApplicationDto>("/api/openiddict/applications", input);
}

/**
 * Delete app
 * @param id application id
 */
export function deleteApi(id: string): Promise<void> {
	return requestClient.delete(`/api/openiddict/applications/${id}`);
}

/**
 * Query application
 * @param id application id
 * @returns application entity data transfer object
 */
export function getApi(id: string): Promise<OpenIddictApplicationDto> {
	return requestClient.get<OpenIddictApplicationDto>(`/api/openiddict/applications/${id}`);
}

/**
 * Update application
 * @param id application id
 * @param input update parameters
 * @returns application entity data transfer object
 */
export function updateApi(id: string, input: OpenIddictApplicationUpdateDto): Promise<OpenIddictApplicationDto> {
	return requestClient.put<OpenIddictApplicationDto>(`/api/openiddict/applications/${id}`, input);
}

/**
 * Query application paging list
 * @param input filter parameter
 * @returns paging list of application entity data transfer objects
 */
export function getPagedListApi(
	input?: OpenIddictApplicationGetListInput,
): Promise<PagedResultDto<OpenIddictApplicationDto>> {
	return requestClient.get<PagedResultDto<OpenIddictApplicationDto>>("/api/openiddict/applications", {
		params: input,
	});
}
