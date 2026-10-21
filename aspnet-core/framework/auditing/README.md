# LINGYUN.Abp audit module

## Module overview

The audit module provides comprehensive logging and auditing capabilities, supports multiple storage methods and highly configurable auditing options.

## Features

### Core functions

- Audit logging infrastructure
- Audit log warehousing interface definition
- Audit log manager interface definition
- Support ignoring specific types of audit logging

### Storage support

- EntityFrameworkCore implementation
- Elasticsearch implementation
- OpenObserve implementation

> Note: Elastic library limitation, compatible with 8.x; 9.x version, if you need to use 10.x version, please switch **Elastic.Clients.Elasticsearch** to 9.x version
> Reference: https://www.nuget.org/packages/Elastic.Clients.Elasticsearch#readme-body-tab

## Module reference

### Core module

```csharp
[DependsOn(typeof(AbpAuditLoggingModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

### EntityFrameworkCore module

```csharp
[DependsOn(typeof(AbpAuditLoggingEntityFrameworkCoreModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

### Elasticsearch module

```csharp
[DependsOn(typeof(AbpAuditLoggingElasticsearchModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

### OpenObserve module

```csharp
[DependsOn(typeof(AbpAuditLoggingOpenObserveModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration options

### Audit log configuration

```json
{
  "Auditing": {
    "IsEnabled": true, // Whether to enable audit logs
    "HideErrors": true, // Whether to hide error information
    "IsEnabledForAnonymousUsers": true, // Whether to enable audit logs for anonymous users
    "IsEnabledForGetRequests": false, // Whether to enable audit logs for GET requests
    "ApplicationName": null //Application name
  },
  // Audit log enhancement configuration
  "AuditLogging": {
    "IsAuditLogEnabled": true, // Whether to enable audit logging
    "UseAuditLogQueue": true, // Use the audit log queue. If the queue is not enabled, it will be written directly to the persistence facility.
    "MaxAuditLogQueueSize": 10000, // Maximum audit log queue size, default 10000
    "BatchAuditLogSize": 100, //The size of the audit log queue for processing at one time. After 100 records are written to the queue at the same time, they are immediately written to the persistence facility.
    "IsSecurityLogEnabled": true, // Whether to enable security logging
    "UseSecurityLogQueue": true, // Use the security log queue. If the queue is not enabled, it will be written directly to the persistence facility.
    "MaxSecurityLogQueueSize": 10000, // Maximum security log queue size, default 10000
    "BatchSecurityLogSize": 100 // The size of the security log queue for one processing, and 100 records are written to the queue at the same time and immediately written to the persistence facility.
  }
}
```

### Elasticsearch configuration

```json
{
  "AuditLogging": {
    "Elasticsearch": {
      "IndexPrefix": "auditlogging", // Index prefix
      "ThrowIfIndexInitFailed": true // If the index initialization fails, an exception is thrown. The default is: true. The application stops running after the index initialization fails.
    }
  }
}
```

### OpenObserve configuration

```json
{
  "OpenObserve": {
    "Endpoint": "http://localhost:5080", // OpenObserve service address
    "Organization": "default", // organization name
    "AccessToken": "Basic XXXXXXXXXXX" // Authorization header, or use UserName/Password
  },
  "AuditLogging": {
    "OpenObserve": {
      "Organization": null, // The organization to which the data flow belongs, use OpenObserve:Organization when it is empty
      "AuditLogStream": "audit_log", // Audit log main data stream (root)
      "SecurityLogStream": "security_log", // Security log data stream
      "WriteEntityChanges": true, // Whether to flatten entity changes and write them into {AuditLogStream}_entity_changes
      "WriteActions": true, // Whether to flatten operations and write them into {AuditLogStream}_actions
      "DefaultQueryDays": 365, //Default query span (days) when no time range is specified
      "UseLogTimeAsTimestamp": true, // Whether to overwrite _timestamp with log business time
      "TimestampAllowedUptoHours": 5 // The maximum number of backtracking hours to override _timestamp must be consistent with the server ZO_INGEST_ALLOWED_UPTO
    }
  }
}
```

> The substream name is calculated by `AuditLogStream` (entity changes = `{AuditLogStream}_entity_changes`, action = `{AuditLogStream}_actions`), no separate configuration is required.

## Database connection configuration

```json
{
  "ConnectionStrings": {
    "AbpIdentity": "Server=127.0.0.1;Database=Identity;User Id=root;Password=*",
    "AbpAuditLogging": "Server=127.0.0.1;Database=AuditLogging;User Id=root;Password=*"
  }
}
```

## Advanced features

### Ignore specific types

By default, the module ignores the following types of audit logs:

- CancellationToken
- CancellationTokenSource

You can add more types to ignore via configuration:

```csharp
Configure<AbpAuditingOptions>(options =>
{
    options.IgnoredTypes.AddIfNotContains(typeof(YourType));
});
```

## Special instructions

- Elasticsearch implementation supports cross-tenants and will automatically switch indexes according to tenants
- EntityFrameworkCore implementation is mainly used as a bridge, and the specific implementation is managed by the Abp official module
- OpenObserve implements the same isolation of data streams by tenant. The tenant data stream is in the shape of `audit_log_{tenantId:N}`: writing is based on the tenant of the log itself, querying is based on the current tenant, and the naming rules can be overridden through `IOpenObserveStreamNameNormalizer`
- OpenObserve is append-only storage and does not support SQL `DELETE`/`UPDATE`, so `IAuditLogManager.DeleteAsync`/`DeleteManyAsync` will throw a business exception `AuditLoggingOpenObserve:010001` ("The current logging facility does not support deleting records!"). For data cleaning, please configure the retention policy on the OpenObserve side or use its delete interface by time range
- OpenObserve will save nested arrays as JSON string columns (column names are field names), so entity changes and operations are flattened and written into independent data streams; the `Actions.*` condition of the operation uses a two-stage query (first match `auditlogid` in the operation data stream, and then return to the main data stream to filter by `id in (...)`)
- The data stream is automatically created by OpenObserve when it is first written. The module does not detect, create, or modify the data stream settings; when querying the data stream that has not yet been created, it returns an empty result and records a warning log.