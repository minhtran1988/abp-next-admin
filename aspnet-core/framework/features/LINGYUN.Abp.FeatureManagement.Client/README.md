# LINGYUN.Abp.FeatureManagement.Client

Functional verification management authorization for clients

## Features

* Provide client feature management provider (ClientFeatureManagementProvider)
* Support permission management of client functions
* Support localized resource management
* Seamlessly integrated with the function management module of the ABP framework

## Configuration usage

1. Add module dependencies

```csharp
[DependsOn(typeof(AbpFeatureManagementClientModule))]
public class YouProjectModule : AbpModule
{
    // other
}
```

2. Permission configuration

The module comes predefined with the following permissions:
* FeatureManagement.ManageClientFeatures: Permission to manage client features

3. Usage examples

```csharp
public class YourService
{
    private readonly IFeatureManager _featureManager;

    public YourService(IFeatureManager featureManager)
    {
        _featureManager = featureManager;
    }

    public async Task SetClientFeatureAsync(string clientId, string featureName, string value)
    {
        //Set client function value
        await _featureManager.SetForClientAsync(clientId, featureName, value);
    }
}
```

## More

* This module depends on the LINGYUN.Abp.Features.Client module
* Support localized configuration through AbpFeatureManagementResource
* Provides permission control for client function management

[Simplified Chinese](./README.md) | [English](./README.EN.md)
