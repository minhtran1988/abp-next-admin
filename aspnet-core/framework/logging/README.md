# LINGYUN.Abp log module

## Module overview

The log module provides log access and retrieval capabilities: Serilog-based log writing and attribute enhancement, as well as retrieval of logs from persistence facilities
`ILoggingProvider` implementation (providing a unified query interface to the outside through `ILoggingManager`).

## Module list

| Module | Description | Documentation |
| --- | --- | --- |
| LINGYUN.Abp.Logging | Logging infrastructure, defines and implements `ILoggingManager`, and implements log information query | [README](./LINGYUN.Abp.Logging/README.md) |
| LINGYUN.Abp.Logging.Serilog.Elasticsearch | Elasticsearch implementation of `ILoggingProvider` | [README](./LINGYUN.Abp.Logging.Serilog.Elasticsearch/README.md) |
| LINGYUN.Abp.Logging.Serilog.OpenObserve | OpenObserve implementation of `ILoggingProvider` | [README](./LINGYUN.Abp.Logging.Serilog.OpenObserve/README.md) |
| LINGYUN.Abp.Serilog.Enrichers.Application | Add application identification to log properties | [README](./LINGYUN.Abp.Serilog.Enrichers.Application/README.md) |
| LINGYUN.Abp.Serilog.Enrichers.UniqueId | Add a unique identifier to the log attribute (snowflake algorithm) | [README](./LINGYUN.Abp.Serilog.Enrichers.UniqueId/README.md) |

## Features

### Core functions

- Unified log query interface `ILoggingManager`, and replaceable `ILoggingProvider` abstraction
- Supports multiple log field queries (timestamp, log level, message, etc.)
-Support exception information recording and query
- Supports rich log field information (machine name, environment name, application name, request context, etc.)
- Support paging query and counting statistics

### Storage support

- Elasticsearch implementation (Serilog writing + `ILoggingProvider` retrieval)
- OpenObserve implementation (Serilog writing + `ILoggingProvider` retrieval)

> Both implementations specify the retrieval implementation via `Logging:Provider`.

### Log attribute enhancement

- Application name (`WithApplicationName`)
- Unique identifier (`WithUniqueId`, snowflake algorithm)

## Module reference

### Core module

