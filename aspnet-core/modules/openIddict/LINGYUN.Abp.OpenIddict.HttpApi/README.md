# LINGYUN.Abp.OpenIddict.HttpApi

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2Fcolinin%2Fabp-next-admin%2Fmaster%2Faspnet-core%2Fmodules%2FopenIddict%2FLINGYUN.Abp.OpenIddict.HttpApi%2FLINGYUN.Abp.OpenIddict.HttpApi.csproj)](https://abp.io)
[![NuGet](https://img.shields.io/nuget/v/LINGYUN.Abp.OpenIddict.HttpApi.svg?style=flat-square)](https://www.nuget.org/packages/LINGYUN.Abp.OpenIddict.HttpApi)

## Introduction

`LINGYUN.Abp.OpenIddict.HttpApi` is the HTTP API module of OpenIddict, which provides a RESTful API interface for OpenIddict related functions.

[English](./README.EN.md)

## Features

* OpenIddict application management
  * Create, update, delete applications
  * Query application list
  * Get application details

* OpenIddict authorization management
  * Query authorization list
  * Get authorization details
  * Delete authorization record

* OpenIddict token management
  * Query token list
  * Get token details
  * Delete token record

* OpenIddict scope management
  * Create, update, delete scopes
  * Query scope list
  * Get scope details

*Multi-language support
  * Integrate ABP localization framework
  * Support custom localized resources

## Installation

```bash
dotnet add package LINGYUN.Abp.OpenIddict.HttpApi
```

## Use

1. Add `[DependsOn(typeof(AbpOpenIddictHttpApiModule))]` to your module class.

2. Configure permissions:

```csharp
public override void ConfigureServices(ServiceConfigurationContext context)
{
    Configure<AbpPermissionOptions>(options =>
    {
        options.ValueProviders.Add<OpenIddictPermissionValueProvider>();
    });
}
```

3. API interface usage examples:

```csharp
//Inject service
private readonly IOpenIddictApplicationAppService _applicationService;

public YourService(IOpenIddictApplicationAppService applicationService)
{
    _applicationService = applicationService;
}

//Create application
var input = new OpenIddictApplicationCreateDto
{
    ClientId = "your-client-id",
    DisplayName = "Your App",
    // ...other properties
};
var result = await _applicationService.CreateAsync(input);

// Query application list
var query = new OpenIddictApplicationGetListInput
{
    MaxResultCount = 10,
    SkipCount = 0,
    Filter = "search-term"
};
var list = await _applicationService.GetListAsync(query);
```

## Permissions

* OpenIddict.Applications
  * OpenIddict.Applications.Create
  * OpenIddict.Applications.Update
  * OpenIddict.Applications.Delete
  * OpenIddict.Applications.ManagePermissions

* OpenIddict.Scopes
  * OpenIddict.Scopes.Create
  * OpenIddict.Scopes.Update
  * OpenIddict.Scopes.Delete
  * OpenIddict.Scopes.ManagePermissions

* OpenIddict.Authorizations
  * OpenIddict.Authorizations.Delete
  * OpenIddict.Authorizations.ManagePermissions

* OpenIddict.Tokens
  * OpenIddict.Tokens.Delete
  * OpenIddict.Tokens.ManagePermissions

## Notes

* All API interfaces require corresponding permissions to access
* Deleting the application will also delete related authorizations and tokens
* API interface supports multi-tenant scenarios
* It is recommended to enable API authentication and authorization in production environments
