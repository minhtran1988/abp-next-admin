import type { ListResultDto } from '@abp/core';

import type {
  PermissionGroupDefinitionCreateDto,
  PermissionGroupDefinitionDto,
  PermissionGroupDefinitionGetListInput,
  PermissionGroupDefinitionUpdateDto,
} from '../types/groups';

import { useRequest } from '@abp/request';

export function usePermissionGroupDefinitionsApi() {
  const { cancel, request } = useRequest();

  /**
   * Delete permission definition
   * @param name permission name
   */
  function deleteApi(name: string): Promise<void> {
    return request(`/api/permission-management/definitions/groups/${name}`, {
      method: 'DELETE',
    });
  }

  /**
   * Query permission definition
   * @param name permission name
   * @returns permission definition data transfer object
   */
  function getApi(name: string): Promise<PermissionGroupDefinitionDto> {
    return request<PermissionGroupDefinitionDto>(
      `/api/permission-management/definitions/groups/${name}`,
      {
        method: 'GET',
      },
    );
  }

  /**
   * Query permission definition list
   * @param input permission filter conditions
   * @returns Permission definition data transfer object list
   */
  function getListApi(
    input?: PermissionGroupDefinitionGetListInput,
  ): Promise<ListResultDto<PermissionGroupDefinitionDto>> {
    return request<ListResultDto<PermissionGroupDefinitionDto>>(
      `/api/permission-management/definitions/groups`,
      {
        method: 'GET',
        params: input,
      },
    );
  }

  /**
   * Create permission definition
   * @param input permission definition parameters
   * @returns permission definition data transfer object
   */
  function createApi(
    input: PermissionGroupDefinitionCreateDto,
  ): Promise<PermissionGroupDefinitionDto> {
    return request<PermissionGroupDefinitionDto>(
      '/api/permission-management/definitions/groups',
      {
        data: input,
        method: 'POST',
      },
    );
  }

  /**
   * Update permission definitions
   * @param name permission name
   * @param input permission definition parameters
   * @returns permission definition data transfer object
   */
  function updateApi(
    name: string,
    input: PermissionGroupDefinitionUpdateDto,
  ): Promise<PermissionGroupDefinitionDto> {
    return request<PermissionGroupDefinitionDto>(
      `/api/permission-management/definitions/groups/${name}`,
      {
        data: input,
        method: 'PUT',
      },
    );
  }

  return {
    cancel,
    createApi,
    deleteApi,
    getApi,
    getListApi,
    updateApi,
  };
}
