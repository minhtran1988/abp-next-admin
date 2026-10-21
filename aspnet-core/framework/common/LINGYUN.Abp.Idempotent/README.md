# LINGYUN.Abp.Idempotent

The interface idempotence checking module is used to prevent repeated submission of interfaces and ensure the idempotence of interface calls.

## Function

* Automatic idempotence check
* Support custom idempotent key generation
* Flexible timeout configuration
* Support distributed locks
* Support multi-language error messages
* Support ignoring specific interfaces or methods

## Installation

```bash
dotnet add package LINGYUN.Abp.Idempotent
```

## Configuration usage

```csharp
[DependsOn(typeof(AbpIdempotentModule))]
public class YouProjectModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpIdempotentOptions>(options =>
        {
            // Enable idempotent checking globally
            options.IsEnabled = true;
            // Each interface provides a 5 second timeout by default
            options.DefaultTimeout = 5000;
            // Idempotent token name, passed through HttpHeader
            options.IdempotentTokenName = "X-With-Idempotent-Token";
            //Http response code when idempotent verification fails
            options.HttpStatusCode = 429;
        });
    }
}
```

## Configuration item description

* `IsEnabled` - Whether to enable idempotent checking, default: false
* `DefaultTimeout` - Default idempotence check timeout (milliseconds), default: 5000
* `IdempotentTokenName` - HTTP header name of idempotent Token, default: X-With-Idempotent-Token
* `HttpStatusCode` - HTTP status code when idempotence check fails, default: 429 (Too Many Requests)

## Usage example

### 1. Basic usage

```csharp
[Idempotent]
public class OrderAppService : ApplicationService
{
    public async Task<OrderDto> CreateAsync(CreateOrderDto input)
    {
        // The method will automatically check for idempotence
        return await _orderRepository.CreateAsync(input);
    }
}
```

### 2. Custom idempotent keys

```csharp
[Idempotent(
    iodempotentKey: "custom-key", 
    timeout: 10000,
    keyMap: new[] { "orderId", "userId" })]
public async Task<OrderDto> UpdateAsync(UpdateOrderDto input)
{
    return await _orderRepository.UpdateAsync(input);
}
```

### 3. Ignore idempotence checks

```csharp
[IgnoreIdempotent]
public async Task<OrderDto> QueryAsync(string orderId)
{
    return await _orderRepository.GetAsync(orderId);
}
```

## Notes

1. Idempotence checking is enabled by default for all services inherited from `ICreateAppService`, `IUpdateAppService` and `IDeleteAppService`
2. You can ignore the idempotence check of a specific method through the `[IgnoreIdempotent]` attribute
3. Idempotence check is implemented based on distributed locks to ensure correctness in a distributed environment
4. It is recommended to enable idempotence checking on all interfaces that modify data.

## Link

* [English document](./README.EN.md)
