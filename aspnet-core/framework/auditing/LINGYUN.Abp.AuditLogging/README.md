# LINGYUN.Abp.AuditLogging

The audit log core module provides the basic functions and interface definitions of audit logs.

[English](./README.EN.md)

## Features

* Audit logging infrastructure
* Audit log warehousing interface definition
* Audit log manager interface definition
* Support ignoring specific types of audit logging

## Module reference

```csharp
[DependsOn(typeof(AbpAuditLoggingModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

```json
{
  "Auditing": {
    "IsEnabled": true, // Whether to enable audit logs
    "HideErrors": true, // Whether to hide error information
    "IsEnabledForAnonymousUsers": true, // Whether to enable audit logs for anonymous users
    "IsEnabledForGetRequests": false, // Whether to enable audit logs for GET requests
    "ApplicationName": null //Application name
  }
}
```

## Basic usage

1. Reference module
2. Configure audit log options
3. Implement an audit log storage provider (for example: EntityFrameworkCore or Elasticsearch)

## Advanced features

### Ignore specific types

By default, the module ignores the following types of audit logs:
* CancellationToken
* CancellationTokenSource

You can add more types to ignore via configuration:

```csharp
Configure<AbpAuditingOptions>(options =>
{
    options.IgnoredTypes.AddIfNotContains(typeof(YourType));
});
```
