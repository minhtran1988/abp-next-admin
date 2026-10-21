# LINGYUN.Abp.Notifications.PushPlus

PushPlus implementation of notification module

Enable apps to publish real-time notifications via PushPlus

## Features

* Support multiple message types
  * Text message
  * HTML message
  * Markdown message
  * Picture message
  * Custom template message
* Support message callback
  * Support custom callback URL
  * Support message sending status callback
*Support multi-channel push
  * WeChat public account
  * Enterprise WeChat
  * Email
  * SMS
  * WebHook
* Support group messages
  * Support group push
  * Support group management

## Module reference

```csharp
[DependsOn(typeof(AbpNotificationsPushPlusModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

```json
{
  "PushPlus": {
    "Token": "Your PushPlus Token",
    "DefaultChannel": "wechat", //Default push channel: wechat/webhook/mail/sms
    "DefaultTemplate": "html", //Default message template: html/json/markdown/txt
    "DefaultWebhook": "", //Default Webhook address
    "DefaultCallbackUrl": "" //Default callback address
  }
}
```

## Basic usage

1. Configure PushPlus service
```csharp
public override void ConfigureServices(ServiceConfigurationContext context)
{
    Configure<AbpNotificationsPushPlusOptions>(options =>
    {
        options.Token = "Your PushPlus Token";
        options.DefaultChannel = "wechat";
        options.DefaultTemplate = "html";
    });
}
```

2. Send notification
```csharp
public class YourService
{
    private readonly INotificationSender _notificationSender;

    public YourService(INotificationSender notificationSender)
    {
        _notificationSender = notificationSender;
    }

    public async Task SendNotificationAsync()
    {
        var notificationData = new NotificationData();
        notificationData.TrySetData("title", "Message title");
        notificationData.TrySetData("content", "Message content");
        notificationData.SetWebhook("https://your-webhook.com");
        notificationData.SetCallbackUrl("https://your-callback.com");

        await _notificationSender.SendNofiterAsync(
            "YourNotification",
            notificationData,
            userIds: new[] { CurrentUser.Id }
        );
    }
}
```

## Notes

1. You need to register on the PushPlus platform and obtain a Token before use.
2. Make sure the configured Token is valid and has sufficient permissions.
3. The callback URL must be an address accessible from the public network.
4. Different channels may have different message format requirements.
5. Message sending relies on PushPlus API, and you need to ensure that the network connection is normal.

## More information

* [PushPlus official document](http://www.pushplus.plus/doc/)
* [ABP Documentation](https://docs.abp.io)
