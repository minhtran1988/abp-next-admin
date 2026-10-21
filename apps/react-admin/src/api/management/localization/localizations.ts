import type { ApplicationLocalizationDto } from "#/abp-core";

import requestClient from "../../request";

/**
 * Get application language
 * @returns localization configuration
 */
export function getLocalizationApi(options: {
	cultureName: string;
	onlyDynamics?: boolean;
}): Promise<ApplicationLocalizationDto> {
	return requestClient.get<ApplicationLocalizationDto>("/api/abp/application-localization", {
		params: options,
	});
}
