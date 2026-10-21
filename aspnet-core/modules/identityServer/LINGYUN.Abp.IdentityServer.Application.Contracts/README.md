# LINGYUN.Abp.IdentityServer.Application.Contracts

The IdentityServer application service contract module defines the application service interface and DTO of IdentityServer4 resource management related functions.

## Features

* Permission definition
  * Client permissions
    * Default permissions - `AbpIdentityServer.Clients`
    * Create permission - `AbpIdentityServer.Clients.Create`
    * Update permissions - `AbpIdentityServer.Clients.Update`
    * Delete permission - `AbpIdentityServer.Clients.Delete`
    * Clone permission - `AbpIdentityServer.Clients.Clone`
    * Management permissions - `AbpIdentityServer.Clients.ManagePermissions`
    * Management claims - `AbpIdentityServer.Clients.ManageClaims`
    * Management keys - `AbpIdentityServer.Clients.ManageSecrets`
    * Management properties - `AbpIdentityServer.Clients.ManageProperties`

  * API resource permissions
    * Default permissions - `AbpIdentityServer.ApiResources`
    * Create permission - `AbpIdentityServer.ApiResources.Create`
    * Update permissions - `AbpIdentityServer.ApiResources.Update`
    * Delete permission - `AbpIdentityServer.ApiResources.Delete`
    * Management claims - `AbpIdentityServer.ApiResources.ManageClaims`
    * Management keys - `AbpIdentityServer.ApiResources.ManageSecrets`
    * Management scope - `AbpIdentityServer.ApiResources.ManageScopes`
    * Management properties - `AbpIdentityServer.ApiResources.ManageProperties`

  * API scope permissions
    * Default permissions - `AbpIdentityServer.ApiScopes`
    * Create permission - `AbpIdentityServer.ApiScopes.Create`
    * Update permissions - `AbpIdentityServer.ApiScopes.Update`
    * Delete permission - `AbpIdentityServer.ApiScopes.Delete`
    * Management claims - `AbpIdentityServer.ApiScopes.ManageClaims`
    * Management properties - `AbpIdentityServer.ApiScopes.ManageProperties`

  * Identity resource permissions
    * Default permissions - `AbpIdentityServer.IdentityResources`
    * Create permission - `AbpIdentityServer.IdentityResources.Create`
    * Update permission - `AbpIdentityServer.IdentityResources.Update`
    * Delete permission - `AbpIdentityServer.IdentityResources.Delete`
    * Manage claims - `AbpIdentityServer.IdentityResources.ManageClaims`
    * Management properties - `AbpIdentityServer.IdentityResources.ManageProperties`

  * Authorization permissions
    * Default permissions - `AbpIdentityServer.Grants`
    * Delete permissions - `AbpIdentityServer.Grants.Delete`

* Localized resources
  * Support multi-language localization
  * Built-in Chinese and English resources

## Module reference

```csharp
[DependsOn(
    typeof(AbpIdentityServerApplicationContractsModule)
)]
public class YourModule : AbpModule
{
    // ...
}
```

## Dependent modules

* `AbpAuthorizationModule` - ABP authorization module
* `AbpDddApplicationContractsModule` - ABP DDD application service contract module
* `AbpIdentityServerDomainSharedModule` - IdentityServer domain shared module

## Configuration usage

The module provides application service interface definitions and data transmission objects required for IdentityServer4 resource management. All permissions are only open to the host tenant by default.

Related documents:
* [IdentityServer4 Documentation](https://identityserver4.readthedocs.io/)
* [ABP Authorization Document](https://docs.abp.io/en/abp/latest/Authorization)

[View English document](README.EN.md)
