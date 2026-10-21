# LINGYUN.Abp.EntityChange.Application.Contracts

Application layer contract module for entity change tracking and recovery.

## Function

* Provide entity change query interface
* Provide entity recovery interface
* Provide DTO objects related to entity changes
* Provide multi-language localization resources

## Basic usage

```csharp
[DependsOn(typeof(AbpEntityChangeApplicationContractsModule))]
public class YouProjectModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // ...
    }
}
```

## API

* `IEntityChangeAppService`: Entity change query service interface
  * `GetListAsync`: Get the entity change history list

* `IEntityRestoreAppService`: Entity recovery service interface
  * `RestoreEntityAsync`: Restore a single entity to the specified version
  * `RestoreEntitesAsync`: Batch restore entities to the specified version

## Localization

The module provides localized resources in the following languages:
* en
* zh-Hans

Localization resources are located in the `/LINGYUN/Abp/EntityChange/Localization/Resources` directory.

[English](./README.EN.md)
