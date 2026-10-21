# LINGYUN.Abp.Dapr.Actors

Dapr.IActor client proxy module

## Features

* Dynamic proxy generation for Dapr Actors
* Integrated with ABP remote service system
* Support Actor authentication and authorization
*Multi-tenant support
* Automatic request/response handling
* Custom error handling
* Culture and language header support

## Configuration usage

Module reference on demand

```csharp
[DependsOn(typeof(AbpDaprActorsModule))]
public class YouProjectModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // The registered proxy is similar to the Volo.Abp.Http.Client module
        context.Services.AddDaprActorProxies(
            typeof(YouProjectActorInterfaceModule).Assembly, // Search for the IActor definition under the YouProjectActorInterfaceModule module
            RemoteServiceName
        );
    }
}
```

## Configuration options

```json
{
    "RemoteServices": {
        "Default": {
            "BaseUrl": "http://localhost:3500", // Required, Dapr HTTP endpoint
            "DaprApiToken": "your-api-token", // Optional, Dapr API Token
            "RequestTimeout": "30000" // Optional, request timeout (milliseconds, default: 30000)
        }
    }
}
```

## Implementation example

1. Actor interface definition

```csharp
public interface ICounterActor : IActor
{
    Task<int> GetCountAsync();
    Task IncrementCountAsync();
}
```

2. Actor implementation

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

3. Client use

```csharp
public class CounterService
{
    private readonly ICounterActor _counterActor;

    public CounterService(ICounterActor counterActor)
    {
        _counterActor = counterActor;
    }

    public async Task<int> GetAndIncrementCountAsync()
    {
        var count = await _counterActor.GetCountAsync();
        await _counterActor.IncrementCountAsync();
        return count;
    }
}
```

## Notes

* Actor methods must return `Task` or `Task<T>`
* Actor methods can only have at most one parameter
* Actor instances are single-threaded and can only handle one request at a time
* Actor state is managed by Dapr runtime
* Module automatic processing:
  * Authentication header
  * Tenant context
  * Cultural information
  * Request timeout
  * Error handling

## Error handling

Module provides custom error handling for Actor calls:
* `AbpDaprActorCallException`: thrown when the Actor method call fails
* `ActorMethodInvocationException`: Contains details about the failure
* Error responses include:
  * Error message
  * Error code
  * Original exception type

[View more](README.EN.md)
