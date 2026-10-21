# LINGYUN.Abp.LocalizationManagement.Application

The localized management application service layer implementation module provides application service implementation for localized resource management.

## Features

* Implement language management application services
* Implement resource management application services
* Implement text management application services
* Support AutoMapper object mapping
* Provide standardized CRUD operations

## Module reference

```csharp
[DependsOn(typeof(AbpLocalizationManagementApplicationModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Application Services

* `LanguageAppService`: Language management application service
  - Create languages
  - Update language
  - Delete language
  - Get language list
  - Get language details

* `ResourceAppService`: Resource management application service
  - Create resources
  - Update resources
  - Delete resources
  - Get resource list
  - Get resource details

* `TextAppService`: text management application service
  - Create text
  - Update text
  - Delete text
  - Get text list
  - Get text details

## Permissions

All application services follow the permission requirements defined by the module. For details, see the permission definition of the Domain.Shared module.

## More information

* [English documentation](./README.EN.md)
