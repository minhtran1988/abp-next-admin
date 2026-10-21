import type { PagedResultDto } from "#/abp-core";

import type { EditionCreateDto, EditionDto, EditionUpdateDto, GetEditionPagedListInput } from "#/saas/editions";

import requestClient from "../request";

/**
 * Create version
 * @param {EditionCreateDto} input parameter
 * @returns created version
 */
export function createApi(input: EditionCreateDto): Promise<EditionDto> {
	return requestClient.post<EditionDto>("/api/saas/editions", input);
}

/**
 *Edited version
 * @param {string} id parameter
 * @param {EditionUpdateDto} input parameter
 * @returns edited version
 */
export function updateApi(id: string, input: EditionUpdateDto): Promise<EditionDto> {
	return requestClient.put<EditionDto>(`/api/saas/editions/${id}`, input);
}

/**
 * Query version
 * @param {string} id Id
 * @returns Query version
 */
export function getApi(id: string): Promise<EditionDto> {
	return requestClient.get<EditionDto>(`/api/saas/editions/${id}`);
}

/**
 * delete version
 * @param {string} id Id
 * @returns {void}
 */
export function deleteApi(id: string): Promise<void> {
	return requestClient.delete(`/api/saas/editions/${id}`);
}

/**
 * Query version paging list
 * @param {GetEditionPagedListInput} input parameter
 * @returns {void}
 */
export function getPagedListApi(input?: GetEditionPagedListInput): Promise<PagedResultDto<EditionDto>> {
	return requestClient.get<PagedResultDto<EditionDto>>("/api/saas/editions", {
		params: input,
	});
}
