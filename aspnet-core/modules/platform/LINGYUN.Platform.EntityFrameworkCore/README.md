# LINGYUN.Platform.EntityFrameworkCore

The EntityFrameworkCore implementation of the platform management module provides data access and persistence functions.

## Features

* Implement platform management of all warehousing interfaces
* Supports multiple database providers
* Entity relationship mapping configuration
* Database context definition
* Support query optimization and performance tuning

## Module reference

```csharp
[DependsOn(typeof(PlatformEntityFrameworkCoreModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Warehousing implementation

* `EfCoreUserMenuRepository`: User menu storage implementation
  * Supports obtaining user startup menu
  * Support user menu list query
  * Support user menu permission verification

* `EfCorePackageRepository`: package management repository implementation
  * Support package version query
  * Support package specification filtering
  * Support package details loading

* `EfCoreEnterpriseRepository`: Enterprise warehousing implementation
  * Support tenant related query
  * Support enterprise list paging

## Database context

* `IPlatformDbContext`: platform management database context interface
  * Define DbSet for all entities
  * Support multi-tenant data isolation

## Configuration items

```json
{
  "ConnectionStrings": {
    "Platform": "Server=localhost;Database=Platform;Trusted_Connection=True"
  }
}
```

## More

For more information please refer to [Platform](../README.md)
