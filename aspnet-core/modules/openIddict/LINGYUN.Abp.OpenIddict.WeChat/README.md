# LINGYUN.Abp.OpenIddict.WeChat

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2Fcolinin%2Fabp-next-admin%2Fmaster%2Faspnet-core%2Fmodules%2FopenIddict%2FLINGYUN.Abp.OpenIddict.WeChat%2FLINGYUN.Abp.OpenIddict.WeChat.csproj)](https://abp.io)
[![NuGet](https://img.shields.io/nuget/v/LINGYUN.Abp.OpenIddict.WeChat.svg?style=flat-square)](https://www.nuget.org/packages/LINGYUN.Abp.OpenIddict.WeChat)

## Introduction

`LINGYUN.Abp.OpenIddict.WeChat` is a WeChat authentication extension module based on OpenIddict that supports the authentication process of WeChat public platform and WeChat applet.

[English](./README.EN.md)

## Function

* WeChat public platform certification
  * Support official account authorized login
  * Support automatic registration of new users
  * Support cross-tenant authentication
  * Support security logging

* WeChat Mini Program Certification
  * Support mini program authorized login
  * Support automatic registration of new users
  * Support cross-tenant authentication
  * Support security logging

* User management integration
  * Support WeChat account binding
  *Support account locking strategy
  * Support failed attempt counting

* Security log
  * Log login attempts
  * Record authentication failure
  * Record account lock

* Localization support
  * Integrate ABP localization framework
  * Support custom error messages

## Installation

```bash
dotnet add package LINGYUN.Abp.OpenIddict.WeChat
```

## Use

1. Add `[DependsOn(typeof(AbpOpenIddictWeChatModule))]` to your module class.

2. Configure the OpenIddict server:

```csharp
public override void PreConfigureServices(ServiceConfigurationContext context)
{
    PreConfigure<OpenIddictServerBuilder>(builder =>
    {
        // Allow WeChat authentication process
        builder.AllowWeChatFlow();
    });
}
```

3. Usage examples:

WeChat public platform certification:
```http
POST /connect/token
Content-Type: application/x-www-form-urlencoded

grant_type=wechat_official&
code=021iZ1Ga1TpLOB0dXJIa1Zr6RR1iZ1G7&
scope=openid profile wechat
```

WeChat Mini Program Certification:
```http
POST /connect/token
Content-Type: application/x-www-form-urlencoded

grant_type=wechat_mini_program&
code=021iZ1Ga1TpLOB0dXJIa1Zr6RR1iZ1G7&
scope=openid profile wechat
```

## Parameters

* code (required)
  * WeChat authorization code
  * Must be a valid WeChat authorization code

* scope (optional)
  * Requested permission scope
  * Contains openid profile wechat by default

## Error code

* invalid_grant
  * GrantTypeInvalid - Grant type not allowed
  * WeChatTokenInvalid - WeChat authentication failed
  * WeChatCodeNotFound - WeChat authorization code is empty or does not exist
  * WeChatNotRegister - The user's WeChat account is not bound

## Notes

* Corresponding functions must be enabled (public platform or mini program authorization)
* The correct WeChat application ID and key must be configured
*The authorization code has a limited validity period
* Multiple authentication failures may result in account lockout
* All authentication operations will be recorded in the security log
* It is recommended to use HTTPS in production environment
