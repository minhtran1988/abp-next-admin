# LINGYUN.Abp.EntityChange.HttpApi

HTTP API module for entity change tracking and recovery.

## Function

* Provide HTTP API for entity change query
* Provide HTTP API for entity recovery
* Support multi-language localization

## Basic usage

```csharp
[DependsOn(typeof(AbpEntityChangeHttpApiModule))]
public class YouProjectModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // ...
    }
}
```

## API endpoint

* `EntityChangeController`: Entity change controller
  * GET `/api/entity-changes`: Get the entity change history list

* `EntityRestoreController`: Entity recovery controller
  * PUT `/api/entity-restore`: Restore a single entity to the specified version
  * PUT `/api/{EntityId}/entity-restore`: Restore the entity with the specified ID
  * PUT `/api/{EntityId}/v/{EntityChangeId}/entity-restore`: Restore the entity with the specified ID to the specified version
  * PUT `/api/entites-restore`: Restore entities to the specified version in batches

## Localization

The module uses `AbpEntityChangeResource` as a localized resource to support multi-language display.

[English](./README.EN.md)
