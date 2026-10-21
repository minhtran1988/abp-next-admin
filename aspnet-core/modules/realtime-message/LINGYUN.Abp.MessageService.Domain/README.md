# LINGYUN.Abp.MessageService.Domain

Message service domain layer module.

## Features

* Implement domain logic of message service
* Integrated object extension function
* Integrated caching function
* Integrated notification function
* Support automatic mapping
* Support multiple languages

## Dependent modules

* `AbpAutoMapperModule`
* `AbpCachingModule`
* `AbpNotificationsModule`
* [LINGYUN.Abp.MessageService.Domain.Shared](../LINGYUN.Abp.MessageService.Domain.Shared/README.md)

## Configuration usage

1. First, you need to install LINGYUN.Abp.MessageService.Domain into your project:

```bash
dotnet add package LINGYUN.Abp.MessageService.Domain
```

2. Add `AbpMessageServiceDomainModule` to your module’s dependency list:

```csharp
[DependsOn(typeof(AbpMessageServiceDomainModule))]
public class YourModule : AbpModule
{
}
```

## More

[English document](README.EN.md)
