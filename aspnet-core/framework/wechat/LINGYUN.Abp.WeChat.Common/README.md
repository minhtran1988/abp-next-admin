# LINGYUN.Abp.WeChat.Common

WeChat general module provides basic function implementation for WeChat product line sharing.

## Features

* Unified message processing framework
* Unified message encryption and decryption mechanism
* Unified event handling mechanism
* Unified API calling interface
* Unified error handling mechanism

## Module reference

```csharp
[DependsOn(typeof(AbpWeChatCommonModule))]
public class YouProjectModule : AbpModule
{
}
```

## Configuration items

```json
{
  "WeChat": {
    "Common": {
      "MessageHandlerFactory": {
        "MessageResolvers": [], // Message resolver list
        "MessageHandlers": [] // Message handler list
      }
    }
  }
}
```

## Message processing

* Support text message processing
* Support picture message processing
* Support voice message processing
* Support video message processing
* Support event message processing
* Support custom message processor extension

## More documentation

* [WeChat general module document](README.EN.md)

### Update log
