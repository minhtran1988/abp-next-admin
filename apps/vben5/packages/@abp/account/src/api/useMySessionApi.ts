import type { PagedResultDto } from '@abp/core';
import type { GetMySessionsInput, IdentitySessionDto } from '@abp/identity';

import { useRequest } from '@abp/request';

export function useMySessionApi() {
  const { cancel, request } = useRequest();

  /**
   * Query session list
   * @param { GetMySessionsInput } input query parameter
   * @returns { Promise<PagedResultDto<IdentitySessionDto>> } User session list
   */
  function getSessionsApi(
    input?: GetMySessionsInput,
  ): Promise<PagedResultDto<IdentitySessionDto>> {
    return request<PagedResultDto<IdentitySessionDto>>(
      '/api/account/my-profile/sessions',
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
    return request(`/api/account/my-profile/sessions/${sessionId}/revoke`, {
      method: 'DELETE',
    });
  }

  return {
    cancel,
    getSessionsApi,
    revokeSessionApi,
  };
}
