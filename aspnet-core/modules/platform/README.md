# LINGYUN.Platform

The platform management module provides a complete set of platform management functions, including menu management, layout management, data dictionary, package management and other functions.

## Features

* Menu management
  * Multi-level menu structure
  * User menu customization
  * Role menu permissions
  * Menu collection function
  * Dynamic menu presets

* Layout management
  * Layout view entity
  * Layout data association
  *Multiple framework support

* Data dictionary
  * Data dictionary management
  * Data dictionary item management
  * Data dictionary seed data

* Package management
  * Package version control
  * Package file management
  * Blob storage integration
  *Packet filtering specifications

* VueVbenAdmin integration
  * Theme settings
  * Layout settings
  * Menu settings
  * Title bar settings
  *Multi-tab settings

## Project structure

* `LINGYUN.Platform.Domain.Shared`: shared domain layer
* `LINGYUN.Platform.Domain`: Domain layer
* `LINGYUN.Platform.EntityFrameworkCore`: data access layer
* `LINGYUN.Platform.Application.Contracts`: Application service contract layer
* `LINGYUN.Platform.Application`: application service implementation layer
* `LINGYUN.Platform.HttpApi`: HTTP API layer
* `LINGYUN.Platform.HttpApi.Client`: HTTP API proxy layer
* `LINGYUN.Platform.Settings.VueVbenAdmin`: VueVbenAdmin front-end framework settings module

## Quick start

1. Reference module
```csharp
[DependsOn(
    typeof(PlatformDomainModule),
    typeof(PlatformApplicationModule),
    typeof(PlatformHttpApiModule),
    typeof(PlatformSettingsVueVbenAdminModule)
)]
public class YouProjectModule : AbpModule
{
    // other
}
```

2. Configure database
```json
{
  "ConnectionStrings": {
    "Platform": "Server=localhost;Database=Platform;Trusted_Connection=True"
  }
}
```

3. Update database
```bash
dotnet ef database update
```

## Notes

1. Dynamic menu management
   * The module has initialized the menu related to vue-admin by default
   * Menu data can be preset through the `IDataSeedContributor` interface
   * Layout (path) and menu (component) do not need to add @/ prefix

2. Database migration
   * Please perform database migration before running platform services
   * Use the `dotnet ef database update` command to update the database structure

## More information

* [Shared Domain Layer](./LINGYUN.Platform.Domain.Shared/README.md)
* [Domain layer](./LINGYUN.Platform.Domain/README.md)
* [Data Access Layer](./LINGYUN.Platform.EntityFrameworkCore/README.md)
* [Application Service Contract Layer](./LINGYUN.Platform.Application.Contracts/README.md)
* [Application service implementation layer](./LINGYUN.Platform.Application/README.md)
* [HTTP API layer](./LINGYUN.Platform.HttpApi/README.md)
* [VueVbenAdmin settings module](./LINGYUN.Platform.Settings.VueVbenAdmin/README.md)
