# LINGYUN.Abp.CachingManagement.HttpApi

HTTP API implementation of the cache management module.

## API interface

### /api/caching-management/cache

* GET `/api/caching-management/cache/keys`: Get the cache key list
  * Parameters:
    * prefix (string, optional): key prefix
    * filter (string, optional): filter conditions
    * marker (string, optional): paging mark
  * Permission: AbpCachingManagement.Cache

* GET `/api/caching-management/cache/{key}`: Get the cache value of the specified key
  * Parameters:
    * key (string, required): cache key
  * Permission: AbpCachingManagement.Cache

* POST `/api/caching-management/cache`: Set cache value
  * Parameters:
    * key (string, required): cache key
    * value (string, required): cache value
    * absoluteExpiration (DateTime, optional): absolute expiration time
    * slidingExpiration (DateTime, optional): sliding expiration time
  * Permission: AbpCachingManagement.Cache.ManageValue

* PUT `/api/caching-management/cache/refresh`: Refresh cache
  * Parameters:
    * key (string, required): cache key
    * absoluteExpiration (DateTime, optional): absolute expiration time
    * slidingExpiration (DateTime, optional): sliding expiration time
  * Permission: AbpCachingManagement.Cache.Refresh

* DELETE `/api/caching-management/cache`: delete cache
  * Parameters:
    * key (string, required): cache key
  * Permission: AbpCachingManagement.Cache.Delete

## Installation

```bash
abp add-module LINGYUN.Abp.CachingManagement
```

## More

For more information, see the following resources:

* [Application service implementation](../LINGYUN.Abp.CachingManagement.Application/README.md)
* [Application Service Contract](../LINGYUN.Abp.CachingManagement.Application.Contracts/README.md)
* [Domain layer](../LINGYUN.Abp.CachingManagement.Domain/README.md)
