# LINGYUN.Platform.Domain

The domain layer of the platform management module implements the core business logic and domain model required for platform management.

## Features

* Menu management
  * Support multi-level menu structure
  * User menu customization
  * Role menu permissions
  * Menu standardization conversion

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

* Enterprise portal
  * Enterprise information management
  * Enterprise data storage

## Module reference

```csharp
[DependsOn(typeof(PlatformDomainModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Domain Services

* `DataDictionaryDataSeeder`: Data dictionary seed data service
* `PackageBlobManager`: package file management service
* `DefaultStandardMenuConverter`: Standard menu conversion service

## Warehousing interface

* `IMenuRepository`: menu storage interface
* `IUserMenuRepository`: user menu storage interface
* `IRoleMenuRepository`: role menu storage interface
* `ILayoutRepository`: layout storage interface
* `IEnterpriseRepository`: Enterprise warehousing interface

## Entity

* `Menu`: menu entity
* `UserMenu`: User menu entity
* `RoleMenu`: role menu entity
* `Layout`: layout entity
* `Package`: package entity
* `Data`: data dictionary entity
* `DataItem`: data dictionary item entity

## More

For more information please refer to [Platform](../README.md)
