import type { ListResultDto, NameValue } from '@abp/core';

import type {
  FeatureDefinitionCreateDto,
  FeatureDefinitionDto,
  FeatureDefinitionGetListInput,
  FeatureDefinitionUpdateDto,
} from '../types/definitions';

import { useRequest } from '@abp/request';

export function useFeatureDefinitionsApi() {
  const { cancel, request } = useRequest();

  /**
   * Delete function definition
   * @param name function name
   */
  function deleteApi(name: string): Promise<void> {
    return request(`/api/feature-management/definitions/${name}`, {
      method: 'DELETE',
    });
  }

  /**
   * Query function definition
   * @param name function name
   * @returns function definition data transfer object
   */
  function getApi(name: string): Promise<FeatureDefinitionDto> {
    return request<FeatureDefinitionDto>(
      `/api/feature-management/definitions/${name}`,
      {
        method: 'GET',
      },
    );
  }

  /**
   * Query function definition list
   * @param input function filter conditions
   * @returns function definition data transfer object list
   */
  function getListApi(
    input?: FeatureDefinitionGetListInput,
  ): Promise<ListResultDto<FeatureDefinitionDto>> {
    return request<ListResultDto<FeatureDefinitionDto>>(
      `/api/feature-management/definitions`,
      {
        method: 'GET',
        params: input,
      },
    );
  }

  /**
   * Create function definition
   * @param input function definition parameters
   * @returns function definition data transfer object
   */
  function createApi(
    input: FeatureDefinitionCreateDto,
  ): Promise<FeatureDefinitionDto> {
    return request<FeatureDefinitionDto>(
      '/api/feature-management/definitions',
      {
        data: input,
        method: 'POST',
      },
    );
  }

  /**
   * Update function definition
   * @param name function name
   * @param input function definition parameters
   * @returns function definition data transfer object
   */
  function updateApi(
    name: string,
    input: FeatureDefinitionUpdateDto,
  ): Promise<FeatureDefinitionDto> {
    return request<FeatureDefinitionDto>(
      `/api/feature-management/definitions/${name}`,
      {
        data: input,
        method: 'PUT',
      },
    );
  }

  /**
   * Get the list of available function providers
   * @returns {Promise<ListResultDto<NameValue<string>>>} function provider list
   */
  function getAssignableProvidersApi(): Promise<
    ListResultDto<NameValue<string>>
  > {
    return request<ListResultDto<NameValue<string>>>(
      '/api/feature-management/definitions/assignable-providers',
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
