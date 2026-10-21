# LINGYUN.Abp.IdentityServer.SmsValidator

The IdentityServer SMS verification module provides identity authentication functions based on mobile phone numbers and SMS verification codes.

## Features

* SMS verification
  * `SmsTokenGrantValidator` - SMS verification authorizer
    *Support mobile phone number verification
    *Support SMS verification code verification
    * Anti-brute force protection
    * User lock check
    * Security logging
    *Event notification

* Certification process
  1. The user uses the mobile phone number and SMS verification code to initiate a login request
  2. Verify the validity of mobile phone number and verification code
  3. Verify user status (whether locked)
  4. Generate an access token after passing the verification
  5. Record security logs and events

## Module reference

```csharp
[DependsOn(
    typeof(AbpIdentityServerSmsModule)
)]
public class YourModule : AbpModule
{
    // ...
}
```

## Dependent modules

* `AbpIdentityServerDomainModule` - ABP IdentityServer domain module

## Configuration usage

### Configure SMS verification

```csharp
public override void PreConfigureServices(ServiceConfigurationContext context)
{
    PreConfigure<IIdentityServerBuilder>(builder =>
    {
        builder.AddExtensionGrantValidator<SmsTokenGrantValidator>();
    });
}
```

### Authentication request parameters

* `grant_type`: "phone_verify" (required)
* `phone_number`: mobile phone number (required)
* `phone_verify_code`: SMS verification code (required)
* `scope`: request scope (optional)

### Authentication response

* Authentication successful:
```json
{
    "access_token": "Access token",
    "expires_in": validity period,
    "token_type": "Bearer",
    "refresh_token": "Refresh token"
}
```

*Authentication failed:
```json
{
    "error": "invalid_grant",
    "error_description": "Error description"
}
```

### Error type

* `invalid_grant`: Authorization verification failed
  * Mobile phone number is not registered
  * Verification code is invalid
  * User is locked
  * Parameter missing

Related documents:
* [IdentityServer4 Documentation](https://identityserver4.readthedocs.io/)
* [ABP Identity Documentation](https://docs.abp.io/en/abp/latest/Modules/Identity)

[View English document](README.EN.md)
