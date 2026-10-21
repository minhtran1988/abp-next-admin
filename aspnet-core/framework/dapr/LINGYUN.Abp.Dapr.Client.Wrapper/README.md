# LINGYUN.Abp.Dapr.Client.Wrapper

Call between Dapr services and unpack the packaged response results

## Features

* Automatic response result packaging/unpacking
* Integrated with ABP packaging system
* Error handling of custom packaged responses
* Support success/error code configuration
* HTTP status code mapping
* Support custom response packaging format
* Flexible packaging control options

## Configuration usage

Module reference on demand

```csharp
[DependsOn(typeof(AbpDaprClientModule))]
public class AbpDaprClientWrapperModule : AbpModule
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

1. Service definition

```csharp
public interface IProductService
{
    Task<ProductDto> GetAsync(string id);
    Task<List<ProductDto>> GetListAsync();
    Task<ProductDto> CreateAsync(CreateProductDto input);
}
```

2. Service implementation

```csharp
public class ProductService : IProductService
{
    private readonly DaprClient _daprClient;

    public ProductService(DaprClient daprClient)
    {
        _daprClient = daprClient;
    }

    public async Task<ProductDto> GetAsync(string id)
    {
        // The call will automatically handle response packaging
        return await _daprClient.InvokeMethodAsync<ProductDto>(
            "product-service", // Target service ID
            $"api/products/{id}", // method path
            HttpMethod.Get
        );
    }

    public async Task<List<ProductDto>> GetListAsync()
    {
        return await _daprClient.InvokeMethodAsync<List<ProductDto>>(
            "product-service", 
            "api/products",
            HttpMethod.Get
        );
    }

    public async Task<ProductDto> CreateAsync(CreateProductDto input)
    {
        return await _daprClient.InvokeMethodAsync<ProductDto>(
            "product-service",
            "api/products",
            HttpMethod.Post,
            input
        );
    }
}
```

## Response format

When wrapping is enabled, the response will be in the following format:

```json
{
    "code": "0", //Response code, default "0" means success
    "message": "Success", // response message
    "details": null, // Additional details (optional)
    "result": { // actual response data
        // ...response content
    }
}
```

## Error handling

The module handles error responses automatically:
* For wrapped responses (with `AbpWrapResult` header):
  * Unpack the response and inspect the code
  * If the code does not match `CodeWithSuccess`, throw `AbpRemoteCallException`
  * Include error message, details and code in exception
  * Support custom error code mapping
* For unwrapped responses:
  * Pass the original response
  * Use standard HTTP error handling
  * Keep original error message

### Error response example

```json
{
    "code": "ERROR_001",
    "message": "Product not found",
    "details": "The product with ID '123' does not exist",
    "result": null
}
```

## Advanced usage

### 1. Control response packaging

Response packaging for individual requests can be controlled via HTTP headers:

```csharp
//Add in request header
var headers = new Dictionary<string, string>
{
    { "X-Abp-Wrap-Result", "true" }, // Force wrapping to be enabled
    // or
    { "X-Abp-Dont-Wrap-Result", "true" } // Forcefully disable wrapping
};

await _daprClient.InvokeMethodAsync<ProductDto>(
    "product-service",
    "api/products",
    HttpMethod.Get,
    null,
    headers
);
```

### 2. Custom error handling

```csharp
public class CustomErrorHandler : IAbpWrapperErrorHandler
{
    public Task HandleAsync(AbpWrapperErrorContext context)
    {
        // Custom error handling logic
        if (context.Response.Code == "CUSTOM_ERROR")
        {
            //Special handling
        }
        return Task.CompletedTask;
    }
}
```

## Notes

* Response wrapping can be controlled via:
  * Global settings in configuration files
  * HTTP headers control individual requests
  * Dynamic control in code
* Error responses keep the original error structure as much as possible
* The module is integrated with ABP's remote service error handling system
* It is recommended to use response packaging uniformly in microservice architecture
* The packaging format can be customized by inheriting `IAbpWrapperResponseBuilder`

[View in English](README.EN.md)
