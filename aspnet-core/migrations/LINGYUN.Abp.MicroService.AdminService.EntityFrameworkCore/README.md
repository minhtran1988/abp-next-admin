# LINGYUN.Abp.MicroService.AdminService.EntityFrameworkCore

The backend management data migration module provides database migration functions related to the backend management system.

[English](./README.EN.md)

## Features

* Integrated SaaS multi-tenant data migration
* Integrated settings to manage data migration
* Integrated data protection management data migration
* Integrated rights management data migration
* Integrated features to manage data migration
* Integrated text template data migration
* Support MySQL database

## Module dependencies

```csharp
[DependsOn(
    typeof(AbpSaasEntityFrameworkCoreModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpDataProtectionManagementEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule),
    typeof(AbpTextTemplatingEntityFrameworkCoreModule),
    typeof(AbpDataDbMigratorModule)
)]
```

## Configuration items

```json
{
  "ConnectionStrings": {
    "BackendAdminDbMigrator": "Your database connection string"
  }
}
```

## Basic usage

1. Configure database connection string
   * Configure the BackendAdminDbMigrator connection string in appsettings.json

2. Add module dependencies
   ```csharp
   [DependsOn(typeof(BackendAdminMigrationsEntityFrameworkCoreModule))]
   public class YourModule : AbpModule
   {
       // ...
   }
   ```

## Database table description

* Saas related tables - multi-tenant related data such as tenants and versions
* AbpSettings - system setting data
* AbpDataProtection - Data protection related data
* AbpPermissionGrants - permission authorization data
* AbpFeatures - Functional feature data
* AbpTextTemplates - text template data

## More information

* [ABP Documentation](https://docs.abp.io)
* [ASP.NET Core Data Protection](https://docs.microsoft.com/zh-cn/aspnet/core/security/data-protection/introduction)
