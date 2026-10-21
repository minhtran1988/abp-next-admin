# LINGYUN.Abp.Notifications.SignalR

The SignalR module of the notification system provides real-time notification functions based on SignalR.

## Features

* Real-time notification push
* Notify Hub implementation
* Client connection management
* Support group notifications
* Support user online status management

## Module reference

```csharp
[DependsOn(typeof(AbpNotificationsSignalRModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

```json
{
  "SignalR": {
    "HubUrl": "/signalr-hubs/notifications",
    "UseMessagePack": false
  }
}
```

## Hub definition

### NotificationHub

* SubscribeToNotifications - Subscribe to notifications
* UnsubscribeFromNotifications - Unsubscribe notifications
* GetNotifications - Get notification list
* MarkNotificationAsRead - mark notification as read
* MarkAllNotificationsAsRead - mark all notifications as read

## Basic usage

1. Server configuration
```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSignalR()
                .AddMessagePackProtocol(); // Optional, use MessagePack protocol
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<NotificationHub>("/signalr-hubs/notifications");
        });
    }
}
```

2. Client use
```javascript
// Connect to notification Hub
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/signalr-hubs/notifications")
    .build();

//Listen for notifications
connection.on("ReceiveNotification", (notification) => {
    console.log("New notification received:", notification);
});

// Start connection
await connection.start();

// Subscribe to notifications
await connection.invoke("SubscribeToNotifications");
```

## More information

* [ABP Documentation](https://docs.abp.io)
* [SignalR Documentation](https://docs.microsoft.com/aspnet/core/signalr)
