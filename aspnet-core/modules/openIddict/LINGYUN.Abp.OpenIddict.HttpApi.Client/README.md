# LINGYUN.Abp.OpenIddict.HttpApi.Client

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2Fcolinin%2Fabp-next-admin%2Fmaster%2Faspnet-core%2Fmodules%2FopenIddict%2FLINGYUN.Abp.OpenIddict.HttpApi.Client%2FLINGYUN.Abp.OpenIddict.HttpApi.Client.csproj)](https://abp.io)
[![NuGet](https://img.shields.io/nuget/v/LINGYUN.Abp.OpenIddict.HttpApi.Client.svg?style=flat-square)](https://www.nuget.org/packages/LINGYUN.Abp.OpenIddict.HttpApi.Client)

## Introduction

`LINGYUN.Abp.OpenIddict.HttpApi.Client` is the HTTP API client module of OpenIddict, which provides a client proxy for remotely calling the OpenIddict HTTP API.

[English](./README.EN.md)

## Features

* HTTP API client proxy
  * Automatically generate HTTP client proxy
  * Support remote service calling
  * Integrated ABP dynamic HTTP client proxy

* Remote service configuration
  * Support configuring remote service address
  *Support configuration authentication method
  * Support configuration request header

## Installation

```bash
dotnet add package LINGYUN.Abp.OpenIddict.HttpApi.Client
```

## Use

1. Add `[DependsOn(typeof(AbpOpenIddictHttpApiClientModule))]` to your module class.

2. Configure remote service:

```csharp
public override void ConfigureServices(ServiceConfigurationContext context)
{
    var configuration = context.Services.GetConfiguration();
    
    Configure<AbpRemoteServiceOptions>(options =>
    {
        options.RemoteServices.Default = new RemoteServiceConfiguration
        {
            BaseUrl = configuration["RemoteServices:Default:BaseUrl"]
        };
    });
}
```

3. Usage example:

```csharp
//Inject client proxy
private readonly IOpenIddictApplicationAppService _applicationService;

public YourService(IOpenIddictApplicationAppService applicationService)
{
    _applicationService = applicationService;
}

// Call remote service
var input = new OpenIddictApplicationCreateDto
{
    ClientId = "your-client-id",
    DisplayName = "Your App",
    // ...other properties
};
var result = await _applicationService.CreateAsync(input);
```

## Configuration items

* RemoteServices
  * Default:BaseUrl - the default remote service address
  * OpenIddict:BaseUrl - OpenIddict remote service address

## Notes

* Need to configure the correct remote service address
* If the remote service requires authentication, the corresponding authentication information needs to be configured
* It is recommended to use HTTPS in production environments
* The client agent will automatically handle the delivery of the authentication token
