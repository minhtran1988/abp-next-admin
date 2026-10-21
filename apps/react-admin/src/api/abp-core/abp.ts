import type { ApplicationConfigurationDto, ApplicationLocalizationDto } from "#/abp-core";
import requestClient from "../request";

/**
 * Get application configuration information
 */
export function getConfigApi(options?: {
	includeLocalizationResources?: boolean;
}): Promise<ApplicationConfigurationDto> {
	return requestClient.get<ApplicationConfigurationDto>("/api/abp/application-configuration", {
		params: options,
	});
}

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
