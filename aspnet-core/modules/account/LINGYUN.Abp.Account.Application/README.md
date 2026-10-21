# LINGYUN.Abp.Account.Application

The application service implementation of the ABP account module provides complete account management function implementation.

[English](./README.EN.md)

## Features

*Account management service implementation
  * Mobile phone number registration
  * WeChat mini program registration
  * Password reset
  *Send verification code (SMS, email)
* Personal information management
  *Basic information maintenance
  * Password change
  *Mobile phone number change
  * Avatar update
  * Two-factor authentication
* User statement management
* Email service
  * Email confirmation
  * Email verification
* SMS service
  * Verification code sent
* WeChat mini program integration
* Virtual file system support

## Module reference

```csharp
[DependsOn(
    typeof(AbpAccountApplicationModule),
    typeof(AbpAccountApplicationContractsModule),
    typeof(AbpAccountTemplatesModule),
    typeof(AbpIdentityDomainModule),
    typeof(AbpWeChatMiniProgramModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Application Services

### AccountAppService

Implement the `IAccountAppService` interface and provide:
* User registration (mobile phone number, WeChat applet)
* Password reset
* Verification code sent

### MyProfileAppService

Implements the `IMyProfileAppService` interface and provides:
* Personal information management
* Password change
*Mobile phone number change
* Avatar update
* Two-factor authentication management

### MyClaimAppService

Implements the `IMyClaimAppService` interface and provides:
* User statement management

## Mail service

The following email services are provided:
* `IAccountEmailConfirmSender` - Email confirmation service
* `IAccountEmailVerifySender` - Email verification service
* `AccountEmailSender` - Email sending implementation

## SMS service

The following SMS services are provided:
* `IAccountSmsSecurityCodeSender` - SMS verification code sending service
* `AccountSmsSecurityCodeSender` - SMS verification code sending implementation

## URL configuration

The module is preconfigured with the following URL:
* EmailConfirm - Email confirmation URL, default is "Account/EmailConfirm"
