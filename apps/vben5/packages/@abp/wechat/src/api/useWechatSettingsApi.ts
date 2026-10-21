import type { ListResultDto } from '@abp/core';
import type { SettingGroup } from '@abp/settings';

import { useRequest } from '@abp/request';

export function useWechatSettingsApi() {
  const { cancel, request } = useRequest();

  /**
   * Get global settings
   * @returns Set the data transfer object list
   */
  function getGlobalSettingsApi(): Promise<ListResultDto<SettingGroup>> {
    return request<ListResultDto<SettingGroup>>(
      `/api/wechat/setting-management/by-global`,
      {
        method: 'GET',
      },
    );
  }

  /**
   * Get tenant settings
   * @returns Set the data transfer object list
   */
  function getTenantSettingsApi(): Promise<ListResultDto<SettingGroup>> {
    return request<ListResultDto<SettingGroup>>(
      `/api/wechat/setting-management/by-current-tenant`,
      {
        method: 'GET',
      },
    );
  }

  return {
    cancel,
    getGlobalSettingsApi,
    getTenantSettingsApi,
  };
}
