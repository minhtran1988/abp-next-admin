# LINGYUN.Abp.Sms.Platform

Message processing platform implementation of abp framework SMS sending interface **ISmsSender**

Implement SMS message management, sending, query and other functions through the platform service interface

## Configuration usage

Module reference on demand

```csharp
[DependsOn(typeof(AbpSmsPlatformModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```
## Configuration item description

```json

{
  "RemoteServices": {
    "Platform": {
      "BaseUrl": "http://127.0.0.1:30025",
      "IdentityClient": "InternalServiceClient",
      "UseCurrentAccessToken": "False"
    }
  },
  "IdentityClients": {
    "InternalServiceClient": {
      "Authority": "http://127.0.0.1:44385",
      "ClientId": "InternalServiceClient",
      "ClientSecret": "1q2w3e*",
      "GrantType": "client_credentials",
      "RequireHttps": "False",
      "Scope": "lingyun-abp-application"
    }
  },
}

```

## Related links

* [Platform Service Module](../README.md)
* [SMS sending integration](https://abp.io/docs/latest/framework/infrastructure/sms-sending)
* [Dynamic Interface Proxy](https://abp.io/docs/latest/framework/api-development/dynamic-csharp-clients)

## Others

