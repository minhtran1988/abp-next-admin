# LINGYUN.Abp.WeChat.Official.Senparc

WeChat public account Senparc SDK integration module provides integration support with Senparc.Weixin SDK.

## Features

* Senparc.Weixin SDK integration
* Compatible with Senparc message processing mechanism
* Compatible with Senparc event processing mechanism
* Support Senparc configuration system
* Support Senparc caching strategy

## Module reference

```csharp
[DependsOn(typeof(AbpWeChatOfficialSenparcModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

```json
{
  "WeChat": {
    "Official": {
      "Senparc": {
        "IsEnabled": true, // Whether to enable Senparc integration
        "Cache": {
          "Type": "Local", // Cache type: Local/Redis/Memcached
          "Configuration": "" // Cache configuration string
        }
      }
    }
  }
}
```

## Senparc Compatibility

This module is compatible with Senparc.Weixin SDK and can:

* Use Senparc's message processor
* Use Senparc's event handler
* Use Senparc’s API calling method
* Use Senparc's caching mechanism
* Use Senparc's extended functionality

## More documentation

* [WeChat public account Senparc integration module document](README.EN.md)
* [Senparc.Weixin SDK Documentation](https://github.com/JeffreySu/WeiXinMPSDK)
