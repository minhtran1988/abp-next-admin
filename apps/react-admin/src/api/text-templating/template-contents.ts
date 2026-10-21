import type {
	TextTemplateContentDto,
	TextTemplateContentGetInput,
	TextTemplateContentUpdateDto,
	TextTemplateRestoreInput,
} from "#/text-templating/contents";

import requestClient from "../request";

/**
 * Get template content
 * @param input parameter
 * @returns template content data transfer object
 */
export function getApi(input: TextTemplateContentGetInput): Promise<TextTemplateContentDto> {
	let url = "/api/text-templating/templates/content";
	url += input.culture ? `/${input.culture}/${input.name}` : `/${input.name}`;
	return requestClient.get<TextTemplateContentDto>(url);
}

/**
 *Reset template content to default values
 * @param name template name
 * @param input parameter
 * @returns Template definition data transfer object list
 */
export function restoreToDefaultApi(name: string, input: TextTemplateRestoreInput): Promise<void> {
	return requestClient.put(`/api/text-templating/templates/content/${name}/restore-to-default`, input);
}

/**
 * Update template content
 * @param name template name
 * @param input parameter
 * @returns template content data transfer object
 */
export function updateApi(name: string, input: TextTemplateContentUpdateDto): Promise<TextTemplateContentDto> {
	return requestClient.put<TextTemplateContentDto>(`/api/text-templating/templates/content/${name}`, input);
}
