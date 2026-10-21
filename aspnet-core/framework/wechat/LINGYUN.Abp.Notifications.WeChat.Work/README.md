# LINGYUN.Abp.Notifications.WeChat.Work

The enterprise WeChat application message notification module provides the function of sending messages to users through the enterprise WeChat application.

## Features

* Support enterprise WeChat application messaging
* Support text messages
* Support picture messages
* Support voice messages
* Support video messages
* Support file messages
* Support graphic messages
* Support template card messages
* Integrated ABP notification system

## Module reference

```csharp
[DependsOn(typeof(AbpNotificationsWeChatWorkModule))]
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
      "Notifications": {
        "DefaultAgentId": 0, //Default application ID
        "DefaultToParty": "", // List of department IDs that receive messages by default
        "DefaultToTag": "", // Default tag ID list for receiving messages
        "DefaultSafe": 0, //The default indicates whether the message is confidential, 0 indicates that it can be shared externally, 1 indicates that it cannot be shared and the content displays a watermark
        "DefaultEnableIdTrans": 0, //The default indicates whether to enable id translation, 0 indicates no, 1 indicates yes
        "DefaultEnableDuplicateCheck": 0, //The default indicates whether to enable duplicate message checking, 0 indicates no, 1 indicates yes
        "DefaultDuplicateCheckInterval": 1800 //The default indicates the time interval for checking duplicate messages. The default is 1800 seconds.
      }
    }
  }
}
```

## More documentation

* [Enterprise WeChat Application Message Notification Document](README.EN.md)
