# LINGYUN.Abp.Elsa.EntityFrameworkCore

EntityFrameworkCore integration module for Elsa workflow

## Function

* Provides EntityFrameworkCore integration for Elsa workflow
* Depends on `AbpElsaModule` and `AbpEntityFrameworkCoreModule`

## Configuration usage

```csharp
[DependsOn(
    typeof(AbpElsaEntityFrameworkCoreModule)
    )]
public class YouProjectModule : AbpModule
{
}
```

## Database provider

This module is a basic module that is independent of the database provider. You need to select the following corresponding modules according to the actual database used:

* [SqlServer](../LINGYUN.Abp.Elsa.EntityFrameworkCore.SqlServer/README.md)
* [PostgreSql](../LINGYUN.Abp.Elsa.EntityFrameworkCore.PostgreSql/README.md)
* [MySql](../LINGYUN.Abp.Elsa.EntityFrameworkCore.MySql/README.md)

[English](./README.EN.md)
