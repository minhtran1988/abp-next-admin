# LINGYUN.Abp.WxPusher

Integrate the ABP module of WxPusher WeChat push service, implement WxPusher related API documents, and have WxPusher open capabilities.

[English](./README.EN.md)

For details, see the WxPusher documentation: https://wxpusher.dingliqc.com/docs/#/

## Features

* Integrate WxPusher API
* Support pushing messages to users and topics
* Supports multiple message types (text, HTML, Markdown)
* Support message sending limits and quota management
* Support QR code generation
* Support user subscription management

## Installation

```bash
dotnet add package LINGYUN.Abp.WxPusher
```

## Module reference

```csharp
[DependsOn(typeof(AbpWxPusherModule))]
public class YouProjectModule : AbpModule
{
    // other
}
```

## Configuration

### SettingsConfiguration

* `WxPusher.Security.AppToken`: The identity token of the application. Once you have APP_TOKEN, you can send messages to the users of the corresponding application. Please keep it strictly confidential.

### Features

* `WxPusher`: WxPusher feature grouping
* `WxPusher.Enable`: Enable WxPusher globally
* `WxPusher.Message.Enable`: Globally enable WxPusher message channel
* `WxPusher.Message`: WxPusher message push
* `WxPusher.Message.SendLimit`: WxPusher message push limit number of times
* `WxPusher.Message.SendLimitInterval`: WxPusher message push limit period (days)

## Usage

### Send message

```csharp
public class YourService
{
    private readonly IWxPusherMessageSender _messageSender;

    public YourService(IWxPusherMessageSender messageSender)
    {
        _messageSender = messageSender;
    }

    public async Task SendMessageAsync()
    {
        await _messageSender.SendAsync(
            content: "Hello, WxPusher!",
            summary: "Message summary",
            contentType: MessageContentType.Text,
            topicIds: new List<int> { 1, 2 }, // Optional: send to the specified Topic
            uids: new List<string> { "UID1", "UID2" }, // Optional: send to specified user
            url: "https://example.com" // Optional: Click the URL to jump to the message
        );
    }
}
```

### User subscription

Implement the `IWxPusherUserStore` interface to manage user subscriptions:

```csharp
public class YourWxPusherUserStore : IWxPusherUserStore
{
    public async Task<List<string>> GetBindUidsAsync(
        IEnumerable<Guid> userIds, 
        CancellationToken cancellationToken = default)
    {
        // Implement the logic of obtaining the WxPusher UID bound to the user
    }

    public async Task<List<int>> GetSubscribeTopicsAsync(
        IEnumerable<Guid> userIds, 
        CancellationToken cancellationToken = default)
    {
        //Implement the logic of obtaining the Topic list subscribed by the user
    }
}
```

## Source code location

[LINGYUN.Abp.WxPusher](https://github.com/colinin/abp-next-admin/tree/master/aspnet-core/framework/wx-pusher/LINGYUN.Abp.WxPusher)
