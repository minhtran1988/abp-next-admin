# LINGYUN.Abp.Logging.Serilog.OpenObserve

Simplified Chinese | [English](./README.EN.md)

OpenObserve implementation of the ILoggingProvider interface, retrieving log information from OpenObserve

## Features

* Log storage and retrieval based on OpenObserve
* Supports multiple log levels (Debug, Information, Warning, Error, Critical)
* Support automatic mapping of Serilog and Microsoft.Extensions.Logging log levels
* Supports rich query conditions (time range, log level, machine name, environment name, etc.)
* Supports storage and retrieval of exception information
*Support multi-tenancy

## Module reference

```csharp
[DependsOn(typeof(AbpLoggingSerilogOpenObserveModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

* AbpLoggingSerilogOpenObserveOptions.Stream stream name to store logs
* AbpLoggingSerilogOpenObserveOptions.Organization organization name where logs are stored

## appsettings.json

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

## Dependent public components

* Connection and serialization: `LINGYUN.Abp.OpenObserve` (`OpenObserveClient`, `IOpenObserveSerializer`, `AbpOpenObserveOptions`)
* Query conditions and SQL construction: `ExpressionParser`, `Condition`, `ComparisonOp` under `LINGYUN.Abp.OpenObserve.Utils`
  and `OpenObserveSqlBuilder`

## Notes
