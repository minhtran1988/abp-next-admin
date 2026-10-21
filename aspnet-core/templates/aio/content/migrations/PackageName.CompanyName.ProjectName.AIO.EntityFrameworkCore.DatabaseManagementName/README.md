# MySQL Database Migration Guide

This guide will help you use migration scripts to manage MySQL database migration operations.

## Preconditions

1. Make sure .NET Core SDK is installed
2. Make sure the Entity Framework Core tools are installed
   ```powershell
   dotnet tool install --global dotnet-ef
   ```
3. Make sure the MySQL connection string is configured correctly

## Instructions for use

### 1. Create a new migration

1. Run the migration script in the `aspnet-core/migrations` directory:
   ```powershell
   # Use Chinese version
   .\Migrate.ps1
   
   # Or use the English version
   .\MigrateEn.ps1
   ```

2. Select the MySQL database context in the menu:
   ```
   [1] PackageName.CompanyName.ProjectName.AIO.EntityFrameworkCore.MySql
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
aspnet-core/InitSql/PackageName.CompanyName.ProjectName.AIO.EntityFrameworkCore.MySql/
```

### 3. Application migration

The generated SQL script can be applied to the database in the following ways:

1. Use MySQL client tools to directly execute SQL scripts
2. Or use the command line:
   ```powershell
   mysql -u your_username -p your_database < your_script.sql
   ```
