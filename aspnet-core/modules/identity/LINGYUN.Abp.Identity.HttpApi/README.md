# LINGYUN.Abp.Identity.HttpApi

The identity authentication HTTP API module provides identity authentication-related HTTP API interfaces.

## Features

* Extend the Volo.Abp.Identity.AbpIdentityHttpApiModule module
* Provide HTTP API interface related to identity authentication
* Support MVC data annotations for localized resources
* Automatically register MVC application components

## Module reference

```csharp
[DependsOn(
    typeof(AbpIdentityHttpApiModule),
    typeof(AbpIdentityApplicationContractsModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## API Controller

* IdentityUserController - User management API
* IdentityRoleController - role management API
* IdentityClaimTypeController - Claim type management API
* IdentitySecurityLogController - Security Log API
* IdentitySettingsController - Identity authentication settings API
* ProfileController - User Profile API

## API routing

* `/api/identity/users` - User management API route
* `/api/identity/roles` - role management API routing
* `/api/identity/claim-types` - Claim types manage API routing
* `/api/identity/security-logs` - Security log API route
* `/api/identity/settings` - Identity authentication settings API routing
* `/api/identity/my-profile` - User profile API route

## Basic usage

1. Configure localization
```csharp
public override void PreConfigureServices(ServiceConfigurationContext context)
{
    PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
    {
        options.AddAssemblyResource(typeof(IdentityResource), typeof(AbpIdentityApplicationContractsModule).Assembly);
    });
}
```

2. Use API
```csharp
// Get user list
GET /api/identity/users

// Get the specified user
GET /api/identity/users/{id}

//Create user
POST /api/identity/users
{
    "userName": "admin",
    "email": "admin@abp.io",
    "password": "1q2w3E*"
}

// update user
PUT /api/identity/users/{id}
{
    "userName": "admin",
    "email": "admin@abp.io"
}

// delete user
DELETE /api/identity/users/{id}
```

## More information

* [ABP Identity Authentication Document](https://docs.abp.io/en/abp/latest/Identity)
* [ABP HTTP API Documentation](https://docs.abp.io/en/abp/latest/API/HTTP-API)
