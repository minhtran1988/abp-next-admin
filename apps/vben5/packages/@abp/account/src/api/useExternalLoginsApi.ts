import type {
  ExternalLoginResultDto,
  RemoveExternalLoginInput,
  WorkWeixinLoginBindInput,
} from '../types/external-logins';

import { useRequest } from '@abp/request';

export function useExternalLoginsApi() {
  const { cancel, request } = useRequest();

  /**
   * Bind to corporate WeChat
   * @param input binding parameters
   * @returns { Promise<void> }
   */
  async function bindWorkWeixinApi(
    input: WorkWeixinLoginBindInput,
  ): Promise<void> {
    return await request(`/api/account/oauth/work-weixin/bind`, {
      method: 'POST',
      data: input,
    });
  }

  /**
   * Get the list of external login providers
   * @returns List of external login providers
   */
  async function getExternalLoginsApi(): Promise<ExternalLoginResultDto> {
    return await request<ExternalLoginResultDto>(
      `/api/account/external-logins`,
      {
        method: 'GET',
      },
    );
  }

  /**
   * Remove external login provider
   * @returns { Promise<void> }
   */
  async function removeExternalLoginApi(
    input: RemoveExternalLoginInput,
  ): Promise<void> {
    return await request(`/api/account/external-logins/remove`, {
      method: 'DELETE',
      params: input,
    });
  }

  return {
    cancel,
    bindWorkWeixinApi,
    getExternalLoginsApi,
    removeExternalLoginApi,
  };
}
