import type { ListResultDto } from '@abp/core';

import type {
  DeleteTextInput,
  GetTextByKeyInput,
  GetTextsInput,
  SetTextInput,
  TextDifferenceDto,
  TextDto,
} from '../types/texts';

import { useRequest } from '@abp/request';

export function useTextsApi() {
  const { cancel, request } = useRequest();

  /**
   * Query text list
   * @param input parameter
   * @returns text list
   */
  function getListApi(
    input: GetTextsInput,
  ): Promise<ListResultDto<TextDifferenceDto>> {
    return request<ListResultDto<TextDifferenceDto>>(
      '/api/localization/texts',
      {
        method: 'GET',
        params: input,
      },
    );
  }

  /**
   * Query text
   * @param input parameter
   * @returns query text
   */
  function getApi(input: GetTextByKeyInput): Promise<TextDto> {
    return request<TextDto>('/api/localization/texts/by-key', {
      method: 'GET',
      params: input,
    });
  }

  /**
   * Set text
   * @param input parameter
   */
  function setApi(input: SetTextInput): Promise<void> {
    return request('/api/localization/texts', {
      data: input,
      method: 'PUT',
    });
  }

  /**
   * Delete text
   * @param input parameter
   */
  function deleteApi(input: DeleteTextInput): Promise<void> {
    return request('/api/localization/texts', {
      data: input,
      method: 'DELETE',
    });
  }

  return {
    cancel,
    deleteApi,
    getApi,
    getListApi,
    setApi,
  };
}
