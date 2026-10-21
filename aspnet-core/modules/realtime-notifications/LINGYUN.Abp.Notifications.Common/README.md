# LINGYUN.Abp.Notifications.Common

The public module of the notification system provides the basic definition and sharing functions of the notification system.

## Features

*Notification definition
  * Notification group definition
  *Notification type definition
  *Notification level definition
* General tools
  * Notification data processing
  * Notification status management
*Extended functions
  * Notification data extension
  * Notification provider extension

## Module reference

```csharp
[DependsOn(typeof(AbpNotificationsCommonModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Basic definition

### Notification Group

```csharp
public class NotificationGroupDefinition
{
    public string Name { get; }
    public string DisplayName { get; }
    public string Description { get; }
    public bool AllowSubscriptionToClients { get; }
}
```

### Notification definition

```csharp
public class NotificationDefinition
{
    public string Name { get; }
    public string DisplayName { get; }
    public string Description { get; }
    public NotificationType NotificationType { get; }
    public NotificationLifetime Lifetime { get; }
    public bool AllowSubscriptionToClients { get; }
}
```

## Basic usage

1. Define notification groups
```csharp
public class YourNotificationGroupDefinitionProvider : NotificationGroupDefinitionProvider
{
    public override void Define(INotificationGroupDefinitionContext context)
    {
        context.Add(
            new NotificationGroupDefinition(
                name: "App.Notifications",
                displayName: L("AppNotifications"),
                description: L("AppNotificationsDescription")
            )
        );
    }
}
```

2. Define notifications
```csharp
public class YourNotificationDefinitionProvider : NotificationDefinitionProvider
{
    public override void Define(INotificationDefinitionContext context)
    {
        context.Add(
            new NotificationDefinition(
                name: "App.NewMessage",
                displayName: L("NewMessage"),
                description: L("NewMessageDescription"),
                notificationType: NotificationType.Application,
                lifetime: NotificationLifetime.Persistent
            )
        );
    }
}
```

3. Use notification data extensions
```csharp
public static class NotificationDataExtensions
{
    public static void SetTitle(this NotificationData data, string title)
    {
        data.ExtraProperties["Title"] = title;
    }

    public static string GetTitle(this NotificationData data)
    {
        return data.ExtraProperties.GetOrDefault("Title") as string;
    }
}
```

## More information

* [ABP Documentation](https://docs.abp.io)
* [Notification System Documentation](https://docs.abp.io/en/abp/latest/Notifications)
