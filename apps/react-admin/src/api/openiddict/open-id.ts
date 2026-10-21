import type { OpenIdConfiguration } from "#/abp-core/openid";

import requestClient from "../request";

/**
 * openid discovery endpoint
 * @returns OpenId configuration data
 */
export function discoveryApi(): Promise<OpenIdConfiguration> {
	return requestClient.get<OpenIdConfiguration>("/.well-known/openid-configuration");
}
