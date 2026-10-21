# LINGYUN.Abp.FeatureManagement.HttpApi

The function management HTTP API module provides a REST API interface for function definition management.

## Features

* Function definition management API
  * Create, update, delete function definitions
  * Query function definition list
* Functional group definition management API
  * Create, update, and delete function group definitions
  * Query function group definition list
* Support localization
* Integrated ABP function management module

## Module dependencies

```csharp
[DependsOn(
    typeof(AbpFeatureManagementApplicationContractsModule),
    typeof(VoloAbpFeatureManagementHttpApiModule))]
public class AbpFeatureManagementHttpApiModule : AbpModule
{
}
```

## API routing

### Function definition

* GET /api/feature-management/definitions
* GET /api/feature-management/definitions/{name}
* POST /api/feature-management/definitions
* PUT /api/feature-management/definitions/{name}
* DELETE /api/feature-management/definitions/{name}

### Functional group definition

* GET /api/feature-management/group-definitions
* GET /api/feature-management/group-definitions/{name}
* POST /api/feature-management/group-definitions
* PUT /api/feature-management/group-definitions/{name}
* DELETE /api/feature-management/group-definitions/{name}

## Localization configuration

The module uses ABP's localization system and mainly uses the following resources:
* AbpFeatureManagementResource
* AbpValidationResource

## More information

* [ABP Web API Documentation](https://docs.abp.io/en/abp/latest/API/Auto-API-Controllers)
* [ABP Function Management Document](https://docs.abp.io/en/abp/latest/Features)

[English](README.EN.md)
