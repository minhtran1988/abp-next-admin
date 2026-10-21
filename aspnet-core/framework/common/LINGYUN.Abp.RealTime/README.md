# LINGYUN.Abp.RealTime

## Introduction

`LINGYUN.Abp.RealTime` is a real-time communication basic module that provides real-time messaging infrastructure. This module is mainly used to handle the delivery of real-time notifications, messages and events.

## Function

* Real-time event delivery infrastructure
* Support localized string processing
* Distributed event integration
* Extensible event handling mechanism

## Installation

```bash
dotnet add package LINGYUN.Abp.RealTime
```

## Use

1. Add `[DependsOn(typeof(AbpRealTimeModule))]` to your module class.

```csharp
[DependsOn(typeof(AbpRealTimeModule))]
public class YourModule : AbpModule
{
    // ...
}
```

2. Create a real-time event data transmission object:

```csharp
public class YourRealTimeEto : RealTimeEto<YourData>
{
    public YourRealTimeEto(YourData data) 
        : base(data)
    {
    }
}
```

3. Use localized strings:

```csharp
public class LocalizedMessage
{
    private readonly LocalizableStringInfo _localizableString;

    public LocalizedMessage()
    {
        _localizableString = new LocalizableStringInfo(
            "YourResource",
            "MessageKey",
            new Dictionary<object, object>
            {
                { "param1", "value1" }
            });
    }
}
```

## Advanced usage

### 1. Customize real-time events

```csharp
[EventName("your-custom-event")]
public class CustomRealTimeEto : RealTimeEto<CustomData>
{
    public CustomRealTimeEto(CustomData data) 
        : base(data)
    {
    }
}
```

### 2. Real-time event processing

```csharp
public class YourRealTimeEventHandler : 
    IDistributedEventHandler<YourRealTimeEto>,
    ITransientDependency
{
    public async Task HandleEventAsync(YourRealTimeEto eventData)
    {
        // Handle real-time events
    }
}
```

## Link

* [English document](./README.EN.md)
