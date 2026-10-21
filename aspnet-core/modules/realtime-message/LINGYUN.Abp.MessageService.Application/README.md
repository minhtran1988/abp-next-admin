# LINGYUN.Abp.MessageService.Application

Message service application layer module.

## Features

* Implement the application layer interface of the message service
* Implement business logic of message service
* Support automatic object mapping

## Dependent modules

* [LINGYUN.Abp.MessageService.Application.Contracts](../LINGYUN.Abp.MessageService.Application.Contracts/README.md)
* [LINGYUN.Abp.MessageService.Domain](../LINGYUN.Abp.MessageService.Domain/README.md)

## Configuration usage

1. First, you need to install LINGYUN.Abp.MessageService.Application into your project:

```bash
dotnet add package LINGYUN.Abp.MessageService.Application
```

2. Add `AbpMessageServiceApplicationModule` to your module’s dependency list:

```csharp
[DependsOn(typeof(AbpMessageServiceApplicationModule))]
public class YourModule : AbpModule
{
}
```

## More

[English document](README.EN.md)
