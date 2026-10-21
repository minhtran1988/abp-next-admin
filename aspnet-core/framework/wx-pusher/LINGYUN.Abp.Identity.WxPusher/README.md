# LINGYUN.Abp.Identity.WxPusher

Identity module implementation of IWxPusherUserStore interface, obtains the list of followed topics through user Claims

[English](./README.EN.md)

## Features

* Integrated WxPusher user storage interface
* Manage WxPusher's UID and Topic through user claims
* Supports batch acquisition of user-bound UIDs
* Supports batch acquisition of topics subscribed by users

## Installation

```bash
dotnet add package LINGYUN.Abp.Identity.WxPusher
```

## Module reference

```csharp
[DependsOn(typeof(AbpIdentityWxPusherModule))]
public class YouProjectModule : AbpModule
{
    // other
}
```

## Usage

This module implements the `IWxPusherUserStore` interface and stores WxPusher related information through user Claims:

* `AbpWxPusherClaimTypes.Uid`: stores the WxPusher UID bound to the user
* `AbpWxPusherClaimTypes.Topic`: stores the Topic ID subscribed by the user

### Get the UID bound to the user

```csharp
public class YourService
{
    private readonly IWxPusherUserStore _wxPusherUserStore;

    public YourService(IWxPusherUserStore wxPusherUserStore)
    {
        _wxPusherUserStore = wxPusherUserStore;
    }

    public async Task DoSomethingAsync(IEnumerable<Guid> userIds)
    {
        var uids = await _wxPusherUserStore.GetBindUidsAsync(userIds);
        //Use the obtained uids for message push and other operations
    }
}
```

### Get the Topic subscribed by the user

```csharp
public class YourService
{
    private readonly IWxPusherUserStore _wxPusherUserStore;

    public YourService(IWxPusherUserStore wxPusherUserStore)
    {
        _wxPusherUserStore = wxPusherUserStore;
    }

    public async Task DoSomethingAsync(IEnumerable<Guid> userIds)
    {
        var topics = await _wxPusherUserStore.GetSubscribeTopicsAsync(userIds);
        // Use the obtained topics to perform operations such as message push
    }
}
```

## Source code location

[LINGYUN.Abp.Identity.WxPusher](https://github.com/colinin/abp-next-admin/tree/master/aspnet-core/framework/wx-pusher/LINGYUN.Abp.Identity.WxPusher)
