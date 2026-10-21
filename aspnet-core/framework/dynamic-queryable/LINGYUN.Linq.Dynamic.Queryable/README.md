# LINGYUN.Linq.Dynamic.Queryable

Dynamically query basic libraries, extend Linq, and dynamically build expression trees

## Features

* Support dynamic construction of query conditions
* Supports multiple comparison operators: equal to, not equal to, greater than, less than, greater than or equal to, less than or equal to, contains, does not contain, starts with, does not start with, ends with, does not end with, etc.
* Support dynamic type conversion and null value processing
* Support dynamic query of enumeration types

## Configuration usage

Modules are referenced on demand and only extensions for Linq are provided.

### Basic usage

```csharp
//Create dynamic query parameters
var parameter = new DynamicParamter 
{
    Field = "Name",
    Comparison = DynamicComparison.Equal,
    Value = "test"
};

//Create query conditions
var queryable = new DynamicQueryable
{
    Paramters = new List<DynamicParamter> { parameter }
};

//Apply to Expression
Expression<Func<TEntity, bool>> condition = e => true;
condition = condition.DynamicQuery(queryable);

// Query using conditions
var result = await repository.Where(condition).ToListAsync();
```

### Supported comparison operators

* `Equal` - equal to
* `NotEqual` - not equal to
* `LessThan` - less than
* `LessThanOrEqual` - less than or equal to
* `GreaterThan` - greater than
* `GreaterThanOrEqual` - greater than or equal to
* `Contains` - contains
* `NotContains` - does not contain
* `StartsWith` - starts with
* `NotStartsWith` - does not start with
* `EndsWith` - ends with
* `NotEndsWith` - does not end with

## Related links

* [LINGYUN.Abp.Dynamic.Queryable.Application](../LINGYUN.Abp.Dynamic.Queryable.Application/README.md)
