# LINGYUN.Abp.MicroService.MessageService.EntityFrameworkCore

The real-time message data migration module provides database migration functions related to real-time messages and notification systems.

[English](./README.EN.md)

## Features

* Integrated SaaS multi-tenant data migration
* Integrated notification system data migration
* Integrated messaging service data migration
* Integrated text template data migration
* Integrated settings to manage data migration
* Integrated rights management data migration
* Integrated features to manage data migration
* Support MySQL database

## Module dependencies

```csharp
[DependsOn(
    typeof(AbpSaasEntityFrameworkCoreModule),
    typeof(AbpNotificationsEntityFrameworkCoreModule),
    typeof(AbpMessageServiceEntityFrameworkCoreModule),
    typeof(AbpTextTemplatingEntityFrameworkCoreModule),
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
    "RealtimeMessageDbMigrator": "Your database connection string"
  }
}
```

## Basic usage

1. Configure database connection string
   * Configure the RealtimeMessageDbMigrator connection string in appsettings.json

2. Add module dependencies
   ```csharp
   [DependsOn(typeof(RealtimeMessageMigrationsEntityFrameworkCoreModule))]
   public class YourModule : AbpModule
   {
       // ...
   }
   ```

## Database table description

* AbpNotifications - notification system data table
* AbpNotificationsDefinition - notification definition data table
* AbpMessageService - message service data table
* Saas related tables - multi-tenant related data such as tenants and versions
* AbpSettings - system setting data
* AbpPermissionGrants - permission authorization data
* AbpFeatures - Functional feature data
* AbpTextTemplates - text template data

## More information

* [ABP Documentation](https://docs.abp.io)
* [ABP Notification System Documentation](https://docs.abp.io/zh-Hans/abp/latest/Notification-System)
