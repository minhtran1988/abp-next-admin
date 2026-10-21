# LINGYUN.Abp.Notifications.WeChat.Work

The Enterprise WeChat notification publishing module provides the function of sending messages to users through the Enterprise WeChat application.

## Features

* Support multiple message types
  * Text message
  * Text card messages
  * Markdown message
* Flexible message sending destination
  *Support sending to designated users
  *Support sending to designated departments
  *Support sending to specified tags
  *Supports sending to multiple applications
*Multi-language support
  * Support localized message content
  * Supports multi-language titles and descriptions
* Feature switch control
  * Supports controlling message sending through function switch
* Extensibility
  * Support custom notification definitions
  * Support custom message processing

## Module reference

```csharp
[DependsOn(typeof(AbpNotificationsWeChatWorkModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration instructions

### 1. Basic configuration

To send messages, you need to configure the application in the enterprise WeChat background and obtain the corresponding application ID (AgentId).

### 2. Notification definition configuration

```csharp
public class YourNotificationDefinitionProvider : NotificationDefinitionProvider
{
    public override void Define(INotificationDefinitionContext context)
    {
        var notification = context.Create(
            name: "App.Notification.Test",
            displayName: L("TestNotification"))
            .WithAgentId("1000001") //Set application ID
            .WithParty("1|2|3") //Set the receiving department
            .WithTag("TagId1|TagId2") //Set the receiving tag
            .WithAllAgent(); // Send to all applications
    }
}
```

### 3. Send notification

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
        notificationData.TrySetData("description", "Message description");

        //Set sending target
        notificationData.SetAgentId("1000001"); //Set application ID
        notificationData.SetParty("1|2|3"); //Set the receiving department
        notificationData.SetTag("TagId1|TagId2"); // Set the receiving tag
        notificationData.WithAllAgent(); //Send to all applications

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

1. When sending messages, you need to ensure that the enterprise WeChat application is configured correctly, including the application ID and related permissions.
2. The department ID and tag ID need to be consistent with the enterprise WeChat background configuration.
3. Use '|' to separate multiple recipients (departments/labels), up to 100 are supported.
4. When using `WithAllAgent()`, messages will be sent to all configured applications.
5. Message sending relies on the Enterprise WeChat API, and you need to ensure that the network connection is normal.

## More documentation

* [English Documentation](README.EN.md)
* [Enterprise WeChat Application Message Development Document](https://developer.work.weixin.qq.com/document/path/90236)
