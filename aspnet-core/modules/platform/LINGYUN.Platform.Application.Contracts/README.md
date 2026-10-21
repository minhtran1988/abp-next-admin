# LINGYUN.Platform.Application.Contracts

The application service contract layer of the platform management module defines application service interfaces, DTO objects and permission definitions.

## Features

* Menu management interface
  * Menu CRUD operation
  * User menu management
  * Role menu management
  * Menu collection function

* Package management interface
  * Includes CRUD operations
  * Package version management
  * Package file upload and download

* Permission definition
  * Platform management authority group
  * Data dictionary permissions
  * Menu management permissions
  * Package management permissions

## Module reference

```csharp
[DependsOn(typeof(PlatformApplicationContractsModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Application service interface

* `IMenuAppService`: menu management service interface
* `IUserFavoriteMenuAppService`: User favorite menu service interface
* `IPackageAppService`: Package management service interface

## Data transfer object

* Menu related DTO
  * `MenuDto`: menu DTO
  * `MenuCreateDto`: Create menu DTO
  * `MenuUpdateDto`: Update menu DTO
  * `MenuItemDto`: menu item DTO
  * `UserFavoriteMenuDto`: User favorite menu DTO

* DTO related to package management
  * `PackageDto`: package DTO
  * `PackageCreateDto`: Create package DTO
  * `PackageUpdateDto`: update package DTO
  * `PackageBlobDto`: package file DTO

## Permission definition

```json
{
  "Platform": {
    "Default": "Platform Management",
    "DataDictionary": {
      "Default": "Data dictionary management",
      "Create": "Create",
      "Update": "Update",
      "Delete": "Delete"
    },
    "Menu": {
      "Default": "Menu Management",
      "Create": "Create",
      "Update": "Update",
      "Delete": "Delete"
    },
    "Package": {
      "Default": "Package Management",
      "Create": "Create",
      "Update": "Update",
      "Delete": "Delete"
    }
  }
}
```

## More

For more information please refer to [Platform](../README.md)
