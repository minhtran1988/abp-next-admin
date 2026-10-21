# LINGYUN.Abp.MicroService.LocalizationService.EntityFrameworkCore

The localization management data migration module provides database migration functions related to the localization management system.

[English](./README.EN.md)

## Features

* Integrated SaaS multi-tenant data migration
* Integrate localized management data migration
* Integrated settings to manage data migration
* Integrated rights management data migration
* Integrated features to manage data migration
* Support MySQL database

## Module dependencies

```csharp
[DependsOn(
    typeof(AbpSaasEntityFrameworkCoreModule),
    typeof(AbpLocalizationManagementEntityFrameworkCoreModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule),
    typeof(AbpDataDbMigratorModule)
)]
```

## Configuration items

```json
{
  "ConnectionStrings": {
    "LocalizationManagementDbMigrator": "Your database connection string"
  }
}
```

## Basic usage

1. Configure database connection string
   * Configure the LocalizationManagementDbMigrator connection string in appsettings.json

2. Add module dependencies
   ```csharp
   [DependsOn(typeof(LocalizationManagementMigrationsEntityFrameworkCoreModule))]
   public class YourModule : AbpModule
   {
       // ...
   }
   ```

## Database table description

* AbpLocalization - localized resources and text data
* Saas related tables - multi-tenant related data such as tenants and versions
* AbpSettings - system setting data
* AbpPermissionGrants - permission authorization data
* AbpFeatures - Functional feature data

## More information

* [ABP Documentation](https://docs.abp.io)
* [ABP Localization Document](https://docs.abp.io/zh-Hans/abp/latest/Localization)
