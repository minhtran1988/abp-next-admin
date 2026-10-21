# LINGYUN.Abp.OpenIddict.LinkUser

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2Fcolinin%2Fabp-next-admin%2Fmaster%2Faspnet-core%2Fmodules%2FopenIddict%2FLINGYUN.Abp.OpenIddict.LinkUser%2FLINGYUN.Abp.OpenIddict.LinkUser.csproj)](https://abp.io)
[![NuGet](https://img.shields.io/nuget/v/LINGYUN.Abp.OpenIddict.LinkUser.svg?style=flat-square)](https://www.nuget.org/packages/LINGYUN.Abp.OpenIddict.LinkUser)

## Introduction

`LINGYUN.Abp.OpenIddict.LinkUser` is the user link extension module of OpenIddict, which provides link authentication function between users.

[English](./README.EN.md)

## Features

* User link authentication
  * Support link authentication between users
  * Support cross-tenant user linking
  * Support access token exchange

* Extended authorization types
  * Add link_user authorization type
  * Support custom authorization process
  * Security logging

*Multi-language support
  * Integrate ABP localization framework
  * Support custom error messages

## Installation

```bash
dotnet add package LINGYUN.Abp.OpenIddict.LinkUser
```

## Use

1. Add `[DependsOn(typeof(AbpOpenIddictLinkUserModule))]` to your module class.

2. Configure OpenIddict server:

```csharp
public override void PreConfigureServices(ServiceConfigurationContext context)
{
    PreConfigure<OpenIddictServerBuilder>(builder =>
    {
        //Allow link_user authorization type
        builder.AllowLinkUserFlow();
    });
}
```

3. Usage example:

```http
POST /connect/token
Content-Type: application/x-www-form-urlencoded

grant_type=link_user&
access_token=current-user-access-token&
LinkUserId=target-user-id&
LinkTenantId=target-tenant-id&
scope=openid profile
```

## Parameter description

* access_token (required)
  * The access token of the current user
  * Used to verify the identity of the current user

* LinkUserId (required)
  * ID of target link user
  * Must be in valid GUID format

* LinkTenantId (optional)
  * Tenant ID to which the target link user belongs
  * If specified, must be in valid GUID format

* scope (optional)
  * Requested permission scope
  * Includes openid profile by default

## Notes

* A valid access token must be provided
* The target user must have a link relationship with the current user
* You need to specify the correct tenant ID when linking across tenants
* All operations will be recorded in security logs
* It is recommended to use HTTPS in production environments
