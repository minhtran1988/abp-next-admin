import type { ListResultDto, PagedResultDto } from '@abp/core';

import type {
  AIToolDefinitionRecordCreateDto,
  AIToolDefinitionRecordDto,
  AIToolDefinitionRecordGetPagedListInput,
  AIToolDefinitionRecordUpdateDto,
  AIToolProviderDto,
} from '../types/tools';

import { useRequest } from '@abp/request';

export function useAIToolsApi() {
  const { cancel, request } = useRequest();

  /**
   * Get details of AI tools
   * @param id AI tool id
   * @returns AI tool details
   */
  function getApi(id: string): Promise<AIToolDefinitionRecordDto> {
    return request<AIToolDefinitionRecordDto>(
      `/api/ai-management/tools/${id}`,
      {
        method: 'GET',
      },
    );
  }

  /**
   * Get the paginated list of AI tools
   * @param input query filter parameters
   * @returns AI tool paginated list
   */
  function getPagedListApi(
    input?: AIToolDefinitionRecordGetPagedListInput,
  ): Promise<PagedResultDto<AIToolDefinitionRecordDto>> {
    return request<PagedResultDto<AIToolDefinitionRecordDto>>(
      '/api/ai-management/tools',
      {
        method: 'GET',
        params: input,
      },
    );
  }

  /**
   * Get a list of available AI tool providers
   * @returns AI tool provider list
   */
  function getAvailableProviderListApi(): Promise<
    ListResultDto<AIToolProviderDto>
  > {
    return request<ListResultDto<AIToolProviderDto>>(
      '/api/ai-management/tools/available-providers',
      {
        method: 'GET',
      },
    );
  }

  /**
   * Remove AI tools
   * @param id AI tool id
   */
  function deleteApi(id: string): Promise<void> {
    return request(`/api/ai-management/tools/${id}`, {
      method: 'DELETE',
    });
  }

  /**
   * Added AI tools
   * @param input parameter
   * @returns AI tool details
   */
  function createApi(
    input: AIToolDefinitionRecordCreateDto,
  ): Promise<AIToolDefinitionRecordDto> {
    return request<AIToolDefinitionRecordDto>(`/api/ai-management/tools`, {
      data: input,
      method: 'POST',
    });
  }

  /**
   * Edit AI tools
   * @param id AI tool id
   * @param input parameter
   * @returns AI tool details
   */
  function updateApi(
    id: string,
    input: AIToolDefinitionRecordUpdateDto,
  ): Promise<AIToolDefinitionRecordDto> {
    return request<AIToolDefinitionRecordDto>(
      `/api/ai-management/tools/${id}`,
      {
        data: input,
        method: 'PUT',
      },
    );
  }

  return {
    cancel,
    createApi,
    deleteApi,
    getApi,
    getAvailableProviderListApi,
    getPagedListApi,
    updateApi,
  };
}
