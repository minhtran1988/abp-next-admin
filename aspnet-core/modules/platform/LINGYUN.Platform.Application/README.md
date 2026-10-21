# LINGYUN.Platform.Application

The application service implementation layer of the platform management module implements all functions defined by the application service interface.

## Features

* User favorite menu service
  * Create favorite menu
  * Update collection menu
  * Delete favorite menu
  * Query favorite menu list
  * Manage other users' favorite menus

* Object mapping configuration
  * Automatic mapping of entities to DTO
  * Support custom mapping rules
  * Support additional attribute mapping

* Permission verification
  * Policy-based permission verification
  * Integrated ABP authorization system
  * Fine-grained permission control

## Module reference

```csharp
[DependsOn(typeof(PlatformApplicationModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Application service implementation

* `UserFavoriteMenuAppService`: Implementation of user favorite menu service
  * Support user-defined menu icons
  * Support user-defined menu color
  * Support user-defined menu aliases
  * Support multi-frame menu management

## Object mapping

```csharp
public class PlatformApplicationMappingProfile : Profile
{
    public PlatformApplicationMappingProfile()
    {
        CreateMap<PackageBlob, PackageBlobDto>();
        CreateMap<Package, PackageDto>();
        CreateMap<DataItem, DataItemDto>();
        CreateMap<Data, DataDto>();
        CreateMap<Menu, MenuDto>();
        CreateMap<Layout, LayoutDto>();
        CreateMap<UserFavoriteMenu, UserFavoriteMenuDto>();
    }
}
```

## Basic services

* `PlatformApplicationServiceBase`: Platform management application service base class
  * Provide common functions and help methods
  * Unified exception handling
  * Unified permission verification

## More

For more information please refer to [Platform](../README.md)
