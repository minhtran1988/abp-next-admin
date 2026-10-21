# LINGYUN.Abp.AuditLogging.Elasticsearch

Simplified Chinese | [English](./README.EN.md)

Audit module Elasticsearch implementation

ElasticsearchAuditLogManager implements IAuditLogManager, and the audit log is managed by ES
ElasticsearchSecurityLogManager implements ISecurityLogManager, and security logs are managed by ES

## Module reference


```csharp
[DependsOn(typeof(AbpAuditLoggingElasticsearchModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

* AbpAuditLoggingElasticsearchOptions.IndexPrefix index prefix, default auditlogging

## Notes

Integrated with the tenant module, the index will be switched when crossing tenants

## appsettings.json

```json
{
  "AuditLogging": {
    "Elasticsearch": {
      "IndexPrefix": "auditlogging"
    }
  }
}