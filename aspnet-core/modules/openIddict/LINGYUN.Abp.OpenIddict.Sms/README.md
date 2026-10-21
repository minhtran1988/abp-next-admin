# LINGYUN.Abp.OpenIddict.Sms

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2Fcolinin%2Fabp-next-admin%2Fmaster%2Faspnet-core%2Fmodules%2FopenIddict%2FLINGYUN.Abp.OpenIddict.Sms%2FLINGYUN.Abp.OpenIddict.Sms.csproj)](https://abp.io)
[![NuGet](https://img.shields.io/nuget/v/LINGYUN.Abp.OpenIddict.Sms.svg?style=flat-square)](https://www.nuget.org/packages/LINGYUN.Abp.OpenIddict.Sms)

## Introduction

`LINGYUN.Abp.OpenIddict.Sms` is the SMS verification extension module of OpenIddict, which provides authentication functions based on mobile phone numbers and SMS verification codes.

[English](./README.EN.md)

## Features

* SMS verification code authentication
  *Support mobile phone number login
  *Support SMS verification code verification
  * Support multi-tenant authentication

* User management integration
  *Support mobile phone number to find users
  *Support account locking policy
  * Support failed attempt counting

* Security log
  * Log login attempts
  * Record verification code verification failure
  * Record account lock

*Multi-language support
  * Integrate ABP localization framework
  * Support custom error messages

## Installation

```bash
dotnet add package LINGYUN.Abp.OpenIddict.Sms
```

## Use

1. Add `[DependsOn(typeof(AbpOpenIddictSmsModule))]` to your module class.

2. Configure OpenIddict server:

```csharp
public override void PreConfigureServices(ServiceConfigurationContext context)
{
    PreConfigure<OpenIddictServerBuilder>(builder =>
    {
        // Allow SMS verification code authentication process
        builder.AllowSmsFlow();
    });
}
```

3. Usage example:

```http
POST /connect/token
Content-Type: application/x-www-form-urlencoded

grant_type=sms&
phone_number=13800138000&
phone_verify=123456&
scope=openid profile
```

## Parameter description

* phone_number (required)
  *Mobile phone number
  * Must be a valid mobile phone number format

* phone_verify (required)
  * SMS verification code
  * Must be a valid verification code

* scope (optional)
  * Requested permission scope
  * Includes openid profile by default

## Error code description

* invalid_grant
  * GrantTypeInvalid - Grant type not allowed
  * PhoneVerifyInvalid - The phone verification code is invalid or expired
  * PhoneOrTokenCodeNotFound - The mobile phone number or verification code was not entered
  * PhoneNumberNotRegister - The logged-in mobile phone number is not registered

## Notes

*Mobile phone number must be registered
*The verification code is valid for a limited period
* Verification failure will increase the failure count
* Multiple verification failures may result in account lockout
* All authentication operations will be recorded in security logs
* It is recommended to use HTTPS in production environments
