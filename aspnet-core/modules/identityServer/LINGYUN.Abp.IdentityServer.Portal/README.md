# LINGYUN.Abp.IdentityServer.Portal

The IdentityServer portal authentication module provides authentication functions for enterprise portals.

## Features

* Portal certification
  * `PortalGrantValidator` - portal grant validator
    * Support enterprise portal login
    * Support multi-tenant authentication
    * Automatically switch tenants
    * Enterprise information verification
    * User password verification
    * Security logging

* Certification process
  1. The user uses the portal to initiate a login request
  2. Check whether it carries the enterprise identification field (EnterpriseId)
     * Without EnterpriseId: Returns a list of enterprises associated with tenant information
     * Carrying EnterpriseId: retrieve associated tenant information and switch to the specified tenant
  3. Use password method for login verification
  4. Log in successfully and return token.

## Module reference

```csharp
[DependsOn(
    typeof(AbpIdentityServerPortalModule)
)]
public class YourModule : AbpModule
{
    // ...
}
```

## Dependent modules

* `AbpIdentityServerDomainModule` - ABP IdentityServer domain module
* `AbpAspNetCoreMultiTenancyModule` - ABP multi-tenancy module
* `PlatformDomainModule` - Platform domain module

## Configuration usage

### Configure portal authentication

```csharp
public override void PreConfigureServices(ServiceConfigurationContext context)
{
    PreConfigure<IIdentityServerBuilder>(builder =>
    {
        builder.AddExtensionGrantValidator<PortalGrantValidator>();
    });
}
```

### Authentication request parameters

* `grant_type`: "portal" (required)
* `enterpriseId`: enterprise ID (optional)
* `username`: username (required)
* `password`: Password (required)
* `scope`: request scope (optional)

### Authentication response

* When enterpriseId is not provided:
```json
{
    "error": "invalid_grant",
    "enterprises": [
        {
            "id": "Enterprise Identity",
            "name": "Company name",
            "code": "Enterprise code"
        }
    ]
}
```

* Authentication successful:
```json
{
    "access_token": "Access token",
    "expires_in": validity period,
    "token_type": "Bearer",
    "refresh_token": "Refresh token"
}
```

Related documents:
* [IdentityServer4 Documentation](https://identityserver4.readthedocs.io/)
* [ABP Multi-Tenancy Document](https://docs.abp.io/en/abp/latest/Multi-Tenancy)

[View English document](README.EN.md)
