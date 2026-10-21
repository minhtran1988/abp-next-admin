# LINGYUN.Abp.MicroService.AllInOne.EntityFrameworkCore

The single application data migration module provides complete application database migration functions.

[English](./README.EN.md)

## Features

* Integrated audit log data migration
* Integrated settings to manage data migration
* Integrated rights management data migration
* Integrated features to manage data migration
* Integrated notification system data migration
* Integrated messaging service data migration
* Integrated platform management data migration
* Integrate localized management data migration
* Integrated identity authentication data migration
* Integrate IdentityServer data migration
* Integrate OpenIddict data migration
* Integrated text template data migration
* Integrate Webhooks to manage data migration
* Integrated task management data migration
* Integrated SaaS multi-tenant data migration
* Support database migration event processing
* Provide database migration services

## Module dependencies

```csharp
[DependsOn(
    typeof(AbpSaasEntityFrameworkCoreModule),
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule),
    typeof(AbpNotificationsEntityFrameworkCoreModule),
    typeof(AbpMessageServiceEntityFrameworkCoreModule),
    typeof(PlatformEntityFrameworkCoreModule),
    typeof(AbpLocalizationManagementEntityFrameworkCoreModule),
    typeof(AbpIdentityEntityFrameworkCoreModule),
    typeof(AbpIdentityServerEntityFrameworkCoreModule),
    typeof(AbpOpenIddictEntityFrameworkCoreModule),
    typeof(AbpTextTemplatingEntityFrameworkCoreModule),
    typeof(WebhooksManagementEntityFrameworkCoreModule),
    typeof(TaskManagementEntityFrameworkCoreModule),
    typeof(AbpWeChatModule),
    typeof(AbpDataDbMigratorModule)
)]
```

## Configuration items

```json
{
  "ConnectionStrings": {
    "SingleDbMigrator": "Your database connection string"
  }
}
```

## Basic usage

1. Configure database connection string
   * Configure the SingleDbMigrator connection string in appsettings.json

2. Add module dependencies
   ```csharp
   [DependsOn(typeof(SingleMigrationsEntityFrameworkCoreModule))]
   public class YourModule : AbpModule
   {
       // ...
   }
   ```

## Database table description

* AbpAuditLogs - audit log data
* Identity related table - user, role, statement and other identity authentication related data
* IdentityServer related table - OAuth/OpenID Connect related data such as client, API resources, identity resources, etc.
* OpenIddict related table - OpenID Connect authentication related data
* AbpPermissionGrants - permission authorization data
* AbpSettings - system setting data
* AbpFeatures - Functional feature data
* AbpNotifications - Notification system data
* AbpMessageService - message service data
* Platform related table - platform management related data
* AbpLocalization - localization management data
* AbpTextTemplates - text template data
*AbpWebhooks - Webhooks management data
* AbpTasks - task management data
* Saas related tables - multi-tenant related data such as tenants and versions

## More information

* [ABP Documentation](https://docs.abp.io)
* [OpenIddict Documentation](https://documentation.openiddict.com)
* [IdentityServer4 Documentation](https://identityserver4.readthedocs.io)
