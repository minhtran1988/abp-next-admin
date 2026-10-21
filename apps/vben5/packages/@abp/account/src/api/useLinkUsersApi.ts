import type { ListResultDto } from '@abp/core';

import type { LinkUserDto, UnLinkUserInput } from '../types/link-user';

import { useRequest } from '@abp/request';

export function useLinkUsersApi() {
  const { cancel, request } = useRequest();

  /**
   * Query the list of associated users
   * @returns { Promise<ListResultDto<LinkUserDto>> } associated user list
   */
  function getListApi(): Promise<ListResultDto<LinkUserDto>> {
    return request<ListResultDto<LinkUserDto>>('api/account/link-users', {
      method: 'GET',
    });
  }
  /**
   * Generate associated user Token
   */
  function generateLinkTokenApi(): Promise<string> {
    return request(`api/account/link-users/generate-link-token`, {
      method: 'POST',
    });
  }
  /**
   * Generate associated user login token
   */
  function generateLinkLoginTokenApi(): Promise<string> {
    return request(`api/account/link-users/generate-link-login-token`, {
      method: 'POST',
    });
  }

  /**
   * Cancel user association
   * @param input parameter
   */
  function unLinkApi(input: UnLinkUserInput): Promise<void> {
    return request(`api/account/link-users/unlink`, {
      method: 'POST',
      data: input,
    });
  }

  return {
    cancel,
    getListApi,
    generateLinkTokenApi,
    generateLinkLoginTokenApi,
    unLinkApi,
  };
}
