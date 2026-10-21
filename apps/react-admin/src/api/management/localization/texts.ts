import type { ListResultDto } from "#/abp-core";

import type {
	GetTextByKeyInput,
	GetTextsInput,
	SetTextInput,
	TextDifferenceDto,
	TextDto,
} from "#/management/localization/texts";

import requestClient from "../../request";

/**
 * Query text list
 * @param input parameter
 * @returns text list
 */
export function getListApi(input: GetTextsInput): Promise<ListResultDto<TextDifferenceDto>> {
	return requestClient.get<ListResultDto<TextDifferenceDto>>("/api/abp/localization/texts", {
		params: input,
	});
}

/**
 * Query text
 * @param input parameter
 * @returns query text
 */
export function getApi(input: GetTextByKeyInput): Promise<TextDto> {
	return requestClient.get<TextDto>("/api/abp/localization/texts/by-culture-key", {
		params: input,
	});
}

/**
 * Set text
 * @param input parameter
 */
export function setApi(input: SetTextInput): Promise<void> {
	return requestClient.put("/api/localization/texts", input);
}
