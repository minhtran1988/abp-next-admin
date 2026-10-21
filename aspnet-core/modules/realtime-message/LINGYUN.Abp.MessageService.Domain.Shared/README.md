# LINGYUN.Abp.MessageService.Domain.Shared

Message service domain shared layer module.

## Features

* Define the infrastructure for messaging services
* Provide multi-language resources
* Define exception localization
* Define error code

## Dependent modules

* `AbpLocalizationModule`

## Configuration usage

1. First, you need to install LINGYUN.Abp.MessageService.Domain.Shared into your project:

```bash
dotnet add package LINGYUN.Abp.MessageService.Domain.Shared
```

2. Add `AbpMessageServiceDomainSharedModule` to your module’s dependency list:

```csharp
[DependsOn(typeof(AbpMessageServiceDomainSharedModule))]
public class YourModule : AbpModule
{
}
```

## More

[English document](README.EN.md)
