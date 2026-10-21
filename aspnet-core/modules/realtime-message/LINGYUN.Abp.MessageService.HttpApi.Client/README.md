# LINGYUN.Abp.MessageService.HttpApi.Client

Messaging service HTTP API client module.

## Features

* HTTP client proxy that provides messaging services
* Automatically register HTTP client proxy service

## Dependent modules

* [LINGYUN.Abp.MessageService.Application.Contracts](../LINGYUN.Abp.MessageService.Application.Contracts/README.md)
* `AbpHttpClientModule`

## Configuration usage

1. First, you need to install LINGYUN.Abp.MessageService.HttpApi.Client into your project:

```bash
dotnet add package LINGYUN.Abp.MessageService.HttpApi.Client
```

2. Add `AbpMessageServiceHttpApiClientModule` to your module’s dependency list:

```csharp
[DependsOn(typeof(AbpMessageServiceHttpApiClientModule))]
public class YourModule : AbpModule
{
}
```

3. Configure the remote service address:

```json
{
  "RemoteServices": {
    "AbpMessageService": {
      "BaseUrl": "http://your-service-url"
    }
  }
}
```

## More

[English document](README.EN.md)
