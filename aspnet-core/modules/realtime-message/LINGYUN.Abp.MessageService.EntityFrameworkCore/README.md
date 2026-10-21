# LINGYUN.Abp.MessageService.EntityFrameworkCore

EntityFrameworkCore implementation module for messaging services.

## Features

* Implement the data access layer of the message service
* Provide default warehousing implementation
* Implement warehousing of the following entities:
  * ChatGroup - chat group
  * UserChatGroup - User chat group
  * UserChatCard - User chat card
  * UserChatSetting - User chat settings
  * UserChatFriend - User Chat Friend

## Dependent modules

* [LINGYUN.Abp.MessageService.Domain](../LINGYUN.Abp.MessageService.Domain/README.md)
* `AbpEntityFrameworkCoreModule`

## Configuration usage

1. First, you need to install LINGYUN.Abp.MessageService.EntityFrameworkCore into your project:

```bash
dotnet add package LINGYUN.Abp.MessageService.EntityFrameworkCore
```

2. Add `AbpMessageServiceEntityFrameworkCoreModule` to your module’s dependency list:

```csharp
[DependsOn(typeof(AbpMessageServiceEntityFrameworkCoreModule))]
public class YourModule : AbpModule
{
}
```

3. Add message service-related DbSet to your DbContext:

```csharp
public class YourDbContext : AbpDbContext<YourDbContext>, IMessageServiceDbContext
{
    public DbSet<ChatGroup> ChatGroups { get; set; }
    public DbSet<UserChatGroup> UserChatGroups { get; set; }
    public DbSet<UserChatCard> UserChatCards { get; set; }
    public DbSet<UserChatSetting> UserChatSettings { get; set; }
    public DbSet<UserChatFriend> UserChatFriends { get; set; }
    
    public YourDbContext(DbContextOptions<YourDbContext> options) 
        : base(options)
    {
    }
}
```

## More

[English document](README.EN.md)
