import { preferences } from '@vben/preferences';
import {
  authenticateResponseInterceptor,
  errorMessageResponseInterceptor,
} from '@vben/request';
import { useAccessStore, useTimezoneStore } from '@vben/stores';

import { useOAuthError } from '@abp/account';
import { useAbpStore } from '@abp/core';
import { requestClient, useWrapperResult } from '@abp/request';
import { message } from 'ant-design-vue';

import { useAuthStore } from '#/store';

export function initRequestClient() {
  /**
   * Re-authentication logic
   */
  async function doReAuthenticate() {
    console.warn('Access token or refresh token is invalid or expired. ');
    const accessStore = useAccessStore();
    const authStore = useAuthStore();
    accessStore.setAccessToken(null);
    if (
      preferences.app.loginExpiredMode === 'modal' &&
      accessStore.isAccessChecked
    ) {
      accessStore.setLoginExpired(true);
    } else {
      await authStore.logout();
    }
  }

  /**
   * Refresh token logic
   */
  async function doRefreshToken() {
    const authStore = useAuthStore();
    try {
      const token = await authStore.refreshSession();
      return token ?? '';
    } catch {
      console.warn('The refresh token has expired or is unavailable.');
    }
    return '';
  }

  function formatToken(token: null | string) {
    return token ? `Bearer ${token}` : null;
  }
  // Request header processing
  requestClient.addRequestInterceptor({
    fulfilled: async (config) => {
      const abpStore = useAbpStore();
      const accessStore = useAccessStore();
      const timezoneStore = useTimezoneStore();

      if (accessStore.accessToken) {
        config.headers.Authorization = `${accessStore.accessToken}`;
      }
      config.headers['Accept-Language'] = preferences.app.locale;
      config.headers['X-Request-From'] = 'vben';
      if (abpStore.tenantId) {
        // see: https://github.com/abpframework/abp/blob/dev/framework/src/Volo.Abp.MultiTenancy.Abstractions/Volo/Abp/MultiTenancy/TenantResolverConsts.cs
        config.headers.__tenant = abpStore.tenantId;
      }
      if (abpStore.xsrfToken) {
        config.headers.RequestVerificationToken = abpStore.xsrfToken;
      }
      if (timezoneStore.timezone) {
        // see: https://github.com/abpframework/abp/blob/dev/framework/src/Volo.Abp.Timing/Volo/Abp/Timing/TimeZoneConsts.cs
        config.headers.__timezone = timezoneStore.timezone;
      }
      return config;
    },
  });

  // response data deconstruction
  requestClient.addResponseInterceptor<any>({
    fulfilled: (response) => {
      const { data, status } = response;
      const { hasWrapResult, getData } = useWrapperResult(response);

      if (hasWrapResult()) {
        return getData();
      }

      if (status >= 200 && status < 400) {
        return data;
      }

      throw Object.assign({}, response, { response });
    },
  });

  // Token expiration processing
  requestClient.addResponseInterceptor(
    authenticateResponseInterceptor({
      client: requestClient,
      doReAuthenticate,
      doRefreshToken,
      enableRefreshToken: preferences.app.enableRefreshToken,
      formatToken,
    }),
  );

  // General error handling, if you do not enter the above error handling logic, you will enter here
  requestClient.addResponseInterceptor(
    errorMessageResponseInterceptor((msg: string, error) => {
      // This can be customized according to the business. You can get the information in the error for customized processing and make different prompts according to different codes instead of directly using message.error to prompt msg.
      // The error field returned by the current mock interface is error or message
      const responseData = error?.response?.data ?? {};
      if (responseData?.error_description) {
        const { formatError } = useOAuthError();
        message.error(formatError(responseData) || msg);
        return;
      }
      const errorMessage = responseData?.error ?? responseData?.message ?? '';
      // If there is no error message, a prompt will be given based on the status code.
      message.error(errorMessage || msg);
    }),
  );
}
