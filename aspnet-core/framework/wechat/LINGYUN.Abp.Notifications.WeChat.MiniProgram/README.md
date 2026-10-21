# LINGYUN.Abp.Notifications.WeChat.MiniProgram

The WeChat applet message notification module provides the function of sending subscription messages to users through the WeChat applet.

## Features

* Support WeChat applet subscription message sending
* Support message template management
* Support dynamic message data configuration
* Integrated ABP notification system

## Module reference

```csharp
[DependsOn(typeof(AbpNotificationsWeChatMiniProgramModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

```json
{
  "WeChat": {
    "MiniProgram": {
      "Notifications": {
        "DefaultTemplateId": "", //Default template ID
        "DefaultPage": "pages/index/index", // Default jump page
        "DefaultState": "formal", //The default jump applet type, developer is the development version; trial is the experience version; formal is the official version
        "DefaultLang": "zh_CN" //Default language
      }
    }
  }
}
```

## More documentation

* [WeChat Mini Program Message Notification Document](README.EN.md)
