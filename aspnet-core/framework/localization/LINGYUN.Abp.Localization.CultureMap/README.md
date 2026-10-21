# LINGYUN.Abp.Localization.CultureMap

## Module description

This module is used to solve regional localization problems where multiple formats exist. It allows you to map different formats of culture identifiers to a standard format.

Reference project: [Owl.Abp.CultureMap](https://github.com/maliming/Owl.Abp.CultureMap)

## Features

* Support mapping multiple formats of culture identifiers to standard formats
* Support independent mapping of culture (Culture) and UI culture (UICulture)
* Integrated with ABP request localization
* Support custom culture mapping rules

## Installation

```bash
dotnet add package LINGYUN.Abp.Localization.CultureMap
```

## Basic module

* Volo.Abp.AspNetCore

## Configuration instructions

The module provides the following configuration options:

* CulturesMaps: list of culture maps
* UiCulturesMaps: UI culture mapping list

Each mapping item contains:
* TargetCulture: target culture identifier
* SourceCultures: list of source culture identifiers

## How to use

1. Add module dependencies:

```csharp
[DependsOn(
    typeof(AbpLocalizationCultureMapModule))]
public class YouProjectModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationCultureMapOptions>(options =>
        {
            var zhHansCultureMapInfo = new CultureMapInfo
            {
                TargetCulture = "zh-Hans",
                SourceCultures = new string[] { "zh", "zh_CN", "zh-CN" }
            };

            options.CulturesMaps.Add(zhHansCultureMapInfo);
            options.UiCulturesMaps.Add(zhHansCultureMapInfo);
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();

        app.UseMapRequestLocalization();
    }
}
```

## Common use cases

1. Unify simplified Chinese regional identification:
```csharp
options.CulturesMaps.Add(new CultureMapInfo
{
    TargetCulture = "zh-Hans",
    SourceCultures = new string[] { "zh", "zh_CN", "zh-CN" }
});
```

2. Unify Traditional Chinese regional identification:
```csharp
options.CulturesMaps.Add(new CultureMapInfo
{
    TargetCulture = "zh-Hant",
    SourceCultures = new string[] { "zh_TW", "zh-TW", "zh_HK", "zh-HK" }
});
```

## More information

* [English Documentation](./README.EN.md)
* [ABP Localization Document](https://docs.abp.io/zh-Hans/abp/latest/Localization)

## Update log
