import { setTimezoneHandler } from '@vben/stores';

import { getTimezoneApi, getTimezoneOptionsApi, setTimezoneApi } from '#/api';

/**
 * Initialize time zone processing and save time zone settings through API
 */
export function initTimezone() {
  setTimezoneHandler({
    getTimezone() {
      return getTimezoneApi();
    },
    setTimezone(timezone: string) {
      return setTimezoneApi(timezone);
    },
    getTimezoneOptions() {
      return getTimezoneOptionsApi();
    },
  });
}
