# LINGYUN.Abp.Http.Client.Wrapper

HTTP client wrapper module for automatically adding wrapper request headers in HTTP client requests.

[English](./README.EN.md)

## Features

* Automatically add wrapper request headers
* Integrated with ABP HTTP client
* Support global configuration wrapper switch

## Installation

```bash
dotnet add package LINGYUN.Abp.Http.Client.Wrapper
```

## Configuration usage

```csharp
[DependsOn(typeof(AbpHttpClientWrapperModule))]
public class YouProjectModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpWrapperOptions>(options =>
        {
            // enable wrapper
            options.IsEnabled = true;
        });
    }
}
```

## Working principle

When the wrapper is enabled (`AbpWrapperOptions.IsEnabled = true`), the module automatically adds the `_AbpWrapResult` request header to all HTTP client requests.
When the wrapper is disabled (`AbpWrapperOptions.IsEnabled = false`), the module automatically adds the `_AbpDontWrapResult` request header to all HTTP client requests.

This ensures that the HTTP client's request results are consistent with the server-side packaging configuration.

## Source code location

[LINGYUN.Abp.Http.Client.Wrapper](https://github.com/colinin/abp-next-admin/tree/master/aspnet-core/framework/common/LINGYUN.Abp.Http.Client.Wrapper)
