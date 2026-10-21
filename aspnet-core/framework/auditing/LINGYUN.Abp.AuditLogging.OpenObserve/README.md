# LINGYUN.Abp.AuditLogging.OpenObserve

[English](./README.EN.md) | Simplified Chinese

## Introduction

Write audit logs and security logs to [OpenObserve](https://openobserve.ai/) and query them through OpenObserve SQL,
Available as an alternative implementation of `LINGYUN.Abp.AuditLogging.Elasticsearch` (`IAuditLogWriter` / `ISecurityLogWriter` /
`IAuditLogManager` / `ISecurityLogManager` / `IEntityChangeStore` are all replaced).

## Configuration

`OpenObserve` is the connection configuration (provided by `LINGYUN.Abp.OpenObserve`), and the audit-related configuration section is `AuditLogging:OpenObserve`:

```json
{
  "OpenObserve": {
    "Endpoint": "http://localhost:5080",
    "Organization": "default",
    "AccessToken": "Basic YWRtaW5AYWJwLmlvOnd3MVo1TCU2"
  },
  "AuditLogging": {
    "OpenObserve": {
      "Organization": null,
      "AuditLogStream": "audit_log",
      "SecurityLogStream": "security_log",
      "WriteEntityChanges": true,
      "WriteActions": true,
      "DefaultQueryDays": 365,
      "UseLogTimeAsTimestamp": true,
      "TimestampAllowedUptoHours": 5
    }
  }
}
```

| Configuration items | Default value | Description |
| --- | --- | --- |
| `Organization` | NULL | The organization to which the data flow belongs. If it is NULL, use `OpenObserve:Organization` |
| `AuditLogStream` | `audit_log` | Audit log main data stream (root), sub-data streams are calculated from it |
| `SecurityLogStream` | `security_log` | Security log data stream |
| `WriteEntityChanges` | `true` | Whether to flatten written entity changes (`IEntityChangeStore` cannot find data after closing) |
| `WriteActions` | `true` | Whether to flatten write operations (the `Actions.*` condition cannot be matched after closing) |
| `DefaultQueryDays` | `365` | Default query span (days) when no time range is specified |
| `UseLogTimeAsTimestamp` | `true` | Whether to overwrite `_timestamp` with log business time (see Note 2) |
| `TimestampAllowedUptoHours` | `5` | The maximum number of lookback hours allowed to override `_timestamp`, which must be consistent with the server's `ZO_INGEST_ALLOWED_UPTO` |

> Substream name **computed** by `AuditLogStream` (`AbpAuditLoggingOpenObserveOptions.GetEntityChangeStream()` /
> `GetActionStream()`), not configured separately: entity changes = `{AuditLogStream}_entity_changes`, operations = `{AuditLogStream}_actions`
> (Default is `audit_log_entity_changes`, `audit_log_actions`).

## Usage

```csharp
[DependsOn(typeof(AbpAuditLoggingOpenObserveModule))]
public class MyModule : AbpModule
{
}
```

The module does not do any processing on the data stream (no detection, no creation, no modification of settings): OpenObserve will automatically create the data stream when writing for the first time.
Please configure data stream settings such as retention policy on the OpenObserve side (or call `UpdateStreamSettingsAsync` yourself).

## Multi-tenant

The data flow is isolated by tenant (consistent with Elasticsearch implementation, refer to its `IIndexNameNormalizer`): the tenant data flow is as follows
`audit_log_{tenantId:N}` (entity changes/operations are `audit_log_entity_changes_{tenantId:N}`, `audit_log_actions_{tenantId:N}` respectively),
The host (no tenant specified) uses the base name.

* **Write**: Select the data stream according to the `TenantId`** of the **log itself (audit log writing occurs in the background queue, and the current tenant may no longer be the tenant that generates the log),
  Batch writes are grouped by tenant;
* **Query**: Select data flow by **current tenant** (`ICurrentTenant`), so tenants are naturally isolated;
* Naming rules can be overridden through `IStreamNameNormalizer` (default `StreamNameNormalizer`);
  According to the actual test, OpenObserve will normalize the `-` in the stream name to `_`, so the default delimiter is `_`;
* The module does not do any processing of the data stream: the data stream is automatically created by OpenObserve when it is first written,
  Retention policy and other settings should be configured on the OpenObserve side.

## Data flow structure

There are three data streams on the audit log side (the last two names are calculated by `AuditLogStream`):

| Data flow | Content |
| --- | --- |
| `audit_log` | Audit log header |
| `audit_log_entity_changes` | Flattened entity changes |
| `audit_log_actions` | Flattened operations (`parameters` may be large, so stored separately) |

```sql
select * from audit_log where httpmethod = 'POST' and url like '%/api/identity%' order by executiontime desc
```

Entity change data flow fields:

| Column | Description |
| --- | --- |
| `id` / `auditlogid` | Entity change Id / Owning audit log Id |
| `tenantid` / `entitytenantid` | Tenant |
| `changetime` / `changetype` | Change time / `Created`, `Updated`, `Deleted` |
| `entityid` / `entitytypefullname` | Entity ID |
| `userid` / `username` | Operator (redundant self-audit log, association-free) |
| `propertychanges` | Property change details (JSON string) |
| `_timestamp` | Document time (default overridden by `changetime`) |

Operation data flow fields:

| Column | Description |
| --- | --- |
| `id` / `auditlogid` | Operation Id / Owned audit log Id |
| `servicename` / `methodname` | Services and methods |
| `parameters` | Operation parameters (may be large, retrieved with `like` or `str_match`) |
| `executiontime` / `executionduration` | Execution time / time consumption |
| `userid` / `username` | Operator (redundant self-audit log) |
| `_timestamp` | Document time (default overridden by `executiontime`) |

`Actions.*` condition uses **two-stage query**:

```sql
-- 1) Operate data flow matching first
select distinct auditlogid from audit_log_actions where parameters like '%keyword%'
-- 2) Return to the main data stream and filter by id
select * from audit_log where id in (...)
```

## Notes

1. **Data stream column names must be lowercase**: OpenObserve will lowercase field names (`httpMethod` → `httpmethod`),
   Therefore, SQL conditions use lowercase column names, see `MapedFields` in each manager;
2. **`_timestamp` is overwritten by business time**: use `AuditLog.ExecutionTime` / `SecurityLog.CreationTime` when writing
   (Use `ChangeTime` for entity changes, `ExecutionTime` for operations) overrides `_timestamp` (`UseLogTimeAsTimestamp`, enabled by default),
   The `start_time` / `end_time` queried in this way correspond to the business time. But OpenObserve only accepts by default
   Data within `ZO_INGEST_ALLOWED_UPTO` (default 5) hours, **records beyond the window will be directly discarded by the server**
   (`Too old data … Data discarded`), so logs beyond `TimestampAllowedUptoHours` are no longer overwritten
   `_timestamp` (use server writing time instead); if you need to write earlier logs, please increase the server environment variable and synchronize the configuration;
3. **Time filter**: The query uses a wide window (`DefaultQueryDays`) by default, and the business time is filtered through `executiontime` /
   ISO 8601 string comparison of `creationtime` / `changetime` columns completed (covering fallback when limited by 5 hour window);
4. **Deleting records is not supported**: OpenObserve is append-only and neither supports SQL `DELETE` (`DELETE not supported for Base table`)
   In-place `UPDATE` is also not supported, so `IAuditLogManager.DeleteAsync` / `DeleteManyAsync` (the same is true for security logs) will directly throw a business exception
   `AuditLoggingOpenObserveErrorCodes.DeleteNotSupported` (error code `AuditLoggingOpenObserve:010001`,
   Localization copy: **The current logging facility does not support deleting records!**). For data cleaning, please configure the retention policy on the OpenObserve side, or use its deletion interface by time range;
5. **Operations and entity changes are written to independent data streams**: The main document no longer saves the `actions` / `entitychanges` nested arrays (OpenObserve can only write them
   Saved as a JSON string column, structured retrieval is not possible). `x => x.Actions.Any(a => a.Parameters.Contains("..."))` uses a two-stage query;
   `auditlogid in (…)` when `includeDetails` is used to assemble details from both subdataflows. When `parameters` is very large, it is recommended to use
   `str_match(parameters, 'value')` and configure `full_text_search_keys` for the sub-data stream to avoid `like` full column scan.
   Entity change details (`propertychanges`) are also stored as JSON strings: the conversion logic is concentrated in `OpenObserveEntityChangeConverter`,
   Serialization is done through `IAuditLoggingOpenObserveSerializer` (the default implementation delegates ABP's `IJsonSerializer`,
   The entity itself does not use `JsonSerializer` directly); when you need to customize JSON behavior for a specific type, use
   `AbpSystemTextJsonSerializerModifiersOptions` register modifiers;
   When the data stream has not been created (no data has been written yet), the query will be processed as an empty result and no error will be reported;
6. **String query is fuzzy matching**: `url` / `userName` / `applicationName` and other parameters use `like '%value%'`,
   `correlationId` / `clientId` / `clientIpAddress` / `httpMethod` / `action` is an exact match;
7. **Field mapping**: Expression member names (PascalCase) are mapped to lowercase data flow column names;
   `LINGYUN.Abp.OpenObserve` package provides `ExpressionParser` (supports `Any(set member)` expression) and
   `OpenObserveSqlBuilder` is used for SQL conditional building reuse.

## Related projects

* [LINGYUN.Abp.AuditLogging](../LINGYUN.Abp.AuditLogging/README.md)
* [LINGYUN.Abp.OpenObserve](../../open-observe/LINGYUN.Abp.OpenObserve/README.md)
* [LINGYUN.Abp.Logging.Serilog.OpenObserve](../../logging/LINGYUN.Abp.Logging.Serilog.OpenObserve/README.md)
