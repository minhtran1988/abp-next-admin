# LINGYUN.Abp.MicroService.AuthServer.EntityFrameworkCore

The authentication server database migration module provides the database migration functions required by the authentication server.

[English](./README.EN.md)

## Features

* Database migration integrating OpenIddict authentication framework
* Integrate database migration of ABP Identity module
* Integrate database migration of ABP permission management module
* Integrate database migration of ABP settings management module
* Integrate database migration of ABP feature management module
* Integrate database migration of ABP SaaS multi-tenant module
* Integrated database migration of text template module
* Support MySQL database

## Module reference

```csharp
[DependsOn(
    typeof(AbpAuthorizationModule),
    typeof(AbpSaasEntityFrameworkCoreModule),
    typeof(AbpOpenIddictEntityFrameworkCoreModule),
    typeof(AbpIdentityEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule),
    typeof(AbpTextTemplatingEntityFrameworkCoreModule),
    typeof(AbpDataDbMigratorModule)
)]
public class YourModule : AbpModule
{
    // other
}
```

## Configuration items

```json
{
  "ConnectionStrings": {
    "AuthServerDbMigrator": "Your database connection string"
  }
}
```

## Basic usage

1. Configure database connection string
   * Configure the AuthServerDbMigrator connection string in appsettings.json

2. Add database context
   ```csharp
   public override void ConfigureServices(ServiceConfigurationContext context)
   {
       context.Services.AddAbpDbContext<AuthServerMigrationsDbContext>();

       Configure<AbpDbContextOptions>(options =>
       {
           options.UseMySQL();
       });
   }
   ```

3. Perform database migration
   * Use the EF Core command line tool to perform migrations
   ```bash
   dotnet ef database update
   ```

## Notes

* Make sure the database connection string contains the correct permissions to be able to create and modify database tables
* Before performing migration, it is recommended to back up the database first
* The migration script will automatically handle dependencies between modules
