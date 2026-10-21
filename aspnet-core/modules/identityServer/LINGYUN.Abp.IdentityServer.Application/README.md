# LINGYUN.Abp.IdentityServer.Application

The IdentityServer application service module provides application layer implementation of IdentityServer4 resource management related functions.

## Features

* Client management services
  * Client key management
  * Client scope management
  * Client authorization type management
  * Client cross-domain source management
  * Client redirect URI management
  * Client logout redirect URI management
  * Client identity provider restriction management
  * Client claim management
  * Client attribute management

* API resource management service
  * API resource attribute management
  * API resource key management
  * API resource scope management
  * API resource declaration management

* API scope management service
  * API scope declaration management
  * API scope attribute management

* Identity resource management services
  * Identity resource claim management
  * Identity resource attribute management

* Persistent authorization management service

## Module reference

```csharp
[DependsOn(
    typeof(AbpIdentityServerApplicationModule)
)]
public class YourModule : AbpModule
{
    // ...
}
```

## Dependent modules

* `AbpIdentityServerApplicationContractsModule` - IdentityServer application service contract module
* `AbpIdentityServerDomainModule` - IdentityServer domain module
* `AbpDddApplicationModule` - ABP DDD application service basic module
* `AbpAutoMapperModule` - ABP AutoMapper object mapping module

## Configuration usage

The module implements the CRUD operation of IdentityServer4 resources and is mainly used to manage the configuration resources of IdentityServer4.

Related documents:
* [IdentityServer4 Documentation](https://identityserver4.readthedocs.io/)
* [ABP Authorization Document](https://docs.abp.io/en/abp/latest/Authorization)

[View English document](README.EN.md)
