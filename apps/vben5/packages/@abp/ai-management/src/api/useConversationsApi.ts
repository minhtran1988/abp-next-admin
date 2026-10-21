import type { PagedResultDto } from '@abp/core';

import type {
  ConversationCreateDto,
  ConversationDto,
  ConversationGetPagedListInput,
  ConversationUpdateDto,
} from '../types/conversations';

import { useRequest } from '@abp/request';

export function useConversationsApi() {
  const { cancel, request } = useRequest();

  /**
   * Get session details
   * @param id session id
   * @returns session details
   */
  function getApi(id: string): Promise<ConversationDto> {
    return request<ConversationDto>(
      `/api/ai-management/chats/conversations/${id}`,
      {
        method: 'GET',
      },
    );
  }

  /**
   * Get the session paging list
   * @param input query filter parameters
   * @returns Session paging list
   */
  function getPagedListApi(
    input?: ConversationGetPagedListInput,
  ): Promise<PagedResultDto<ConversationDto>> {
    return request<PagedResultDto<ConversationDto>>(
      '/api/ai-management/chats/conversations',
      {
        method: 'GET',
        params: input,
      },
    );
  }

  /**
   * Delete session
   * @param id session id
   */
  function deleteApi(id: string): Promise<void> {
    return request(`/api/ai-management/chats/conversations/${id}`, {
      method: 'DELETE',
    });
  }

  /**
   * Add session
   * @param input parameter
   * @returns session details
   */
  function createApi(input: ConversationCreateDto): Promise<ConversationDto> {
    return request<ConversationDto>(`/api/ai-management/chats/conversations`, {
      data: input,
      method: 'POST',
    });
  }

  /**
   * Edit session
   * @param id session id
   * @param input parameter
   * @returns session details
   */
  function updateApi(
    id: string,
    input: ConversationUpdateDto,
  ): Promise<ConversationDto> {
    return request<ConversationDto>(`/api/ai-management/conversations/${id}`, {
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
