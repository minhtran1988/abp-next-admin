import type { PagedResultDto } from "#/abp-core";
import type { OpenIddictTokenDto, OpenIddictTokenGetListInput } from "#/openiddict/tokens";
import requestClient from "../request";

/**
 * Delete token
 * @param id token id
 */
export function deleteApi(id: string): Promise<void> {
	return requestClient.delete(`/api/openiddict/tokens/${id}`);
}

/**
 * Get token
 * @param id token id
 * @returns token entity data transfer object
 */
export function getApi(id: string): Promise<OpenIddictTokenDto> {
	return requestClient.get<OpenIddictTokenDto>(`/api/openiddict/tokens/${id}`);
}

/**
 * Get the token paging list
 * @param input filter parameter
 * @returns Token entity data transfer object paginated list
 */
export function getPagedListApi(input?: OpenIddictTokenGetListInput): Promise<PagedResultDto<OpenIddictTokenDto>> {
	return requestClient.get<PagedResultDto<OpenIddictTokenDto>>("/api/openiddict/tokens", {
		params: input,
	});
}
