import type { ListResultDto } from "#/abp-core";

import type {
	TextTemplateDefinitionCreateDto,
	TextTemplateDefinitionDto,
	TextTemplateDefinitionGetListInput,
	TextTemplateDefinitionUpdateDto,
} from "#/text-templating/definitions";

import requestClient from "../request";

/**
 * Added template definition
 * @param input parameter
 * @returns template definition data transfer object
 */
export function createApi(input: TextTemplateDefinitionCreateDto): Promise<TextTemplateDefinitionDto> {
	return requestClient.post<TextTemplateDefinitionDto>("/api/text-templating/template/definitions", input);
}

/**
 * Delete template definition
 * @param name template name
 */
export function deleteApi(name: string): Promise<void> {
	return requestClient.delete(`/api/text-templating/template/definitions/${name}`);
}

/**
 * Get template definition
 * @param name template name
 * @returns template definition data transfer object
 */
export function getApi(name: string): Promise<TextTemplateDefinitionDto> {
	return requestClient.get<TextTemplateDefinitionDto>(`/api/text-templating/template/definitions/${name}`);
}

/**
 * Get template definition list
 * @param input filter parameter
 * @returns Template definition data transfer object list
 */
export function getListApi(
	input?: TextTemplateDefinitionGetListInput,
): Promise<ListResultDto<TextTemplateDefinitionDto>> {
	return requestClient.get<ListResultDto<TextTemplateDefinitionDto>>("/api/text-templating/template/definitions", {
		params: input,
	});
}

/**
 * Update template definition
 * @param name template name
 * @returns template definition data transfer object
 */
export function updateApi(name: string, input: TextTemplateDefinitionUpdateDto): Promise<TextTemplateDefinitionDto> {
	return requestClient.put<TextTemplateDefinitionDto>(`/api/text-templating/template/definitions/${name}`, input);
}
