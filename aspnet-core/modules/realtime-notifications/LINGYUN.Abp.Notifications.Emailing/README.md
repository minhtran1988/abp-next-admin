# LINGYUN.Abp.Notifications.Emailing

The email sending module of the notification system provides the function of sending notifications by email.

## Features

* Send email notification
* Email template support
* Support HTML format email
* Support multiple recipients
* Support CC and BCC

## Module reference

```csharp
[DependsOn(typeof(AbpNotificationsEmailingModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

```json
{
  "Notifications": {
    "Emailing": {
      "Templates": {
        "Default": {
          "Template": "DefaultTemplate",
          "Culture": "zh-Hans"
        }
      }
    }
  }
}
```

## Basic usage

1. Configure email settings
```csharp
Configure<AbpEmailingOptions>(options =>
{
    options.DefaultFromAddress = "noreply@example.com";
    options.DefaultFromDisplayName = "Notification System";
});
```

2. Send email notification
```csharp
public class YourNotificationHandler : INotificationHandler
{
    private readonly IEmailSender _emailSender;

    public YourNotificationHandler(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public async Task HandleAsync(NotificationInfo notification)
    {
        await _emailSender.SendAsync(
            to: notification.UserEmail,
            subject: notification.Title,
            body: notification.Content,
            isBodyHtml: true
        );
    }
}
```

3. Use email templates
```csharp
public async Task SendWithTemplateAsync()
{
    var template = await _templateRenderer.RenderAsync(
        "DefaultTemplate",
        new { 
            Title = "Notification Title",
            Content = "Notification content"
        }
    );

    await _emailSender.SendAsync(
        to: "user@example.com",
        subject: "notification",
        body: template,
        isBodyHtml: true
    );
}
```

## More information

* [ABP Documentation](https://docs.abp.io)
* [E-mail document](https://docs.abp.io/en/abp/latest/Emailing)
