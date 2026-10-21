import type { PagedResultDto } from '@abp/core';

import type {
  EmailMessageDto,
  EmailMessageGetListInput,
} from '../types/messages';

import { useRequest } from '@abp/request';

export function useEmailMessagesApi() {
  const { cancel, request } = useRequest();
  /**
   * Get the paging list of email messages
   * @param {EmailMessageGetListInput} input parameter
   * @returns {Promise<PagedResultDto<EmailMessageDto>>} email message list
   */
  function getPagedListApi(
    input?: EmailMessageGetListInput,
  ): Promise<PagedResultDto<EmailMessageDto>> {
    return request<PagedResultDto<EmailMessageDto>>(
      '/api/platform/messages/email',
      {
        method: 'GET',
        params: input,
      },
    );
  }
  /**
   * Get email messages
   * @param id Id
   * @returns {EmailMessageDto} email message
   */
  function getApi(id: string): Promise<EmailMessageDto> {
    return request<EmailMessageDto>(`/api/platform/messages/email/${id}`, {
      method: 'GET',
    });
  }
  /**
   * Delete email message
   * @param id Id
   * @returns {void}
   */
  function deleteApi(id: string): Promise<void> {
    return request(`/api/platform/messages/email/${id}`, {
      method: 'DELETE',
    });
  }
  /**
   * Send email message
   * @param id Id
   * @returns {void}
   */
  function sendApi(id: string): Promise<void> {
    return request(`/api/platform/messages/email/${id}/send`, {
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
