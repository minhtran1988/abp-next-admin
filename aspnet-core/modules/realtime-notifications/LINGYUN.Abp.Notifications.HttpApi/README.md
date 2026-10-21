# LINGYUN.Abp.Notifications.HttpApi

The HTTP API module of the notification system provides the REST API interface of the notification system.

## Features

* Notification management API
* Notification subscription API
* Notification status management API
* Support API version control
* Support Swagger documentation

## Module reference

```csharp
[DependsOn(typeof(AbpNotificationsHttpApiModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## API interface

### NotificationController

* GET /api/notifications/{id} - Get notification details
* GET /api/notifications - Get notification list
* DELETE /api/notifications/{id} - delete notifications
* PUT /api/notifications/{id}/read - Mark notification as read
* PUT /api/notifications/read - Mark all notifications as read

### NotificationSubscriptionController

* POST /api/notifications/subscriptions - Subscription notifications
* DELETE /api/notifications/subscriptions - Unsubscribe from notifications
* GET /api/notifications/subscribers - Get the list of assignable subscribers
* GET /api/notifications/subscriptions - Get a list of subscribed notifications

## Basic usage

1. Configure Startup
```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<YourHttpApiModule>();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.InitializeApplication();
    }
}
```

2. API call example
```bash
# Get notification list
curl -X GET "https://localhost:44300/api/notifications"

# Subscribe to notifications
curl -X POST "https://localhost:44300/api/notifications/subscriptions" \
     -H "Content-Type: application/json" \
     -d '{"notificationName":"YourNotification"}'
```

## More information

* [ABP Documentation](https://docs.abp.io)
* [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
