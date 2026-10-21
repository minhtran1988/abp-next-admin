# LINGYUN.Abp.MicroService.AllInOne.DbMigrator

Single application database migration tool, used to automatically perform database migration and initialize data.

[English](./README.EN.md)

## Features

* Automatically perform database migration
*Support multi-environment configuration
* Integrated Serilog logging
* Support data migration environment configuration
* Automatically check and apply database migrations
* Console application for easy integration into CI/CD processes

## Configuration items

```json
{
  "ConnectionStrings": {
    "Default": "Your database connection string"
  },
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "Volo.Abp": "Warning"
      }
    }
  }
}
```

## Basic usage

1. Configure database connection
   * Configure the database connection string in appsettings.json
   * You can configure the connection strings for different environments through appsettings.{Environment}.json

2. Run the migration tool
   ```bash
   dotnet run
   ```

3. View the migration log
   *Console output
   * Logs/migrations.txt file

## Environment variables

* `ASPNETCORE_ENVIRONMENT`: Set the running environment (Development, Staging, Production, etc.)
* `DOTNET_ENVIRONMENT`: Same as above, for compatibility

## Notes

* Make sure the database connection string contains sufficient permissions
* It is recommended to back up the database before performing migration
* Check the migrations.txt log file for migration details
* If the migration fails, check the log for error messages

## Development and debugging

1. Set environment variables
   ```bash
   export ASPNETCORE_ENVIRONMENT=Development
   ```

2. Debugging using Visual Studio or Visual Studio Code
   * Can set breakpoints
   * View detailed migration process

3. Customize migration logic
   * Modify SingleDbMigrationService class
   * Add new data seeds
