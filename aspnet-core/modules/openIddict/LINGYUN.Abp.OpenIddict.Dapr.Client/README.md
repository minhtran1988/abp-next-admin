# LINGYUN.Abp.OpenIddict.Dapr.Client

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2Fcolinin%2Fabp-next-admin%2Fmaster%2Faspnet-core%2Fmodules%2FopenIddict%2FLINGYUN.Abp.OpenIddict.Dapr.Client%2FLINGYUN.Abp.OpenIddict.Dapr.Client.csproj)](https://abp.io)
[![NuGet](https://img.shields.io/nuget/v/LINGYUN.Abp.OpenIddict.Dapr.Client.svg?style=flat-square)](https://www.nuget.org/packages/LINGYUN.Abp.OpenIddict.Dapr.Client)

## Introduction

`LINGYUN.Abp.OpenIddict.Dapr.Client` is a Dapr-based OpenIddict client module that provides the functionality to call the OpenIddict remote service using the Dapr service invocation building block.

[English](./README.EN.md)

## Function

* Dapr service call integration
  * Automatically register the Dapr client proxy for the OpenIddict application contract
  * Support access to OpenIddict remote service through Dapr service call
  * Support inter-service communication in distributed systems

* Remote service support
  * Supports all services defined by the OpenIddict application contract
  * Support application management
  *Support authorization management
  * Support scope management
  * Support token management

## Installation

```bash
dotnet add package LINGYUN.Abp.OpenIddict.Dapr.Client
```

## Use

1. Add `[DependsOn(typeof(AbpOpenIddictDaprClientModule))]` to your module class.

2. Configure Dapr service call:

```csharp
public override void ConfigureServices(ServiceConfigurationContext context)
{
    Configure<AbpDaprClientOptions>(options =>
    {
        options.ApplicationServices.Configure(config =>
        {
            // Configure the AppId of the OpenIddict service
            config.AppId = "openiddict-service";
        });
    });
}
```

3. Usage examples:

```csharp
public class MyService
{
    private readonly IOpenIddictApplicationAppService _applicationAppService;

    public MyService(IOpenIddictApplicationAppService applicationAppService)
    {
        _applicationAppService = applicationAppService;
    }

    public async Task DoSomethingAsync()
    {
        //Access the OpenIddict application service via Dapr service call
        var applications = await _applicationAppService.GetListAsync(
            new GetApplicationsInput());
    }
}
```

## Configuration

* AppId
  * Application identifier for the OpenIddict service
  * Must match the app ID in the Dapr component configuration

* RemoteServiceName
  * OpenIddict The name of the remote service
  * The default value is "OpenIddict"

## Notes

* Make sure Dapr Sidecar is configured and running correctly
* Make sure the OpenIddict service is registered in Dapr
* It is recommended to configure identity authentication between services in the production environment
* It is recommended to configure the retry strategy of service calls
* It is recommended to configure the service discovery mechanism
