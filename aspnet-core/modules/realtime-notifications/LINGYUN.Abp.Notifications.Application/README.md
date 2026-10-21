# LINGYUN.Abp.Notifications.Application

The application layer module of the notification system provides the application service implementation of the notification system.

## Features

* Notification management service
* Notification subscription service
*Notification publishing service
*Notification inquiry service
* Notification status management service

## Module reference

```csharp
[DependsOn(typeof(AbpNotificationsApplicationModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Application Services

### INotificationAppService

* GetAsync - Get notification details
* GetListAsync - Get notification list
* DeleteAsync - delete notification
* MarkReadAsync - mark notification as read
* MarkAllReadAsync - Mark all notifications as read

### INotificationSubscriptionAppService

* SubscribeAsync - subscribe to notifications
* UnSubscribeAsync - Unsubscribe notification
* GetAssignableSubscribersAsync - Get the list of assignable subscribers
* GetSubscribedListAsync - Get the subscribed notification list

## Basic usage

1. Send notification
```csharp
await NotificationAppService.PublishAsync(
    name: "YourNotification",
    data: new NotificationData(),
    userIds: new[] { CurrentUser.Id });
```

2. Manage notification subscriptions
```csharp
await NotificationSubscriptionAppService.SubscribeAsync(
    notificationName: "YourNotification");
```

## More information

* [ABP Documentation](https://docs.abp.io)
