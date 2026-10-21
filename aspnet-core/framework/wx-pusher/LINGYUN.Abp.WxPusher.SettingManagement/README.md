# LINGYUN.Abp.WxPusher.SettingManagement

WxPusher settings management module provides management functions for WxPusher related settings.

[English](./README.EN.md)

## Features

* Support WxPusher settings management
  *AppToken management
  * Security settings management
* Permission control
  * Based on ABP permission system
  * Fine-grained setting management permissions
*Multi-tenant support
  * Support global settings
  *Support tenant-level settings
* Localization support
  * Support multi-language interface
  * Support localized setting description

## Installation

```bash
dotnet add package LINGYUN.Abp.WxPusher.SettingManagement
```

## Module reference

```csharp
[DependsOn(typeof(AbpWxPusherSettingManagementModule))]
public class YouProjectModule : AbpModule
{
    // other
}
```

## Permission configuration

### 1. Permission definition

* `WxPusher`: WxPusher permission group
* `WxPusher.ManageSetting`: Permission to manage WxPusher settings

### 2. Authorization configuration

```csharp
public class YourAuthorizationProvider : AuthorizationProvider
{
    public override void Define(IAuthorizationDefinitionContext context)
    {
        var wxPusher = context.GetPermissionOrNull(WxPusherSettingPermissionNames.GroupName)
            ?? context.AddGroup(WxPusherSettingPermissionNames.GroupName);

        wxPusher.AddPermission(WxPusherSettingPermissionNames.ManageSetting);
    }
}
```

## Usage

### 1. Get settings

```csharp
public class YourService
{
    private readonly IWxPusherSettingAppService _wxPusherSettingAppService;

    public YourService(IWxPusherSettingAppService wxPusherSettingAppService)
    {
        _wxPusherSettingAppService = wxPusherSettingAppService;
    }

    public async Task GetSettingsAsync()
    {
        // Get global settings
        var globalSettings = await _wxPusherSettingAppService.GetAllForGlobalAsync();

        // Get current tenant settings
        var tenantSettings = await _wxPusherSettingAppService.GetAllForCurrentTenantAsync();
    }
}
```

### 2. API interface

* `GET /api/setting-management/wxpusher/by-global`: Get global settings
* `GET /api/setting-management/wxpusher/by-current-tenant`: Get the current tenant settings

## Notes

1. Properly configured permissions are required to manage settings.
2. Sensitive information such as AppToken needs to be properly kept.
3. In a multi-tenant environment, you need to pay attention to the scope of settings.

## Source code location

[LINGYUN.Abp.WxPusher.SettingManagement](https://github.com/colinin/abp-next-admin/tree/master/aspnet-core/framework/wx-pusher/LINGYUN.Abp.WxPusher.SettingManagement)
