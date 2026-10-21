# LINGYUN.Abp.DataProtectionManagement.Application.Contracts

The data rights management application service contract module provides application service interfaces and DTOs for data rights management.

## Function

* Data rights management application service interface
* Data permission DTO definition
* Data permission query definition

## Application service interface

* `IDataProtectionAppService` - Data permission application service interface
  * `GetAsync` - Get data permissions
  * `GetListAsync` - Get the data permission list
  * `CreateAsync` - Create data permissions
  * `UpdateAsync` - update data permissions
  * `DeleteAsync` - Delete data permission

## DTO definition

* `DataProtectionDto` - data permission DTO
  * `Id` - primary key
  * `Name` - name
  * `DisplayName` - display name
  * `Description` - description
  * `AllowProperties` - list of allowed properties
  * `FilterGroup` - filtering rule group

* `DataProtectionCreateDto` - Create data permission DTO
* `DataProtectionUpdateDto` - update data permission DTO
* `DataProtectionGetListInput` - Get data permission list input DTO

## Related links

* [English document](./README.EN.md)
