# LINGYUN.Abp.MicroService.LocalizationService.DbMigrator

The localization management database migration console application is used to perform database migration and initialize seed data of the localization management system.

[English](./README.EN.md)

## Features

* Automatically perform database migration
* Initialize the necessary seed data for the system
* Support command line parameter configuration
* Integrate Autofac dependency injection container
* Inherit all migration functions of the localization management system
*Support multi-language resource management
* Support dynamic language text management
*Support language management

## Module dependencies

```csharp
[DependsOn(
    typeof(LocalizationManagementMigrationsEntityFrameworkCoreModule),
    typeof(AbpAutofacModule)
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

## Seed data

*Default language configuration
*Basic localization resources
* System text resources
*Default permission configuration

## More information

* [ABP Documentation](https://docs.abp.io)
* [ABP Localization Document](https://docs.abp.io/zh-Hans/abp/latest/Localization)
