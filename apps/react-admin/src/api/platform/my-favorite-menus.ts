import type { ListResultDto } from "#/abp-core";

import type { UserFavoriteMenuCreateDto, UserFavoriteMenuDto } from "#/platform/favorites";

import requestClient from "../request";

/**
 * Added common menu
 * @param input parameter
 * @returns Common menu
 */
export function createApi(input: UserFavoriteMenuCreateDto): Promise<UserFavoriteMenuDto> {
	return requestClient.post<UserFavoriteMenuDto>("/api/platform/menus/favorites/my-favorite-menus", input);
}

/**
 * Delete frequently used menus
 * @param id menuId
 */
export function deleteApi(id: string): Promise<void> {
	return requestClient.delete(`/api/platform/menus/favorites/my-favorite-menus/${id}`);
}

/**
 * Get a list of frequently used menus
 * @param framework ui framework
 * @returns menu list
 */
export function getListApi(framework?: string): Promise<ListResultDto<UserFavoriteMenuDto>> {
	return requestClient.get<ListResultDto<UserFavoriteMenuDto>>("/api/platform/menus/favorites/my-favorite-menus", {
		params: { framework },
	});
}
