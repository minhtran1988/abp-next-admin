# LINGYUN.Abp.Hangfire.Dashboard

[English](README.EN.md) | Simplified Chinese

## 1. Introduction

`LINGYUN.Abp.Hangfire.Dashboard` is an ABP module for integrating Hangfire dashboard, which provides a user-friendly web interface to monitor and manage Hangfire background jobs. This module supports permission control and authentication to ensure secure access to the dashboard.

## 2. Features

* Integrated Hangfire dashboard
*Access control based on ABP permission system
* Support loading dashboard through iframe
* Support access token authentication
* Dashboard permission caching mechanism

## 3. Installation

```bash
dotnet add package LINGYUN.Abp.Hangfire.Dashboard
```

## 4. How to use

1. Add `AbpHangfireDashboardModule` to module dependencies:

```csharp
[DependsOn(typeof(AbpHangfireDashboardModule))]
public class YourModule : AbpModule
{
}
```

2. Configure middleware:

```csharp
public override void OnApplicationInitialization(ApplicationInitializationContext context)
{
    var app = context.GetApplicationBuilder();
    
    //Add Hangfire authentication middleware
    app.UseHangfireAuthorication();
}
```

3. Configure dashboard options:

```csharp
public override void ConfigureServices(ServiceConfigurationContext context)
{
    PreConfigure<DashboardOptions>(options =>
    {
        options.Authorization = new[]
        {
            new DashboardAuthorizationFilter("YourPermissionName")
        };
    });
}
```

## 5. Authentication and Authorization

### 5.1 Authentication method

The module supports the following authentication methods:
* Pass the access token via URL parameter: `?access_token=your_token`
* Pass access token via cookie
* Pass the access token through the Authorization Header

### 5.2 Permission cache

Permission check results are cached for 5 minutes to improve performance.

## 6. Dependencies

* Volo.Abp.Authorization
* Volo.Abp.Hangfire
* Microsoft.Extensions.Caching.Memory

## 7. Documentation and Resources

* [Hangfire official documentation](https://docs.hangfire.io/)
* [ABP Framework Documentation](https://docs.abp.io/)
