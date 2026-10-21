# LINGYUN.Abp.Elsa.EntityFrameworkCore.PostgreSql

PostgreSql database provider module for Elsa workflow

## Function

* Provide PostgreSql database support for Elsa workflow
* Depends on `AbpElsaEntityFrameworkCoreModule` and `AbpEntityFrameworkCorePostgreSqlModule`
* Pre-configured with the following features:
  * Persistent storage
  * Webhooks storage
  * Workflow settings storage

## Configuration usage

```csharp
[DependsOn(
    typeof(AbpElsaEntityFrameworkCorePostgreSqlModule)
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
