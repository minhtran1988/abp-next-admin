# LINGYUN.Abp.Dynamic.Queryable.Application

The dynamic query application service module provides dynamic query function implementation based on the ABP framework.

## Features

* Provide dynamic query application service base class `DynamicQueryableAppService<TEntity, TEntityDto>`
* Automatically generate a list of available fields and support field localization
* Support field filtering and ignoring
* Supports option list generation of enumeration types

## Configuration usage

1. Install the `LINGYUN.Abp.Dynamic.Queryable.Application` NuGet package

2. Add `[DependsOn(typeof(AbpDynamicQueryableApplicationModule))]` to your module class

3. Configuration options (optional)

```csharp
Configure<AbpDynamicQueryableOptions>(options =>
{
    //Add fields that need to be ignored
    options.IgnoreFields.Add("FieldName");
});
```

### Implement dynamic query service

```csharp
public class MyEntityAppService : DynamicQueryableAppService<MyEntity, MyEntityDto>
{
    private readonly IRepository<MyEntity> _repository;

    public MyEntityAppService(IRepository<MyEntity> repository)
    {
        _repository = repository;
    }

    protected override async Task<int> GetCountAsync(Expression<Func<MyEntity, bool>> condition)
    {
        return await _repository.CountAsync(condition);
    }

    protected override async Task<List<MyEntity>> GetListAsync(
        Expression<Func<MyEntity, bool>> condition,
        GetListByDynamicQueryableInput dynamicInput)
    {
        return await _repository
            .Where(condition)
            .PageBy(dynamicInput.SkipCount, dynamicInput.MaxResultCount)
            .ToListAsync();
    }

    protected override List<TEntityDto> MapToEntitiesDto(List<MyEntity> entities)
    {
        return ObjectMapper.Map<List<MyEntity>, List<MyEntityDto>>(entities);
    }

    // Customize fields to be ignored (optional)
    protected override IEnumerable<string> GetUserDefineIgnoreFields()
    {
        return new[] { "CustomField" };
    }
}
```

### Configuration item description

* `IgnoreFields` - list of fields to be ignored in the query
  * Default includes: Id, TenantId, IsDeleted, DeleterId, DeletionTime and other audit fields

## Related links

* [LINGYUN.Linq.Dynamic.Queryable](../LINGYUN.Linq.Dynamic.Queryable/README.md)
* [LINGYUN.Abp.Dynamic.Queryable.Application.Contracts](../LINGYUN.Abp.Dynamic.Queryable.Application.Contracts/README.md)
