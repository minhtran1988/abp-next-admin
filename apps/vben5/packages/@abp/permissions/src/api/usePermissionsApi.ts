import type {
  GetPermissionGrantedWithProviderListResultDto,
  PermissionProvider,
  PermissionResultDto,
  PermissionsUpdateDto,
} from '../types/permissions';

import { useRequest } from '@abp/request';

export function usePermissionsApi() {
  const { cancel, request } = useRequest();

  /**
   * Query permissions
   * @param provider
   * @returns permission entity data transfer object
   */
  function getApi(provider: PermissionProvider): Promise<PermissionResultDto> {
    return request<PermissionResultDto>(
      `/api/permission-management/permissions`,
      {
        method: 'GET',
        params: provider,
      },
    );
  }

  /**
   * Update permissions
   * @param provider
   * @param input
   */
  function updateApi(
    provider: PermissionProvider,
    input: PermissionsUpdateDto,
  ): Promise<void> {
    return request(`/api/permission-management/permissions`, {
      data: input,
      method: 'PUT',
      params: provider,
    });
  }

  /**
   * Get authorized objects
   * @param permissionName permission name
   * @param providerName authorized provider
   * @returns
   */
  function getGrantedByProviderApi(
    permissionName: string,
    providerName: string,
  ): Promise<GetPermissionGrantedWithProviderListResultDto> {
    return request<GetPermissionGrantedWithProviderListResultDto>(
      `/api/permission-management/permissions/granted/by-provider`,
      {
        method: 'GET',
        params: {
          permissionName,
          providerName,
        },
      },
    );
  }

  return {
    cancel,
    getApi,
    updateApi,
    getGrantedByProviderApi,
  };
}
