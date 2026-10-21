# LINGYUN.Abp.Elasticsearch.EsqlQuery

ES|QL query integration for [IExpressionQueryService](../LINGYUN.Abp.Elasticsearch/LINGYUN/Abp/Elasticsearch/IExpressionQueryService.cs).

**Note**:
* Deep paging cannot be performed using the ES|QL query plan. When the data entries exceed 10,000, there are still ES paging restrictions.
If you need to support deep paging, please use the default [ExpressionQueryService](../LINGYUN.Abp.Elasticsearch/LINGYUN/Abp/Elasticsearch/ExpressionQueryService.cs) `search_after` scheme

* Not supported. Since ES|QL does not support `Skip`, paging query is not supported yet.

* For collection methods of nested types that fail to pass the test, please do not use the filter conditions of the collection.


## Module reference

```csharp
[DependsOn(typeof(AbpElasticsearchEsqlQueryModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```
