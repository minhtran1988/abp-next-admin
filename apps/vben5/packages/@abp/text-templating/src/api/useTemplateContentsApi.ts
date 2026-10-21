import type {
  TextTemplateContentDto,
  TextTemplateContentGetInput,
  TextTemplateContentUpdateDto,
  TextTemplateRestoreInput,
} from '../types/contents';

import { useRequest } from '@abp/request';

export function useTemplateContentsApi() {
  const { cancel, request } = useRequest();
  /**
   * Get template content
   * @param input parameter
   * @returns template content data transfer object
   */
  function getApi(
    input: TextTemplateContentGetInput,
  ): Promise<TextTemplateContentDto> {
    let url = '/api/text-templating/templates/content';
    url += input.culture ? `/${input.culture}/${input.name}` : `/${input.name}`;
    return request<TextTemplateContentDto>(url, {
      method: 'GET',
    });
  }
  /**
   *Reset template content to default values
   * @param name template name
   * @param input parameter
   * @returns Template definition data transfer object list
   */
  function restoreToDefaultApi(
    name: string,
    input: TextTemplateRestoreInput,
  ): Promise<void> {
    return request(
      `/api/text-templating/templates/content/${name}/restore-to-default`,
      {
        data: input,
        method: 'PUT',
      },
    );
  }
  /**
   * Update template content
   * @param name template name
   * @param input parameter
   * @returns template content data transfer object
   */
  function updateApi(
    name: string,
    input: TextTemplateContentUpdateDto,
  ): Promise<TextTemplateContentDto> {
    return request<TextTemplateContentDto>(
      `/api/text-templating/templates/content/${name}`,
      {
        data: input,
        method: 'PUT',
      },
    );
  }

  return {
    cancel,
    getApi,
    restoreToDefaultApi,
    updateApi,
  };
}
