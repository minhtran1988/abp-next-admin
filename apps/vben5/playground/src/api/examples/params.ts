import type { Recordable } from '@vben/types';

import { requestClient } from '#/api/request';

/**
 * Initiate array request
 */
async function getParamsData(
  params: Recordable<any>,
  type: 'brackets' | 'comma' | 'indices' | 'repeat',
) {
  return requestClient.get('/status', {
    params,
    paramsSerializer: type,
    responseReturn: 'raw',
  });
}

export { getParamsData };
