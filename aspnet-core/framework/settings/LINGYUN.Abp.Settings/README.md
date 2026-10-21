# LINGYUN.Abp.Settings

## Module description

ABP settings management extension module provides additional settings management functions.

### Basic module

* Volo.Abp.Settings

### Function definition

* Extended the ISettingProvider interface to provide more convenient settings acquisition methods
  * GetOrDefaultAsync - Get the setting value, if it is empty, return the default value

### Configuration definition

No special configuration items

### How to use

1. Add module dependencies

```csharp
[DependsOn(typeof(AbpSettingsModule))]
public class YouProjectModule : AbpModule
{
}
```

2. Use extension methods

```csharp
public class YourService
{
    private readonly ISettingProvider _settingProvider;
    private readonly IServiceProvider _serviceProvider;

    public YourService(
        ISettingProvider settingProvider,
        IServiceProvider serviceProvider)
    {
        _settingProvider = settingProvider;
        _serviceProvider = serviceProvider;
    }

    public async Task<string> GetSettingValueAsync(string name)
    {
        // Get the setting value, if it is empty, return the default value
        return await _settingProvider.GetOrDefaultAsync(name, _serviceProvider);
    }
}
```

[Return to directory](../../../README.md)