```csharp
[DependsOn(typeof(AbpLoggingModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

### Elasticsearch module

```csharp
[DependsOn(typeof(AbpLoggingSerilogElasticsearchModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

### OpenObserve module

```csharp
[DependsOn(typeof(AbpLoggingSerilogOpenObserveModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

### Apply Name Enhancer

```csharp
[DependsOn(typeof(AbpSerilogEnrichersApplicationModule))]
public class YouProjectModule : AbpModule
{
  public override void PreConfigureServices(ServiceConfigurationContext context)
  {
    AbpSerilogEnrichersConsts.ApplicationName = "demo-app";
  }
}
```

### Unique ID Enhancer

```csharp
[DependsOn(typeof(AbpSerilogEnrichersUniqueIdModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration options

### Log basic configuration

```json
{
  "Logging": {
    "MachineName": "Your machine name",
    "EnvironmentName": "Environment Name"
  }
}
```

### Elasticsearch configuration

* AbpLoggingSerilogElasticsearchOptions.IndexFormat must be the same as IndexFormat in the Serilog configuration item, otherwise the correct index cannot be located

```json
{
  "Logging": {
    "Provider": "ElasticsearchLoggingProvider",
    "Serilog": {
      "Elasticsearch": {
        "IndexFormat": "logstash-{0:yyyy.MM.dd}"
      }
    }
  }
}
```

### OpenObserve configuration

* AbpLoggingSerilogOpenObserveOptions.Stream is the stream name for storing logs.
* AbpLoggingSerilogOpenObserveOptions.Organization The name of the organization where logs are stored

```json
{
  "Logging": {
    "Provider": "OpenObserveLoggingProvider",
    "Serilog": {
      "OpenObserve": {
        "Stream": "default",
        "Organization": "default"
      }
    }
  },
  "OpenObserve": {
    "Endpoint": "http://localhost:5080",
    "AccessToken": "Basic XXXXXXXXXXX"
  }
}
```

### Unique identification configuration

* AbpSerilogUniqueIdConsts.UniqueIdPropertyName The name of the unique identification field, the default is "UniqueId"

```json
{
  "UniqueId": {
    "Snowflake": {
      "WorkerId": 1, // Worker machine ID
      "DatacenterId": 1, // Data center ID
      "Sequence": 0, // sequence number
      "BaseTime": "2020-01-01 00:00:00" // Base time
    }
  }
}
```

### Log attribute enhancement (Serilog Enrich)

```json
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information"
    },
    "Enrich": [ "WithApplicationName", "WithUniqueId" ]
  }
}
```

## Basic usage

1. Inject the `ILoggingManager` interface:

```csharp
public class YourService
{
    private readonly ILoggingManager _loggingManager;

    public YourService(ILoggingManager loggingManager)
    {
        _loggingManager = loggingManager;
    }
}
```

2. Query log:

```csharp
// Get the log list
var logs = await _loggingManager.GetListAsync(
    startTime: DateTime.Now.AddDays(-1),
    maxResultCount: 10,
    skipCount: 0,
    level: LogLevel.Error,
    environment: "Production",
    application: "YourApp"
);

// Get the total number of logs
var count = await _loggingManager.GetCountAsync(
    startTime: DateTime.Now.AddDays(-1),
    level: LogLevel.Error
);

// Get a single log
var log = await _loggingManager.GetAsync(id);
```

### Supported query conditions

* Time range (startTime, endTime)
* Log level (level)
* Machine name (machineName)
* Environment name (environment)
* Application name (application)
* context
* Request ID (requestId)
* Request path (requestPath)
* Correlation ID (correlationId)
* Process ID (processId)
* Thread ID (threadId)
* Whether it contains exceptions (hasException)

## Log field description

* TimeStamp - log timestamp
* Level - log level
* Message - log message
* Fields - log field information
  *Id - the unique identifier of the log
  * MachineName - machine name
  * Environment - environment name
  *Application - application name
  * Context - context
  * ActionId - Action ID
  * ActionName - action name
  * RequestId - request ID
  * RequestPath - request path
  * ConnectionId - connection ID
  * CorrelationId - Correlation ID
  * ClientId - client ID
  *UserId - User ID
  * ProcessId - Process ID
  * ThreadId - Thread ID
* Exceptions - Exception information list

## Special instructions

- Elasticsearch implementation: `IndexFormat` must be consistent with Serilog configuration (default `logstash-{0:yyyy.MM.dd}`);
  Supports multi-tenancy, logs will be filtered based on the current tenant ID; query supports paging, sorting and request path fuzzy matching
- OpenObserve implementation: supports retrieval by `Stream`/`Organization`; writes to the server side `ZO_INGEST_ALLOWED_UPTO`
  (default 5 hours) limit; storage is append-only, deletion by record is not supported
- Application name enhancer: The application name is a static value, once set it takes effect for all log entries of the application; it is only written if the attribute does not exist for the log event
- Unique identification enhancer: The ID generated by the snowflake algorithm tends to increase but does not guarantee strict increment; the work machine ID and the data center ID must be unique in the cluster;
  The base time cannot be modified after it is set.

## Related modules

* OpenObserve implementation of audit log: [LINGYUN.Abp.AuditLogging.OpenObserve](../auditing/LINGYUN.Abp.AuditLogging.OpenObserve/README.md)
* Elasticsearch implementation of audit log: [LINGYUN.Abp.AuditLogging.Elasticsearch](../auditing/LINGYUN.Abp.AuditLogging.Elasticsearch/README.md)
