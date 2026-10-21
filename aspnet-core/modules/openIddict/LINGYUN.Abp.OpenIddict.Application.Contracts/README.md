# LINGYUN.Abp.OpenIddict.Application.Contracts

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2Fcolinin%2Fabp-next-admin%2Fmaster%2Faspnet-core%2Fmodules%2FopenIddict%2FLINGYUN.Abp.OpenIddict.Application.Contracts%2FLINGYUN.Abp.OpenIddict.Application.Contracts.csproj)](https://abp.io)
[![NuGet](https://img.shields.io/nuget/v/LINGYUN.Abp.OpenIddict.Application.Contracts.svg?style=flat-square)](https://www.nuget.org/packages/LINGYUN.Abp.OpenIddict.Application.Contracts)

## Introduction

`LINGYUN.Abp.OpenIddict.Application.Contracts` is the contract layer of the OpenIddict application service and defines the interfaces, DTOs and permissions required for OpenIddict management.

[English](./README.EN.md)

## Features

* Define OpenIddict application service interface
  * IOpenIddictApplicationAppService
  * IOpenIddictAuthorizationAppService
  * IOpenIddictTokenAppService
  * IOpenIddictScopeAppService

* Provide standardized DTO objects
  * OpenIddictApplicationDto
  * OpenIddictAuthorizationDto
  * OpenIddictTokenDto
  * OpenIddictScopeDto
  * And the corresponding creation and update DTO

* Permission definition
  * OpenIddict.Applications
  * OpenIddict.Authorizations
  * OpenIddict.Tokens
  * OpenIddict.Scopes

*Multi-language support
  * Built-in Chinese and English localization resources
  * Support custom language extensions

## Installation

```bash
dotnet add package LINGYUN.Abp.OpenIddict.Application.Contracts
```

## Use

1. Add `[DependsOn(typeof(AbpOpenIddictApplicationContractsModule))]` to your module class.

2. Inject and use the corresponding application service interface:

```csharp
public class YourService
{
    private readonly IOpenIddictApplicationAppService _applicationAppService;
    
    public YourService(IOpenIddictApplicationAppService applicationAppService)
    {
        _applicationAppService = applicationAppService;
    }
    
    public async Task DoSomethingAsync()
    {
        var applications = await _applicationAppService.GetListAsync(
            new OpenIddictApplicationGetListInput());
        // ...
    }
}
```

## Permissions

The module defines the following permissions:

* OpenIddict.Applications
  * OpenIddict.Applications.Create
  * OpenIddict.Applications.Update
  * OpenIddict.Applications.Delete
  * OpenIddict.Applications.ManagePermissions
  * OpenIddict.Applications.ManageSecret
* OpenIddict.Authorizations
  * OpenIddict.Authorizations.Delete
* OpenIddict.Scopes
  * OpenIddict.Scopes.Create
  * OpenIddict.Scopes.Update
  * OpenIddict.Scopes.Delete
* OpenIddict.Tokens
  * OpenIddict.Tokens.Delete

## Localization

The module supports multiple languages and has the following built-in languages:

* English (en)
* Simplified Chinese (zh-Hans)

New languages can be extended in the following ways:

```csharp
Configure<AbpLocalizationOptions>(options =>
{
    options.Resources
        .Get<AbpOpenIddictResource>()
        .AddVirtualJson("/YourPath/Localization/Resources");
});
```
