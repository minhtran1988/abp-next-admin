# LINGYUN.Abp.Notifications.Application.Contracts

The application layer contract module of the notification system provides the application service interface definition and data transmission objects of the notification system.

## Features

* Notification application service interface definition
* Notification subscription application service interface definition
* Notification data transfer object (DTO) definition
* Notification permission definition

## Module reference

```csharp
[DependsOn(typeof(AbpNotificationsApplicationContractsModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Application service interface

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

## Data transfer object

### NotificationInfo

*Id - notification unique identifier
* NotificationName - notification name
* Data - notification data
* CreationTime - Creation time
* Type - notification type
*Severity - Notification severity

### NotificationSubscriptionInfo

* NotificationName - notification name
* DisplayName - display name
* Description - description
* IsSubscribed - whether it has been subscribed

## Permission definition

* Notifications - notification management
  * Notifications.Manage - Manage notifications
  * Notifications.Delete - delete notifications
  * Notifications.Subscribe - subscribe to notifications

## Basic usage

1. Implement notification application services
```csharp
public class NotificationAppService : ApplicationService, INotificationAppService
{
    public async Task<NotificationInfo> GetAsync(Guid id)
    {
        //Implement the logic of obtaining notification details
    }
}
```

## More information

* [ABP Documentation](https://docs.abp.io)
