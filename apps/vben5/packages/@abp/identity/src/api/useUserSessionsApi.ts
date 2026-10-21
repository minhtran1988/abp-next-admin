import type { PagedResultDto } from '@abp/core';

import type { GetUserSessionsInput, IdentitySessionDto } from '../types';

import { useRequest } from '@abp/request';

export function useUserSessionsApi() {
  const { cancel, request } = useRequest();

  /**
   * Query session list
   * @param { GetUserSessionsInput } input query parameter
   * @returns { Promise<PagedResultDto<IdentitySessionDto>> } User session list
   */
  function getSessionsApi(
    input?: GetUserSessionsInput,
  ): Promise<PagedResultDto<IdentitySessionDto>> {
    return request<PagedResultDto<IdentitySessionDto>>(
      '/api/identity/sessions',
      {
        method: 'GET',
        params: input,
      },
    );
  }
  /**
   * Cancel session
   * @param { string } sessionId session id
   * @returns { Promise<void> }
   */
  function revokeSessionApi(sessionId: string): Promise<void> {
    return request(`/api/identity/sessions/${sessionId}/revoke`, {
      method: 'DELETE',
    });
  }

  return {
    cancel,
    getSessionsApi,
    revokeSessionApi,
  };
}
