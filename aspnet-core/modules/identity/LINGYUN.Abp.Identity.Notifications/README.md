# LINGYUN.Abp.Identity.Notifications

The identity authentication notification module provides notification functions related to identity authentication.

## Features

* Extend AbpNotificationsModule module
* Provide notification definitions related to identity authentication
* Support session expiration notification
* Provide identity session revocation event processing

## Module reference

```csharp
[DependsOn(
    typeof(AbpNotificationsModule),
    typeof(AbpDddDomainSharedModule),
    typeof(AbpIdentityDomainSharedModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Notification definition

### Session notification

* `AbpIdentity.Session.Expiration` - session expiration notification
  * This notification is sent when the user session expires
  * Notify the user that their session has expired and they need to log in again

## Event handling

### IdentitySessionRevokeEventHandler

Handle identity session revocation events when a session is revoked:
* Send session expiration notification to relevant users
* Notify the user that they need to log in again

## Basic usage

1. Subscription session expiration notification
```csharp
public class YourNotificationHandler : INotificationHandler<SessionExpirationNotification>
{
    public async Task HandleNotificationAsync(SessionExpirationNotification notification)
    {
        // Handle session expiration notification
    }
}
```

2. Send session expiration notification
```csharp
public class YourService
{
    private readonly INotificationSender _notificationSender;

    public YourService(INotificationSender notificationSender)
    {
        _notificationSender = notificationSender;
    }

    public async Task SendSessionExpirationNotificationAsync(Guid userId)
    {
        await _notificationSender.SendAsync(
            IdentityNotificationNames.Session.ExpirationSession,
            new NotificationData(),
            userIds: new[] { userId });
    }
}
```

## More information

* [ABP Identity Authentication Document](https://docs.abp.io/en/abp/latest/Identity)
* [ABP Notification System Documentation](https://docs.abp.io/en/abp/latest/Notification-System)
