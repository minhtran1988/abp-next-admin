# LINGYUN.Abp.MicroService.MessageService.DbMigrator

Real-time messaging database migration console application, used to perform database migration and initialize seed data for real-time messaging systems.

[English](./README.EN.md)

## Features

* Automatically perform database migration
* Initialize the necessary seed data for the system
* Support command line parameter configuration
* Integrate Autofac dependency injection container
* Inherit all migration functions of real-time messaging system
* Support SignalR message management
* Support message group management
* Support message subscription management
* Support message history

## Module dependencies

```csharp
[DependsOn(
    typeof(RealtimeMessageMigrationsEntityFrameworkCoreModule),
    typeof(AbpAutofacModule)
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

*Default message group configuration
*Basic message subscription configuration
* System message template
*Default permission configuration

## More information

* [ABP Documentation](https://docs.abp.io)
* [SignalR Documentation](https://docs.microsoft.com/aspnet/core/signalr/introduction)
