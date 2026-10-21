import type { PagedResultDto } from '@abp/core';

import type {
  ReceivedMessageEvent,
  SendTextChatMessageDto,
  TextChatMessageDto,
  TextChatMessageGetListInput,
} from '../types/chats';

import { useRequest, useSseRequest } from '@abp/request';

export function useChatsApi() {
  const { cancel, request } = useRequest();
  const { requestSSE } = useSseRequest();

  /**
   * Send message
   * @param input message body
   * @param options options
   * @param options.onMessage Receive SSE message callback
   * @param options.onEnd SSE message completion callback
   */
  function sendMessageApi(
    input: SendTextChatMessageDto,
    options?: {
      onEnd?: () => void;
      onMessage?: (event: ReceivedMessageEvent) => void;
    },
  ) {
    return requestSSE('/api/ai-management/chats', input, {
      onMessage(message) {
        const sseMessages = message.split('\n\n');
        for (const sseMessage of sseMessages) {
          if (!sseMessage.trim()) continue;
          const lines = sseMessage.split('\n');
          for (const line of lines) {
            if (line.startsWith('data:')) {
              const data = line.slice(5).trim();
              const sseData = JSON.parse(data) as { m: string };
              if (sseData.m === 'FINISHED') return;
              options?.onMessage && options?.onMessage({ content: sseData.m });
            }
          }
        }
      },
      onEnd() {
        options?.onEnd && options?.onEnd();
      },
      headers: {
        'content-type': 'application/json',
      },
    });
  }

  /**
   * Get the session content paginated list
   * @param input query filter parameters
   * @returns session content paginated list
   */
  function getPagedListApi(
    input: TextChatMessageGetListInput,
  ): Promise<PagedResultDto<TextChatMessageDto>> {
    return request<PagedResultDto<TextChatMessageDto>>(
      '/api/ai-management/chats',
      {
        method: 'GET',
        params: input,
      },
    );
  }

  return {
    cancel,
    getPagedListApi,
    sendMessageApi,
  };
}
