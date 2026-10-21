# LINGYUN.Abp.DataProtectionManagement.Domain

The data rights management domain module provides the core business logic of data rights management.

## Function

* Data rights management
  * Create data permissions
  * Update data permissions
  * Delete data permission
  * Query data permissions
* Data permission resource management
  * Resource definition
  * Resource grouping
  * Resource attributes
* Data permission rule management
  * Rule definition
  * Rule grouping
  *Rule operator

## Domain Services

* `IDataProtectionManager` - data rights management service
  * `CreateAsync` - Create data permissions
  * `UpdateAsync` - update data permissions
  * `DeleteAsync` - Delete data permission
  * `GetAsync` - Get data permissions
  * `GetListAsync` - Get the data permission list

## Entity

* `DataProtection` - data permission entity
  * `Id` - primary key
  * `Name` - name
  * `DisplayName` - display name
  * `Description` - description
  * `AllowProperties` - list of allowed properties
  * `FilterGroup` - filtering rule group

## Warehousing

* `IDataProtectionRepository` - data permission warehousing interface
  * `GetListAsync` - Get the data permission list
  * `FindByNameAsync` - Find data permissions by name
  * `GetCountAsync` - Get the number of data permissions

## Related links

* [English document](./README.EN.md)
