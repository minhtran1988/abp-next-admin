# LINGYUN.Abp.Serilog.Enrichers.Application

Simplified Chinese | [English](./README.EN.md)

Log properties add application identification

## Features

* Add application name to log events
* Support custom application name field
* Supports both code configuration and JSON configuration
* Cache log event properties to improve performance
* Seamless integration with Serilog

## Module reference

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

## Configuration items

The following are field constants that need to be changed explicitly:

*AbpSerilogEnrichersConsts.ApplicationNamePropertyName - the name used to customize the ApplicationName field
* AbpSerilogEnrichersConsts.ApplicationName - the name that identifies the current application in the log

## How to use

### Code configuration method

```csharp
Log.Logger = new LoggerConfiguration()
    .Enrich.WithApplicationName()
    // ...other configurations...
    .CreateLogger();
```

### JSON configuration method

```json
{
   "Serilog": {
    "MinimumLevel": {
      "Default": "Information"
    },
    "Enrich": [ "WithApplicationName" ]
  }
}
```

## Implementation details

This enricher adds a (configurable) property called "ApplicationName" to each log event, with the value specified in `AbpSerilogEnrichersConsts.ApplicationName`. To improve performance, properties are cached.

## Best Practices

1. Set the application name as early as possible on application startup:
```csharp
public override void PreConfigureServices(ServiceConfigurationContext context)
{
    AbpSerilogEnrichersConsts.ApplicationName = "your-app-name";
}
```

2. Use a consistent naming convention for applications to facilitate log filtering.

3. Consider setting the application name via a configuration file:
```json
{
  "App": {
    "Name": "your-app-name"
  }
}
```
```csharp
AbpSerilogEnrichersConsts.ApplicationName = configuration["App:Name"];
```

## Notes

1. Once set, the application name is static and the same for all log entries for the application
2. Enricher uses attribute caching to improve performance
3. This attribute will only be added if it does not exist in the log event