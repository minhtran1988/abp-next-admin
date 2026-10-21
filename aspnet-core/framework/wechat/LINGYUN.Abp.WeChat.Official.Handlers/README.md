# LINGYUN.Abp.WeChat.Official.Handlers

The WeChat public account message processing module provides the basic implementation of WeChat public account message and event processing.

## Features

* Text message processing
* Picture message processing
* Voice message processing
* Video message processing
* Small video message processing
* Geolocation message processing
* Link message processing
* Follow/unfollow event processing
* Scan QR code event processing with parameters
* Report geographical location event processing
* Custom menu event handling
* Template message event processing

## Module reference

```csharp
[DependsOn(typeof(AbpWeChatOfficialHandlersModule))]
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
      "Handlers": {
        "DefaultResponseType": "text", //Default response type: text/image/voice/video/music/news
        "DefaultResponse": "", //Default response content
        "EnableDefaultResponse": true // Whether to enable the default response
      }
    }
  }
}
```

## Custom processor

To implement a custom message processor, you need to inherit the corresponding base class:

* Text message: `WeChatOfficialTextMessageHandlerBase`
* Image message: `WeChatOfficialImageMessageHandlerBase`
* Voice message: `WeChatOfficialVoiceMessageHandlerBase`
* Video message: `WeChatOfficialVideoMessageHandlerBase`
* Event message: `WeChatOfficialEventMessageHandlerBase`

## More documentation

* [WeChat public account message processing module document](README.EN.md)
