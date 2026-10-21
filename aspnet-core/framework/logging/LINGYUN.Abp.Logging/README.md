# LINGYUN.Abp.Logging

Log basic module

Define the ILoggingManager interface to implement log information query

[English](./README.EN.md)

## Features

* Provide unified log query interface ILoggingManager
* Supports multiple log field queries, including timestamp, log level, message, etc.
* Support exception information recording and query
* Supports rich log field information, such as machine name, environment name, application name, etc.
* Support paging query and counting statistics

## Module reference

```csharp
[DependsOn(typeof(AbpLoggingModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

Configure in `appsettings.json`:

```json
{
  "Logging": {
    "MachineName": "Your machine name",
    "EnvironmentName": "Environment Name"
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
    level: LogLevel.Error
);

// Get the total number of logs
var count = await _loggingManager.GetCountAsync(
    startTime: DateTime.Now.AddDays(-1),
    level: LogLevel.Error
);

// Get a single log
var log = await _loggingManager.GetAsync(id);
```

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
