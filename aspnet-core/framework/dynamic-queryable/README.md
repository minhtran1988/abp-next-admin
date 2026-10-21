# Dynamic.Queryable

Dynamic query support library provides dynamic query support for background services

* The application interface definition layer refers to the **LINGYUN.Abp.Dynamic.Queryable.Application.Contracts** module and implements the **IDynamicQueryableAppService** interface to provide external dynamic query support

* The application interface implementation layer refers to the **LINGYUN.Abp.Dynamic.Queryable.Application** module, which inherits from the **DynamicQueryableAppService** interface and implements the **GetCountAsync** and **GetListAsync** methods (provides the passed **Expression<Func<TEntity, bool>>** structure for the warehouse layer interface)

* The controller layer references the **LINGYUN.Abp.Dynamic.Queryable.HttpApi** module, inherits the **DynamicQueryableControllerBase** interface, and automatically exposes the dynamic query interface.

## Interface description

* **GetAvailableFieldsAsync**(<font color="green">[GET]</font> <font color="blue">/{controller}/available-fields</font>): Provides optional attribute details to the outside world, and returns all fields by default (excluding defined excluded fields).

* **GetListAsync**(<font color="orange">[POST]</font> <font color="blue">/{controller}/search</font>): Returns a list of query results based on the dynamic conditions passed by the user.

## Configuration usage

* AbpDynamicQueryableOptions.IgnoreFields: Defines a list of fields that are not open to users. Tenant identification and soft delete filter interface fields are excluded by default.

