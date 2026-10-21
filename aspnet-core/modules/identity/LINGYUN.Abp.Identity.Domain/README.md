# LINGYUN.Abp.Identity.Domain

The identity authentication field module provides the implementation of core functions related to identity authentication.

## Features

* Extend the Volo.Abp.Identity.AbpIdentityDomainModule module
* Provide identity session management function
* Provide identity session cleaning function
* Support distributed events

## Module reference

```csharp
[DependsOn(
    typeof(AbpIdentityDomainModule),
    typeof(Volo.Abp.Identity.AbpIdentityDomainModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

### IdentitySessionCleanupOptions

```json
{
  "Identity": {
    "Session": {
      "Cleanup": {
        "IsEnabled": false, // Whether to enable session cleaning, default: false
        "CleanupPeriod": 3600000, // Session cleanup interval (milliseconds), default: 1 hour
        "InactiveTimeSpan": "30.00:00:00" // How long to keep inactive sessions, default: 30 days
      }
    }
  }
}
```

### IdentitySessionSignInOptions

```json
{
  "Identity": {
    "Session": {
      "SignIn": {
        "AuthenticationSchemes": ["Identity.Application"], // Identity authentication schemes used for processing
        "SignInSessionEnabled": false, // Whether to enable SignInManager login session, default: false
        "SignOutSessionEnabled": false // Whether to enable SignInManager logout session, default: false
      }
    }
  }
}
```

## Basic usage

1. Configure identity session management
```csharp
Configure<IdentitySessionSignInOptions>(options =>
{
    options.SignInSessionEnabled = true; // Enable login session
    options.SignOutSessionEnabled = true; // Enable logout session
});
```

2. Configure session cleanup
```csharp
Configure<IdentitySessionCleanupOptions>(options =>
{
    options.IsCleanupEnabled = true; // Enable session cleanup
    options.CleanupPeriod = 3600000; //Set the cleanup interval to 1 hour
    options.InactiveTimeSpan = TimeSpan.FromDays(7); // Set the inactive session retention time to 7 days
});
```

## More information

* [ABP Identity Authentication Document](https://docs.abp.io/en/abp/latest/Identity)
