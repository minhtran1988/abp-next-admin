# LINGYUN.Abp.WeChat.Work.Handlers

The enterprise WeChat message processing module provides the basic implementation of enterprise WeChat message and event processing.

## Features

* Text message processing
* Picture message processing
* Voice message processing
* Video message processing
* Location message processing
* Link message processing
*Event message processing
* Custom message processor extension

## Module reference

```csharp
[DependsOn(typeof(AbpWeChatWorkHandlersModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

```json
{
  "WeChat": {
    "Work": {
      "Handlers": {
        "DefaultResponseType": "text", //Default response type: text/image/voice/video/news
        "DefaultResponse": "", //Default response content
        "EnableDefaultResponse": true // Whether to enable the default response
      }
    }
  }
}
```

## Custom processor

To implement a custom message processor, you need to inherit the corresponding base class:

* Text message: `WeChatWorkTextMessageHandlerBase`
* Image message: `WeChatWorkImageMessageHandlerBase`
* Voice message: `WeChatWorkVoiceMessageHandlerBase`
* Video message: `WeChatWorkVideoMessageHandlerBase`
* Location message: `WeChatWorkLocationMessageHandlerBase`
* Link message: `WeChatWorkLinkMessageHandlerBase`
* Event message: `WeChatWorkEventMessageHandlerBase`

## Event handler

The following event handlers are built-in:

* Application menu event handler
* Enter the application event handler
* Report the geolocation event handler
* Asynchronous task completion event handler
* External contact event handler
* Address book change event handler

## More documentation

* [Enterprise WeChat message processing module document](README.EN.md)
