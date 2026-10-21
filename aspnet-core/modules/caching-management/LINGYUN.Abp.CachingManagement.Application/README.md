# LINGYUN.Abp.CachingManagement.Application

Implementation of cache management application service module.

## Function

* Implemented the `ICacheAppService` interface to provide basic functions of cache management:
  * Get cache key list
  * Get cached value
  * Set cache value
  * Refresh cache
  * Delete cache

## Permissions

* AbpCachingManagement.Cache: cache management
  *AbpCachingManagement.Cache.Refresh: Refresh the cache
  * AbpCachingManagement.Cache.Delete: Delete cache
  * AbpCachingManagement.Cache.ManageValue: Manage cache value

## Installation

```bash
abp add-module LINGYUN.Abp.CachingManagement
```

## Configuration usage

1. First, you need to install the corresponding cache implementation module, for example: [LINGYUN.Abp.CachingManagement.StackExchangeRedis](../LINGYUN.Abp.CachingManagement.StackExchangeRedis/README.md)

2. Add the following code in the `ConfigureServices` method of the module:

```csharp
Configure<AbpAutoMapperOptions>(options =>
{
    options.AddProfile<AbpCachingManagementApplicationAutoMapperProfile>(validate: true);
});
```

## More

For more information, see the following resources:

* [Application Service Contract](../LINGYUN.Abp.CachingManagement.Application.Contracts/README.md)
* [HTTP API](../LINGYUN.Abp.CachingManagement.HttpApi/README.md)
* [Domain layer](../LINGYUN.Abp.CachingManagement.Domain/README.md)
