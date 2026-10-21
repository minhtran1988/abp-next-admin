# LINGYUN.Abp.LocalizationManagement.Domain

The localization management domain layer module implements dynamic localization resource storage and management functions.

## Features

* Implement the `ILocalizationStore` interface to provide storage and retrieval functions for localized resources
* Support language management (add, delete, modify, check)
* Support resource management (add, delete, modify and check)
*Support text management (add, delete, modify and check)
* Support memory caching of localized resources
* Support distributed cache synchronization

## Module reference

```csharp
[DependsOn(typeof(AbpLocalizationManagementDomainModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

```json
{
  "LocalizationManagement": {
    "LocalizationCacheStampTimeOut": "00:02:00", // Localization cache timestamp timeout, default 2 minutes
    "LocalizationCacheStampExpiration": "00:30:00" // Localization cache expiration time, default 30 minutes
  }
}
```

## Domain Services

* `LanguageManager`: Language management service, providing language creation, update, deletion and other functions
* `ResourceManager`: Resource management service, providing functions such as resource creation, update, and deletion
* `TextManager`: Text management service, providing text creation, update, deletion and other functions
* `LocalizationStore`: localization storage service, which implements the `ILocalizationStore` interface
* `LocalizationStoreInMemoryCache`: localized resource memory cache service

## More information

* [English documentation](./README.EN.md)
