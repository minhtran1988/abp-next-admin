# LINGYUN.Abp.Elsa.EntityFrameworkCore.MySql

MySql database provider module for Elsa workflow

## Function

* Provide MySql database support for Elsa workflow
* Depends on `AbpElsaEntityFrameworkCoreModule` and `AbpEntityFrameworkCoreMySQLModule`
* Pre-configured with the following features:
  * Persistent storage
  * Webhooks storage
  * Workflow settings storage

## Configuration usage

```csharp
[DependsOn(
    typeof(AbpElsaEntityFrameworkCoreMySqlModule)
    )]
public class YouProjectModule : AbpModule
{
}
```

## Configuration items

```json
{
    "Elsa": {
        "Persistence": true, // Enable persistent storage
        "Webhooks": true, // Enable Webhooks storage
        "WorkflowSettings": true // Enable workflow settings storage
    }
}
```

[English](./README.EN.md)
