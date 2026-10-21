# LINGYUN.Abp.Identity.HttpApi.Client

The identity authentication HTTP API client module provides identity authentication related HTTP API client proxy.

## Features

* Extend the Volo.Abp.Identity.AbpIdentityHttpApiClientModule module
* Provide HTTP API client proxy related to identity authentication
* Automatically register HTTP client proxy service

## Module reference

```csharp
[DependsOn(
    typeof(AbpIdentityHttpApiClientModule),
    typeof(AbpIdentityApplicationContractsModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

```json
{
  "RemoteServices": {
    "Identity": {
      "BaseUrl": "http://localhost:44388/"
    }
  }
}
```

## Client proxy

* IIdentityUserAppService - User management client agent
* IIdentityRoleAppService - role management client agent
* IIdentityClaimTypeAppService - Claim type management client proxy
* IIdentitySecurityLogAppService - Security log client agent
* IIdentitySettingsAppService - Identity authentication settings client proxy
* IProfileAppService - User profile client proxy

## Basic usage

1. Configure remote services
```csharp
public override void ConfigureServices(ServiceConfigurationContext context)
{
    var configuration = context.Services.GetConfiguration();

    Configure<AbpRemoteServiceOptions>(options =>
    {
        options.RemoteServices.Default =
            new RemoteServiceConfiguration(configuration["RemoteServices:Identity:BaseUrl"]);
    });
}
```

2. Use client proxies
```csharp
public class YourService
{
    private readonly IIdentityUserAppService _userAppService;
    private readonly IIdentityRoleAppService _roleAppService;

    public YourService(
        IIdentityUserAppService userAppService,
        IIdentityRoleAppService roleAppService)
    {
        _userAppService = userAppService;
        _roleAppService = roleAppService;
    }

    public async Task<IdentityUserDto> GetUserAsync(Guid id)
    {
        return await _userAppService.GetAsync(id);
    }

    public async Task<IdentityRoleDto> GetRoleAsync(Guid id)
    {
        return await _roleAppService.GetAsync(id);
    }
}
```

## More information

* [ABP Identity Authentication Document](https://docs.abp.io/en/abp/latest/Identity)
* [ABP HTTP API Client Proxies Documentation](https://docs.abp.io/en/abp/latest/API/HTTP-Client-Proxies)
