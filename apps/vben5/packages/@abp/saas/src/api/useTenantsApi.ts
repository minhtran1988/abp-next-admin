import type { ListResultDto, PagedResultDto } from '@abp/core';

import type {
  GetTenantPagedListInput,
  TenantConnectionStringCheckInput,
  TenantConnectionStringDto,
  TenantConnectionStringSetInput,
  TenantCreateDto,
  TenantDto,
  TenantUpdateDto,
} from '../types';

import { useRequest } from '@abp/request';

export function useTenantsApi() {
  const { cancel, request } = useRequest();
  /**
   * Create tenant
   * @param {TenantCreateDto} input parameter
   * @returns created tenant
   */
  function createApi(input: TenantCreateDto): Promise<TenantDto> {
    return request<TenantDto>('/api/saas/tenants', {
      data: input,
      method: 'POST',
    });
  }

  /**
   * Edit tenant
   * @param {string} id parameter
   * @param {TenantUpdateDto} input parameter
   * @returns edited tenant
   */
  function updateApi(id: string, input: TenantUpdateDto): Promise<TenantDto> {
    return request<TenantDto>(`/api/saas/tenants/${id}`, {
      data: input,
      method: 'PUT',
    });
  }

  /**
   * Query tenants
   * @param {string} id Id
   * @returns queried tenant
   */
  function getApi(id: string): Promise<TenantDto> {
    return request<TenantDto>(`/api/saas/tenants/${id}`, {
      method: 'GET',
    });
  }

  /**
   * Delete tenant
   * @param {string} id Id
   * @returns {void}
   */
  function deleteApi(id: string): Promise<void> {
    return request(`/api/saas/tenants/${id}`, {
      method: 'DELETE',
    });
  }

  /**
   * Query tenant paging list
   * @param {GetTenantPagedListInput} input parameter
   * @returns {void}
   */
  function getPagedListApi(
    input?: GetTenantPagedListInput,
  ): Promise<PagedResultDto<TenantDto>> {
    return request<PagedResultDto<TenantDto>>(`/api/saas/tenants`, {
      method: 'GET',
      params: input,
    });
  }

  /**
   * Set connection string
   * @param {string} id tenant ID
   * @param {TenantConnectionStringSetInput} input parameter
   * @returns connection string
   */
  function setConnectionStringApi(
    id: string,
    input: TenantConnectionStringSetInput,
  ): Promise<TenantConnectionStringDto> {
    return request<TenantConnectionStringDto>(
      `/api/saas/tenants/${id}/connection-string`,
      {
        data: input,
        method: 'PUT',
      },
    );
  }

  /**
   * Query connection string
   * @param {string} id tenant ID
   * @param {string} name connection string name
   * @returns connection string
   */
  function getConnectionStringApi(
    id: string,
    name: string,
  ): Promise<TenantConnectionStringDto> {
    return request<TenantConnectionStringDto>(
      `/api/saas/tenants/${id}/connection-string/${name}`,
      {
        method: 'GET',
      },
    );
  }

  /**
   * Query all connection strings
   * @param {string} id tenant ID
   * @returns connection string list
   */
  function getConnectionStringsApi(
    id: string,
  ): Promise<ListResultDto<TenantConnectionStringDto>> {
    return request<ListResultDto<TenantConnectionStringDto>>(
      `/api/saas/tenants/${id}/connection-string`,
      {
        method: 'GET',
      },
    );
  }

  /**
   * Delete tenant
   * @param {string} id tenant ID
   * @param {string} name connection string name
   * @returns {void}
   */
  function deleteConnectionStringApi(id: string, name: string): Promise<void> {
    return request(`/api/saas/tenants/${id}/connection-string/${name}`, {
      method: 'DELETE',
    });
  }

  /**
   * Check database connection string
   * @param input parameter
   */
  function checkConnectionString(
    input: TenantConnectionStringCheckInput,
  ): Promise<void> {
    return request(`/api/saas/tenants/connection-string/check`, {
      data: input,
      method: 'POST',
    });
  }

  return {
    cancel,
    checkConnectionString,
    createApi,
    deleteApi,
    deleteConnectionStringApi,
    getApi,
    getConnectionStringApi,
    getConnectionStringsApi,
    getPagedListApi,
    setConnectionStringApi,
    updateApi,
  };
}
