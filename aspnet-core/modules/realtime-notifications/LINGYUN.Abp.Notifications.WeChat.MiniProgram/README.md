# LINGYUN.Abp.Notifications.WeChat.MiniProgram

The WeChat applet notification publishing module provides the function of sending subscription messages to users through the WeChat applet.

## Features

* WeChat applet subscription message sending
* Support custom message templates
* Support multi-language messages
* Support different environments (development version, trial version, official version)
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

> Note: This configuration item will be removed in the next WeChat-related major version and merged into LINGYUN.Abp.WeChat.MiniProgram.AbpWeChatMiniProgramOptions

```json
{
  "Notifications": {
    "WeChat": {
      "MiniProgram": {
        "DefaultMsgPrefix": "", //Default message header mark
        "DefaultTemplateId": "", //Default applet template ID
        "DefaultState": "developer", //Default jump applet type: developer (development version), trial (experience version), formal (formal version)
        "DefaultLanguage": "zh_CN" //Default applet language
      }
    }
  }
}
```

## Usage

### Send notification

```csharp
public class YourService
{
    private readonly INotificationPublisher _notificationPublisher;

    public YourService(INotificationPublisher notificationPublisher)
    {
        _notificationPublisher = notificationPublisher;
    }

    public async Task SendNotificationAsync()
    {
        await _notificationPublisher.PublishAsync(
            "WeChatMiniProgram.Notification", // notification name
            new NotificationData(), // notification data
            userIds: new[] { "userId" }, // Receive user ID list
            tenantIds: new[] { "tenantId" } // Tenant ID list
        );
    }
}
```

### Custom notification processing

```csharp
public class YourNotificationHandler : 
    INotificationHandler<WeChatMiniProgramNotification>
{
    public async Task HandleNotificationAsync(
        WeChatMiniProgramNotification notification)
    {
        // Handle notification
    }
}
```

## More documentation

* [WeChat Mini Program Notification Release Module Document](README.EN.md)
* [WeChat Mini Program Subscription Message Development Document](https://developers.weixin.qq.com/miniprogram/dev/framework/open-ability/subscribe-message.html)
