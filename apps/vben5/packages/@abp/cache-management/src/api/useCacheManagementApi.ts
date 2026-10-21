import type {
  CacheKeyInput,
  CacheKeysDto,
  CacheRefreshInput,
  CacheRemoveKeysInput,
  CacheSetInput,
  CacheValueDto,
  GetCacheKeysInput,
} from '../types';

import { useRequest } from '@abp/request';

/** Cache management API interface */
export function useCacheManagementApi() {
  const apiPrefix = '/api/caching-management/cache';
  const { cancel, request } = useRequest();

  /**
   * Get all cache keys
   * @param input parameter
   */
  function getKeysApi(input?: GetCacheKeysInput): Promise<CacheKeysDto> {
    return request<CacheKeysDto>(`${apiPrefix}/keys`, {
      method: 'GET',
      params: input,
    });
  }

  /**
   * Get cached value
   * @param input parameter
   */
  function getKeyValueApi(input: CacheKeyInput): Promise<CacheValueDto> {
    return request<CacheValueDto>(`${apiPrefix}/value`, {
      method: 'GET',
      params: input,
    });
  }

  /**
   * Set cache value
   * @param input parameter
   */
  function setValueApi(input: CacheSetInput): Promise<void> {
    return request(`${apiPrefix}/set`, {
      method: 'PUT',
      data: input,
    });
  }

  /**
   * Refresh cache
   * @param input parameter
   */
  function refreshApi(input: CacheRefreshInput): Promise<void> {
    return request(`${apiPrefix}/refresh`, {
      method: 'PUT',
      data: input,
    });
  }

  /**
   * Remove cache
   * @param input parameter
   */
  function removeApi(input: CacheKeyInput): Promise<void> {
    return request(`${apiPrefix}/remove`, {
      method: 'DELETE',
      params: input,
    });
  }

  /**
   * Remove cache in batches
   * @param input parameter
   */
  function bulkRemoveApi(input: CacheRemoveKeysInput): Promise<void> {
    return request(`${apiPrefix}/bulk/remove`, {
      method: 'DELETE',
      data: input,
    });
  }

  return {
    cancel,
    getKeysApi,
    getKeyValueApi,
    refreshApi,
    setValueApi,
    removeApi,
    bulkRemoveApi,
  };
}
