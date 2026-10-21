# LINGYUN.Abp.CachingManagement.Domain

Domain layer implementation of cache management module.

## Core interface

### ICacheManager

The cache manager interface defines the core functions of cache management:

* `GetKeysAsync`: Get the cache key list
* `GetValueAsync`: Get cached value
* `SetAsync`: Set cache value
* `RefreshAsync`: Refresh cache
* `RemoveAsync`: delete cache

## Domain Services

### CacheManager

The abstract base class of cache manager provides basic cache management implementation. Concrete cache providers (such as Redis) need to inherit this class and implement the corresponding methods.

## Installation

```bash
abp add-module LINGYUN.Abp.CachingManagement
```

## Extension development

If you need to support a new cache provider, you need to:

1. Create a new project and inherit the `CacheManager` class
2. Implement all abstract methods
3. Register as an implementation of `ICacheManager` (using `[Dependency(ReplaceServices = true)]`)

## More

For more information, see the following resources:

* [Application service implementation](../LINGYUN.Abp.CachingManagement.Application/README.md)
* [Application Service Contract](../LINGYUN.Abp.CachingManagement.Application.Contracts/README.md)
* [HTTP API](../LINGYUN.Abp.CachingManagement.HttpApi/README.md)
* [Redis implementation](../LINGYUN.Abp.CachingManagement.StackExchangeRedis/README.md)
