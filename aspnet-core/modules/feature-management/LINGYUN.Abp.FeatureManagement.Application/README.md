# LINGYUN.Abp.FeatureManagement.Application

The function management application service module provides the implementation of function-defined management services.

## Features

* Function definition management
  * Support creating, updating, and deleting function definitions
  * Support localization of function definitions
  * Support value type serialization defined by functions
* Functional group definition management
  * Support creating, updating, and deleting function group definitions
  * Support localization of function groups
* Support static and dynamic function definition storage
* Integrated ABP function management module

## Module dependencies

```csharp
[DependsOn(
    typeof(AbpFeatureManagementApplicationContractsModule),
    typeof(VoloAbpFeatureManagementApplicationModule))]
public class AbpFeatureManagementApplicationModule : AbpModule
{
}
```

## Permission definition

* FeatureManagement.GroupDefinitions
  * FeatureManagement.GroupDefinitions.Create
  * FeatureManagement.GroupDefinitions.Update
  * FeatureManagement.GroupDefinitions.Delete
* FeatureManagement.Definitions
  * FeatureManagement.Definitions.Create
  * FeatureManagement.Definitions.Update
  * FeatureManagement.Definitions.Delete

## More information

* [ABP Function Management Document](https://docs.abp.io/en/abp/latest/Features)
* [Best Practices for Feature Management](https://docs.abp.io/en/abp/latest/Best-Practices/Features)

[English](README.EN.md)
