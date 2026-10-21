import type { ListResultDto } from '@abp/core';

import type {
  TextTemplateDefinitionCreateDto,
  TextTemplateDefinitionDto,
  TextTemplateDefinitionGetListInput,
  TextTemplateDefinitionUpdateDto,
} from '../types/definitions';

import { useRequest } from '@abp/request';

export function useTemplateDefinitionsApi() {
  const { cancel, request } = useRequest();

  /**
   * Added template definition
   * @param input parameter
   * @returns template definition data transfer object
   */
  function createApi(
    input: TextTemplateDefinitionCreateDto,
  ): Promise<TextTemplateDefinitionDto> {
    return request<TextTemplateDefinitionDto>(
      '/api/text-templating/template/definitions',
      {
        data: input,
        method: 'POST',
      },
    );
  }
  /**
   * Delete template definition
   * @param name template name
   */
  function deleteApi(name: string): Promise<void> {
    return request(`/api/text-templating/template/definitions/${name}`, {
      method: 'DELETE',
    });
  }
  /**
   * Get template definition
   * @param name template name
   * @returns template definition data transfer object
   */
  function getApi(name: string): Promise<TextTemplateDefinitionDto> {
    return request<TextTemplateDefinitionDto>(
      `/api/text-templating/template/definitions/${name}`,
      {
        method: 'GET',
      },
    );
  }
  /**
   * Get template definition list
   * @param input filter parameter
   * @returns Template definition data transfer object list
   */
  function getListApi(
    input?: TextTemplateDefinitionGetListInput,
  ): Promise<ListResultDto<TextTemplateDefinitionDto>> {
    return request<ListResultDto<TextTemplateDefinitionDto>>(
      `/api/text-templating/template/definitions/${name}`,
      {
        method: 'GET',
        params: input,
      },
    );
  }
  /**
   * Update template definition
   * @param name template name
   * @returns template definition data transfer object
   */
  function updateApi(
    name: string,
    input: TextTemplateDefinitionUpdateDto,
  ): Promise<TextTemplateDefinitionDto> {
    return request<TextTemplateDefinitionDto>(
      `/api/text-templating/template/definitions/${name}`,
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
    getListApi,
    updateApi,
  };
}
