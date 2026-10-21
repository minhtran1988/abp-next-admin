# LINGYUN.Abp.MessageService.Application.Contracts

Message service application layer contract module.

## Features

* Define the application layer interface of the message service
* Define the DTO of the message service
* Provide multi-language resources
* Support virtual file system

## Dependent modules

* [LINGYUN.Abp.MessageService.Domain.Shared](../LINGYUN.Abp.MessageService.Domain.Shared/README.md)

## Configuration usage

1. First, you need to install LINGYUN.Abp.MessageService.Application.Contracts into your project:

```bash
dotnet add package LINGYUN.Abp.MessageService.Application.Contracts
```

2. Add `AbpMessageServiceApplicationContractsModule` to your module’s dependency list:

```csharp
[DependsOn(typeof(AbpMessageServiceApplicationContractsModule))]
public class YourModule : AbpModule
{
}
```

## More

[English document](README.EN.md)
