# LINGYUN.Abp.MicroService.AuthServer.DbMigrator

Authentication server database migration console application, used to perform database migration and initialize seed data of the authentication server.

[English](./README.EN.md)

## Features

* Automatically perform database migration
* Initialize the necessary seed data for the system
* Support command line parameter configuration
* Integrate Autofac dependency injection container
* Inherit all migration functions of the authentication server

## Module dependencies

```csharp
[DependsOn(
    typeof(AuthServerMigrationsEntityFrameworkCoreModule),
    typeof(AbpAutofacModule)
)]
```

## Configuration items

```json
{
  "ConnectionStrings": {
    "AuthServerDbMigrator": "Your database connection string"
  },
  "IdentityServer": {
    "Clients": {
      "AuthServer_App": {
        "ClientId": "AuthServer_App"
      }
    }
  }
}
```

## Basic usage

1. Configure database connection string
   * Configure the AuthServerDbMigrator connection string in appsettings.json

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
