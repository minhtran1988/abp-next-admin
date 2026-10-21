# LINGYUN.Abp.MessageService.HttpApi

Messaging service HTTP API module.

## Features

* HTTP API interface that provides messaging services
* Support MVC data annotation localization
* Automatically register API controller

## Dependent modules

* [LINGYUN.Abp.MessageService.Application.Contracts](../LINGYUN.Abp.MessageService.Application.Contracts/README.md)
* `AbpAspNetCoreMvcModule`

## Configuration usage

1. First, you need to install LINGYUN.Abp.MessageService.HttpApi into your project:

```bash
dotnet add package LINGYUN.Abp.MessageService.HttpApi
```

2. Add `AbpMessageServiceHttpApiModule` to your module’s dependency list:

```csharp
[DependsOn(typeof(AbpMessageServiceHttpApiModule))]
public class YourModule : AbpModule
{
}
```

## More

[English document](README.EN.md)
