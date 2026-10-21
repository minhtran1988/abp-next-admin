# LINGYUN.Abp.Auditing.Application.Contracts

The audit log application layer contract module defines the application service interface and data transmission object of the audit log.

[English](./README.EN.md)

## Features

* Audit log application service interface definition
* Audit log data transfer object (DTO) definition
* Audit log permission definition
* Audit log query parameter definition

## Module reference

```csharp
[DependsOn(typeof(AbpAuditingApplicationContractsModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Permission definition

* AuditLogging.AuditLogs - Audit log management
  - AuditLogging.AuditLogs.Delete - Delete audit logs
  - AuditLogging.SecurityLogs - Security log management
  - AuditLogging.SecurityLogs.Delete - Delete security logs

## Service interface

* IAuditLogAppService
  ```csharp
  public interface IAuditLogAppService : IApplicationService
  {
      Task<AuditLogDto> GetAsync(Guid id);
      Task<PagedResultDto<AuditLogDto>> GetListAsync(GetAuditLogsInput input);
      Task DeleteAsync(Guid id);
      Task DeleteManyAsync(DeleteManyAuditLogsInput input);
  }
  ```

* ISecurityLogAppService
  ```csharp
  public interface ISecurityLogAppService : IApplicationService
  {
      Task<SecurityLogDto> GetAsync(Guid id);
      Task<PagedResultDto<SecurityLogDto>> GetListAsync(GetSecurityLogsInput input);
      Task DeleteAsync(Guid id);
      Task DeleteManyAsync(DeleteManySecurityLogsInput input);
  }
  ```

## Data transfer object

* AuditLogDto - Audit log DTO
* SecurityLogDto - Security log DTO
* GetAuditLogsInput - Get audit log input parameters
* GetSecurityLogsInput - Get security log input parameters
* DeleteManyAuditLogsInput - batch delete audit log input parameters
* DeleteManySecurityLogsInput - batch delete security log input parameters
