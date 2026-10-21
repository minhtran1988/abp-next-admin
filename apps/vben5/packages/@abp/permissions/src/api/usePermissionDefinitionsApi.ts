import type { ListResultDto, NameValue } from '@abp/core';

import type {
  PermissionDefinitionCreateDto,
  PermissionDefinitionDto,
  PermissionDefinitionGetListInput,
  PermissionDefinitionUpdateDto,
} from '../types/definitions';

import { useRequest } from '@abp/request';

export function usePermissionDefinitionsApi() {
  const { cancel, request } = useRequest();

  /**
   * Delete permission definition
   * @param name permission name
   */
  function deleteApi(name: string): Promise<void> {
    return request(`/api/permission-management/definitions/${name}`, {
      method: 'DELETE',
    });
  }

  /**
   * Query permission definition
   * @param name permission name
   * @returns permission definition data transfer object
   */
  function getApi(name: string): Promise<PermissionDefinitionDto> {
    return request<PermissionDefinitionDto>(
      `/api/permission-management/definitions/${name}`,
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
    input?: PermissionDefinitionGetListInput,
  ): Promise<ListResultDto<PermissionDefinitionDto>> {
    return request<ListResultDto<PermissionDefinitionDto>>(
      `/api/permission-management/definitions`,
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
    input: PermissionDefinitionCreateDto,
  ): Promise<PermissionDefinitionDto> {
    return request<PermissionDefinitionDto>(
      '/api/permission-management/definitions',
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
    input: PermissionDefinitionUpdateDto,
  ): Promise<PermissionDefinitionDto> {
    return request<PermissionDefinitionDto>(
      `/api/permission-management/definitions/${name}`,
      {
        data: input,
        method: 'PUT',
      },
    );
  }

  /**
   * Get the list of available permission providers
   * @returns {Promise<ListResultDto<NameValue<string>>>} Permission provider list
   */
  function getAssignableProvidersApi(): Promise<
    ListResultDto<NameValue<string>>
  > {
    return request<ListResultDto<NameValue<string>>>(
      '/api/permission-management/definitions/assignable-providers',
      {
        method: 'GET',
      },
    );
  }

  return {
    cancel,
    createApi,
    deleteApi,
    getApi,
    getAssignableProvidersApi,
    getListApi,
    updateApi,
  };
}
