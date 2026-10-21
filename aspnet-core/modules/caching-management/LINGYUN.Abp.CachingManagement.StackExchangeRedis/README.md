# LINGYUN.Abp.CachingManagement.StackExchangeRedis

Cache management implementation module based on StackExchange.Redis.

## Function

* Implemented the `ICacheManager` interface to provide cache management functions based on Redis:
  * Get cache key list (supports prefix matching and filtering)
  * Get cached value
  * Set cache value (supports absolute expiration time and sliding expiration time)
  * Refresh cache
  * Delete cache

## Installation

```bash
abp add-module LINGYUN.Abp.CachingManagement
```

## Configuration usage

1. First you need to configure the Redis connection string and add: in `appsettings.json`:

```json
{
  "Redis": {
    "Configuration": "127.0.0.1:6379"
  }
}
```

2. Add the following code in the `ConfigureServices` method of the module:

```csharp
Configure<RedisCacheOptions>(options =>
{
    // Optional: configure the Redis instance name prefix
    options.InstanceName = "MyApp:";
});

Configure<AbpDistributedCacheOptions>(options =>
{
    // Optional: configure cache key prefix
    options.KeyPrefix = "MyApp:";
});
```

## Multi-tenant support

The module has built-in multi-tenant support and will automatically isolate cached data based on the current tenant ID:
* Key format when there is a tenant: `{InstanceName}t:{TenantId}:{KeyPrefix}:{Key}`
* Key format when there is no tenant: `{InstanceName}c:{KeyPrefix}:{Key}`

## More

For more information, see the following resources:

* [Application service implementation](../LINGYUN.Abp.CachingManagement.Application/README.md)
* [Application Service Contract](../LINGYUN.Abp.CachingManagement.Application.Contracts/README.md)
* [HTTP API](../LINGYUN.Abp.CachingManagement.HttpApi/README.md)
* [Domain layer](../LINGYUN.Abp.CachingManagement.Domain/README.md)
