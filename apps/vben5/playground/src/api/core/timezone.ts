import { requestClient } from '#/api/request';

/**
 * Get the list of time zones supported by the system
 */
export async function getTimezoneOptionsApi() {
  return await requestClient.get<
    {
      label: string;
      value: string;
    }[]
  >('/timezone/getTimezoneOptions');
}
/**
 * Get user time zone
 */
export async function getTimezoneApi(): Promise<null | string | undefined> {
  return requestClient.get<null | string | undefined>('/timezone/getTimezone');
}
/**
 * Set user time zone
 * @param timezone time zone
 */
export async function setTimezoneApi(timezone: string): Promise<void> {
  return requestClient.post('/timezone/setTimezone', { timezone });
}
