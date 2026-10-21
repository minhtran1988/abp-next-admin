# LINGYUN.Abp.AuditLogging.EntityFrameworkCore

The audit module is implemented by EntityFrameworkCore. This module is only used as a bridge. The specific implementation is left to the abp official module.

AuditLogManager implements IAuditLogManager, and audit logs are managed by the Volo.Abp.AuditLogging module
SecurityLogManager implements ISecurityLogManager, and security logs are managed by the Volo.Abp.Identity module

## Module reference

```csharp
[DependsOn(typeof(AbpAuditLoggingEntityFrameworkCoreModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

Please follow the configuration in Volo.Abp.AuditLogging, Volo.Abp.Identity modules

## appsettings.json

```json
{
  "ConnectionStrings": {
    "AbpIdentity": "Server=127.0.0.1;Database=Identity;User Id=root;Password=*",
    "AbpAuditLogging": "Server=127.0.0.1;Database=AuditLogging;User Id=root;Password=*",
  }
}

```