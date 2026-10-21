# LINGYUN.Abp.Hangfire.Storage.SqlServer

[English](README.EN.md) | Simplified Chinese

## 1. Introduction

`LINGYUN.Abp.Hangfire.Storage.SqlServer` is an ABP module used to configure Hangfire to use SQL Server as storage. This module provides a simple configuration method, allowing you to easily use SQL Server as Hangfire's persistent storage.

## 2. Features

* Simple SQL Server storage configuration
* Support custom connection string
* Support SQL Server storage option configuration
* Seamless integration with ABP configuration system

## 3. Installation

```bash
dotnet add package LINGYUN.Abp.Hangfire.Storage.SqlServer
```

## 4. How to use

1. Add `AbpHangfireSqlServerStorageModule` to module dependencies:

```csharp
[DependsOn(typeof(AbpHangfireSqlServerStorageModule))]
public class YourModule : AbpModule
{
}
```

2. Configure the connection string and storage options in appsettings.json:

```json
{
  "Hangfire": {
    "SqlServer": {
      "Connection": "Server=localhost;Database=YourDb;Trusted_Connection=True;",
      "CommandBatchMaxTimeout": "00:05:00",
      "SlidingInvisibilityTimeout": "00:05:00",
      "QueuePollInterval": "00:00:00",
      "UseRecommendedIsolationLevel": true,
      "DisableGlobalLocks": true
    }
  }
}
```

## 5. Configuration items

### 5.1 Connection string configuration

The module looks for connection strings in the following order:
1. `Hangfire:SqlServer:Connection`
2. `ConnectionStrings:Default`

### 5.2 Storage options

* `CommandBatchMaxTimeout`: maximum timeout for command batch processing
* `SlidingInvisibilityTimeout`: sliding invisible timeout
* `QueuePollInterval`: Queue polling interval
* `UseRecommendedIsolationLevel`: whether to use the recommended isolation level
* `DisableGlobalLocks`: whether to disable global locks

## 6. Dependencies

* Volo.Abp.Hangfire
* Hangfire.SqlServer

## 7. Documentation and Resources

* [Hangfire official documentation](https://docs.hangfire.io/)
* [Hangfire.SqlServer Documentation](https://docs.hangfire.io/en/latest/configuration/using-sql-server.html)
* [ABP Framework Documentation](https://docs.abp.io/)
