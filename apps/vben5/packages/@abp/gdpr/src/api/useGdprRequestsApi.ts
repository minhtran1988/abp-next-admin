import type { PagedResultDto } from '@abp/core';

import type { GdprRequestDto, GdprRequestGetListInput } from '../types';

import { useRequest } from '@abp/request';

export function useGdprRequestsApi() {
  const { cancel, request } = useRequest();

  /**
   * Request to delete personal data
   * @param {string} id request ID
   * @returns {void}
   */
  function deleteApi(id: string): Promise<void> {
    return request(`/api/gdpr/requests/${id}`, {
      method: 'DELETE',
    });
  }

  /**
   * Delete user account
   * @returns {Promise<void>}
   */
  function deletePersonalAccountApi(): Promise<void> {
    return request(`/api/gdpr/requests/personal-account`, {
      method: 'DELETE',
    });
  }

  /**
   * Delete user data
   * @returns {Promise<void>}
   */
  function deletePersonalDataApi(): Promise<void> {
    return request(`/api/gdpr/requests/personal-data`, {
      method: 'DELETE',
    });
  }

  /**
   * Prepare user data
   * @returns {Promise<void>}
   */
  function preparePersonalDataApi(): Promise<void> {
    return request(`/api/gdpr/requests/personal-data/prepare`, {
      method: 'POST',
    });
  }

  /**
   * Download user data
   * @returns {Promise<Blob>} user data file
   */
  function downloadPersonalDataApi(requestId: string): Promise<Blob> {
    return request<Blob>(
      `/api/gdpr/requests/personal-data/download/${requestId}`,
      {
        method: 'GET',
        responseType: 'blob',
      },
    );
  }

  /**
   * Personal data query request
   * @param {string} id request ID
   * @returns {Promise<GdprRequestDto>} personal data request
   */
  function getApi(id: string): Promise<GdprRequestDto> {
    return request<GdprRequestDto>(`/api/gdpr/requests/${id}`, {
      method: 'GET',
    });
  }

  /**
   * Query personal data request paged list
   * @param {GdprRequestGetListInput} input parameter
   * @returns {Promise<PagedResultDto<GdprRequestDto>>} Personal data request list
   */
  function getPagedListApi(
    input?: GdprRequestGetListInput,
  ): Promise<PagedResultDto<GdprRequestDto>> {
    return request<PagedResultDto<GdprRequestDto>>(`/api/gdpr/requests`, {
      method: 'GET',
      params: input,
    });
  }

  return {
    cancel,
    deleteApi,
    deletePersonalAccountApi,
    deletePersonalDataApi,
    downloadPersonalDataApi,
    getApi,
    getPagedListApi,
    preparePersonalDataApi,
  };
}
