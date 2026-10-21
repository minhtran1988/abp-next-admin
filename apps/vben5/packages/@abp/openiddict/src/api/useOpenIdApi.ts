import type { OpenIdConfiguration } from '@abp/core';

import { useRequest } from '@abp/request';

export function useOpenIdApi() {
  const { cancel, request } = useRequest();

  /**
   * openid discovery endpoint
   * @returns OpenId configuration data
   */
  function discoveryApi(): Promise<OpenIdConfiguration> {
    return request<OpenIdConfiguration>('/.well-known/openid-configuration', {
      method: 'GET',
    });
  }

  return {
    cancel,
    discoveryApi,
  };
}
