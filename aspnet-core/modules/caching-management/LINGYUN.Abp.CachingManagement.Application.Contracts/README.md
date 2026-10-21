# LINGYUN.Abp.CachingManagement.Application.Contracts

Cache management application service contract module.

## Interface

### ICacheAppService

Cache management application service interface provides the following functions:

* `GetKeysAsync`: Get the cache key list
* `GetValueAsync`: Get cached value
* `SetAsync`: Set cache value
* `RefreshAsync`: Refresh cache
* `RemoveAsync`: delete cache

## Permissions

* AbpCachingManagement.Cache: cache management
  *AbpCachingManagement.Cache.Refresh: Refresh the cache
  * AbpCachingManagement.Cache.Delete: Delete cache
  * AbpCachingManagement.Cache.ManageValue: Manage cache value

## Installation

```bash
abp add-module LINGYUN.Abp.CachingManagement
```

## More

For more information, see the following resources:

* [Application service implementation](../LINGYUN.Abp.CachingManagement.Application/README.md)
* [HTTP API](../LINGYUN.Abp.CachingManagement.HttpApi/README.md)
* [Domain layer](../LINGYUN.Abp.CachingManagement.Domain/README.md)
