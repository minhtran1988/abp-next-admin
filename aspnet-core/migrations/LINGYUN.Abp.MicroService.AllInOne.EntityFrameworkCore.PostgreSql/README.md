# PostgreSQL Database Migration Guide

This guide will help you use migration scripts to manage PostgreSQL database migration operations.

## Preconditions

1. Make sure .NET Core SDK is installed
2. Make sure the Entity Framework Core tools are installed
   ```powershell
   dotnet tool install --global dotnet-ef
   ```
3. Make sure the PostgreSQL connection string is configured correctly

## Instructions for use

### 1. Create a new migration

1. Run the migration script in the `aspnet-core/migrations` directory:
   ```powershell
   # Use Chinese version
   .\Migrate.ps1
   
   # Or use the English version
   .\MigrateEn.ps1
   ```

2. Select the PostgreSQL database context in the menu:
   ```
   [2] LINGYUN.Abp.MicroService.AllInOne.EntityFrameworkCore.PostgreSql
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
aspnet-core/InitSql/LINGYUN.Abp.MicroService.AllInOne.EntityFrameworkCore.PostgreSql/
```

### 3. Application migration

The generated SQL script can be applied to the database in the following ways:

1. Use pgAdmin or other PostgreSQL client tools to execute SQL scripts
2. Or use the command line:
   ```bash
   psql -U your_username -d your_database -f your_script.sql
   ```
