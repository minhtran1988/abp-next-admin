# LINGYUN.Abp.Notifications.Domain.Shared

The shared domain layer module of the notification system provides shared constants, enumerations and other domain objects of the notification system.

## Features

*Notification type definition
* Definition of notification severity
* Notification status definition
* Notification life cycle definition
* Notification constant definition

## Module reference

```csharp
[DependsOn(typeof(AbpNotificationsDomainSharedModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Enumeration definition

### NotificationType

*Application - application notification
* System - system notification
* User - user notification

### NotificationSeverity

* Info - information
* Success - success
* Warn - warning
* Error - error
* Fatal - Fatal error

### NotificationLifetime

* Persistent - persistent notification
*OnlyOne - one-time notification

## Basic usage

1. Use notification types
```csharp
var notificationType = NotificationType.Application;
```

2. Use notification severity
```csharp
var severity = NotificationSeverity.Info;
```

## More information

* [ABP Documentation](https://docs.abp.io)
