# LINGYUN.Abp.Notifications.Domain

The domain layer module of the notification system provides the domain model and business logic of the notification system.

## Features

* Notification entity definition
* Notification subscription management
* Notification status management
* Notification data extension support
* Support custom notification data

## Module reference

```csharp
[DependsOn(typeof(AbpNotificationsDomainModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Domain Model

### Notification

*Id - notification unique identifier
* Name - notification name
* NotificationData - notification data
* CreationTime - Creation time
* Type - notification type
*Severity - Notification severity
* ExtraProperties - extended properties

### NotificationSubscription

* UserId - User ID
* NotificationName - notification name
* CreationTime - Creation time

## Basic usage

1. Create a notification
```csharp
var notification = new Notification(
    id: GuidGenerator.Create(),
    name: "YourNotification",
    data: new NotificationData(),
    tenantId: CurrentTenant.Id);
```

2. Manage notification subscriptions
```csharp
await NotificationSubscriptionManager.SubscribeAsync(
    userId: CurrentUser.Id,
    notificationName: "YourNotification");
```

## More information

* [ABP Documentation](https://docs.abp.io)
