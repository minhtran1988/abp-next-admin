import type { ListResultDto } from "#/abp-core";

import type { SettingGroup, SettingsUpdateInput } from "#/management/settings";

import requestClient from "@/api/request";

/**
 * Get global settings
 * @returns Set the data transfer object list
 */
export function getGlobalSettingsApi(): Promise<ListResultDto<SettingGroup>> {
	return requestClient.get<ListResultDto<SettingGroup>>("/api/setting-management/settings/by-global");
}

/**
 * Set global settings
 * @returns Set the data transfer object list
 */
export function setGlobalSettingsApi(input: SettingsUpdateInput): Promise<void> {
	return requestClient.put("/api/setting-management/settings/change-global", input);
}

/**
 * Get tenant settings
 * @returns Set the data transfer object list
 */
export function getTenantSettingsApi(): Promise<ListResultDto<SettingGroup>> {
	return requestClient.get<ListResultDto<SettingGroup>>("api/setting-management/settings/by-current-tenant");
}

/**
 * Set tenant settings
 * @returns Set the data transfer object list
 */
export function setTenantSettingsApi(input: SettingsUpdateInput): Promise<void> {
	return requestClient.put("/api/setting-management/settings/change-current-tenant", input);
}
/**
 * Get user settings
 * @returns Set the data transfer object list
 */
export function getUserSettingsApi(): Promise<ListResultDto<SettingGroup>> {
	return requestClient.get<ListResultDto<SettingGroup>>("/api/setting-management/settings/by-current-user");
}

/**
 * Set user settings
 * @returns Set the data transfer object list
 */
export function setUserSettingsApi(input: SettingsUpdateInput): Promise<void> {
	return requestClient.put("/api/setting-management/settings/change-current-user", input);
}

/**
 * Send test email
 * @param emailAddress Email recipient address
 */
export const sendTestEmailApi = (emailAddress: string) => {
	return requestClient.post("/api/setting-management/settings/send-test-email", {
		emailAddress,
	});
};
