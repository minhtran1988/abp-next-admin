# LINGYUN.Abp.Elsa.Server

Elsa.Server.Api integration, handles elsa default endpoint


## Configuration usage

```csharp

    [DependsOn(
        typeof(AbpElsaServerModule)
        )]
    public class YouProjectModule : AbpModule
    {
    }
```