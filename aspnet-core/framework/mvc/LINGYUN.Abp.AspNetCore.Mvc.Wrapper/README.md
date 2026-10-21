# LINGYUN.Abp.AspNetCore.Mvc.Wrapper

Wrapper MVC implementation module, used to uniformly wrap the response results of ASP.NET Core MVC.

[English](./README.EN.md)

## Features

* Automatically wrap MVC response results
* Support custom packaging rules
* Support abnormal result packaging
* Support localized error messages
* Support API document packaging description
* Supports packaging that ignores specific controllers, namespaces, and return types

## Installation

```bash
dotnet add package LINGYUN.Abp.AspNetCore.Mvc.Wrapper
```

## Configuration usage

```csharp
[DependsOn(typeof(AbpAspNetCoreMvcWrapperModule))]
public class YouProjectModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpWrapperOptions>(options =>
        {
            // enable wrapper
            options.IsEnabled = true;
            
            // Ignore specific return types
            options.IgnoreReturnTypes.Add<IRemoteStreamContent>();
            
            // Ignore specific controllers
            options.IgnoreControllers.Add<AbpApiDefinitionController>();
            
            // Ignore specific URL prefixes
            options.IgnorePrefixUrls.Add("/connect");
            
            // Customize empty result message
            options.MessageWithEmptyResult = (serviceProvider) =>
            {
                var localizer = serviceProvider.GetRequiredService<IStringLocalizer<AbpMvcWrapperResource>>();
                return localizer["Wrapper:NotFound"];
            };
        });
    }
}
```

## Configuration item description

* `IsEnabled`: Whether to enable the wrapper
* `IsWrapUnauthorizedEnabled`: whether to wrap unauthorized responses
* `HttpStatusCode`: wraps the HTTP status code of the response
* `IgnoreReturnTypes`: Ignore the wrapped return type list
* `IgnoreControllers`: Ignore the wrapped controller list
* `IgnoreNamespaces`: Ignore the wrapped namespace list
* `IgnorePrefixUrls`: Ignore the wrapped URL prefix list
* `IgnoreExceptions`: Ignore the wrapped exception type list
* `ErrorWithEmptyResult`: whether an empty result returns an error message
* `CodeWithEmptyResult`: error code for empty results
* `MessageWithEmptyResult`: error message for empty results
* `CodeWithSuccess`: code on success

## Advanced usage

### 1. Use attribute tag to ignore packaging

```csharp
[IgnoreWrapResult]
public class TestController : AbpController
{
    // All Actions under this controller will not be wrapped
}

public class TestController : AbpController
{
    [IgnoreWrapResult]
    public IActionResult Test()
    {
        // This Action will not be wrapped
    }
}
```

### 2. Control packaging through request headers

* Add `_AbpDontWrapResult` request header to disable wrapping
* Add `_AbpWrapResult` request header to force wrapping to be enabled

## Source code location

[LINGYUN.Abp.AspNetCore.Mvc.Wrapper](https://github.com/colinin/abp-next-admin/tree/master/aspnet-core/framework/mvc/LINGYUN.Abp.AspNetCore.Mvc.Wrapper)
