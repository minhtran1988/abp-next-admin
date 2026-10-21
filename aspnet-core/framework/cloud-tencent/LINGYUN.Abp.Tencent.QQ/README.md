# LINGYUN.Abp.Tencent.QQ

Tencent QQ Internet module integrates Tencent QQ Internet services into ABP applications.

## Features

* Support QQ Internet quick login
* Support multi-tenant configuration
* Provide QQ Internet client factory to support dynamic creation of clients

## Configuration item description

### Basic configuration

```json
{
  "Settings": {
    "Abp.TencentCloud": {
      "SecretId": "Your Tencent Cloud SecretId", // Get it from the Tencent Cloud console
      "SecretKey": "Your Tencent Cloud SecretKey", // Get it from the Tencent Cloud console
      "DurationSecond": "600" // Session duration (seconds)
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

## Basic usage

1. Add module dependencies
```csharp
[DependsOn(typeof(AbpTencentQQModule))]
public class YourModule : AbpModule
{
    // ...
}
```

2. Configure QQ interconnection service

Refer to the above configuration item descriptions to configure accordingly.

3. QQ Internet service usage examples
```csharp
public class YourService
{
    private readonly TencentQQClientFactory _qqClientFactory;

    public YourService(TencentQQClientFactory qqClientFactory)
    {
        _qqClientFactory = qqClientFactory;
    }

    public async Task QQConnectAsync()
    {
        var qqClient = await _qqClientFactory.CreateAsync();
        // Use qqClient to call QQ Internet service API
        // For detailed API usage, please refer to QQ Internet development documentation
    }
}
```

## More documentation

* [QQ Internet Open Platform](https://connect.qq.com/)
* [QQ Internet Development Documents](https://wiki.connect.qq.com/)

[English](./README.EN.md)
