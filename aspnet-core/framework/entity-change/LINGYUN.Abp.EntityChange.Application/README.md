# LINGYUN.Abp.EntityChange.Application

Application layer implementation module for entity change tracking and recovery.

## Function

* Implement entity change query service
* Implement entity recovery service
* Provide automatic mapping configuration for entity changes

## Basic usage

```csharp
[DependsOn(typeof(AbpEntityChangeApplicationModule))]
public class YouProjectModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // ...
    }
}
```

## Service implementation

* `EntityRestoreAppService<TEntity, TKey>`: Entity recovery service implementation
  * `RestoreEntityAsync`: Restore a single entity to a specified version through the audit log
  * `RestoreEntitesAsync`: Batch restore entities to a specified version through audit logs
  * Support configuring permission recovery policy through `RestorePolicy`

## Object mapping

The module uses AutoMapper to implement automatic mapping of the following objects:
* `EntityPropertyChange` -> `EntityPropertyChangeDto`
* `EntityChange` -> `EntityChangeDto`

[English](./README.EN.md)
