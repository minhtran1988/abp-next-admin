# LINGYUN.Abp.DistributedLocking.Dapr

ABP distributed lock implementation based on Dapr distributed lock API. This module provides seamless integration with the Dapr distributed lock service and supports cross-service and cross-instance distributed locking functions.

Reference document: [Dapr Distributed Lock API](https://docs.dapr.io/developing-applications/building-blocks/distributed-lock/distributed-lock-api-overview/)

## Features

* Integrated with ABP distributed lock system
* Support custom lock resource owner identification
* Supports configurable lock timeout
* Support automatic release of lock resources
* Supports a variety of lock storage components
* Support event notification for lock acquisition and release
* Support health check of distributed locks

## Configuration options

```json
{
    "DistributedLocking": {
        "Dapr": {
            "StoreName": "lockstore", // The storage name defined in the Dapr component
            "DefaultIdentifier": "dapr-lock-owner", //Default lock resource owner identification
            "DefaultTimeout": "00:00:30" //Default lock timeout (30 seconds)
        }
    }
}
```

## Basic usage

### 1. Module configuration

```csharp
[DependsOn(typeof(AbpDistributedLockingDaprModule))]
public class YourProjectModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        //Basic configuration
        Configure<AbpDistributedLockingDaprOptions>(options =>
        {
            options.StoreName = "redis-lock"; // Use Redis as lock storage
            options.DefaultIdentifier = "my-service"; // Customize the lock owner identification
            options.DefaultTimeout = TimeSpan.FromMinutes(1); // Set the default timeout to 1 minute
        });
    }
}
```

### 2. Basic usage

```csharp
public class OrderService
{
    private readonly IDistributedLockProvider _lockProvider;

    public OrderService(IDistributedLockProvider lockProvider)
    {
        _lockProvider = lockProvider;
    }

    public async Task ProcessOrderAsync(string orderId)
    {
        //Try to acquire the lock
        using (var handle = await _lockProvider.TryAcquireAsync($"order:{orderId}"))
        {
            if (handle != null)
            {
                try
                {
                    // Execute business logic that requires locking
                    await ProcessOrderInternalAsync(orderId);
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    _logger.LogError(ex, "An error occurred while processing the order");
                    throw;
                }
            }
            else
            {
                throw new ConcurrencyException("The order is being processed by another process");
            }
        }
    }
}
```

### 3. Advanced usage

```csharp
public class InventoryService
{
    private readonly IDistributedLockProvider _lockProvider;
    private readonly ILogger<InventoryService> _logger;

    public InventoryService(
        IDistributedLockProvider lockProvider,
        ILogger<InventoryService> logger)
    {
        _lockProvider = lockProvider;
        _logger = logger;
    }

    public async Task UpdateInventoryAsync(string productId, int quantity)
    {
        // Custom lock configuration
        var lockOptions = new DistributedLockOptions
        {
            Timeout = TimeSpan.FromSeconds(10), // Custom timeout time
            RetryDelay = TimeSpan.FromMilliseconds(100) //Retry delay
        };

        try
        {
            using (var handle = await _lockProvider.TryAcquireAsync(
                $"inventory:{productId}",
                lockOptions))
            {
                if (handle == null)
                {
                    _logger.LogWarning("Unable to obtain inventory lock, product ID: {ProductId}", productId);
                    throw new ConcurrencyException("Unable to acquire inventory lock");
                }

                //Perform inventory update operation
                await UpdateInventoryInternalAsync(productId, quantity);
            }
        }
        catch (Exception ex) when (ex is not ConcurrencyException)
        {
            _logger.LogError(ex, "An error occurred while updating inventory");
            throw;
        }
    }
}
```

## Component configuration

### Redis lock storage configuration example

```yaml
apiVersion: dapr.io/v1alpha1
kind: Component
metadata:
  name: redis-lock # Corresponding to StoreName configuration
spec:
  type: lock.redis
  version: v1
  metadata:
    - name: redisHost
      value: localhost:6379
    - name: redisPassword
      value: ""
    - name: enableTLS
      value: false
    - name: maxRetries
      value: 5
    - name: maxRetryBackoff
      value: 5s
```

### Consul lock storage configuration example

```yaml
apiVersion: dapr.io/v1alpha1
kind: Component
metadata:
  name: consul-lock
spec:
  type: lock.consul
  version: v1
  metadata:
    - name: host
      value: localhost:8500
    - name: sessionTTL
      value: 10
    - name: scheme
      value: http
```

## Core interface

### ILockOwnerFinder

Provides an interface for lock resource holder identification.

```csharp
public interface ILockOwnerFinder
{
    string GetOwner();
}
```

The default implementation of `LockOwnerFinder` will:
1. Prioritize using the current user ID as the lock owner ID
2. If the user is not logged in, use the configured `DefaultIdentifier`

### Implementation of custom lock owner identification

```csharp
public class CustomLockOwnerFinder : ILockOwnerFinder
{
    private readonly ICurrentTenant _currentTenant;

    public CustomLockOwnerFinder(ICurrentTenant currentTenant)
    {
        _currentTenant = currentTenant;
    }

    public string GetOwner()
    {
        // Use the tenant ID and machine name combination as the lock owner ID
        return $"{_currentTenant.Id ?? "host"}-{Environment.MachineName}";
    }
}

//Register custom implementation
context.Services.AddTransient<ILockOwnerFinder, CustomLockOwnerFinder>();
```

## Best Practices

1. **Set the timeout reasonably**
   - Set an appropriate timeout based on the expected execution time of the business operation
   - Avoid setting too long timeouts to prevent deadlocks

2. **Correct lock granularity**
   - The scope of the lock should be as small as possible, locking only necessary resources
   - Avoid holding locks for a long time and release them promptly

3. **Exception handling**
   - Always use locks in using blocks
   - Properly handle lock acquisition failure
   - Record key lock operation logs

4. **Performance Optimization**
   - Use appropriate storage components
   - Configure appropriate retry strategies
   - Monitor lock usage

## Notes

* Make sure Dapr Sidecar is correctly configured and running
* The distributed lock component needs to be correctly defined in the Dapr configuration
* Set the lock timeout reasonably to avoid deadlock
* Correctly handle lock acquisition failure
* Pay attention to the performance impact in high concurrency scenarios
* It is recommended to configure the health check of the lock component
* It is recommended to add log records for important operations

[View in English](README.EN.md)
