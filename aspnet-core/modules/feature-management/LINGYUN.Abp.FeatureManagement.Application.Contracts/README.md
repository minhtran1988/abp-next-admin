# LINGYUN.Abp.FeatureManagement.Application.Contracts

The function management application service contract module defines the interfaces, DTOs and permissions required for function management.

## Features

* Function definition management interface
  * IFeatureDefinitionAppService
  * Support function-defined CRUD operations
* Functional group defines management interface
  * IFeatureGroupDefinitionAppService
  * Support CRUD operations defined by functional groups
* Complete DTO definition
  * FeatureDefinitionDto
  * FeatureGroupDefinitionDto
  * Create, update and query DTO
* Permission definition
  * Function definition management rights
  * Functional group definition management rights

## Module dependencies

```csharp
[DependsOn(
    typeof(AbpFeatureManagementDomainSharedModule),
    typeof(VoloAbpFeatureManagementApplicationContractsModule))]
public class AbpFeatureManagementApplicationContractsModule : AbpModule
{
}
```

## Permission constants

```csharp
public static class FeatureManagementPermissionNames
{
    public const string GroupName = "FeatureManagement";

    public static class GroupDefinition
    {
        public const string Default = GroupName + ".GroupDefinitions";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static class Definition
    {
        public const string Default = GroupName + ".Definitions";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }
}
```

## Error code

* Error:100001 - Function definition already exists
* Error:100002 - Function group definition already exists
* Error:100003 - Unable to delete static function definition
* Error:100004 - Unable to delete static function group definition

## More information

* [ABP Function Management Document](https://docs.abp.io/en/abp/latest/Features)
* [ABP Application Service Documentation](https://docs.abp.io/en/abp/latest/Application-Services)

[English](README.EN.md)
