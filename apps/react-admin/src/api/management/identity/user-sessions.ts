import type { PagedResultDto } from "#/abp-core";
import type { GetUserSessionsInput, IdentitySessionDto } from "#/management/identity/sessions";
import requestClient from "@/api/request";

/**
 * Query session list
 * @param { GetUserSessionsInput } input query parameter
 * @returns { Promise<PagedResultDto<IdentitySessionDto>> } User session list
 */
export function getSessionsApi(input?: GetUserSessionsInput): Promise<PagedResultDto<IdentitySessionDto>> {
	return requestClient.get<PagedResultDto<IdentitySessionDto>>("/api/identity/sessions", {
		params: input,
	});
}
/**
 * Cancel session
 * @param { string } sessionId session id
 * @returns { Promise<void> }
 */
export function revokeSessionApi(sessionId: string): Promise<void> {
	return requestClient.delete(`/api/identity/sessions/${sessionId}/revoke`);
}
