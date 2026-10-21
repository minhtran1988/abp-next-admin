# LINGYUN.Abp.IM.SignalR

Instant messaging module implemented based on SignalR.

## Features

* Message sender provider implemented based on SignalR
* Integrated ABP SignalR module
*Multi-language support

## Dependent modules

* [LINGYUN.Abp.IM](../LINGYUN.Abp.IM/README.md)
* `AbpAspNetCoreSignalRModule`

## Configuration usage

1. First, you need to install LINGYUN.Abp.IM.SignalR into your project:

```bash
dotnet add package LINGYUN.Abp.IM.SignalR
```

2. Add `AbpIMSignalRModule` to your module’s dependency list:

```csharp
[DependsOn(typeof(AbpIMSignalRModule))]
public class YourModule : AbpModule
{
}
```

## More

[English document](README.EN.md)
