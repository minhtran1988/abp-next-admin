import type {
  FeatureProvider,
  GetFeatureListResultDto,
  UpdateFeaturesDto,
} from '../types/features';

import { useRequest } from '@abp/request';

export function useFeaturesApi() {
  const { cancel, request } = useRequest();

  /**
   * Delete function
   * @param {FeatureProvider} provider parameter
   * @returns {Promise<void>}
   */
  function deleteApi(provider: FeatureProvider): Promise<void> {
    return request(`/api/feature-management/features`, {
      method: 'DELETE',
      params: provider,
    });
  }

  /**
   * Query function
   * @param {FeatureProvider} provider parameter
   * @returns {Promise<GetFeatureListResultDto>} Function entity data transfer object
   */
  function getApi(provider: FeatureProvider): Promise<GetFeatureListResultDto> {
    return request<GetFeatureListResultDto>(
      `/api/feature-management/features`,
      {
        method: 'GET',
        params: provider,
      },
    );
  }

  /**
   *Update function
   * @param {FeatureProvider} provider
   * @param {UpdateFeaturesDto} input parameter
   * @returns {Promise<void>}
   */
  function updateApi(
    provider: FeatureProvider,
    input: UpdateFeaturesDto,
  ): Promise<void> {
    return request(`/api/feature-management/features`, {
      data: input,
      method: 'PUT',
      params: provider,
    });
  }

  return {
    cancel,
    deleteApi,
    getApi,
    updateApi,
  };
}
