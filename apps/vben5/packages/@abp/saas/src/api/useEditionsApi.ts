import type { PagedResultDto } from '@abp/core';

import type {
  EditionCreateDto,
  EditionDto,
  EditionUpdateDto,
  GetEditionPagedListInput,
} from '../types';

import { useRequest } from '@abp/request';

export function useEditionsApi() {
  const { cancel, request } = useRequest();

  /**
   * Create version
   * @param {EditionCreateDto} input parameter
   * @returns created version
   */
  function createApi(input: EditionCreateDto): Promise<EditionDto> {
    return request<EditionDto>('/api/saas/editions', {
      data: input,
      method: 'POST',
    });
  }

  /**
   *Edited version
   * @param {string} id parameter
   * @param {EditionUpdateDto} input parameter
   * @returns edited version
   */
  function updateApi(id: string, input: EditionUpdateDto): Promise<EditionDto> {
    return request<EditionDto>(`/api/saas/editions/${id}`, {
      data: input,
      method: 'PUT',
    });
  }

  /**
   * Query version
   * @param {string} id Id
   * @returns Query version
   */
  function getApi(id: string): Promise<EditionDto> {
    return request<EditionDto>(`/api/saas/editions/${id}`, {
      method: 'GET',
    });
  }

  /**
   * delete version
   * @param {string} id Id
   * @returns {void}
   */
  function deleteApi(id: string): Promise<void> {
    return request(`/api/saas/editions/${id}`, {
      method: 'DELETE',
    });
  }

  /**
   * Query version paging list
   * @param {GetEditionPagedListInput} input parameter
   * @returns {void}
   */
  function getPagedListApi(
    input?: GetEditionPagedListInput,
  ): Promise<PagedResultDto<EditionDto>> {
    return request<PagedResultDto<EditionDto>>(`/api/saas/editions`, {
      method: 'GET',
      params: input,
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
