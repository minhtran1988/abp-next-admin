# LY.MicroService.Platform.EntityFrameworkCore

The platform management data migration module provides database migration functions related to the platform management system.

[English](./README.EN.md)

## Features

* Integrated SaaS multi-tenant data migration
* Integrated platform management data migration
* Integrated settings to manage data migration
* Integrated rights management data migration
* Integrated features to manage data migration
* Integrate Vue Vben Admin UI navigation data migration
* Support MySQL database

## Module dependencies

```csharp
[DependsOn(
    typeof(AbpSaasEntityFrameworkCoreModule),
    typeof(PlatformEntityFrameworkCoreModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule),
    typeof(AbpUINavigationVueVbenAdminModule),
    typeof(AbpDataDbMigratorModule)
)]
```

## Configuration items

```json
{
  "ConnectionStrings": {
    "PlatformDbMigrator": "Your database connection string"
  }
}
```

## Basic usage

1. Configure database connection string
   * Configure the PlatformDbMigrator connection string in appsettings.json

2. Add module dependencies
   ```csharp
   [DependsOn(typeof(PlatformMigrationsEntityFrameworkCoreModule))]
   public class YourModule : AbpModule
   {
       // ...
   }
   ```

## Database table description

* Platform related tables - including data dictionary, organization, menu, version and other platform basic data
* Saas related tables - multi-tenant related data such as tenants and versions
* AbpSettings - system setting data
* AbpPermissionGrants - permission authorization data
* AbpFeatures - Functional feature data
* AbpUINavigation - Vue Vben Admin UI navigation data

## More information

* [ABP Documentation](https://docs.abp.io)
* [Vue Vben Admin Document](https://doc.vvbin.cn/)
