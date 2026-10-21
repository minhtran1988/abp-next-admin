import type { ListResultDto, NameValue } from '@abp/core';

import type {
  SettingDefinitionCreateDto,
  SettingDefinitionDto,
  SettingDefinitionGetListInput,
  SettingDefinitionUpdateDto,
} from '../types';

import { useRequest } from '@abp/request';

export function useDefinitionsApi() {
  const { cancel, request } = useRequest();
  /**
   * Delete setting definition
   * @param name set name
   */
  function deleteApi(name: string): Promise<void> {
    return request(`/api/setting-management/settings/definitions/${name}`, {
      method: 'DELETE',
    });
  }

  /**
   * Query setting definition
   * @param name set name
   * @returns Set the definition data transfer object
   */
  function getApi(name: string): Promise<SettingDefinitionDto> {
    return request<SettingDefinitionDto>(
      `/api/setting-management/settings/definitions/${name}`,
      {
        method: 'GET',
      },
    );
  }

  /**
   * Query setting definition list
   * @param input Set filter conditions
   * @returns Set the definition data transfer object list
   */
  function getListApi(
    input?: SettingDefinitionGetListInput,
  ): Promise<ListResultDto<SettingDefinitionDto>> {
    return request<ListResultDto<SettingDefinitionDto>>(
      `/api/setting-management/settings/definitions`,
      {
        method: 'GET',
        params: input,
      },
    );
  }

  /**
   * Create settings definition
   * @param input Set definition parameters
   * @returns Set the definition data transfer object
   */
  function createApi(
    input: SettingDefinitionCreateDto,
  ): Promise<SettingDefinitionDto> {
    return request<SettingDefinitionDto>(
      '/api/setting-management/settings/definitions',
      {
        data: input,
        method: 'POST',
      },
    );
  }

  /**
   * Update settings definition
   * @param name set name
   * @param input Set definition parameters
   * @returns Set the definition data transfer object
   */
  function updateApi(
    name: string,
    input: SettingDefinitionUpdateDto,
  ): Promise<SettingDefinitionDto> {
    return request<SettingDefinitionDto>(
      `/api/setting-management/settings/definitions/${name}`,
      {
        data: input,
        method: 'PUT',
      },
    );
  }

  /**
   * Get the list of available settings providers
   * @returns {Promise<ListResultDto<NameValue<string>>>} Set the provider list
   */
  function getAssignableProvidersApi(): Promise<
    ListResultDto<NameValue<string>>
  > {
    return request<ListResultDto<NameValue<string>>>(
      '/api/setting-management/settings/definitions/assignable-providers',
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
