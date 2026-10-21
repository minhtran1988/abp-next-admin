# LINGYUN.Abp.IdentityServer.HttpApi

IdentityServer HTTP API module provides HTTP API interface for IdentityServer4 resource management.

## Features

* API controller
  * API scope controller - `ApiScopeController`
    * Create API scope - POST `/api/identity-server/api-scopes`
    * Delete API scope - DELETE `/api/identity-server/api-scopes/{id}`
    * Get API scope - GET `/api/identity-server/api-scopes/{id}`
    * Get the list of API scopes - GET `/api/identity-server/api-scopes`
    * Update API scope - PUT `/api/identity-server/api-scopes/{id}`

  * API resource controller - `ApiResourceController`
    * Provide CRUD operation interface for API resources
    * Routing prefix: `/api/identity-server/api-resources`

* Localization support
  * Inherit the localization configuration of ABP UI resources
  * Support multiple languages

## Module reference

```csharp
[DependsOn(
    typeof(AbpIdentityServerHttpApiModule)
)]
public class YourModule : AbpModule
{
    // ...
}
```

## Dependent modules

* `AbpIdentityServerApplicationContractsModule` - IdentityServer application service contract module
* `AbpAspNetCoreMvcModule` - ABP ASP.NET Core MVC module

## Configuration usage

### Configure remote service name

```csharp
[RemoteService(Name = AbpIdentityServerConsts.RemoteServiceName)]
[Area("identity-server")]
[Route("api/identity-server/[controller]")]
public class YourController : AbpControllerBase
{
    // ...
}
```

### Add localized resources

```csharp
Configure<AbpLocalizationOptions>(options =>
{
    options.Resources
        .Get<AbpIdentityServerResource>()
        .AddBaseTypes(typeof(AbpUiResource));
});
```

Related documents:
* [IdentityServer4 Documentation](https://identityserver4.readthedocs.io/)
* [ABP ASP.NET Core MVC Documentation](https://docs.abp.io/en/abp/latest/AspNetCore-MVC)

[View English document](README.EN.md)
