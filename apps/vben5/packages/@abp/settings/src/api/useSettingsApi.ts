import type { ListResultDto } from '@abp/core';

import type { SettingGroup, SettingsUpdateInput } from '../types';

import { useRequest } from '@abp/request';

export function useSettingsApi() {
  const { cancel, request } = useRequest();
  /**
   * Get settings
   * @returns Set the data transfer object list
   */
  function getSettingsApi(): Promise<ListResultDto<SettingGroup>> {
    return request<ListResultDto<SettingGroup>>(
      `/api/v2/setting-management/settings`,
      {
        method: 'GET',
      },
    );
  }

  /**
   * Change settings
   * @returns Set the data transfer object list
   */
  function setSettingsApi(input: SettingsUpdateInput): Promise<void> {
    return request(`/api/v2/setting-management/settings`, {
      data: input,
      method: 'PUT',
    });
  }

  /**
   * Get global settings
   * @returns Set the data transfer object list
   */
  function getGlobalSettingsApi(): Promise<ListResultDto<SettingGroup>> {
    return request<ListResultDto<SettingGroup>>(
      `/api/setting-management/settings/by-global`,
      {
        method: 'GET',
      },
    );
  }

  /**
   * Set global settings
   * @returns Set the data transfer object list
   */
  function setGlobalSettingsApi(input: SettingsUpdateInput): Promise<void> {
    return request(`/api/setting-management/settings/change-global`, {
      data: input,
      method: 'PUT',
    });
  }

  /**
   * Get tenant settings
   * @returns Set the data transfer object list
   */
  function getTenantSettingsApi(): Promise<ListResultDto<SettingGroup>> {
    return request<ListResultDto<SettingGroup>>(
      `/api/setting-management/settings/by-current-tenant`,
      {
        method: 'GET',
      },
    );
  }

  /**
   * Set tenant settings
   * @returns Set the data transfer object list
   */
  function setTenantSettingsApi(input: SettingsUpdateInput): Promise<void> {
    return request(`/api/setting-management/settings/change-current-tenant`, {
      data: input,
      method: 'PUT',
    });
  }
  /**
   * Get user settings
   * @returns Set the data transfer object list
   */
  function getUserSettingsApi(): Promise<ListResultDto<SettingGroup>> {
    return request<ListResultDto<SettingGroup>>(
      `/api/v2/setting-management/my-settings`,
      {
        method: 'GET',
      },
    );
  }

  /**
   * Set user settings
   * @returns Set the data transfer object list
   */
  function setUserSettingsApi(input: SettingsUpdateInput): Promise<void> {
    return request(`/api/v2/setting-management/my-settings`, {
      data: input,
      method: 'PUT',
    });
  }

  /**
   * Send test email
   * @param emailAddress Email recipient address
   */
  const sendTestEmailApi = (emailAddress: string) => {
    return request(`/api/setting-management/settings/send-test-email`, {
      data: {
        emailAddress,
      },
      method: 'POST',
    });
  };

  return {
    cancel,
    getSettingsApi,
    setSettingsApi,
    getGlobalSettingsApi,
    getTenantSettingsApi,
    getUserSettingsApi,
    sendTestEmailApi,
    setGlobalSettingsApi,
    setTenantSettingsApi,
    setUserSettingsApi,
  };
}
