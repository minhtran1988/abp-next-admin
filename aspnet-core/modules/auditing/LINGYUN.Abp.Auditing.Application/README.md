# LINGYUN.Abp.Auditing.Application

The audit log application layer module provides the application service implementation of audit logs.

[English](./README.EN.md)

## Features

* Audit log query service
* Audit log management service
* Security log query service
* Integrated automatic mapping function

## Module reference

```csharp
[DependsOn(typeof(AbpAuditingApplicationModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Dependent modules

* `LINGYUN.Abp.AuditLogging` - audit log core module
* `LINGYUN.Abp.Logging` - basic log module
* `AbpAutoMapper` - ABP automatic mapping module

## Service interface

* IAuditLogAppService - audit log application service
  - GetAsync - Get the specified audit log
  - GetListAsync - Get the audit log list
  - DeleteAsync - delete the specified audit log
  - DeleteManyAsync - delete audit logs in batches

* ISecurityLogAppService - Security log application service
  - GetAsync - Get the specified security log
  - GetListAsync - Get the security log list
  - DeleteAsync - Delete the specified security log
  - DeleteManyAsync - Delete security logs in batches

## Basic usage

1. Reference module
2. Inject the required application services
3. Call the corresponding service method

Example:
```csharp
public class YourService
{
    private readonly IAuditLogAppService _auditLogAppService;

    public YourService(IAuditLogAppService auditLogAppService)
    {
        _auditLogAppService = auditLogAppService;
    }

    public async Task<AuditLogDto> GetAuditLogAsync(Guid id)
    {
        return await _auditLogAppService.GetAsync(id);
    }
}
```
