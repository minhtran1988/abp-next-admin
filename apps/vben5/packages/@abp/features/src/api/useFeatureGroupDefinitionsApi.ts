import type { ListResultDto } from '@abp/core';

import type {
  FeatureGroupDefinitionCreateDto,
  FeatureGroupDefinitionDto,
  FeatureGroupDefinitionGetListInput,
  FeatureGroupDefinitionUpdateDto,
} from '../types/groups';

import { useRequest } from '@abp/request';

export function useFeatureGroupDefinitionsApi() {
  const { cancel, request } = useRequest();

  /**
   * Delete function definition
   * @param name function name
   */
  function deleteApi(name: string): Promise<void> {
    return request(`/api/feature-management/definitions/groups/${name}`, {
      method: 'DELETE',
    });
  }

  /**
   * Query function definition
   * @param name function name
   * @returns function definition data transfer object
   */
  function getApi(name: string): Promise<FeatureGroupDefinitionDto> {
    return request<FeatureGroupDefinitionDto>(
      `/api/feature-management/definitions/groups/${name}`,
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
    input?: FeatureGroupDefinitionGetListInput,
  ): Promise<ListResultDto<FeatureGroupDefinitionDto>> {
    return request<ListResultDto<FeatureGroupDefinitionDto>>(
      `/api/feature-management/definitions/groups`,
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
    input: FeatureGroupDefinitionCreateDto,
  ): Promise<FeatureGroupDefinitionDto> {
    return request<FeatureGroupDefinitionDto>(
      '/api/feature-management/definitions/groups',
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
    input: FeatureGroupDefinitionUpdateDto,
  ): Promise<FeatureGroupDefinitionDto> {
    return request<FeatureGroupDefinitionDto>(
      `/api/feature-management/definitions/groups/${name}`,
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
