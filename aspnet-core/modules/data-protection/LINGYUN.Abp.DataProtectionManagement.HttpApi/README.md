# LINGYUN.Abp.DataProtectionManagement.HttpApi

The data permission management HTTP API module provides a REST API interface for data permission management.

## Function

* Data rights management REST API
  * Create data permissions
  * Update data permissions
  * Delete data permission
  * Query data permissions

## API Controller

* `DataProtectionController` - data permission controller
  * `GET /api/data-protection-management/data-protection/{id}` - Get the specified data permissions
  * `GET /api/data-protection-management/data-protection` - Get the data permission list
  * `POST /api/data-protection-management/data-protection` - Create data permissions
  * `PUT /api/data-protection-management/data-protection/{id}` - update data permissions
  * `DELETE /api/data-protection-management/data-protection/{id}` - delete data permission

## Permission verification

* All APIs require authentication
* Creating data permissions requires the `DataProtectionManagement.DataProtection.Create` permission
*Updating data permission requires `DataProtectionManagement.DataProtection.Update` permission
* Delete data permission requires `DataProtectionManagement.DataProtection.Delete` permission
* Querying data permission requires `DataProtectionManagement.DataProtection` permission

## Related links

* [English document](./README.EN.md)
