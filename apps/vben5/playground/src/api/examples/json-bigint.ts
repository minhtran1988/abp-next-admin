import { requestClient } from '#/api/request';

/**
 * Initiate a request
 */
async function getBigIntData() {
  return requestClient.get('/demo/bigint');
}

export { getBigIntData };
