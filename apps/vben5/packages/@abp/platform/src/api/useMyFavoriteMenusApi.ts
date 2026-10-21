import type { ListResultDto } from '@abp/core';

import type {
  UserFavoriteMenuCreateDto,
  UserFavoriteMenuDto,
} from '../types/favorites';

import { useRequest } from '@abp/request';

export function useMyFavoriteMenusApi() {
  const { cancel, request } = useRequest();

  /**
   * Added common menu
   * @param input parameter
   * @returns Common menu
   */
  function createApi(
    input: UserFavoriteMenuCreateDto,
  ): Promise<UserFavoriteMenuDto> {
    return request<UserFavoriteMenuDto>(
      `/api/platform/menus/favorites/my-favorite-menus`,
      {
        data: input,
        method: 'POST',
      },
    );
  }

  /**
   * Delete frequently used menus
   * @param id menuId
   */
  function deleteApi(id: string): Promise<void> {
    return request(`/api/platform/menus/favorites/my-favorite-menus/${id}`, {
      method: 'DELETE',
    });
  }

  /**
   * Get a list of frequently used menus
   * @param framework ui framework
   * @returns menu list
   */
  function getListApi(
    framework?: string,
  ): Promise<ListResultDto<UserFavoriteMenuDto>> {
    return request<ListResultDto<UserFavoriteMenuDto>>(
      `/api/platform/menus/favorites/my-favorite-menus?framework=${framework}`,
      {
        method: 'GET',
      },
    );
  }

  return {
    cancel,
    createApi,
    deleteApi,
    getListApi,
  };
}
