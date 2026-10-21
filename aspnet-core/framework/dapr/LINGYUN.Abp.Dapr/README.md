# LINGYUN.Abp.Dapr

Dapr integrates basic modules to implement the named singleton DaprClient in the dapr document

See: https://docs.dapr.io/developing-applications/sdks/dotnet/dotnet-client/dotnet-daprclient-usage

## Features

* Support creating default and named DaprClient instances
* Supports configuring HTTP and gRPC endpoints
* Support custom JSON serialization options
* Support Dapr API Token authentication
* Support gRPC channel configuration
* Support extension for DaprClient instance configuration and build configuration
* Support multiple Dapr Sidecar connections
* Support custom DaprClient behavior

## Configuration options

```json
{
  "Dapr": {
    "Client": {
      "DaprApiToken": "your-api-token", // Optional, Dapr API Token
      "HttpEndpoint": "http://localhost:3500", // Optional, HTTP endpoint
      "GrpcEndpoint": "http://localhost:50001", // Optional, gRPC endpoint
      "JsonSerializerOptions": { // Optional, JSON serialization options
        "PropertyNamingPolicy": "CamelCase",
        "PropertyNameCaseInsensitive": true,
        "WriteIndented": true,
        "DefaultIgnoreCondition": "WhenWritingNull"
      },
      "GrpcChannelOptions": { // Optional, gRPC channel options
        "Credentials": "Insecure",
        "MaxReceiveMessageSize": 1048576,
        "MaxSendMessageSize": 1048576
      }
    }
  }
}
```

## Configuration usage

Modules are referenced on demand:

```csharp
[DependsOn(typeof(AbpDaprModule))]
public class YouProjectModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        //Create a DaprClient
        context.Services.AddDaprClient();

        //Create a named DaprClient
        context.Services.AddDaprClient("__DaprClient");
        
        //Configure DaprClient options
        Configure<DaprClientFactoryOptions>(options =>
        {
            options.HttpEndpoint = "http://localhost:3500";
            options.GrpcEndpoint = "http://localhost:50001";
            options.DaprApiToken = "your-api-token";
            
            //Add DaprClient configuration action
            options.DaprClientActions.Add(client =>
            {
                //Configure DaprClient instance
            });
            
            //Add DaprClientBuilder configuration action
            options.DaprClientBuilderActions.Add(builder =>
            {
                //Configure DaprClientBuilder
            });
        });
    }
}
```

## Advanced usage

### 1. Configure DaprClient

```csharp
public override void ConfigureServices(ServiceConfigurationContext context)
{
    //Configure named DaprClient
    context.Services.AddDaprClient("CustomClient", builder =>
    {
        // Configure HTTP endpoint
        builder.UseHttpEndpoint("http://localhost:3500");
        
        // Configure gRPC endpoint
        builder.UseGrpcEndpoint("http://localhost:50001");
        
        //Configure API Token
        builder.UseDaprApiToken("your-api-token");
        
        //Configure JSON serialization options
        builder.UseJsonSerializerOptions(new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        });
        
        //Configure gRPC channel options
        builder.UseGrpcChannelOptions(new GrpcChannelOptions
        {
            MaxReceiveMessageSize = 1024 * 1024,
            MaxSendMessageSize = 1024 * 1024
        });
    });
}
```

### 2. Using DaprClient

```csharp
public class YourService
{
    private readonly IDaprClientFactory _daprClientFactory;

    public YourService(IDaprClientFactory daprClientFactory)
    {
        _daprClientFactory = daprClientFactory;
    }

    public async Task InvokeMethodAsync()
    {
        // Use default client
        var defaultClient = _daprClientFactory.CreateClient();
        
        // use named client
        var namedClient = _daprClientFactory.CreateClient("CustomClient");
        
        // Call service method
        var response = await defaultClient.InvokeMethodAsync<OrderDto>(
            HttpMethod.Get,
            "order-service", // Target service ID
            "api/orders/1", // method path
            new { id = 1 } // Request parameters
        );
        
        // Publish event
        await defaultClient.PublishEventAsync(
            "pubsub", // Pub/sub component name
            "order-created", // theme name
            response // event data
        );
        
        // save state
        await defaultClient.SaveStateAsync(
            "statestore", // Name of state storage component
            "order-1", // key
            response // value
        );
        
        // Get status
        var state = await defaultClient.GetStateAsync<OrderDto>(
            "statestore", // Name of state storage component
            "order-1" // key
        );
    }
}
```

### 3. Customize DaprClient behavior

```csharp
public class CustomDaprClientBehavior
{
    public void Configure(DaprClient client)
    {
        // Configure custom behavior
    }
}

//Register in module
public override void ConfigureServices(ServiceConfigurationContext context)
{
    Configure<DaprClientFactoryOptions>(options =>
    {
        options.DaprClientActions.Add(client =>
        {
            new CustomDaprClientBehavior().Configure(client);
        });
    });
}
```

## Notes

* DaprClient instances are thread-safe and it is recommended to use singleton mode
* Named DaprClient can have different configurations, suitable for scenarios where different Dapr Sidecars need to be connected
* After configuration changes, you need to recreate the DaprClient instance to take effect.
* gRPC channel configuration needs to pay attention to performance and resource consumption
* JSON serialization options will affect all requests using this DaprClient
* API Token should be managed through a secure configuration management system
* It is recommended to use different named DaprClients for different microservices
* Timeouts and retry strategies should be configured appropriately in production environments

[View in English](README.EN.md)
