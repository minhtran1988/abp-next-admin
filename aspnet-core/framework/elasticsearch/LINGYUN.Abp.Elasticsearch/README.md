# LINGYUN.Abp.Elasticsearch

Simplified Chinese | [English](./README.EN.md)

Abp Elasticsearch integration provides a globally unique IElasticClient access interface

## Module reference

```csharp
[DependsOn(typeof(AbpElasticsearchModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Interface definition

* [IIndexMappingProvider](./LINGYUN/Abp/Elasticsearch/IIndexMappingProvider.cs) Get index mapping properties
* [IExpressionQueryTranslator](./LINGYUN/Abp/Elasticsearch/IExpressionQueryTranslator.cs) Expression tree is translated into ES query class
* [IExpressionQueryService](./LINGYUN/Abp/Elasticsearch/IExpressionQueryService.cs) Expression tree data query
* [IElasticsearchClientFactory](./LINGYUN/Abp/Elasticsearch/IElasticsearchClientFactory.cs) ES client management

## Configuration items

* AbpElasticsearchOptions.FieldCamelCase Whether the field adopts camelCase format, the default is false
* AbpElasticsearchOptions.NodeUris ES endpoint, multiple endpoints separated by, or;
* AbpElasticsearchOptions.TypeName document name, default_doc
* AbpElasticsearchOptions.ConnectionLimit maximum number of connections, see NEST documentation for details
* AbpElasticsearchOptions.UserName connection user, see NEST documentation for details
* AbpElasticsearchOptions.Password user password, see NEST documentation for details
* AbpElasticsearchOptions.ConnectionTimeout connection timeout, see NEST documentation for details

## appsettings.json

```json
{
  "Elasticsearch": {
    "NodeUris": "http://localhost:9200"
  }
}