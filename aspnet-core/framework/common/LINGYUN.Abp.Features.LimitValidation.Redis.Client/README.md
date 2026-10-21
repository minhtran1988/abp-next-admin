# LINGYUN.Abp.Features.LimitValidation.Redis.Client

Redis-based client function restriction verification component

## Features

* All functions inherited from LINGYUN.Abp.Features.LimitValidation.Redis
* Function restriction verification specifically for client applications
* Support client-specific restriction policies

## Configuration usage

1. Add module dependencies

```csharp
[DependsOn(typeof(AbpFeaturesValidationRedisClientModule))]
public class YouProjectModule : AbpModule
{
    // other
}
```

2. Configure Redis options (same as LINGYUN.Abp.Features.LimitValidation.Redis)

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

## Usage example

```csharp
//Limit the client to call a certain function up to 1,000 times a day
[RequiresLimitFeature("YourClientFeature.DailyLimit", "YourClientFeature.Interval", LimitPolicy.Days)]
public async Task YourClientMethod()
{
    //Client business logic
}
```

[Simplified Chinese](./README.md) | [English](./README.EN.md)
