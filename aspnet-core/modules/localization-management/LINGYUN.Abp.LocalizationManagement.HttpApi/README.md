# LINGYUN.Abp.LocalizationManagement.HttpApi

Localized management HTTP API module, providing RESTful style API interface.

## Features

* Provide HTTP API interface for localized management
* Support REST operations for languages, resources, and texts
* Integrate ABP dynamic API function
* Support API version control
* Support Swagger document generation

## Module reference

```csharp
[DependsOn(typeof(AbpLocalizationManagementHttpApiModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## API Controller

* `LanguageController`: Language management API controller
* `ResourceController`: Resource management API controller
* `TextController`: text management API controller

## Configuration items

```csharp
public override void PreConfigureServices(ServiceConfigurationContext context)
{
    PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
    {
        options.AddAssemblyResource(
            typeof(LocalizationManagementResource),
            typeof(AbpLocalizationManagementApplicationContractsModule).Assembly);
    });

    PreConfigure<IMvcBuilder>(mvcBuilder =>
    {
        mvcBuilder.AddApplicationPartIfNotExists(typeof(AbpLocalizationManagementApplicationContractsModule).Assembly);
    });
}
```

## API routing

* `/api/localization-management/languages` - Language management related API
* `/api/localization-management/resources` - Resource management related API
* `/api/localization-management/texts` - Text management related API

## More information

* [English documentation](./README.EN.md)
