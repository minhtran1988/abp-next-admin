# LINGYUN.Abp.OpenApi.Authorization

The OpenApi authentication and authorization middleware module provides AppKey/AppSecret-based API signature authentication middleware functions for ABP applications.

## Features

* Provide OpenApi authentication middleware
* Support request signature verification
* Supports anti-replay attacks (Nonce random number verification)
* Support request timestamp verification
* Support client whitelist verification
* Support IP address whitelist verification
* Support custom authentication logic
* Support exception handling and error message packaging

## Installation

```bash
dotnet add package LINGYUN.Abp.OpenApi.Authorization
```

## Module reference

```csharp
[DependsOn(typeof(AbpOpenApiAuthorizationModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Basic usage

1. Enable OpenApi authentication middleware
   ```csharp
   public void Configure(IApplicationBuilder app)
   {
       //Add OpenApi authentication middleware
       app.UseOpenApiAuthorization();
   }
   ```

2. Custom authentication service (optional)
   ```csharp
   public class CustomOpenApiAuthorizationService : OpenApiAuthorizationService
   {
       public CustomOpenApiAuthorizationService(
           INonceStore nonceStore,
           IAppKeyStore appKeyStore,
           IClientChecker clientChecker,
           IIpAddressChecker ipAddressChecker,
           IWebClientInfoProvider clientInfoProvider,
           IOptionsMonitor<AbpOpenApiOptions> options,
           IOptions<AbpExceptionHandlingOptions> exceptionHandlingOptions)
           : base(nonceStore, appKeyStore, clientChecker, ipAddressChecker,
                 clientInfoProvider, options, exceptionHandlingOptions)
       {
       }

       // Rewrite authentication logic
       public override async Task<bool> AuthorizeAsync(HttpContext httpContext)
       {
           // Implement custom authentication logic
           return await base.AuthorizeAsync(httpContext);
       }
   }
   ```

## Certification process

1. Verify client IP address
   * Verify whether the client IP address is within the allowed range through the `IIpAddressChecker` interface

2. Verify application credentials
   * Verify the AppKey, signature, nonce and timestamp in the request header
   * Obtain application information through the `IAppKeyStore` interface
   * Verify whether the client allows access through the `IClientChecker` interface
   * Verify the validity and timeliness of the signature

## Signature rules

1. The request parameters are sorted from small to large according to the ASCII code of the parameter name.
2. Convert the sorted parameters into a query string using URL encoding (UTF-8)
3. Concatenate the request path and query string and perform MD5 encryption to obtain the signature.

Example:
```
Request path:/api/test
Parameters:
  appKey=test
  appSecret=123456
  nonce=abc
  t=1577808000000

Signature calculation:
1. Parameter sorting and splicing: appKey=test&appSecret=123456&nonce=abc&t=1577808000000
2. Splicing request path:/api/test?appKey=test&appSecret=123456&nonce=abc&t=1577808000000
3. URL encoding and MD5 encryption to obtain the final signature
```

## More information

* [LINGYUN.Abp.OpenApi](../LINGYUN.Abp.OpenApi/README.md)
* [ABP Framework](https://abp.io/)
