import type { ListResultDto, PagedResultDto } from '@abp/core';

import type {
  ChatClientProviderDto,
  WorkspaceDefinitionRecordCreateDto,
  WorkspaceDefinitionRecordDto,
  WorkspaceDefinitionRecordGetPagedListInput,
  WorkspaceDefinitionRecordUpdateDto,
} from '../types/workspaces';

import { useRequest } from '@abp/request';

export function useWorkspaceDefinitionsApi() {
  const { cancel, request } = useRequest();

  /**
   * Get workspace details
   * @param id workspace id
   * @returns workspace details
   */
  function getApi(id: string): Promise<WorkspaceDefinitionRecordDto> {
    return request<WorkspaceDefinitionRecordDto>(
      `/api/ai-management/workspaces/${id}`,
      {
        method: 'GET',
      },
    );
  }

  /**
   * Get the workspace page list
   * @param input query filter parameters
   * @returns workspace page list
   */
  function getPagedListApi(
    input?: WorkspaceDefinitionRecordGetPagedListInput,
  ): Promise<PagedResultDto<WorkspaceDefinitionRecordDto>> {
    return request<PagedResultDto<WorkspaceDefinitionRecordDto>>(
      '/api/ai-management/workspaces',
      {
        method: 'GET',
        params: input,
      },
    );
  }

  /**
   * Get the list of available model providers
   * @returns model provider list
   */
  function getAvailableProviderListApi(): Promise<
    ListResultDto<ChatClientProviderDto>
  > {
    return request<ListResultDto<ChatClientProviderDto>>(
      '/api/ai-management/workspaces/available-providers',
      {
        method: 'GET',
      },
    );
  }

  /**
   * Delete workspace
   * @param id workspace id
   */
  function deleteApi(id: string): Promise<void> {
    return request(`/api/ai-management/workspaces/${id}`, {
      method: 'DELETE',
    });
  }

  /**
   * Add new workspace
   * @param input parameter
   * @returns workspace details
   */
  function createApi(
    input: WorkspaceDefinitionRecordCreateDto,
  ): Promise<WorkspaceDefinitionRecordDto> {
    return request<WorkspaceDefinitionRecordDto>(
      `/api/ai-management/workspaces`,
      {
        data: input,
        method: 'POST',
      },
    );
  }

  /**
   * Edit workspace
   * @param id workspace id
   * @param input parameter
   * @returns workspace details
   */
  function updateApi(
    id: string,
    input: WorkspaceDefinitionRecordUpdateDto,
  ): Promise<WorkspaceDefinitionRecordDto> {
    return request<WorkspaceDefinitionRecordDto>(
      `/api/ai-management/workspaces/${id}`,
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
