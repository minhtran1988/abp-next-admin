# LINGYUN.Abp.DataProtectionManagement.Application

The data rights management application service module provides application service implementation of data rights management.

## Function

* Data rights management application service
  * Create data permissions
  * Update data permissions
  * Delete data permission
  * Query data permissions
* Automatic mapping configuration
* Permission verification

## Application service implementation

* `DataProtectionAppService` - Data permission application service
  * Implement the `IDataProtectionAppService` interface
  * Provide CRUD operations for data permissions
  * Contains permission verification
  * Contains data validation

## Automatic mapping configuration

* `DataProtectionManagementApplicationAutoMapperProfile` - automatic mapping configuration file
  * `DataProtection` -> `DataProtectionDto`
  * `DataProtectionCreateDto` -> `DataProtection`
  * `DataProtectionUpdateDto` -> `DataProtection`

## Permission verification

* Creating data permissions requires the `DataProtectionManagement.DataProtection.Create` permission
*Updating data permission requires `DataProtectionManagement.DataProtection.Update` permission
* Delete data permission requires `DataProtectionManagement.DataProtection.Delete` permission
* Querying data permission requires `DataProtectionManagement.DataProtection` permission

## Related links

* [English document](./README.EN.md)
