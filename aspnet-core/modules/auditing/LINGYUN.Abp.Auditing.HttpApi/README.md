# LINGYUN.Abp.Auditing.HttpApi

The audit log HTTP API module provides a REST API interface for audit logs.

[English](./README.EN.md)

## Features

* Audit log REST API interface
* Security log REST API interface
* Support API version control
* Integrate Swagger documentation

## Module reference

```csharp
[DependsOn(typeof(AbpAuditingHttpApiModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## API interface

### Audit log

* GET /api/audit-logging/audit-logs/{id} - Get the specified audit log
* GET /api/audit-logging/audit-logs - Get the audit log list
* DELETE /api/audit-logging/audit-logs/{id} - delete the specified audit log
* DELETE /api/audit-logging/audit-logs/batch - delete audit logs in batches

### Security Log

* GET /api/audit-logging/security-logs/{id} - Get the specified security log
* GET /api/audit-logging/security-logs - Get the security log list
* DELETE /api/audit-logging/security-logs/{id} - delete the specified security log
* DELETE /api/audit-logging/security-logs/batch - delete security logs in batches

## Basic usage

1. Reference module
2. Configure permissions (if necessary)
3. Call the corresponding API interface through HTTP client

## API documentation

After launching the application, the full API documentation can be viewed through the Swagger UI:
* /swagger - Swagger UI interface
* /swagger/v1/swagger.json - OpenAPI specification document
