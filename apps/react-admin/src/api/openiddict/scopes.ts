import type { PagedResultDto } from "#/abp-core";
import type {
	OpenIddictScopeCreateDto,
	OpenIddictScopeDto,
	OpenIddictScopeGetListInput,
	OpenIddictScopeUpdateDto,
} from "#/openiddict/scopes";

import requestClient from "../request";

/**
 * Create range
 * @param input parameter
 * @returns scope entity data transfer object
 */
export function createApi(input: OpenIddictScopeCreateDto): Promise<OpenIddictScopeDto> {
	return requestClient.post<OpenIddictScopeDto>("/api/openiddict/scopes", input);
}

/**
 * Delete range
 * @param id range id
 */
export function deleteApi(id: string): Promise<void> {
	return requestClient.delete(`/api/openiddict/scopes/${id}`);
}

/**
 * Get range
 * @param id range id
 * @returns scope entity data transfer object
 */
export function getApi(id: string): Promise<OpenIddictScopeDto> {
	return requestClient.get<OpenIddictScopeDto>(`/api/openiddict/scopes/${id}`);
}

/**
 *Update range
 * @param id range id
 * @param input update parameters
 * @returns scope entity data transfer object
 */
export function updateApi(id: string, input: OpenIddictScopeUpdateDto): Promise<OpenIddictScopeDto> {
	return requestClient.put<OpenIddictScopeDto>(`/api/openiddict/scopes/${id}`, input);
}

/**
 * Get range paging list
 * @param input filter parameter
 * @returns range entity data transfer object paginated list
 */
export function getPagedListApi(input?: OpenIddictScopeGetListInput): Promise<PagedResultDto<OpenIddictScopeDto>> {
	return requestClient.get<PagedResultDto<OpenIddictScopeDto>>("/api/openiddict/scopes", {
		params: input,
	});
}
