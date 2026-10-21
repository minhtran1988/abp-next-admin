import type { NameValue } from '@abp/core';

import { useRequest } from '@abp/request';

export function useTimeZoneSettingsApi() {
  const { cancel, request } = useRequest();

  /**
   * Query system time zone settings
   * @returns system time zone
   */
  function getApi(): Promise<string> {
    return request<string>(`/api/setting-management/timezone`, {
      method: 'GET',
    });
  }

  /**
   * Query the current user time zone settings
   * @returns user time zone
   */
  function getMyTimezoneApi(): Promise<string> {
    return request<string>(`/api/setting-management/timezone/my-timezone`, {
      method: 'GET',
    });
  }

  /**
   * Query time zone list
   * @returns time zone list
   */
  function getTimezonesApi(): Promise<NameValue<string>[]> {
    return request<NameValue<string>[]>(
      `/api/setting-management/timezone/timezones`,
      {
        method: 'GET',
      },
    );
  }

  /**
   * Update time zone
   * @param timezone time zone name
   */
  function updateApi(timezone: string): Promise<void> {
    return request(`/api/setting-management/timezone?timezone=${timezone}`, {
      method: 'POST',
    });
  }

  /**
   * Update current user time zone
   * @param timezone time zone name
   */
  function updateMyTimezoneApi(timezone: string): Promise<void> {
    return request(
      `/api/setting-management/timezone/my-timezone?timezone=${timezone}`,
      {
        method: 'POST',
      },
    );
  }

  return {
    cancel,
    getApi,
    getMyTimezoneApi,
    getTimezonesApi,
    updateMyTimezoneApi,
    updateApi,
  };
}
