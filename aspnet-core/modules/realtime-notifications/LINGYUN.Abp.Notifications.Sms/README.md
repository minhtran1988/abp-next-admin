# LINGYUN.Abp.Notifications.Sms

SMS implementation of notification publishing provider

Most of the rewritten modules are consistent with the official module names and are distinguished by namespaces. Mainly, only a small part has been rewritten or additional functions have been added.
If most of the module code is rewritten, or it is completely an extension module, your own name will be defined.

#### NOTE

The customized sending method can be implemented by implementing the ##ISmsNotificationSender## interface or rewriting the ##SmsNotificationSender##

## Features

* Send SMS notification
* SMS template support
* Support multiple SMS service providers
* Support text message variable replacement
* Support batch sending

## Configuration usage

* This configuration item will be removed in the next SMS-related major version

```json

{
  "Notifications": {
    "Sms": {
      "TemplateParamsPrefix": "SMS template variable prefix"
    }
  }
}

```

```csharp
[DependsOn(typeof(AbpNotificationsSmsModule))]
public class YouProjectModule : AbpModule
{
  // other
}

## Basic usage

1. Implement SMS sending interface
```csharp
public class YourSmsNotificationSender : SmsNotificationSender
{
    public override async Task SendAsync(NotificationInfo notification)
    {
        var templateParams = GetTemplateParams(notification);
        await SmsService.SendAsync(
            notification.UserPhoneNumber,
            notification.Title,
            templateParams
        );
    }
}
```

2. Register for SMS sending service
```csharp
Configure<AbpNotificationsSmsOptions>(options =>
{
    options.TemplateParamsPrefix = "sms_"; // SMS template variable prefix
});
```

3. Send SMS notification
```csharp
public class YourService
{
    private readonly INotificationSender _notificationSender;

    public YourService(INotificationSender notificationSender)
    {
        _notificationSender = notificationSender;
    }

    public async Task SendSmsNotificationAsync()
    {
        await _notificationSender.SendNofiterAsync(
            "YourNotification",
            new NotificationData
            {
                // SMS template parameters
                ["sms_code"] = "123456",
                ["sms_time"] = "5"
            },
            userIds: new[] { CurrentUser.Id }
        );
    }
}
```

## More information

* [ABP Documentation](https://docs.abp.io)
* [SMS Documentation](https://docs.abp.io/en/abp/latest/SMS-Sending)
