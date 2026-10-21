# LINGYUN.Abp.Account.Application.Contracts

The application service contract of the ABP account module provides interface definitions related to account management.

[English](./README.EN.md)

## Features

* Register account with mobile phone number
* Register an account on WeChat applet
* Mobile phone number reset password
* Mobile phone verification code function (registration, login, reset password)
* Log in with email verification code
* User profile management
* User session management
* Two-factor authentication
* User statement management

## Module reference

```csharp
[DependsOn(typeof(AbpAccountApplicationContractsModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Interface service

### IAccountAppService

Account management service interface provides the following functions:
* Mobile phone number registration
* WeChat mini program registration
* Mobile phone number reset password
* Send mobile phone verification code (register, log in, reset password)
*Send email verification code (login)

### IMyProfileAppService

Personal data management service interface provides the following functions:
* Get/update personal information
* Change password
* Change mobile phone number
* Change avatar
* Two-factor authentication management
* Get/verify authenticator
* Get recovery code

### IMyClaimAppService

User declaration management service interface provides the following functions:
* Get user statement
* Update user statement

## Localization

The module includes multi-language support, resource files are located at:
* `/LINGYUN/Abp/Account/Localization/Resources`
