# LINGYUN.Abp.AspNetCore.Auditing

Audit date extension module, used to add specific HTTP request header records to the audit log

## Module reference


```csharp
[DependsOn(typeof(AbpAspNetCoreAuditingModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

* AbpAspNetCoreAuditingHeaderOptions.IsEnabled Whether to record HTTP request headers in the audit log, default: true
* AbpAspNetCoreAuditingHeaderOptions.HttpHeaders list of Http request headers that need to be recorded in the audit log
