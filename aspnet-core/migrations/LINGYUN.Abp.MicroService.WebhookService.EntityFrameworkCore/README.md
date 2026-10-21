# LINGYUN.Abp.MicroService.WebhookService.EntityFrameworkCore

The Webhooks management data migration module provides database migration functions related to the Webhooks management system.

[English](./README.EN.md)

## Features

* Integrated SaaS multi-tenant data migration
* Integrate Webhooks to manage data migration
* Integrated settings to manage data migration
* Integrated rights management data migration
* Integrated features to manage data migration
* Support MySQL database

## Module dependencies

```csharp
[DependsOn(
    typeof(AbpSaasEntityFrameworkCoreModule),
    typeof(WebhooksManagementEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCoreMySQLModule),
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
    "WebhooksManagementDbMigrator": "Your database connection string"
  }
}
```

## Basic usage

1. Configure database connection string
   * Configure the WebhooksManagementDbMigrator connection string in appsettings.json

2. Add module dependencies
   ```csharp
   [DependsOn(typeof(WebhooksManagementMigrationsEntityFrameworkCoreModule))]
   public class YourModule : AbpModule
   {
       // ...
   }
   ```

## Database table description

* AbpWebhooks - Webhooks basic information table
*AbpWebhookSubscriptions - Webhooks subscription form
*AbpWebhookGroups - Webhooks grouping table
*AbpWebhookEvents - Webhooks event table
*AbpWebhookSendAttempts - Webhooks sending record table
* Saas related tables - multi-tenant related data such as tenants and versions
* AbpSettings - system setting data
* AbpPermissionGrants - permission authorization data
* AbpFeatures - Functional feature data

## More information

* [ABP Documentation](https://docs.abp.io)
* [ABP Webhooks module](https://github.com/colinin/abp-next-admin/tree/master/aspnet-core/modules/webhooks-management)
