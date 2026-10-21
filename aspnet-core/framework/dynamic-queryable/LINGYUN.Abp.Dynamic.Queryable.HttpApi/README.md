# LINGYUN.Abp.Dynamic.Queryable.HttpApi

The dynamic query HTTP API module provides dynamic query HTTP API implementation based on the ABP framework.

## Features

* Provide dynamic query controller base class `DynamicQueryableControllerBase<TEntity, TEntityDto>`
* Automatically generate REST API endpoints
* Support HTTP transmission of dynamic query parameters

## Configuration usage

1. Install the `LINGYUN.Abp.Dynamic.Queryable.HttpApi` NuGet package

2. Add `[DependsOn(typeof(AbpDynamicQueryableHttpApiModule))]` to your module class

### Implement dynamic query controller

```csharp
[Route("api/my-entity")]
public class MyEntityController : DynamicQueryableControllerBase<MyEntity, MyEntityDto>
{
    public MyEntityController(IDynamicQueryableAppService<MyEntityDto> dynamicQueryableAppService)
        : base(dynamicQueryableAppService)
    {
    }
}
```

### API endpoint

* GET `/api/my-entity/available-fields` - Get the list of available fields
* POST `/api/my-entity/search` - Query data based on dynamic conditions

### Query example

```json
POST /api/my-entity/search
{
    "maxResultCount": 10,
    "skipCount": 0,
    "queryable": {
        "paramters": [
            {
                "field": "Name",
                "comparison": "Equal",
                "value": "test"
            }
        ]
    }
}
```

## Related links

* [LINGYUN.Linq.Dynamic.Queryable](../LINGYUN.Linq.Dynamic.Queryable/README.md)
* [LINGYUN.Abp.Dynamic.Queryable.Application](../LINGYUN.Abp.Dynamic.Queryable.Application/README.md)
* [LINGYUN.Abp.Dynamic.Queryable.Application.Contracts](../LINGYUN.Abp.Dynamic.Queryable.Application.Contracts/README.md)
