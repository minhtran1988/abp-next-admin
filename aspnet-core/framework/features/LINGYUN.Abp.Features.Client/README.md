# LINGYUN.Abp.Features.Client

Functional verification for clients

## Features

* Provide client feature value provider (ClientFeatureValueProvider)
* Support functional verification based on client ID
* Seamlessly integrated with the function management system of the ABP framework

## Configuration usage

1. Add module dependencies

```csharp
[DependsOn(typeof(AbpFeaturesClientModule))]
public class YouProjectModule : AbpModule
{
    // other
}
```

2. Usage examples

```csharp
public class YourService
{
    private readonly IFeatureChecker _featureChecker;

    public YourService(IFeatureChecker featureChecker)
    {
        _featureChecker = featureChecker;
    }

    public async Task DoSomethingAsync()
    {
        // Check whether the client has a function enabled
        if (await _featureChecker.IsEnabledAsync("YourFeature"))
        {
            // business logic
        }
    }
}
```

## More

* This module is mainly used for client function verification, usually used in conjunction with the LINGYUN.Abp.FeatureManagement.Client module
* The name of the client function value provider is "C"

[Simplified Chinese](./README.md) | [English](./README.EN.md)
