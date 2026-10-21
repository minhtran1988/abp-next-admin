# LINGYUN.Abp.OpenApi

OpenApi authentication module provides AppKey/AppSecret-based API signature authentication function for ABP applications.

## Features

* Support AppKey/AppSecret authentication
* Support request signature verification
* Supports anti-replay attacks (Nonce random number verification)
* Support request timestamp verification
* Support client whitelist
* Support IP address whitelist
* Support multi-language error messages

## Installation

```bash
dotnet add package LINGYUN.Abp.OpenApi
```

## Module reference

```csharp
[DependsOn(typeof(AbpOpenApiModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

```json
{
  "OpenApi": {
    "IsEnabled": true, // Whether to enable API signature checking, default: true
    "RequestNonceExpireIn": "00:10:00", //Request random number expiration time, default: 10 minutes
    "AppDescriptors": [ // AppKey configuration list
      {
        "AppName": "Test Application", // Application name
        "AppKey": "Your AppKey", // Application ID
        "AppSecret": "Your AppSecret", // Application secret
        "AppToken": "Optional Token", // Optional application token
        "SignLifetime": 300 // Signature validity period (seconds)
      }
    ]
  }
}
```

## Basic usage

1. Configure AppKey/AppSecret
   * Add AppKey and AppSecret to the configuration file
   * Or implement a custom `IAppKeyStore` interface to manage AppKey

2. Enable OpenApi authentication
   ```csharp
   public override void ConfigureServices(ServiceConfigurationContext context)
   {
       var configuration = context.Services.GetConfiguration();
       Configure<AbpOpenApiOptions>(configuration.GetSection("OpenApi"));
   }
   ```

3. Custom client verification (optional)
   ```csharp
   public class CustomClientChecker : IClientChecker
   {
       public Task<bool> IsGrantAsync(string clientId, CancellationToken cancellationToken = default)
       {
           // Implement custom client verification logic
           return Task.FromResult(true);
       }
   }
   ```

4. Custom IP address verification (optional)
   ```csharp
   public class CustomIpAddressChecker : IIpAddressChecker
   {
       public Task<bool> IsGrantAsync(string ipAddress, CancellationToken cancellationToken = default)
       {
           // Implement customized IP address verification logic
           return Task.FromResult(true);
       }
   }
   ```

## Error code

* AbpOpenApi:9100 - Invalid application identifier
* AbpOpenApi:9101 - No application identifier is carried
*AbpOpenApi:9110 - Invalid signature
* AbpOpenApi:9111 - No signature carried
* AbpOpenApi:9210 - Request timed out or session has expired
* AbpOpenApi:9211 - No timestamp identifier is carried
* AbpOpenApi:9220 - Duplicate request
* AbpOpenApi:9221 - No random number is carried
* AbpOpenApi:9300 - Client is not within allowed range
* AbpOpenApi:9400 - Client IP is not within the allowed range

## More information

* [ABP Framework](https://abp.io/)
