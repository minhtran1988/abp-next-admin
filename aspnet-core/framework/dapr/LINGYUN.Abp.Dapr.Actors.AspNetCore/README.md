# LINGYUN.Abp.Dapr.Actors.AspNetCore

Abp framework integration of Dapr.Asp.NetCore scans the list of Actor services implemented within the assembly and registers them as Dapr.Actors in batches

## Features

* Automatic Actor service registration
* Integrated with ABP dependency injection system
* Support custom Actor type names through `RemoteServiceAttribute`
* Configure the Actor runtime through `ActorRuntimeOptions`
* Automatic Actor endpoint mapping
* Actor interface verification

## Configuration usage

Module reference on demand

```csharp
[DependsOn(typeof(AbpDaprActorsAspNetCoreModule))]
public class YouProjectModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        //Configure Actor runtime options
        PreConfigure<ActorRuntimeOptions>(options =>
        {
            options.ActorIdleTimeout = TimeSpan.FromMinutes(60);
            options.ActorScanInterval = TimeSpan.FromSeconds(30);
            options.DrainOngoingCallTimeout = TimeSpan.FromSeconds(30);
            options.DrainRebalancedActors = true;
            options.RemindersStoragePartitions = 7;
        });
    }
}
```

## Implementation example

1. Define Actor interface

```csharp
[RemoteService("counter")] // Optional: Custom Actor type name
public interface ICounterActor : IActor
{
    Task<int> GetCountAsync();
    Task IncrementCountAsync();
}
```

2. Implement Actor

```csharp
public class CounterActor : Actor, ICounterActor
{
    private const string CountStateName = "count";

    public CounterActor(ActorHost host) : base(host)
    {
    }

    public async Task<int> GetCountAsync()
    {
        var count = await StateManager.TryGetStateAsync<int>(CountStateName);
        return count.HasValue ? count.Value : 0;
    }

    public async Task IncrementCountAsync()
    {
        var currentCount = await GetCountAsync();
        await StateManager.SetStateAsync(CountStateName, currentCount + 1);
    }
}
```

The module will automatically:
1. Detect `CounterActor` implementation
2. Register it to Dapr.Actors
3. Configure Actor runtime
4. Mapping Actor endpoints

## Actor runtime configuration

The module supports all standard Dapr Actor runtime configurations via `ActorRuntimeOptions`:

```csharp
PreConfigure<ActorRuntimeOptions>(options =>
{
    // Actor timeout settings
    options.ActorIdleTimeout = TimeSpan.FromMinutes(60);
    options.ActorScanInterval = TimeSpan.FromSeconds(30);
    
    // Clean up settings
    options.DrainOngoingCallTimeout = TimeSpan.FromSeconds(30);
    options.DrainRebalancedActors = true;
    
    // Reminder settings
    options.RemindersStoragePartitions = 7;
    
    // Custom serialization settings
    options.JsonSerializerOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
});
```

## IMPORTANT NOTE

* Actor implementation must be registered in the dependency injection container
* Actor interface must inherit from `IActor`
* Actor type name can be customized using `RemoteServiceAttribute`
* The module uses ABP's endpoint routing system to automatically map Actor endpoints
* Actor runtime options should be configured in the `PreConfigureServices` phase

## Endpoint mapping

The module automatically maps the following Actor endpoints:
* `/dapr/actors/{actorType}/{actorId}/method/{methodName}`
* `/dapr/actors/{actorType}/{actorId}/state`
* `/dapr/actors/{actorType}/{actorId}/reminders/{reminderName}`
* `/dapr/actors/{actorType}/{actorId}/timers/{timerName}`

[View English](README.EN.md)
