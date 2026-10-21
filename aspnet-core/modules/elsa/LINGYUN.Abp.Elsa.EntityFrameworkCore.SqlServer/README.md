# LINGYUN.Abp.Elsa.EntityFrameworkCore.SqlServer

SqlServer database provider module for Elsa workflow

## Function

* Provide SqlServer database support for Elsa workflow
* Depends on `AbpElsaEntityFrameworkCoreModule` and `AbpEntityFrameworkCoreSqlServerModule`
* Pre-configured with the following features:
  * Persistent storage
  * Webhooks storage
  * Workflow settings storage

## Configuration usage

```csharp
[DependsOn(
    typeof(AbpElsaEntityFrameworkCoreSqlServerModule)
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
