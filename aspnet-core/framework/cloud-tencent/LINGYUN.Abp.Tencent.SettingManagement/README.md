# LINGYUN.Abp.Tencent.SettingManagement

Tencent Cloud service settings management module provides the configuration management interface and API of Tencent Cloud services.

## Features

* Provides configuration management interface for Tencent Cloud services
* Support global and tenant level configuration management
* Supports the configuration of the following Tencent Cloud services:
  *Basic configuration (key, region, etc.)
  * Connection configuration (HTTP method, timeout, proxy, etc.)
  * SMS service configuration (application ID, default signature, default template, etc.)
  * QQ interconnection configuration (application ID, application key, etc.)
* Built-in permission management
* Support multi-language localization
* Supports the configuration of all Tencent Cloud available zones

## Permissions

* `TencentCloud` - Tencent Cloud service permission group
  * `TencentCloud.Settings` - Configure Tencent Cloud service permissions

## API interface

### Get global configuration

```http
GET /api/setting-management/tencent/by-global
```

### Get the current tenant configuration

```http
GET /api/setting-management/tencent/by-current-tenant
```

## Basic usage

1. Add module dependencies
```csharp
[DependsOn(typeof(AbpTencentCloudSettingManagementModule))]
public class YourModule : AbpModule
{
    // ...
}
```

2. Authorization configuration
```csharp
public override void ConfigureServices(ServiceConfigurationContext context)
{
    Configure<AbpPermissionOptions>(options =>
    {
        options.GrantByDefault(TencentCloudSettingPermissionNames.Settings);
    });
}
```

## Configuration item description

### Basic configuration

```json
{
  "Settings": {
    "Abp.TencentCloud": {
      "EndPoint": "ap-guangzhou", // The region where the resource is located, the default is Guangzhou
      "SecretId": "Your Tencent Cloud SecretId", // Get it from the Tencent Cloud console
      "SecretKey": "Your Tencent Cloud SecretKey", // Get it from the Tencent Cloud console
      "DurationSecond": "600" // Session duration, in seconds
    }
  }
}
```

### Connection configuration

```json
{
  "Settings": {
    "Abp.TencentCloud.Connection": {
      "HttpMethod": "POST", // Request method, default POST
      "Timeout": "60", // Connection timeout, in seconds
      "WebProxy": "", // proxy server address, optional
      "EndPoint": "" // The domain name of a specific service, such as the financial district service needs to be specified
    }
  }
}
```

### SMS service configuration

```json
{
  "Settings": {
    "Abp.TencentCloud.Sms": {
      "AppId": "", // SMS application ID, generated after adding the application in the SMS console
      "DefaultSignName": "", //Default SMS signature
      "DefaultTemplateId": "" //Default SMS template ID
    }
  }
}
```

### QQ interconnection configuration

```json
{
  "Settings": {
    "Abp.TencentCloud.QQConnect": {
      "AppId": "", // QQ Internet application ID, obtained from QQ Internet Management Center
      "AppKey": "", // QQ Internet application key, obtained from QQ Internet Management Center
      "IsMobile": "false" // Whether to use mobile style, the default is PC style
    }
  }
}
```

## Available areas

The module supports the following Tencent Cloud regions:

*China region
  * North China (Beijing) - ap-beijing
  * Southwest Region (Chengdu) - ap-chengdu
  * Southwest Region (Chongqing) - ap-chongqing
  * South China (Guangzhou) - ap-guangzhou
  * Hong Kong, Macao and Taiwan (Hong Kong, China) - ap-hongkong
  * East China (Nanjing) - ap-nanjing
  * East China (Shanghai) - ap-shanghai
  * East China (Shanghai Finance) - ap-shanghai-fsi
  * South China (Shenzhen Finance) - ap-shenzhen-fsi

*Asia Pacific
  * Bangkok - ap-bangkok
  * Jakarta - ap-jakarta
  * Mumbai - ap-mumbai
  * Seoul - ap-seoul
  * Singapore - ap-singapore
  * Tokyo - ap-tokyo

* European region
  * Frankfurt - eu-frankfurt
  * Moscow - eu-moscow

* North America
  * Virginia - na-ashburn
  * Silicon Valley - na-siliconvalley
  * Toronto - na-toronto

## More documentation

* [Tencent Cloud API Key Management](https://console.cloud.tencent.com/cam/capi)
* [Tencent Cloud Region and Availability Zone](https://cloud.tencent.com/document/product/213/6091)

[English](./README.EN.md)
