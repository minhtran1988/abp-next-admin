# LINGYUN.Abp.AspNetCore.Mvc.Localization

The ABP framework localization management module provides a Web API interface for localized resources.

## Features

* Provide localized text management API interface
* Support query and comparison of multi-language resources
* Supports filtering by resource name and culture name
* Support the integration of external localization resources
* Supports difference comparison of localized resources

## Installation

```bash
dotnet add package LINGYUN.Abp.AspNetCore.Mvc.Localization
```

## Module dependencies

```csharp
[DependsOn(typeof(AbpAspNetCoreMvcLocalizationModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## API interface

### Language management

* GET /api/localization/languages - Get all available languages
* GET /api/localization/languages/{cultureName} - Get specified language information

### Resource Management

* GET /api/localization/resources - Get all localization resources
* GET /api/localization/resources/{name} - Get the specified localization resource

### Text Management

* GET /api/localization/texts - Get a list of localized texts
* GET /api/localization/texts/by-key - Get localized text based on key
* GET /api/localization/texts/differences - Get text differences between different languages

## Basic usage

1. Configure localized resources
```csharp
Configure<AbpLocalizationOptions>(options =>
{
    options.Resources
        .Get<YourLocalizationResource>()
        .AddVirtualJson("/Your/Resource/Path");
});
```

2. Inject and use the service
```csharp
public class YourService
{
    private readonly ITextAppService _textAppService;
    
    public YourService(ITextAppService textAppService)
    {
        _textAppService = textAppService;
    }
    
    public async Task GetLocalizedText(string key, string cultureName)
    {
        var text = await _textAppService.GetByCultureKeyAsync(new GetTextByKeyInput
        {
            Key = key,
            CultureName = cultureName,
            ResourceName = "YourResourceName"
        });
        // Use the localized text
    }
}
```

## More information

* [English Documentation](./README.EN.md)
* [ABP Localization Document](https://docs.abp.io/zh-Hans/abp/latest/Localization)
