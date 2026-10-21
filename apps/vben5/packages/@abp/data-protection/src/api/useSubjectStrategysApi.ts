import type {
  SubjectStrategyDto,
  SubjectStrategyGetInput,
  SubjectStrategySetInput,
} from '../types';

import { useRequest } from '@abp/request';

export function useSubjectStrategysApi() {
  const { cancel, request } = useRequest();

  /**
   * Obtain subject data permission policy
   * @param input parameter
   * @returns data permission policy configuration
   */
  function getApi(
    input: SubjectStrategyGetInput,
  ): Promise<null | SubjectStrategyDto> {
    return request<null | SubjectStrategyDto>(
      '/api/data-protection-management/subject-strategys',
      {
        method: 'GET',
        params: input,
      },
    );
  }

  /**
   * Set subject data permission policy
   * @param input parameter
   * @returns data permission policy configuration
   */
  function setApi(input: SubjectStrategySetInput): Promise<SubjectStrategyDto> {
    return request<SubjectStrategyDto>(
      '/api/data-protection-management/subject-strategys',
      {
        data: input,
        method: 'PUT',
      },
    );
  }

  return {
    cancel,
    getApi,
    setApi,
  };
}
