# LINGYUN.Abp.Logging.Serilog.Elasticsearch

Simplified Chinese | [English](./README.EN.md)

ES implementation of the ILoggingProvider interface, retrieving log information from ES

## Features

* Log storage and retrieval based on Elasticsearch
* Supports multiple log levels (Debug, Information, Warning, Error, Critical)
* Support automatic mapping of Serilog and Microsoft.Extensions.Logging log levels
* Supports rich query conditions (time range, log level, machine name, environment name, etc.)
* Supports storage and retrieval of exception information
*Support multi-tenancy

## Module reference

```csharp
[DependsOn(typeof(AbpLoggingSerilogElasticsearchModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

* AbpLoggingSerilogElasticsearchOptions.IndexFormat must be the same as the IndexFormat in the Serilog configuration item, otherwise the correct index cannot be located

## appsettings.json

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

## Basic usage

1. Inject the ILoggingManager interface:
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

## Supported query conditions

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

## Notes

1. IndexFormat configuration must be consistent with Serilog configuration. The default format is "logstash-{0:yyyy.MM.dd}"
2. Supports multi-tenancy and will automatically filter logs based on the current tenant ID.
3. Query results support paging and sorting
4. Support fuzzy matching of request paths
5. Support existence query of exception information