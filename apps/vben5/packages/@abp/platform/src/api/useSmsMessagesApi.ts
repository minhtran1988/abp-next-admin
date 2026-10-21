import type { PagedResultDto } from '@abp/core';

import type { SmsMessageDto, SmsMessageGetListInput } from '../types';

import { useRequest } from '@abp/request';

export function useSmsMessagesApi() {
  const { cancel, request } = useRequest();
  /**
   * Get the paging list of SMS messages
   * @param {EmailMessageGetListInput} input parameter
   * @returns {Promise<PagedResultDto<EmailMessageDto>>} SMS message list
   */
  function getPagedListApi(
    input?: SmsMessageGetListInput,
  ): Promise<PagedResultDto<SmsMessageDto>> {
    return request<PagedResultDto<SmsMessageDto>>(
      '/api/platform/messages/sms',
      {
        method: 'GET',
        params: input,
      },
    );
  }
  /**
   * Get SMS messages
   * @param id Id
   * @returns {SmsMessageDto} SMS message
   */
  function getApi(id: string): Promise<SmsMessageDto> {
    return request<SmsMessageDto>(`/api/platform/messages/sms/${id}`, {
      method: 'GET',
    });
  }
  /**
   * Delete SMS messages
   * @param id Id
   * @returns {void}
   */
  function deleteApi(id: string): Promise<void> {
    return request(`/api/platform/messages/sms/${id}`, {
      method: 'DELETE',
    });
  }
  /**
   * Send SMS messages
   * @param id Id
   * @returns {void}
   */
  function sendApi(id: string): Promise<void> {
    return request(`/api/platform/messages/sms/${id}/send`, {
      method: 'POST',
    });
  }

  return {
    cancel,
    deleteApi,
    getApi,
    getPagedListApi,
    sendApi,
  };
}
