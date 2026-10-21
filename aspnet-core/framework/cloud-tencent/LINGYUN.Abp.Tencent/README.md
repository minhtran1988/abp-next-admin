# LINGYUN.Abp.Tencent

Tencent Cloud service basic module provides infrastructure support for other Tencent Cloud service modules.

## Features

* Provides Tencent Cloud SDK client factory to support dynamic creation of clients for Tencent Cloud services
* Support multi-tenant configuration
* Built-in multi-language localization support (Chinese and English)
* Provide unified Tencent Cloud service configuration management
* Supports feature management, enabling/disabling functions as needed
* Support regional (Region) localized display

## Configuration items

### Basic configuration

```json
{
  "Settings": {
    "LINGYUN.Abp.Tencent": {
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
    "LINGYUN.Abp.Tencent.Connection": {
      "HttpMethod": "POST", // Request method, default POST
      "Timeout": "60", // Connection timeout, in seconds
      "WebProxy": "" // proxy server address, optional
    }
  }
}
```

## Basic usage

1. Add module dependencies
```csharp
[DependsOn(typeof(AbpTencentCloudModule))]
public class YourModule : AbpModule
{
    // ...
}
```

2. Configure Tencent Cloud services
```json
{
  "Settings": {
    "LINGYUN.Abp.Tencent": {
      "SecretId": "Your Tencent Cloud SecretId",
      "SecretKey": "Your Tencent Cloud SecretKey"
    }
  }
}
```

## Advanced features

### Feature Management

The module provides the following feature switches:

* TencentSms - Tencent Cloud SMS service
* TencentBlobStoring - Tencent Cloud object storage service
  * MaximumStreamSize - Single upload file stream size limit (MB)

### Multi-tenant support

All configurations support multi-tenant configuration, and different Tencent Cloud service parameters can be configured for different tenants.

## More documentation

* [Tencent Cloud API Key Management](https://console.cloud.tencent.com/cam/capi)
* [Tencent Cloud Region and Availability Zone](https://cloud.tencent.com/document/product/213/6091)

[English](./README.EN.md)
