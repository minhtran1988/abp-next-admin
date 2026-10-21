# LINGYUN.Abp.MicroService.AdminService.DbMigrator

The backend management system database migration console application is used to perform database migration and initialize seed data of the backend management system.

[English](./README.EN.md)

## Features

* Automatically perform database migration
* Initialize the necessary seed data for the system
* Support command line parameter configuration
* Integrate Autofac dependency injection container
* Integrated feature management function
* Integrated settings management function
* Integrated rights management function
* Integrated localization management functions
* Integrated cache management function
* Integrated audit log function
* Integrated text template function
* Integrated identity authentication function
* Integrate IdentityServer function
* Integrate OpenIddict function
* Integrated platform management functions
* Integrated object storage function
* Integrated notification system function
* Integrated messaging service function
* Integrated task management function
* Integrated Webhooks management function

## Module dependencies

```csharp
[DependsOn(
    typeof(BackendAdminMigrationsEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementApplicationContractsModule),
    typeof(AbpSettingManagementApplicationContractsModule),
    typeof(AbpPermissionManagementApplicationContractsModule),
    typeof(AbpLocalizationManagementApplicationContractsModule),
    typeof(AbpCachingManagementApplicationContractsModule),
    typeof(AbpAuditingApplicationContractsModule),
    typeof(AbpTextTemplatingApplicationContractsModule),
    typeof(AbpIdentityApplicationContractsModule),
    typeof(AbpIdentityServerApplicationContractsModule),
    typeof(AbpOpenIddictApplicationContractsModule),
    typeof(PlatformApplicationContractModule),
    typeof(AbpOssManagementApplicationContractsModule),
    typeof(AbpNotificationsApplicationContractsModule),
    typeof(AbpMessageServiceApplicationContractsModule),
    typeof(TaskManagementApplicationContractsModule),
    typeof(WebhooksManagementApplicationContractsModule),
    typeof(AbpAutofacModule)
)]
```

## Configuration items

```json
{
  "ConnectionStrings": {
    "BackendAdminDbMigrator": "Your database connection string"
  },
  "IdentityServer": {
    "Clients": {
      "BackendAdmin_App": {
        "ClientId": "BackendAdmin_App"
      }
    }
  }
}
```

## Basic usage

1. Configure database connection string
   * Configure the BackendAdminDbMigrator connection string in appsettings.json

2. Run the migration program
   ```bash
   dotnet run
   ```

## Command line parameters

* --database-provider
  * Specify database provider (default: MySQL)
* --connection-string
  * Specify database connection string
* --skip-db-migrations
  * Skip database migration
* --skip-seed-data
  * Skip seed data initialization

## More information

* [ABP Documentation](https://docs.abp.io)
* [EF Core Migration Document](https://docs.microsoft.com/zh-cn/ef/core/managing-schemas/migrations/)
* [IdentityServer4 Documentation](https://identityserver4.readthedocs.io)
* [OpenIddict Documentation](https://documentation.openiddict.com)
