import type { ListResultDto } from '@abp/core';

import type {
  EntityChangeGetWithUsernameInput,
  EntityChangeWithUsernameDto,
} from '../types/entity-changes';

import { useRequest } from '@abp/request';

export function useEntityChangesApi() {
  const { cancel, request } = useRequest();

  /**
   * Get the entity change list containing the user name
   * @param input parameter
   */
  function getListWithUsernameApi(
    input: EntityChangeGetWithUsernameInput,
  ): Promise<ListResultDto<EntityChangeWithUsernameDto>> {
    return request<ListResultDto<EntityChangeWithUsernameDto>>(
      '/api/auditing/entity-changes/with-username',
      {
        method: 'GET',
        params: input,
      },
    );
  }

  return {
    cancel,
    getListWithUsernameApi,
  };
}
