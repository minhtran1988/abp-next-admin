# LINGYUN.Abp.Identity.Application.Contracts

The identity authentication application service contract module provides identity authentication-related application service interface definitions.

## Features

* Extend the Volo.Abp.Identity.AbpIdentityApplicationContractsModule module
* Provide application service interface definitions related to identity authentication
* Provide DTO object definitions related to identity authentication
* Integrated ABP authorization module

## Module reference

```csharp
[DependsOn(
    typeof(AbpIdentityApplicationContractsModule),
    typeof(AbpIdentityDomainSharedModule),
    typeof(AbpAuthorizationModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Service interface

* IIdentityUserAppService - User management service interface
* IIdentityRoleAppService - role management service interface
* IIdentityClaimTypeAppService - Claim type management service interface
* IIdentitySecurityLogAppService - Security log service interface
* IIdentitySettingsAppService - Identity authentication settings service interface
* IProfileAppService - User profile service interface

## DTO object

* IdentityUserDto - User DTO
* IdentityRoleDto - role DTO
* IdentityClaimTypeDto - Claim type DTO
* IdentitySecurityLogDto - Security log DTO
* GetIdentityUsersInput - Get user list input DTO
* GetIdentityRolesInput - Get the role list input DTO
* IdentityUserCreateDto - Create user DTO
* IdentityUserUpdateDto - update user DTO
* IdentityRoleCreateDto - Create role DTO
* IdentityRoleUpdateDto - update role DTO

## Basic usage

1. Implement user management service interface
```csharp
public class YourIdentityUserAppService : IIdentityUserAppService
{
    public async Task<IdentityUserDto> GetAsync(Guid id)
    {
        //Implement the logic of obtaining users
    }

    public async Task<PagedResultDto<IdentityUserDto>> GetListAsync(GetIdentityUsersInput input)
    {
        //Implement the logic of obtaining the user list
    }
}
```

2. Implement role management service interface
```csharp
public class YourIdentityRoleAppService : IIdentityRoleAppService
{
    public async Task<IdentityRoleDto> GetAsync(Guid id)
    {
        //Implement the logic of obtaining the role
    }

    public async Task<PagedResultDto<IdentityRoleDto>> GetListAsync(GetIdentityRolesInput input)
    {
        //Implement the logic of obtaining the role list
    }
}
```

## More information

* [ABP Identity Authentication Document](https://docs.abp.io/en/abp/latest/Identity)
