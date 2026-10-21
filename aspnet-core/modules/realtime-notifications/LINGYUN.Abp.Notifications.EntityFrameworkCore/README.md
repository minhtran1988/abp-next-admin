# LINGYUN.Abp.Notifications.EntityFrameworkCore

The EntityFrameworkCore module of the notification system provides the data access implementation of the notification system.

## Features

* Notify entity mapping configuration
* Notify warehousing implementation
* Notification subscription warehousing implementation
* Support multiple databases
* Support custom warehousing expansion

## Module reference

```csharp
[DependsOn(typeof(AbpNotificationsEntityFrameworkCoreModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Entity mapping

### NotificationEfCoreEntityTypeConfiguration

* Notification - Notification entity mapping
  *Id - primary key mapping
  * NotificationName - Notification name mapping
  * Data - notification data mapping
  * CreationTime - Creation time mapping
  * Type - notification type mapping
  * Severity - Notification severity mapping

### NotificationSubscriptionEfCoreEntityTypeConfiguration

* NotificationSubscription - notification subscription entity mapping
  * UserId - User ID mapping
  * NotificationName - Notification name mapping
  * CreationTime - Creation time mapping

## Basic usage

1. Configure DbContext
```csharp
public class YourDbContext : AbpDbContext<YourDbContext>
{
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<NotificationSubscription> NotificationSubscriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureNotifications();
    }
}
```

2. Configure the connection string
```json
{
  "ConnectionStrings": {
    "Default": "Server=localhost;Database=YourDb;Trusted_Connection=True"
  }
}
```

## More information

* [ABP Documentation](https://docs.abp.io)
* [Entity Framework Core Documentation](https://docs.microsoft.com/ef/core/)
