# Localization Management

The localized document management module is not supported by Windows systems because the project path is too long, so the project directory is shortened to lt.

## Features

* Support dynamic management of localized resources
*Support multi-language management (add, delete, modify and check)
* Support resource management (add, delete, modify and check)
*Support text management (add, delete, modify and check)
* Support memory caching of localized resources
* Support distributed cache synchronization
* Provide standard RESTful API interface
* Supports seamless integration with ABP framework

## Module description

### Basic module

* [LINGYUN.Abp.Localization.Persistence](../localization/LINGYUN.Abp.Localization.Persistence) localization persistence module, implements the IStaticLocalizationSaver interface to persist local static resources to storage facilities
* [LINGYUN.Abp.LocalizationManagement.Domain.Shared](./LINGYUN.Abp.LocalizationManagement.Domain.Shared) Domain layer public module, which defines error codes, localization, and module settings
* [LINGYUN.Abp.LocalizationManagement.Domain](./LINGYUN.Abp.LocalizationManagement.Domain) domain layer module, implements the ILocalizationStore interface
* [LINGYUN.Abp.LocalizationManagement.EntityFrameworkCore](./LINGYUN.Abp.LocalizationManagement.EntityFrameworkCore) data access layer module, integrated with EfCore
* [LINGYUN.Abp.LocalizationManagement.Application.Contracts](./LINGYUN.Abp.LocalizationManagement.Application.Contracts) Application service layer public module, which defines the external interface, permissions, and function restriction strategies for managing localization objects
* [LINGYUN.Abp.LocalizationManagement.Application](./LINGYUN.Abp.LocalizationManagement.Application) application service layer implementation, realizing the localized object management interface
* [LINGYUN.Abp.LocalizationManagement.HttpApi](./LINGYUN.Abp.LocalizationManagement.HttpApi) RestApi implementation implements an independent external RestApi interface

### High-level modules

There are no high-level modules yet.

### Permission definition

* Whether the LocalizationManagement.Resource authorization object allows access to resources
* LocalizationManagement.Resource.Create Whether the authorization object allows the creation of resources
* LocalizationManagement.Resource.Update Whether the authorization object allows modification of resources
* LocalizationManagement.Resource.Delete Whether the authorization object allows deletion of resources
* LocalizationManagement.Language Whether the authorization object allows access to the language
* LocalizationManagement.Language.Create Whether the authorization object allows language creation
* LocalizationManagement.Language.Update Whether the authorization object allows language modification
* LocalizationManagement.Language.Delete Whether the authorization object allows language deletion
* LocalizationManagement.Text Whether the authorization object allows access to the document
* LocalizationManagement.Text.Create Whether the authorization object allows document creation
* LocalizationManagement.Text.Update Whether the authorization object allows modification of the document
* LocalizationManagement.Text.Delete Whether the authorization object allows deletion of documents

### Configuration definition

```json
{
  "LocalizationManagement": {
    "LocalizationCacheStampTimeOut": "00:02:00", // Localization cache timestamp timeout, default 2 minutes
    "LocalizationCacheStampExpiration": "00:30:00" // Localization cache expiration time, default 30 minutes
  }
}
```

### Database table

This module uses the following database tables to store localized data:

* Languages - Language table
* Resources - Resource table
* Texts - text table

Table prefix and Schema can be configured through `LocalizationModelBuilderConfigurationOptions`:

```csharp
public class LocalizationModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
{
    public LocalizationModelBuilderConfigurationOptions(
       string tablePrefix = "",
       string schema = null)
       : base(tablePrefix, schema)
    {
    }
}
```

### API interface

This module provides the following REST API interfaces:

* `/api/localization-management/languages` - Language management related API
* `/api/localization-management/resources` - Resource management related API
* `/api/localization-management/texts` - Text management related API

## Error code

* Localization:001100 - Language {CultureName} already exists
* Localization:001400 - The language name {CultureName} does not exist or the built-in language does not allow the operation
* Localization:002100 - Resource {Name} already exists
* Localization:002400 - The resource name {Name} does not exist or the built-in resource does not allow the operation

## More information

* [English documentation](./README.EN.md)

## Update log

### 2024.12
* Improve module documentation
* Add English document support
