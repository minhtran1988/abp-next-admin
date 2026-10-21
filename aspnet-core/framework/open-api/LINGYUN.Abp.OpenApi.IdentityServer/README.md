# LINGYUN.Abp.OpenApi.IdentityServer

OpenApi IdentityServer integration module provides AppKey/AppSecret storage implementation based on IdentityServer.

## Features

* IdentityServer-based AppKey/AppSecret storage
* Automatically map AppKey to IdentityServer client ID
* Automatically map AppSecret to client key
* Support signature validity period configuration
* Support client name configuration
* Automatically create and update client information

## Installation

```bash
dotnet add package LINGYUN.Abp.OpenApi.IdentityServer
```

## Module reference

```csharp
[DependsOn(typeof(AbpOpenApiIdentityServerModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Working principle

1. AppKey mapping
   * AppKey will be mapped to ClientId of IdentityServer
   * Client query and management through `IClientRepository` interface

2. AppSecret Mapping
   * AppSecret will be mapped to Client Secret
   * The key will be stored as an AppSecret as the identity

3. Signature validity period
   * SignLifetime will be stored as a custom property on the client side
   * The attribute name is "SignLifetime", and the value is the validity period in seconds.

## Basic usage

1. Store application credentials
   ```csharp
   public class YourService
   {
       private readonly IAppKeyStore _appKeyStore;

       public YourService(IAppKeyStore appKeyStore)
       {
           _appKeyStore = appKeyStore;
       }

       public async Task CreateAppAsync()
       {
           var descriptor = new AppDescriptor(
               appName: "Test Application",
               appKey: "your-app-key",
               appSecret: "your-app-secret",
               signLifeTime: 300 // 5 minutes validity period
           );

           await _appKeyStore.StoreAsync(descriptor);
       }
   }
   ```

2. Query application credentials
   ```csharp
   public class YourService
   {
       private readonly IAppKeyStore _appKeyStore;

       public YourService(IAppKeyStore appKeyStore)
       {
           _appKeyStore = appKeyStore;
       }

       public async Task<AppDescriptor> GetAppAsync(string appKey)
       {
           return await _appKeyStore.FindAsync(appKey);
       }
   }
   ```

## More information

* [LINGYUN.Abp.OpenApi](../LINGYUN.Abp.OpenApi/README.md)
* [LINGYUN.Abp.OpenApi.Authorization](../LINGYUN.Abp.OpenApi.Authorization/README.md)
* [IdentityServer](https://identityserver4.readthedocs.io/)
