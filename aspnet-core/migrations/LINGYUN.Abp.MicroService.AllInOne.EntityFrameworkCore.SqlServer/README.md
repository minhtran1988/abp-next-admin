#SQL Server Database Migration Guide

This guide will help you use migration scripts to manage SQL Server database migration operations.

## Preconditions

1. Make sure .NET Core SDK is installed
2. Make sure the Entity Framework Core tools are installed
   ```powershell
   dotnet tool install --global dotnet-ef
   ```
3. Make sure the SQL Server connection string is configured correctly

## Instructions for use

### 1. Create a new migration

1. Run the migration script in the `aspnet-core/migrations` directory:
   ```powershell
   # Use Chinese version
   .\Migrate.ps1
   
   # Or use the English version
   .\MigrateEn.ps1
   ```

2. Select the SQL Server database context in the menu:
   ```
   [3] LINGYUN.Abp.MicroService.AllInOne.EntityFrameworkCore.SqlServer
   ```

3. Enter a migration name (optional):
   - Enter directly and the default name will be used: `AddNewMigration_yyyyMMdd_HHmmss`
   - Or enter a custom name, such as: `AddNewFeature`

### 2. Generate SQL script

After creating the migration, the script asks if it needs to generate a SQL script:

1. Select whether to generate SQL script (Y/N)
2. If you select Y, the following options will be available:
   - `[A]` - Generate SQL scripts for all migrations
   - `[L]` - Generate SQL script for latest migration only
   - `[0-9]` - start building from the specified migration version

The generated SQL script will be saved in:
```
aspnet-core/InitSql/LINGYUN.Abp.MicroService.AllInOne.EntityFrameworkCore.SqlServer/
```

### 3. Application migration

The generated SQL script can be applied to the database in the following ways:

1. Use SQL Server Management Studio or other SQL Server client tools to execute the SQL script
2. Or use the command line:
   ```bash
   sqlcmd -S your_server -d your_database -i your_script.sql
   ```
