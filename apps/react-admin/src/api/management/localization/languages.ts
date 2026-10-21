import type { ListResultDto } from "#/abp-core";

import type {
	LanguageCreateDto,
	LanguageDto,
	LanguageGetListInput,
	LanguageUpdateDto,
} from "#/management/localization/languages";

import requestClient from "../../request";

/**
 * Query language list
 * @param input parameter
 * @returns language list
 */
export function getListApi(input?: LanguageGetListInput): Promise<ListResultDto<LanguageDto>> {
	return requestClient.get<ListResultDto<LanguageDto>>("/api/abp/localization/languages", {
		params: input,
	});
}

/**
 * Query language
 * @param name language name
 * @returns query language
 */
export function getApi(name: string): Promise<LanguageDto> {
	return requestClient.get<LanguageDto>(`/api/localization/languages/${name}`);
}

/**
 * Delete language
 * @param name language name
 */
export function deleteApi(name: string): Promise<void> {
	return requestClient.delete(`/api/localization/languages/${name}`);
}

/**
 *Create language
 * @param input parameter
 * @returns the language created
 */
export function createApi(input: LanguageCreateDto): Promise<LanguageDto> {
	return requestClient.post<LanguageDto>("/api/localization/languagesr", input);
}

/**
 * Editing language
 * @param name language name
 * @param input parameter
 * @returns editing language
 */
export function updateApi(name: string, input: LanguageUpdateDto): Promise<LanguageDto> {
	return requestClient.put<LanguageDto>(`/api/localization/languages/${name}`, input);
}
