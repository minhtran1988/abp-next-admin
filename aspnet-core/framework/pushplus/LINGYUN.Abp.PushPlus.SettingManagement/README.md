# LINGYUN.Abp.PushPlus.SettingManagement

PushPlus settings management module provides the management interface and API interface for PushPlus configuration.

## Features

* Provide PushPlus configuration management interface
* Support global configuration management
* Support tenant configuration management
* Support multiple languages
* Integrate ABP settings management module

## Installation

```bash
dotnet add package LINGYUN.Abp.PushPlus.SettingManagement
```

## Module reference

```csharp
[DependsOn(typeof(AbpPushPlusSettingManagementModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## API interface

### Get global configuration

```http
GET /api/setting-management/push-plus/by-global
```

Response example:
```json
{
  "name": "PushPlus",
  "displayName": "PushPlus Configuration",
  "settings": [
    {
      "name": "PushPlus.Security.Token",
      "value": "your-token",
      "displayName": "Token",
      "description": "Token obtained by PushPlus platform"
    },
    {
      "name": "PushPlus.Security.SecretKey",
      "value": "your-secret",
      "displayName": "Key",
      "description": "Key obtained by PushPlus platform"
    }
  ]
}
```

### Get tenant configuration

```http
GET /api/setting-management/push-plus/by-current-tenant
```

The response format is the same as the global configuration.

## Permissions

* Abp.PushPlus.ManageSetting - Permission to manage PushPlus configuration

## Localization

The module supports the following languages:
* Simplified Chinese
*English

## More information

* [LINGYUN.Abp.PushPlus](../LINGYUN.Abp.PushPlus/README.md)
* [ABP Settings Management](https://docs.abp.io/en/abp/latest/Settings)
