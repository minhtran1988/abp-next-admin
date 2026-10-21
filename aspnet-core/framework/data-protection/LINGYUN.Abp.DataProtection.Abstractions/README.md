# LINGYUN.Abp.DataProtection.Abstractions

The data permission abstract module provides interface definitions and basic types related to data permissions.

## Function

* `IDataProtected` - Data permission interface, marking entities need to control data permissions
* `DataProtectedAttribute` - data permission attribute, marking methods or classes that require data permission control
* `DisableDataProtectedAttribute` - disables the data permission attribute, marking methods or classes without data permission control

## Data operation type

* `DataAccessOperation.Read` - query operation
* `DataAccessOperation.Write` - update operation
* `DataAccessOperation.Delete` - delete operation

## Data filtering

* `DataAccessFilterLogic` - data filtering logic
  * `And` - and
  * `Or` - or
* `DataAccessFilterRule` - data filtering rules
  * `Field` - field name
  * `Value` - field value
  * `Operate` - operator
  * `IsLeft` - whether the left bracket

## Related links

* [English document](./README.EN.md)
