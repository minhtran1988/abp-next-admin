import type { ListResultDto } from '@abp/core';

import type {
  IdentityUserDto,
  UserLookupCountInput,
  UserLookupSearchInput,
} from '../types/users';

import { useRequest } from '@abp/request';

export function useUserLookupApi() {
  const { cancel, request } = useRequest();

  /**
   * Query user by id
   * @param id user id
   * @returns User entity data transfer object
   */
  function findByIdApi(id: string): Promise<IdentityUserDto> {
    return request<IdentityUserDto>(`/api/identity/users/lookup/${id}`, {
      method: 'GET',
    });
  }

  /**
   * Query users by username
   * @param userName username
   * @returns User entity data transfer object
   */
  function findByUserNameApi(userName: string): Promise<IdentityUserDto> {
    return request<IdentityUserDto>(
      `/api/identity/users/lookup/by-username/${userName}`,
      {
        method: 'GET',
      },
    );
  }

  /**
   * Search user list
   * @param input search filter conditions
   * @returns User entity data transfer object list
   */
  function searchApi(
    input?: UserLookupSearchInput,
  ): Promise<ListResultDto<IdentityUserDto>> {
    return request<ListResultDto<IdentityUserDto>>(
      `/api/identity/users/lookup/search`,
      {
        method: 'GET',
        params: input,
      },
    );
  }

  /**
   *Number of search users
   * @param input search filter conditions
   */
  function countApi(input?: UserLookupCountInput): Promise<number> {
    return request<number>(`/api/identity/users/lookup/count`, {
      method: 'GET',
      params: input,
    });
  }

  return {
    cancel,
    countApi,
    findByIdApi,
    findByUserNameApi,
    searchApi,
  };
}
