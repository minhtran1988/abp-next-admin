# LINGYUN.Abp.Dynamic.Queryable.Application.Contracts

The dynamic query application service contract module defines dynamic query-related interfaces and DTOs.

## Features

* Define dynamic query application service interface `IDynamicQueryableAppService<TEntityDto>`
* Provide DTO definitions related to dynamic query
* Support the definition of parameter options and comparison operators

## Configuration usage

1. Install the `LINGYUN.Abp.Dynamic.Queryable.Application.Contracts` NuGet package

2. Add `[DependsOn(typeof(AbpDynamicQueryableApplicationContractsModule))]` to your module class

### Interface description

```csharp
public interface IDynamicQueryableAppService<TEntityDto>
{
    // Get the list of available fields
    Task<ListResultDto<DynamicParamterDto>> GetAvailableFieldsAsync();

    //Query data based on dynamic conditions
    Task<PagedResultDto<TEntityDto>> SearchAsync(GetListByDynamicQueryableInput dynamicInput);
}
```

### DTO Description

* `DynamicParamterDto` - dynamic parameter DTO
  * `Name` - field name
  * `Type` - field type
  * `Description` - field description
  * `JavaScriptType` - JavaScript type
  * `AvailableComparator` - available comparison operators
  * `Options` - parameter options (for enumeration types)

* `ParamterOptionDto` - parameter option DTO
  * `Key` - option key
  * `Value` - option value

* `GetListByDynamicQueryableInput` - dynamic query input DTO
  * `SkipCount` - number of skipped records
  * `MaxResultCount` - the maximum number of records returned
  * `Queryable` - query conditions

## Related links

* [LINGYUN.Linq.Dynamic.Queryable](../LINGYUN.Linq.Dynamic.Queryable/README.md)
* [LINGYUN.Abp.Dynamic.Queryable.Application](../LINGYUN.Abp.Dynamic.Queryable.Application/README.md)
