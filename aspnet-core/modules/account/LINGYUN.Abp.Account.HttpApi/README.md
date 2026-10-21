# LINGYUN.Abp.Account.HttpApi

The HTTP API layer of the ABP account module provides RESTful API interface implementation.

[English](./README.EN.md)

## Features

* Provides HTTP API interface for account management
* Support localization and multi-language
* Integrate ABP MVC framework
* Automatic API controller registration

## Module reference

```csharp
[DependsOn(
    typeof(AbpAccountHttpApiModule),
    typeof(AbpAccountApplicationContractsModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## API Controller

### AccountController

The following HTTP API endpoints are provided:
* POST /api/account/register - Mobile phone number registration
* POST /api/account/register-by-wechat - WeChat mini program registration
* POST /api/account/reset-password - reset password
* POST /api/account/send-phone-register-code - Send mobile phone registration verification code
* POST /api/account/send-phone-signin-code - Send mobile phone login verification code
* POST /api/account/send-email-signin-code - Send email login verification code
* POST /api/account/send-phone-reset-password-code - Send phone reset password verification code

### MyProfileController

The following HTTP API endpoints are provided:
* GET /api/account/my-profile - Get personal information
* PUT /api/account/my-profile - update profile
* POST /api/account/my-profile/change-password - change password
* POST /api/account/my-profile/change-phone-number - change mobile phone number
* POST /api/account/my-profile/send-phone-number-change-code - Send phone number change verification code
* POST /api/account/my-profile/change-avatar - update avatar

### MyClaimController

The following HTTP API endpoints are provided:
* GET /api/account/my-claim - Get user statement
* PUT /api/account/my-claim - Update user claim

## Localization configuration

The module comes preconfigured with localization options:
* Support AccountResource resource localization
* Automatically register MVC application components
