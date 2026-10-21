import type { ListResultDto } from "#/abp-core";

import type { IdentityUserDto, UserLookupCountInput, UserLookupSearchInput } from "#/management/identity/user";

import requestClient from "../../request";

/**
 * Query user by id
 * @param id user id
 * @returns User entity data transfer object
 */
export function findByIdApi(id: string): Promise<IdentityUserDto> {
	return requestClient.get<IdentityUserDto>(`/api/identity/users/lookup/${id}`);
}

/**
 * Query users by username
 * @param userName username
 * @returns User entity data transfer object
 */
export function findByUserNameApi(userName: string): Promise<IdentityUserDto> {
	return requestClient.get<IdentityUserDto>(`/api/identity/users/lookup/by-username/${userName}`);
}

/**
 * Search user list
 * @param input search filter conditions
 * @returns User entity data transfer object list
 */
export function searchApi(input?: UserLookupSearchInput): Promise<ListResultDto<IdentityUserDto>> {
	return requestClient.get<ListResultDto<IdentityUserDto>>("/api/identity/users/lookup/search", {
		params: input,
	});
}

/**
 *Number of search users
 * @param input search filter conditions
 */
export function countApi(input?: UserLookupCountInput): Promise<number> {
	return requestClient.get<number>("/api/identity/users/lookup/count", {
		params: input,
	});
}
