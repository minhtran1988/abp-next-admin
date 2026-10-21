import type { FeatureProvider, GetFeatureListResultDto, UpdateFeaturesDto } from "#/management/features/features";

import requestClient from "../../request";

/**
 * Delete function
 * @param {FeatureProvider} provider parameter
 * @returns {Promise<void>}
 */
export function deleteApi(provider: FeatureProvider): Promise<void> {
	return requestClient.delete("/api/feature-management/features", {
		params: provider,
	});
}

/**
 * Query function
 * @param {FeatureProvider} provider parameter
 * @returns {Promise<GetFeatureListResultDto>} Function entity data transfer object
 */
export function getApi(provider: FeatureProvider): Promise<GetFeatureListResultDto> {
	return requestClient.get<GetFeatureListResultDto>("/api/feature-management/features", {
		params: provider,
	});
}

/**
 *Update function
 * @param {FeatureProvider} provider
 * @param {UpdateFeaturesDto} input parameter
 * @returns {Promise<void>}
 */
export function updateApi(provider: FeatureProvider, input: UpdateFeaturesDto): Promise<void> {
	return requestClient.put("/api/feature-management/features", input, {
		params: provider,
	});
}
