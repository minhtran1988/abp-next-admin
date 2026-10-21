# LINGYUN.Abp.OpenIddict.WeChat.Work

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2Fcolinin%2Fabp-next-admin%2Fmaster%2Faspnet-core%2Fmodules%2FopenIddict%2FLINGYUN.Abp.OpenIddict.WeChat.Work%2FLINGYUN.Abp.OpenIddict.WeChat.Work.csproj)](https://abp.io)
[![NuGet](https://img.shields.io/nuget/v/LINGYUN.Abp.OpenIddict.WeChat.Work.svg?style=flat-square)](https://www.nuget.org/packages/LINGYUN.Abp.OpenIddict.WeChat.Work)

## Introduction

`LINGYUN.Abp.OpenIddict.WeChat.Work` is an Enterprise WeChat authentication extension module based on OpenIddict that supports the Enterprise WeChat authentication process.

[English](./README.EN.md)

## Function

* Enterprise WeChat certification
  * Support enterprise WeChat authorized login
  * Support automatic registration of new users
  * Support cross-tenant authentication
  * Support security logging

* User management integration
  * Support enterprise WeChat account binding
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
dotnet add package LINGYUN.Abp.OpenIddict.WeChat.Work
```

## Use

1. Add `[DependsOn(typeof(AbpOpenIddictWeChatWorkModule))]` to your module class.

2. Configure the OpenIddict server:

```csharp
public override void PreConfigureServices(ServiceConfigurationContext context)
{
    PreConfigure<OpenIddictServerBuilder>(builder =>
    {
        // Allow enterprise WeChat authentication process
        builder.AllowWeChatWorkFlow();
    });
}
```

3. Usage examples:

Enterprise WeChat certification:
```http
POST /connect/token
Content-Type: application/x-www-form-urlencoded

grant_type=wechat_work&
agent_id=1000001&
code=021iZ1Ga1TpLOB0dXJIa1Zr6RR1iZ1G7&
scope=openid profile wechat_work
```

## Parameters

* agent_id (required)
  * Enterprise WeChat application ID
  * Must be a valid enterprise WeChat application ID

* code (required)
  * Enterprise WeChat authorization code
  * Must be a valid enterprise WeChat authorization code

* scope (optional)
  * Requested permission scope
  * Contains openid profile wechat_work by default

## Error code

* invalid_grant
  * GrantTypeInvalid - Grant type not allowed
  * WeChatTokenInvalid - Enterprise WeChat authentication failed
  *AgentIdOrCodeNotFound - the application ID or authorization code is empty or does not exist
  * UserIdNotRegister - the user's corporate WeChat account is not bound

## Notes

* The correct Enterprise WeChat application ID and key must be configured
* The correct corporate WeChat corporate ID must be configured
*The authorization code has a limited validity period
* Multiple authentication failures may result in account lockout
* All authentication operations will be recorded in the security log
* It is recommended to use HTTPS in production environment
