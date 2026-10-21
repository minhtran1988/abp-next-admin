# LINGYUN.Abp.AspNetCore.HttpOverrides

## Module description

Some overriding class modules for Http transport headers

The current implementation obtains the real customer address in the reverse proxy by parsing the **X-Forwarded-For** header, usually obtaining the last setting value in the header.

### Basic module

### High-level modules

### Permission definition

### Function definition

### Configuration definition

### How to use


```csharp

    [DependsOn(
        typeof(AbpAspNetCoreHttpOverridesModule))]
    public class YouProjectModule : AbpModule
    {
    }

```

### Update log
