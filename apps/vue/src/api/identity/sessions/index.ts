import { defHttp } from '/@/utils/http/axios';
import {
  IdentitySessionDto,
  GetUserSessionsInput
} from './model';

/**
 * Query session list
 * @param { GetUserSessionsInput } input query parameter
 * @returns { Promise<PagedResultDto<IdentitySessionDto>> }
 */
export const getSessions = (input?: GetUserSessionsInput): Promise<PagedResultDto<IdentitySessionDto>> => {
  return defHttp.get<PagedResultDto<IdentitySessionDto>>({
    url: '/api/identity/sessions',
    params: input,
  });
};
/**
 * Cancel session
 * @param { string } sessionId session id
 * @returns { Promise<void> }
 */
export const revokeSession = (sessionId: string): Promise<void> => {
  return defHttp.delete<void>({
    url: `/api/identity/sessions/${sessionId}/revoke`,
  });
}
