# LINGYUN.Abp.WeChat

WeChat basic module provides basic functions and configurations for WeChat application development.

## Features

* WeChat basic configuration management
* Unified WeChat API calling interface
* WeChat AccessToken management
* Unified error handling mechanism

## Module reference

```csharp
[DependsOn(typeof(AbpWeChatModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

```json
{
  "WeChat": {
    "BaseUrl": "https://api.weixin.qq.com", // WeChat API base address
    "DefaultTimeout": 30000, //Default timeout (milliseconds)
    "RetryCount": 3, //Number of retries
    "RetryMilliseconds": 1000 //Retry interval (milliseconds)
  }
}
```

## SettingsConfiguration

* `WeChat.BaseUrl`: WeChat API base address
* `WeChat.DefaultTimeout`: Default timeout
* `WeChat.RetryCount`: Number of retries
* `WeChat.RetryMilliseconds` : retry interval

## More documentation

* [WeChat basic module document](README.EN.md)
