import type { PagedResultDto } from "#/abp-core";
import type { OpenIddictAuthorizationDto, OpenIddictAuthorizationGetListInput } from "#/openiddict/authorizations";

import requestClient from "../request";

/**
 * Delete authorization
 * @param id authorization id
 */
export function deleteApi(id: string): Promise<void> {
	return requestClient.delete(`/api/openiddict/authorizations/${id}`);
}

/**
 * Query authorization
 * @param id authorization id
 * @returns Authorization entity data transfer object
 */
export function getApi(id: string): Promise<OpenIddictAuthorizationDto> {
	return requestClient.get<OpenIddictAuthorizationDto>(`/api/openiddict/authorizations/${id}`);
}

/**
 * Query the authorized paging list
 * @param input filter parameter
 * @returns Authorized entity data transfer object paginated list
 */
export function getPagedListApi(
	input?: OpenIddictAuthorizationGetListInput,
): Promise<PagedResultDto<OpenIddictAuthorizationDto>> {
	return requestClient.get<PagedResultDto<OpenIddictAuthorizationDto>>("/api/openiddict/authorizations", {
		params: input,
	});
}
