# LINGYUN.Abp.Identity.Application

The identity authentication application service module provides identity authentication-related application service implementation.

## Features

* Extend the Volo.Abp.Identity.AbpIdentityApplicationModule module
* Provide application service implementation related to identity authentication
* Integrate AutoMapper object mapping
* Support user avatar URL extended attributes

## Module reference

```csharp
[DependsOn(
    typeof(AbpIdentityApplicationModule),
    typeof(AbpIdentityApplicationContractsModule),
    typeof(AbpIdentityDomainModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Application Services

* IdentityUserAppService - User management service
* IdentityRoleAppService - role management service
* IdentityClaimTypeAppService - Claim type management service
* IdentitySecurityLogAppService - Security log service
* IdentitySettingsAppService - Identity authentication settings service
* ProfileAppService - User profile service

## Object mapping

The module uses AutoMapper for object mapping. The main mappings include:

* IdentityUser -> IdentityUserDto
* IdentityRole -> IdentityRoleDto
* IdentityClaimType -> IdentityClaimTypeDto
* IdentitySecurityLog -> IdentitySecurityLogDto

## Basic usage

1. Use user management services
```csharp
public class YourService
{
    private readonly IIdentityUserAppService _userAppService;

    public YourService(IIdentityUserAppService userAppService)
    {
        _userAppService = userAppService;
    }

    public async Task<IdentityUserDto> GetUserAsync(Guid id)
    {
        return await _userAppService.GetAsync(id);
    }
}
```

2. Use the role management service
```csharp
public class YourService
{
    private readonly IIdentityRoleAppService _roleAppService;

    public YourService(IIdentityRoleAppService roleAppService)
    {
        _roleAppService = roleAppService;
    }

    public async Task<IdentityRoleDto> GetRoleAsync(Guid id)
    {
        return await _roleAppService.GetAsync(id);
    }
}
```

## More information

* [ABP Identity Authentication Document](https://docs.abp.io/en/abp/latest/Identity)
