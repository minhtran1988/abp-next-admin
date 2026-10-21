# LINGYUN.Abp.OpenIddict.AspNetCore

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2Fcolinin%2Fabp-next-admin%2Fmaster%2Faspnet-core%2Fmodules%2FopenIddict%2FLINGYUN.Abp.OpenIddict.AspNetCore%2FLINGYUN.Abp.OpenIddict.AspNetCore.csproj)](https://abp.io)
[![NuGet](https://img.shields.io/nuget/v/LINGYUN.Abp.OpenIddict.AspNetCore.svg?style=flat-square)](https://www.nuget.org/packages/LINGYUN.Abp.OpenIddict.AspNetCore)

## Introduction

`LINGYUN.Abp.OpenIddict.AspNetCore` is an ASP.NET Core integrated module based on ABP OpenIddict, which provides user information expansion and avatar URL declaration support.

[English](./README.EN.md)

## Features

* Extended user information endpoint
  * Support returning user avatar URL
  * Extended standard OpenID Connect statement
  *Support multi-tenancy

* Avatar URL declaration contributor
  * Automatically add user avatar URL statement
  * Integrate with identity systems

* OpenIddict server configuration
  * Pre-configured avatar URL declaration support
  * Inherited from Volo.Abp.OpenIddict.AbpOpenIddictAspNetCoreModule

## Installation

```bash
dotnet add package LINGYUN.Abp.OpenIddict.AspNetCore
```

## Use

1. Add `[DependsOn(typeof(AbpOpenIddictAspNetCoreModule))]` to your module class.

2. The user information endpoint will automatically expand to include the following declaration:
   * sub (user ID)
   * tenant_id (tenant ID, if multi-tenancy is enabled)
   * preferred_username (username)
   * family_name (last name)
   * given_name (name)
   * picture (avatar URL)
   * email
   * email_verified
   * phone_number
   * phone_number_verified
   * role

3. The avatar URL statement is automatically added to the user's identity statement.

## Configuration

By default, the module is preconfigured with the required settings. If you need to customize the configuration, you can modify it in the `PreConfigureServices` method of the module:

```csharp
public override void PreConfigureServices(ServiceConfigurationContext context)
{
    PreConfigure<OpenIddictServerBuilder>(builder =>
    {
        // Custom configuration
        builder.RegisterClaims(new[] { "your_custom_claim" });
    });
}
```

## Notes

* The type of avatar URL declaration is `IdentityConsts.ClaimType.Avatar.Name`
* The user information endpoint requires corresponding scope permissions to return information:
  * profile: basic information and avatar
  * email: email related information
  * phone: phone number related information
  * roles: user role information
