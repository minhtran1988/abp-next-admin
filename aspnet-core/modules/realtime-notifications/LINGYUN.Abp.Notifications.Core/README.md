# LINGYUN.Abp.Notifications.Core

The core module of the notification system provides the basic functions and definitions of the notification system.

## Features

* Notification definition management
* Notification group definition management
* Extensible notification provider mechanism
* Support custom notification definition provider

## Module reference

```csharp
[DependsOn(typeof(AbpNotificationsCoreModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

```json
{
  "Notifications": {
    "DeletedNotifications": [], // List of notification definitions that need to be deleted
    "DeletedNotificationGroups": [] // List of notification group definitions that need to be deleted
  }
}
```

## Basic usage

1. Implement a custom notification definition provider
```csharp
public class YourNotificationDefinitionProvider : NotificationDefinitionProvider
{
    public override void Define(INotificationDefinitionContext context)
    {
        // define notification
        context.Add(
            new NotificationDefinition(
                name: "YourNotification",
                displayName: L("YourNotification"),
                description: L("YourNotificationDescription"),
                notificationType: NotificationType.Application,
                lifetime: NotificationLifetime.Persistent,
                allowSubscriptionToClients: true)
        );
    }
}
```

2. Register notification definition provider
```csharp
Configure<AbpNotificationsOptions>(options =>
{
    options.DefinitionProviders.Add<YourNotificationDefinitionProvider>();
});
```

## More information

* [ABP Documentation](https://docs.abp.io)
