import type { PagedResultDto } from '@abp/core';

import type {
  LanguageCreateDto,
  LanguageDto,
  LanguageGetPagedListInput,
  LanguageUpdateDto,
} from '../types/languages';

import { useRequest } from '@abp/request';

export function useLanguagesApi() {
  const { cancel, request } = useRequest();

  /**
   * Query language paging list
   * @param input parameter
   * @returns language list
   */
  function getPagedListApi(
    input?: LanguageGetPagedListInput,
  ): Promise<PagedResultDto<LanguageDto>> {
    return request<PagedResultDto<LanguageDto>>('/api/localization/languages', {
      method: 'GET',
      params: input,
    });
  }

  /**
   * Query language
   * @param name language name
   * @returns query language
   */
  function getApi(name: string): Promise<LanguageDto> {
    return request<LanguageDto>(`/api/localization/languages/${name}`, {
      method: 'GET',
    });
  }

  /**
   * Delete language
   * @param name language name
   */
  function deleteApi(name: string): Promise<void> {
    return request(`/api/localization/languages/${name}`, {
      method: 'DELETE',
    });
  }

  /**
   *Create language
   * @param input parameter
   * @returns the language created
   */
  function createApi(input: LanguageCreateDto): Promise<LanguageDto> {
    return request<LanguageDto>(`/api/localization/languages`, {
      data: input,
      method: 'POST',
    });
  }

  /**
   * Editing language
   * @param name language name
   * @param input parameter
   * @returns editing language
   */
  function updateApi(
    name: string,
    input: LanguageUpdateDto,
  ): Promise<LanguageDto> {
    return request<LanguageDto>(`/api/localization/languages/${name}`, {
      data: input,
      method: 'PUT',
    });
  }

  return {
    cancel,
    createApi,
    deleteApi,
    getApi,
    getPagedListApi,
    updateApi,
  };
}
