# LINGYUN.Abp.Notifications.WxPusher

The ABP notification module implemented through WxPusher provides the function of sending real-time notifications through WxPusher.

[English](./README.EN.md)

## Features

* Support multiple message types
  * Text message
  * HTML message
  * Markdown message
* Flexible message sending destination
  *Support sending to designated users
  * Support sending to specified Topic
*Multi-language support
  * Support localized message content
  * Supports multi-language titles and descriptions
* Feature switch control
  * Supports controlling message sending through function switch

## Installation

```bash
dotnet add package LINGYUN.Abp.Notifications.WxPusher
```

## Module reference

```csharp
[DependsOn(typeof(AbpNotificationsWxPusherModule))]
public class YouProjectModule : AbpModule
{
    // other
}
```

## Configuration instructions

### 1. Notification definition configuration

```csharp
public class YourNotificationDefinitionProvider : NotificationDefinitionProvider
{
    public override void Define(INotificationDefinitionContext context)
    {
        var notification = context.Create(
            name: "App.Notification.Test",
            displayName: L("TestNotification"))
            .WithContentType(MessageContentType.Text) //Set message type
            .WithTopics(new List<int> { 1, 2 }) // Set message Topic
            .WithUrl("https://example.com"); // Set the URL to jump to when clicking on the message
    }
}
```

### 2. Send notification

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
        var notificationData = new NotificationData();
        
        //Set message content
        notificationData.TrySetData("title", "Message title");
        notificationData.TrySetData("message", "Message content");
        notificationData.SetUrl("https://example.com"); // Set the URL to jump to when clicking on the message

        await _notificationPublisher.PublishAsync(
            "App.Notification.Test", // notification name
            notificationData, // notification data
            userIds: new[] { "userId" }, // Receive user ID list
            tenantIds: new[] { "tenantId" } // Tenant ID list
        );
    }
}
```

## Notes

1. The `IWxPusherUserStore` interface needs to be implemented to manage the association between users and WxPusher.
2. Message sending relies on WxPusher API, and you need to ensure that the network connection is normal.
3. Please set the message content length appropriately to avoid exceeding the WxPusher limit.
4. When using the multi-language feature, you need to ensure that localization resources are configured correctly.

## Source code location

[LINGYUN.Abp.Notifications.WxPusher](https://github.com/colinin/abp-next-admin/tree/master/aspnet-core/modules/realtime-notifications/LINGYUN.Abp.Notifications.WxPusher)
