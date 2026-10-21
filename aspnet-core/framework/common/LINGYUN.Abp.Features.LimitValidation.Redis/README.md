# LINGYUN.Abp.Features.LimitValidation.Redis

Function restriction verification component based on Redis

## Features

* Limit the number of calls using Redis storage and verification functions
* Support Lua script for atomic operations
* Supports multiple restriction strategies (minutes, hours, days, weeks, months, years)
* Support custom Redis configuration options

## Configuration usage

1. Add module dependencies

```csharp
[DependsOn(typeof(AbpFeaturesValidationRedisModule))]
public class YouProjectModule : AbpModule
{
    // other
}
```

2. Configure Redis options

```json
{
  "Features": {
    "Validation": {
      "Redis": {
        "Configuration": "127.0.0.1",
        "InstanceName": "YourInstanceName"
      }
    }
  }
}
```

## Configuration item description

* Configuration: Redis connection string
*InstanceName: Redis instance name (optional)
* ConfigurationOptions: Redis configuration options (optional, for more detailed Redis configuration)

## Usage example

```csharp
//Limit a method to be called up to 100 times per minute
[RequiresLimitFeature("YourFeature.MethodLimit", "YourFeature.Interval", LimitPolicy.Minute)]
public async Task YourMethod()
{
    // business logic
}
```

[Simplified Chinese](./README.md) | [English](./README.EN.md)
