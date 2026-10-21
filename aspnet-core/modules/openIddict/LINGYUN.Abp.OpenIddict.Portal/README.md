# LINGYUN.Abp.OpenIddict.Portal

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2Fcolinin%2Fabp-next-admin%2Fmaster%2Faspnet-core%2Fmodules%2FopenIddict%2FLINGYUN.Abp.OpenIddict.Portal%2FLINGYUN.Abp.OpenIddict.Portal.csproj)](https://abp.io)
[![NuGet](https://img.shields.io/nuget/v/LINGYUN.Abp.OpenIddict.Portal.svg?style=flat-square)](https://www.nuget.org/packages/LINGYUN.Abp.OpenIddict.Portal)

## Introduction

`LINGYUN.Abp.OpenIddict.Portal` is the portal authentication extension module of OpenIddict, which provides authentication functions for enterprise portals, including multi-tenant selection, two-factor authentication, password policy and other features.

[English](./README.EN.md)

## Features

* Enterprise Portal Certification
  *Support enterprise list selection
  * Support multi-tenant authentication
  *Support corporate logo display

* Password authentication enhancement
  *Support email login
  * Support external login providers
  * Support password policy verification
  * Support forced password change
  *Support regular password changes

* Two-factor authentication
  * Supports multiple authentication providers
  *Support verification code verification
  * Support authenticator verification

* Security log
  * Log login attempts
  * Record login failure
  * Record password changes

## Installation

```bash
dotnet add package LINGYUN.Abp.OpenIddict.Portal
```

## Use

1. Add `[DependsOn(typeof(AbpOpenIddictPortalModule))]` to your module class.

2. Configure OpenIddict server:

```csharp
public override void PreConfigureServices(ServiceConfigurationContext context)
{
    PreConfigure<OpenIddictServerBuilder>(builder =>
    {
        // Allow portal authentication process
        builder.AllowPortalFlow();
    });
}
```

3. Usage example:

```http
POST /connect/token
Content-Type: application/x-www-form-urlencoded

grant_type=portal&
username=admin&
password=1q2w3E*&
enterpriseId=your-enterprise-id&
scope=openid profile
```

## Certification process

1. Business selection
   * User provides enterprise ID (enterpriseId)
   * If not provided or invalid, return to the optional enterprise list

2. Password verification
   *Support username or email login
   * Verify password policy
   * Check account status

3. Two-factor authentication (if enabled)
   * Verify two-factor authentication code
   * Supports multiple authentication providers

4. Password modification (if necessary)
   * Forced password change when logging in for the first time
   * Regularly change password requirements

## Parameter description

* username (required)
  *Username or email address

* password (required)
  * User password

* enterpriseId (required)
  * Enterprise ID, must be in valid GUID format

* TwoFactorProvider (optional)
  * Two-factor authentication provider name
  *Only required when two-factor authentication is enabled

*TwoFactorCode (optional)
  * Two-factor authentication code
  *Only required when two-factor authentication is enabled

* ChangePasswordToken (optional)
  * Modify password token
  *Only required when changing password

* NewPassword (optional)
  * New password
  *Only required when changing password

## Notes

* Enterprise ID must be in valid GUID format
* The password must comply with the system configured password policy
*Two-factor authentication codes are valid for a limited time
* All authentication operations will be recorded in security logs
* It is recommended to use HTTPS in production environments
