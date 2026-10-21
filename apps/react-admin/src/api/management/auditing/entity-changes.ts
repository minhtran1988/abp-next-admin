import type { ListResultDto } from "#/abp-core";

import type { EntityChangeGetWithUsernameInput, EntityChangeWithUsernameDto } from "#/management/auditing";
import requestClient from "../../request";

/**
 * Get the entity change list containing the user name
 * @param input parameter
 */
export function getListWithUsernameApi(
	input: EntityChangeGetWithUsernameInput,
): Promise<ListResultDto<EntityChangeWithUsernameDto>> {
	return requestClient.get<ListResultDto<EntityChangeWithUsernameDto>>("/api/auditing/entity-changes/with-username", {
		params: input,
	});
}
