import type { ListResultDto, PagedResultDto } from "#/abp-core";

import type {
	GetIdentityClaimTypePagedListInput,
	IdentityClaimTypeCreateDto,
	IdentityClaimTypeDto,
	IdentityClaimTypeUpdateDto,
} from "#/management/identity";
import requestClient from "../../request";

/**
 * Added user statement
 * @param input parameter
 * @returns User declared entity data transfer object
 */
export function createApi(input: IdentityClaimTypeCreateDto): Promise<IdentityClaimTypeDto> {
	return requestClient.post<IdentityClaimTypeDto>("/api/identity/claim-types", input);
}

/**
 * Delete user statement
 * @param id user statement id
 */
export function deleteApi(id: string): Promise<void> {
	return requestClient.delete(`/api/identity/claim-types/${id}`);
}

/**
 * Query user statement
 * @param id user statement id
 * @returns User declared entity data transfer object
 */
export function getApi(id: string): Promise<IdentityClaimTypeDto> {
	return requestClient.get<IdentityClaimTypeDto>(`/api/identity/claim-types/${id}`);
}

/**
 * Update user statement
 * @param id user statement id
 * @returns User declared entity data transfer object
 */
export function updateApi(id: string, input: IdentityClaimTypeUpdateDto): Promise<IdentityClaimTypeDto> {
	return requestClient.put<IdentityClaimTypeDto>(`/api/identity/claim-types/${id}`, input);
}

/**
 * Query user statement paging list
 * @param input filter parameter
 * @returns User declared entity data transfer object paginated list
 */
export function getPagedListApi(
	input?: GetIdentityClaimTypePagedListInput,
): Promise<PagedResultDto<IdentityClaimTypeDto>> {
	return requestClient.get<PagedResultDto<IdentityClaimTypeDto>>("/api/identity/claim-types", {
		params: input,
	});
}

/**
 * Get the list of available declaration types
 */
export function getAssignableClaimsApi(): Promise<ListResultDto<IdentityClaimTypeDto>> {
	return requestClient.get<ListResultDto<IdentityClaimTypeDto>>("/api/identity/claim-types/actived-list");
}
