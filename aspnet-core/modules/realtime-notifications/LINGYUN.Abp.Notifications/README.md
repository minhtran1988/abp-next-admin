# LINGYUN.Abp.Notifications

[English](./README.EN.md) | Simplified Chinese

Real-time notification basic module.

## Function

* Supports multiple notification types (system, user, application, service callback)
* Supports multiple notification life cycles (one-time, persistent)
* Supports multiple notification content types (text, JSON, HTML, Markdown)
* Supports multiple notification severity levels (success, information, warning, error, fatal)
* Support notification subscription management
* Support notification status management (read/unread)
*Support multi-tenancy
* Support localization
* Support custom notification providers

## Module dependencies

```csharp
[DependsOn(typeof(AbpNotificationModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Basic usage

### 1. Send notification

```csharp
public class MyService
{
    private readonly INotificationSender _notificationSender;

    public MyService(INotificationSender notificationSender)
    {
        _notificationSender = notificationSender;
    }

    public async Task SendNotificationAsync()
    {
        var data = new NotificationData();
        data.TrySetData("title", "Test Notification");
        data.TrySetData("message", "This is a test notification");

        await _notificationSender.SendNofiterAsync(
            "TestNotification",
            data,
            severity: NotificationSeverity.Info);
    }
}
```

### 2. Manage notification subscriptions

```csharp
public class MyService
{
    private readonly INotificationSubscriptionManager _subscriptionManager;

    public MyService(INotificationSubscriptionManager subscriptionManager)
    {
        _subscriptionManager = subscriptionManager;
    }

    public async Task SubscribeAsync(Guid userId)
    {
        await _subscriptionManager.SubscribeAsync(
            userId,
            "TestNotification");
    }
}
```

## Configuration options

```json
{
  "Notifications": {
    "PublishProviders": [
      "SignalR" // Optional notification provider
    ]
  }
}
```

## Notification type

* Application - platform notification
* System - system notification
* User - user notification
* ServiceCallback - service callback notification

## Notification life cycle

*OnlyOne - one-time notification
* Persistent - persistent notification

## Notification content type

* Text - text
* Json - JSON
* Html - HTML
* Markdown - Markdown

## Notification severity level

* Success - success
* Info - information
* Warn - warning
* Fatal - fatal
* Error - error

## Best Practices

1. Choose the appropriate notification type and life cycle according to actual needs
2. Use notification severity levels appropriately and avoid abusing high-level notifications
3. Add proper localization support for notifications
4. Regularly clean up expired notification data

## Notes

1. Persistent notifications need to implement the INotificationStore interface
2. Custom notification providers need to implement the INotificationPublishProvider interface
3. Notification names should be unique and descriptive
4. Consider data isolation in multi-tenant scenarios
