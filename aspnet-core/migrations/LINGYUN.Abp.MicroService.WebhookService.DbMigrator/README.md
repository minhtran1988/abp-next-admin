# LINGYUN.Abp.MicroService.WebhookService.DbMigrator

The Webhooks management database migration console application is used to perform database migration and initialize seed data of the Webhooks management system.

[English](./README.EN.md)

## Features

* Automatically perform database migration
* Initialize the necessary seed data for the system
* Support command line parameter configuration
* Integrate Autofac dependency injection container
* Inherit all migration functions of Webhooks management system

## Module dependencies

```csharp
[DependsOn(
    typeof(WebhooksManagementMigrationsEntityFrameworkCoreModule),
    typeof(AbpAutofacModule)
)]
```

## Configuration items

```json
{
  "ConnectionStrings": {
    "WebhooksManagementDbMigrator": "Your database connection string"
  },
  "IdentityServer": {
    "Clients": {
      "WebhooksManagement_App": {
        "ClientId": "WebhooksManagement_App"
      }
    }
  }
}
```

## Basic usage

1. Configure database connection string
   * Configure the WebhooksManagementDbMigrator connection string in appsettings.json

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
