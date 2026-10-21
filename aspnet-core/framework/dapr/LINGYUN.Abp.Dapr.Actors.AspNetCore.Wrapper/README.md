# LINGYUN.Abp.Dapr.Actors.AspNetCore.Wrapper

Dapr Actors ASP.NET Core response wrapping module that handles response wrapping and unwrapping of Actor method calls.

## Features

* Automatic packaging/unpacking of Actor response results
* Integrated with ABP packaging system
* Support error handling for Actor method calls
* Support success/error code configuration
* Integrate with Dapr.Actors.AspNetCore
* Support custom response packaging format
* Flexible packaging control options

## Configuration usage

Modules are referenced on demand:

```csharp
[DependsOn(
    typeof(AbpDaprActorsAspNetCoreModule),
    typeof(AbpWrapperModule)
)]
public class YouProjectModule : AbpModule
{
}
```

## Configuration options

Modules are configured using `AbpWrapperOptions` from the `LINGYUN.Abp.Wrapper` package:

```json
{
    "Wrapper": {
        "IsEnabled": true, // Enable/disable response wrapping
        "CodeWithSuccess": "0", // Wrap the success code in the response
        "HttpStatusCode": 200, //Default HTTP status code for wrapped responses
        "WrapOnError": true, // Whether to wrap error response
        "WrapOnSuccess": true // Whether to wrap a successful response
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

## Response format

When wrapping is enabled, the response from the Actor method will be in the following format:

```json
{
    "code": "0", //Response code, default "0" means success
    "message": "Success", // response message
    "details": null, // Additional details (optional)
    "result": { // actual response data
        // ...the return value of the Actor method
    }
}
```

## Error handling

The module automatically handles errors in Actor method calls:
*Response to packaging:
  * Unpack the response and inspect the code
  * If the code does not match `CodeWithSuccess`, throw `AbpRemoteCallException`
  * Include error message, details and code in exception
  * Support custom error code mapping
* For Actor runtime errors:
  * Automatically wrap standard error responses
  * Keep original exception information
  * Contains Actor-related contextual information

### Error response example

```json
{
    "code": "ERROR_001",
    "message": "Actor method call failed",
    "details": "State access failed for Actor 'counter'",
    "result": null
}
```

## Advanced usage

### 1. Control response packaging

Response wrapping for individual Actor calls can be controlled via HTTP headers:

```csharp
//Add in request header
var headers = new Dictionary<string, string>
{
    { "X-Abp-Wrap-Result", "true" }, // Force wrapping to be enabled
    // or
    { "X-Abp-Dont-Wrap-Result", "true" } // Forcefully disable wrapping
};

// used in Actor method
public async Task<int> GetCountAsync()
{
    var context = ActorContext.GetContext();
    context.Headers.Add("X-Abp-Wrap-Result", "true");
    
    var count = await StateManager.TryGetStateAsync<int>(CountStateName);
    return count.HasValue ? count.Value : 0;
}
```

### 2. Custom error handling

```csharp
public class CustomActorErrorHandler : IAbpWrapperErrorHandler
{
    public Task HandleAsync(AbpWrapperErrorContext context)
    {
        if (context.Exception is ActorMethodInvocationException actorException)
        {
            // Customize Actor error handling logic
            context.Response = new WrapperResponse
            {
                Code = "ACTOR_ERROR",
                Message = actorException.Message,
                Details = actorException.ActorId
            };
        }
        return Task.CompletedTask;
    }
}
```

## Notes

* Response wrapping can be controlled via:
  * Global settings in configuration files
  * HTTP headers control individual requests
  *Dynamic control in Actor method
* The error response of the Actor method will maintain the original error structure
* The module is integrated with ABP's remote service error handling system
* It is recommended to use response packaging uniformly in microservice architecture
* The packaging format can be customized by inheriting `IAbpWrapperResponseBuilder`
* Actor state operation errors will be correctly packaged and handled

[View in English](README.EN.md)
