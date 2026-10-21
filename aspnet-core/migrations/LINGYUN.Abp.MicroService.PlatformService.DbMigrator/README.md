# LY.MicroService.Platform.DbMigrator

The platform management database migration console application is used to perform database migration and initialize seed data of the platform management system.

[English](./README.EN.md)

## Features

* Automatically perform database migration
* Initialize the necessary seed data for the system
* Support command line parameter configuration
* Integrate Autofac dependency injection container
* Inherit all migration functions of the platform management system
* Support data dictionary management
* Support organizational management
*Support menu management
*Support version management
* Support Vue Vben Admin UI navigation

## Module dependencies

```csharp
[DependsOn(
    typeof(PlatformMigrationsEntityFrameworkCoreModule),
    typeof(AbpAutofacModule)
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

*Default data dictionary
*Basic organizational structure
* System menu
*Default version configuration
* UI navigation configuration

## More information

* [ABP Documentation](https://docs.abp.io)
* [Vue Vben Admin Document](https://doc.vvbin.cn/)
