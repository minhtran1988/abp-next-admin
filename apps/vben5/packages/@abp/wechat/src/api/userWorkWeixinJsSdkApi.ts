import type { AgentConfigDto } from '../types/js-sdk';

import { useRequest } from '@abp/request';

export function userWorkWeixinJsSdkApi() {
  const { cancel, request } = useRequest();

  /**
   * Obtain enterprise WeChat application configuration
   * @returns Enterprise WeChat application configuration Dto
   */
  function getAgentConfigApi(): Promise<AgentConfigDto> {
    return request<AgentConfigDto>(`/api/wechat/work/jssdk/agent-config`, {
      method: 'GET',
    });
  }

  return {
    cancel,
    getAgentConfigApi,
  };
}
