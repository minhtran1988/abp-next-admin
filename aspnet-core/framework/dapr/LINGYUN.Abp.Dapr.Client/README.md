[Actors](../README.md) | Dapr.Client documentation

# LINGYUN.Abp.Dapr.Client

The inter-service calls in the Dapr document are implemented. The project design is consistent with Volo.Abp.Http.Client. Volo.Abp.Http.Client can be seamlessly replaced through the configuration file.

Configuration reference [AbpRemoteServiceOptions](https://docs.abp.io/zh-Hans/abp/latest/API/Dynamic-CSharp-API-Clients#abpremoteserviceoptions)

## Features

* Integrated with ABP remote service system
* Support dynamic proxy generation
* Support service discovery and load balancing
* Support custom request and response processing
* Support error handling and formatting
* Support multi-service endpoint configuration
* Support request/response interceptor
* Support custom DaprClient behavior

## Configuration options

```json
{
    "RemoteServices": {
        "Default": {
            "AppId": "default-app", // Dapr application ID
            "BaseUrl": "http://localhost:3500", // Dapr HTTP endpoint
            "HealthCheckUrl": "/health", // Health check endpoint
            "RequestTimeout": 30000, // Request timeout (milliseconds)
            "RetryCount": 3, //Number of retries
            "RetryWaitTime": 1000 // Retry waiting time (milliseconds)
        },
        "System": {
            "AppId": "system-app",
            "BaseUrl": "http://localhost:50000",
            "Headers": { // Custom request headers
                "Tenant": "Default",
                "Culture": "zh-Hans"
            }
        }
    }
}
```

## Configuration usage

Modules are referenced on demand:

```csharp
[DependsOn(typeof(AbpDaprClientModule))]
public class YouProjectModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        //Register proxy, similar to Volo.Abp.Http.Client module
        context.Services.AddDaprClientProxies(
            typeof(YouProjectInterfaceModule).Assembly, //Search for remote service definitions under the module
            RemoteServiceName
        );

        //Configure proxy options
        Configure<AbpDaprClientProxyOptions>(options =>
        {
            //Configure request interceptor
            options.ProxyRequestActions.Add((appId, request) =>
            {
                request.Headers.Add("Custom-Header", "Value");
            });

            //Configure response processing
            options.OnResponse(async (response, serviceProvider) =>
            {
                return await response.Content.ReadAsStringAsync();
            });

            //Configure error handling
            options.OnError(async (response, serviceProvider) =>
            {
                var error = await response.Content.ReadAsStringAsync();
                return new RemoteServiceErrorInfo
                {
                    Code = response.StatusCode.ToString(),
                    Message = error
                };
            });
        });
    }
}
```

## Implementation example

### 1. Interface definition

```csharp
// IApplicationService implements IRemoteService
public interface ISystemAppService : IApplicationService
{
    Task<string> GetAsync();
    Task<SystemDto> CreateAsync(CreateSystemDto input);
    Task<List<SystemDto>> GetListAsync();
    Task DeleteAsync(string id);
}

public class SystemInterfaceModule : AbpModule
{
}
```

### 2. Server-side implementation

```csharp
[DependsOn(
    typeof(SystemInterfaceModule),
    typeof(AbpAspNetCoreMvcModule)
)]
public class SystemServerModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(SystemServerModule).Assembly);
        });
    }
}

public class SystemAppService : ApplicationService, ISystemAppService
{
    private readonly ISystemRepository _systemRepository;

    public SystemAppService(ISystemRepository systemRepository)
    {
        _systemRepository = systemRepository;
    }

    public async Task<string> GetAsync()
    {
        return "System";
    }

    public async Task<SystemDto> CreateAsync(CreateSystemDto input)
    {
        var system = await _systemRepository.CreateAsync(
            new System
            {
                Name = input.Name,
                Description = input.Description
            }
        );
        return ObjectMapper.Map<System, SystemDto>(system);
    }

    public async Task<List<SystemDto>> GetListAsync()
    {
        var systems = await _systemRepository.GetListAsync();
        return ObjectMapper.Map<List<System>, List<SystemDto>>(systems);
    }

    public async Task DeleteAsync(string id)
    {
        await _systemRepository.DeleteAsync(id);
    }
}
```

### 3. Client use

```csharp
[DependsOn(typeof(AbpDaprClientModule))]
public class SystemClientModule : AbpModule
{
    private const string RemoteServiceName = "System";

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        //Register agent
        context.Services.AddDaprClientProxies(
            typeof(SystemInterfaceModule).Assembly,
            RemoteServiceName
        );

        //Configure retry strategy
        context.Services.AddDaprClientBuilder(builder =>
        {
            builder.ConfigureHttpClient((sp, client) =>
            {
                client.Timeout = TimeSpan.FromSeconds(30);
            });
        });
    }
}

public class SystemService
{
    private readonly ISystemAppService _systemAppService;

    public SystemService(ISystemAppService systemAppService)
    {
        _systemAppService = systemAppService;
    }

    public async Task<List<SystemDto>> GetSystemsAsync()
    {
        try
        {
            return await _systemAppService.GetListAsync();
        }
        catch (AbpRemoteCallException ex)
        {
            // Handle remote call exceptions
            _logger.LogError(ex, "Failed to get systems");
            throw;
        }
    }
}
```

## Advanced usage

### 1. Custom request processing

```csharp
public class CustomRequestHandler
{
    public void Configure(HttpRequestMessage request)
    {
        request.Headers.Add("Correlation-Id", Guid.NewGuid().ToString());
        request.Headers.Add("Client-Version", "1.0.0");
    }
}

//Register in module
Configure<AbpDaprClientProxyOptions>(options =>
{
    options.ProxyRequestActions.Add((appId, request) =>
    {
        new CustomRequestHandler().Configure(request);
    });
});
```

### 2. Custom response processing

```csharp
public class CustomResponseHandler
{
    public async Task<string> HandleAsync(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        // Custom response processing logic
        return content;
    }
}

//Register in module
Configure<AbpDaprClientProxyOptions>(options =>
{
    options.OnResponse(async (response, sp) =>
    {
        return await new CustomResponseHandler().HandleAsync(response);
    });
});
```

## Notes

* The remote service interface must inherit `IRemoteService`
* Configuration changes require re-creating the proxy instance to take effect
* It is recommended to configure appropriate timeout and retry strategies
* Error handling should consider network abnormalities and service unavailability
* Service discovery should be enabled in production environments
* It is recommended to use health checks to ensure service availability
* Request header configuration should consider security and authentication requirements
* Logging is important for problem diagnosis

[View in English](README.EN.md)
