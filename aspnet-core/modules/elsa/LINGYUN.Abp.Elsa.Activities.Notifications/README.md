# LINGYUN.Abp.Elsa.Activities.Notifications

Notification activity integration module for Elsa workflow

## Function

* Provide **SendNotification** activity for sending notifications
  * Support sending to multiple users
  * Support notification data and template content
  * Support JavaScript and Liquid syntax
  * Support setting notification severity
  * Integrate the `INotificationSender` interface of the ABP framework
  *Support multi-tenancy

## Configuration usage

```csharp
[DependsOn(
    typeof(AbpElsaActivitiesNotificationsModule)
    )]
public class YouProjectModule : AbpModule
{
}
```

## appsettings.json

```json
{
    "Elsa": {
        "Notification": true // Enable notification activity
    }
}
```

## Activity parameters

* **NotificationName**: registered notification name
* **NotificationData**: notification data or template content
* **To**: Receive user ID list
* **Severity**: Notification severity (default is Info)

## Output parameters

* **NotificationId**: Notification ID sent

[English](./README.EN.md)
