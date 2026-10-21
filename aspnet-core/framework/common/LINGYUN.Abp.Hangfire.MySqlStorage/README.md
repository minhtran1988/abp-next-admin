# LINGYUN.Abp.Hangfire.Storage.MySql

[English](README.EN.md) | Simplified Chinese

## 1. Introduction

`LINGYUN.Abp.Hangfire.Storage.MySql` is an ABP module used to configure Hangfire to use MySQL as storage. This module provides a simple configuration method, allowing you to easily use MySQL as Hangfire's persistent storage.

## 2. Features

* Simple MySQL storage configuration
* Support custom connection string
* Support MySQL storage option configuration
* Seamless integration with ABP configuration system

## 3. Installation

```bash
dotnet add package LINGYUN.Abp.Hangfire.Storage.MySql
```

## 4. How to use

1. Add `AbpHangfireMySqlStorageModule` to module dependencies:

```csharp
[DependsOn(typeof(AbpHangfireMySqlStorageModule))]
public class YourModule : AbpModule
{
}
```

2. Configure the connection string and storage options in appsettings.json:

```json
{
  "Hangfire": {
    "MySql": {
      "Connection": "Server=localhost;Database=YourDb;Uid=root;Pwd=123456;",
      "TablePrefix": "Hangfire",
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
1. `Hangfire:MySql:Connection`
2. `ConnectionStrings:Default`

### 5.2 Storage options

* `TablePrefix`: data table prefix
* `CommandBatchMaxTimeout`: maximum timeout for command batch processing
* `SlidingInvisibilityTimeout`: sliding invisible timeout
* `QueuePollInterval`: Queue polling interval
* `UseRecommendedIsolationLevel`: whether to use the recommended isolation level
* `DisableGlobalLocks`: whether to disable global locks

## 6. Dependencies

* Volo.Abp.Hangfire
* Hangfire.MySql.Core

## 7. Documentation and Resources

* [Hangfire official documentation](https://docs.hangfire.io/)
* [Hangfire.MySql.Core Documentation](https://github.com/arnoldasgudas/Hangfire.MySqlStorage)
* [ABP Framework Documentation](https://docs.abp.io/)
