# Notification channel data — User guide

> For frame design instructions, see [CHANNEL-DATA.md](./CHANNEL-DATA.md)

---

## 0. Three access points

| Role | Which API to use | Typical locations |
|---|---|---|
| Producer (business code) | `INotificationChannelDataSerializer.Write` or channel extension method | AppService / DomainService to send notification |
| Notification Definition | `NotificationDefinition.WithProviders(...)` | `NotificationDefinitionProvider` |
| Channel implementation | `INotificationChannelDataResolver` | Each `*NotificationPublishProvider` |

Channel data is a one-way contract of "producer → channel"; the notification definition is responsible for deciding which channels will receive it.
The two are independent: if the channel is not declared in the definition, the channel will not be called at all.

---

## 1. Scenario 1: Enterprise WeChat channel

### 1.1 The simplest - only use standard data

Without writing any channel data, the enterprise WeChat channel will automatically derive the carrier according to the `ContentType` defined in the notification:

| Defined content types | Automatically generated vectors |
|---|---|
| `Text` | Text message (take `message`) |
| `Html` | Text card message (take `title`/`description`/`message`) |
| `Markdown` | markdown message (take `message`) |

```csharp
public class MyAppService : ApplicationService
{
    private readonly INotificationSender _notificationSender;

    public MyAppService(INotificationSender notificationSender)
    {
        _notificationSender = notificationSender;
    }

    public async Task NotifyAsync(Guid userId)
    {
        var data = new NotificationData();
        data.WriteStandardData(
            title: "Build Complete",
            message: "**v1.2.0** has been deployed to the production environment",
            createTime: Clock.Now,
            formUser: CurrentUser.UserName ?? "system");

        await _notificationSender.SendNofiterAsync(
            name: "BuildFinished",
            data: data,
            user: new UserIdentifier(userId, "admin"));
    }
}
```

### 1.2 Designate recipients / Confidentiality / Deduplication

Write "Delivery Options". These options persist across persistence and failed retries:

```csharp
var data = new NotificationData();
data.WriteStandardData("Title", "Content", Clock.Now, "system");

data.TrySetData(WeChatWorkNotificationDataKeys.ToParty, "1|2"); // Specify department
data.TrySetData(WeChatWorkNotificationDataKeys.ToTag, "3"); // Specify tag
data.TrySetData(WeChatWorkNotificationDataKeys.Safe, true); // Confidential message
data.TrySetData(WeChatWorkNotificationDataKeys.EnableIdTrans, true); // Enable id translation
data.TrySetData(WeChatWorkNotificationDataKeys.EnableDuplicateCheck, true);
data.TrySetData(WeChatWorkNotificationDataKeys.DuplicateCheckInterval, 600); // seconds

await _notificationSender.SendNofiterAsync("BuildFinished", data, user);
```

> The key name has the channel prefix (`wx-work:`); reading is not case-sensitive.
> The member (`touser`) is automatically resolved by the enterprise WeChat identity binding relationship and is not specified by the notification data**.
> `touser` / `toparty` / `totag` cannot be empty at the same time.

### 1.3 Text card

```csharp
channelDataSerializer.Write(data, new WeChatWorkTextCardChannelData
{
    TextCard = new TextCardMessage(
        title: "Approval Passed",
        description: "Your leave application has been approved, please go and check",
        url: "https://example.com/leave/123",
        buttonText: "Go and check")
});
```

> `channelDataSerializer` is an injected `INotificationChannelDataSerializer`.
> It writes the corresponding key according to the <xref:NotificationChannelPropertyAttribute> marked on the payload type,
> The production side and the channel side share the same contract definition.

### 1.4 Template card (recommended for approval/alarm type)

```csharp
var card = new TextTemplateCard(
    new TemplateCardCardAction("https://example.com/approval/123"))
{
    MainTitle = new TemplateCardMainTitle("Leave application notification", "Your leave application has been approved"),
    SubTitle = "Please handle the follow-up handover matters in a timely manner",
    HorizontalContents = new List<TemplateCardHorizontalContent>
    {
        TemplateCardHorizontalContent.None("Approval Number", "QJ20251000000136"),
        TemplateCardHorizontalContent.None("Leave Date", "2025/10/01-2025/10/10"),
        TemplateCardHorizontalContent.None("Passing time", "2025-10-01 15:30:00"),
        // You can also bring jump/material/personnel
        TemplateCardHorizontalContent.FromUrl("Details", "https://example.com/oa/123", "Click to view"),
    },
    Jumps = new List<TemplateCardJump>
    {
        new TemplateCardJump("Go to OA to view", "https://example.com/oa/123"),
    }
};

channelDataSerializer.Write(data, new WeChatWorkTemplateCardChannelData { TemplateCard = card });
```

> Available `TemplateCardHorizontalContent` factories:
> `None(keyName, value)`、`FromUrl(keyName, url, value)`、
> `FromMedia(keyName, mediaId, value)`、`FromUser(keyName, userId, value)`。
> `TemplateCardJump` supports two constructors: "jump link" and "jump applet".
> See the complete runnable example in your repository
> `tests/LINGYUN.Abp.WeChat.Work.Tests/.../WeChatWorkMessageSender_Tests.cs`。

### 1.5 Mini Program Notification

```csharp
channelDataSerializer.Write(data, new WeChatWorkMiniProgramNoticeChannelData
{
    MiniProgram = new MiniProgramMessage(
        appId: "wx1234567890",
        title: "Order has been shipped",
        page: "pages/order/detail?id=123",
        description: "Click to view logistics details")
    {
        EmphasisFirstItem = true,
        ContentItems = new List<MiniProgramContent>
        {
            new MiniProgramContent("Order Number", "SO20251001001"),
            new MiniProgramContent("Logistics Company", "SF Express"),
        }
    }
});
```

### 1.6 Graphics/Videos/Voices/Documents

```csharp
channelDataSerializer.Write(data, new WeChatWorkMpNewChannelData
{
    MpNewMessage = new MpNewMessagePayload(new List<MpNewMessage>
    {
        new MpNewMessage(
            title: "Monthly Report",
            thumbMediaId: "MEDIA_ID",
            content: "<p>Report text</p>",
            author: "Operations Department",
            contentSourceUrl: "https://example.com/report",
            description: "Monthly Business Report for October 2025")
    })
});

channelDataSerializer.Write(data, new WeChatWorkVideoChannelData
{
    Video = new VideoMessage("MEDIA_ID", "Training Video", "Newcomer Induction Training")
});

channelDataSerializer.Write(data, new WeChatWorkVoiceChannelData
{
    Voice = new MediaMessage("MEDIA_ID")
});

channelDataSerializer.Write(data, new WeChatWorkFileChannelData
{
    File = new MediaMessage("MEDIA_ID")
});
```

### 1.7 Available payload types and option support

Producers only need to select the appropriate payload type; options are automatically selected by the channel based on carrier capabilities (unsupported fields will not be set).

| payload type | carrier | `safe` | `enable_id_trans` | `enable_duplicate_check` |
|---|---|---|---|---|
| `WeChatWorkTextCardChannelData` | Text Card | ❌ | ✅ | ✅ |
| `WeChatWorkTemplateCardChannelData` | Template Card | ❌ | ✅ | ✅ |
| `WeChatWorkMiniProgramNoticeChannelData` | Mini Program Notification | ❌ | ✅ | ✅ |
| `WeChatWorkMpNewChannelData` | Pictures and text (mpnews) | ✅ | ✅ | ✅ |
| `WeChatWorkVideoChannelData` | Video | ✅ | ❌ | ✅ |
| `WeChatWorkFileChannelData` | File | ✅ | ❌ | ✅ |
| `WeChatWorkVoiceChannelData` | Voice | ❌ | ❌ | ✅ |
| (None, derived by `ContentType=Markdown`) | markdown | ❌ | ❌ | ✅ |
| (None, derived by `ContentType=Text`) | Text | ✅ | ✅ | ✅ |

### 1.8 How to deal with multiple loads existing at the same time

**All will be sent**, the order is determined by the `Priority` of the payload (fixed value, see table below):

| Load | Priority |
|---|---|
| Mini program notification | 100 |
| Graphics (mpnews) | 200 |
| Template Cards | 300 |
| text card | 400 |
| Video | 500 |
| Voice | 600 |
|File|700|

That is, "setting text card and mini program notification at the same time" will **send two messages**. If you only want to send one message, write only one payload.
Fallback to the default bearer deduced by `ContentType` is only done without any explicit payload.

---

## 2. Scenario 2: Other built-in channels

### 2.1 WeChat Mini Program Subscription News

> Note: Keys such as `TemplateId` follow the historical naming (**no prefix**), and the reading has been changed to **case insensitive**.

```csharp
var data = new NotificationData();
data.WriteStandardData("Order Shipped", "Your order has been shipped", Clock.Now, "system");

// Key with this channel prefix wx-mp:; If not written, the default value of AbpNotificationsWeChatMiniProgramOptions will be used
data.TrySetData(WeChatMiniProgramNotificationDataKeys.TemplateId, "TEMPLATE_ID");
data.TrySetData(WeChatMiniProgramNotificationDataKeys.RedirectPage, "pages/order/detail?id=123");
data.TrySetData(WeChatMiniProgramNotificationDataKeys.WeAppState, "developer");
data.TrySetData(WeChatMiniProgramNotificationDataKeys.WeAppLanguage, "zh_CN");

//The template data itself is taken from the attribute package and written according to the WeChat template field name.
data.TrySetData("character_string1", "SO20251001001");
data.TrySetData("thing2", "SF Express");

await _notificationSender.SendNofiterAsync("OrderShipped", data, user);
```

> Template data keys (`character_string1`, `thing2`...) are determined by **WeChat template definition**,
> It does not belong to the channel's own key, so it is not prefixed.
>
> Unprefixed keys (`TemplateId` etc.) used by earlier versions are compatible with channel read-only fallback
> (See `WeChatMiniProgramNotificationDataKeys.Legacy`), please use prefixed constants for new code.

### 2.2 WxPusher

```csharp
// Method 1: Use serializer (recommended)
channelDataSerializer.Write(data, new WxPusherChannelData
{
    Url = "https://example.com/detail/123"
});

//Method 2: Write the key directly
data.TrySetData(WxPusherNotificationDataKeys.Url, "https://example.com/detail/123");
```

### 2.3 PushPlus

```csharp
channelDataSerializer.Write(data, new PushPlusChannelData
{
    Webhook = "https://example.com/hook",
    CallbackUrl = "https://example.com/callback"
});
```

> The `Presence` of PushPlus is `AnyDeclaredProperty`: writing any one of the keys is deemed to declare the payload.

### 2.4 SMS / Email / SignalR

**No channel exclusive key**, only standard data required:

```csharp
var data = new NotificationData();
data.WriteStandardData("Alarm", "CPU usage exceeds 90%", Clock.Now, "monitor");

await _notificationSender.SendNofiterAsync(NotifyNames.SystemAlarm, data, user);
```

- SMS: only get `message`
- Email: `title` as subject, `message` as body (automatically converted to HTML when `ContentType=Markdown`)
- SignalR: Push the entire `NotificationData` directly, interpreted by the client

---

## 3. Scenario 3: Enterprise WeChat Webhook (group robot)

Project: `LINGYUN.Abp.Notifications.Webhook.WeChat.Work`

### 3.1 Differences from enterprise WeChat application messages

| | Application messages | Webhook (group robot) |
|---|---|---|
| Channel name (declared in notification definition) | `WeChat.Work` | `Webhook` |
| Payload registry key | `WeChat.Work` | `WeChat.Work.Webhook` |
| Data key prefix | `wx-work:` | `wx-work-hook:` |
| Recipients | Resolved by identity binding (`touser`) | Determined by webhook definition (group) |

> **Why are they divided into two channels**: The two are different transmission methods and different carrier models.
> If the registry key and key prefix are shared and the same notification is subscribed by two channels at the same time,
> The payload will be parsed by both sides and a message will be sent to each side.

### 3.2 Define side

```csharp
context.AddGroup("Monitor", L("Monitor"))
    .AddNotification("SystemAlarm", L("SystemAlarm"))
    .WithProviders(
        "Webhook");   // WebhookNotificationPublishProvider.ProviderName
```

The delivery target of Webhook is determined by **subscription relationship** (`IWebhookPublisher`),
Notification data is only responsible for "what to send".

### 3.3 Producer side

```csharp
var data = new NotificationData();
data.WriteStandardData("Build failed", "Build #1234 failed", Clock.Now, "ci");

// Use serializer to write payload (recommended)
channelDataSerializer.Write(data, new WeChatWorkWebhookTextChannelData
{
    Content = "Build #1234 failed, please check the log"
});

// You can also write the key directly
data.TrySetData(WeChatWorkWebhookNotificationDataKeys.Markdown, "**Build failed**");
```

### 3.4 Available payload types

| Payload type | `msgtype` | Purpose |
|---|---|---|
| `WeChatWorkWebhookTextChannelData` | `text` | Plain text |
| `WeChatWorkWebhookMarkdownChannelData` | `markdown` | markdown |
| `WeChatWorkWebhookMarkdownV2ChannelData` | `markdown_v2` | markdown_v2 (supports tables and code blocks) |
| `WeChatWorkWebhookImageChannelData` | `image` | Image (base64 + md5) |
| `WeChatWorkWebhookNewsChannelData` | `news` | Graphics |
| `WeChatWorkWebhookFileChannelData` | `file` | File (material ID) |
| `WeChatWorkWebhookVoiceChannelData` | `voice` | Voice (material ID) |
| `WeChatWorkWebhookTemplateCardChannelData` | `template_card` | Template card |

**Each payload is mapped 1:1 to its own `msgtype`, with no implicit degradation. **
(Before the transformation, only `text` / `markdown` / `markdown_v2` were supported, and the remaining content types were silently discarded.)

Template card example:

```csharp
channelDataSerializer.Write(data, new WeChatWorkWebhookTemplateCardChannelData
{
    TemplateCard = new WebhookTextNoticeCardMessage(
        action: WebhookTemplateCardAction.Link("https://example.com/build/1234"),
        mainTitle: new WebhookTemplateCardMainTitle("Build failed", "Please handle it in time"),
        subTitleText: "Build #1234 failed at 15:30",
        horizontalContents: new List<WebhookTemplateCardHorizontalContent>
        {
            WebhookTemplateCardHorizontalContent.Default("branch", "main"),
            WebhookTemplateCardHorizontalContent.Default("submit", "a1b2c3d"),
        })
});
```

Example of graphic message:

```csharp
channelDataSerializer.Write(data, new WeChatWorkWebhookNewsChannelData
{
    News = new WebhookNewsMessage(new List<WebhookArticleMessage>
    {
        new WebhookArticleMessage(
            title: "Monthly Business Report",
            url: "https://example.com/report",
            description: "October 2025",
            pictureUrl: "https://example.com/cover.png")
    })
});
```

### 3.5 Backwards Compatibility

**When the payload is missing** fall back to derivation based on `ContentType` (that is, the behavior before transformation):

| `ContentType` | derived vector |
|---|---|
| `Text` | `text` |
| `Html` | HTML → markdown, then press `UseMarkdownV2` to decide `markdown_v2` or `markdown` |
| `Markdown` | Press `UseMarkdownV2` to decide `markdown_v2` or `markdown` |

Therefore historical producers that only write standard data do not need to be modified.

`UseMarkdownV2` defaults to `true` and can be overridden in the module's `PreConfigure`:

```csharp
[DependsOn(typeof(AbpNotificationsWebhookWeChatWorkModule))]
public class MyModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<AbpNotificationsWebhookWeChatWorkOptions>(options =>
        {
            options.UseMarkdownV2 = false;
        });
    }
}
```

### 3.6 A Webhook only carries one message

When declaring multiple payloads, take the one with the smallest `Priority`:

| Load | Priority |
|---|---|
| text | 100 |
| markdown | 200 |
| markdown_v2 | 300 |
| Pictures | 400 |
| Graphics | 500 |
|File|600|
| Voice | 700 |
| Template Card | 800 |

> This is different from the enterprise WeChat application messaging channel - the latter will send multiple messages with multiple payloads.
> A Webhook publication corresponds to one subscription callback, so it can only carry one.

---

## 4. Scenario 4: Customized channels

Take a "DingTalk Robot" channel as an example.

### 4.1 Define payload

```csharp
using LINGYUN.Abp.Notifications;

namespace MyProject.Notifications.DingTalk;

public static class DingTalkNotificationDataKeys
{
    public const string Prefix = "dingtalk:";
    public const string Webhook = Prefix + "webhook";
    public const string AtMobiles = Prefix + "at_mobiles";
}

[NotificationChannelData(DingTalkNotificationPublishProvider.ProviderName, Priority = 100)]
public class DingTalkChannelData : NotificationChannelData
{
    [NotificationChannelProperty(DingTalkNotificationDataKeys.Webhook)]
    public virtual string? Webhook { get; set; }

    [NotificationChannelProperty(DingTalkNotificationDataKeys.AtMobiles)]
    public virtual string? AtMobiles { get; set; }
}
```

> ✅ **No need to implement `IsSet`**.
> Existence is deduced declaratively by `Presence` (default "all matching attributes exist").
> If you want "only writing atMobiles counts as a statement", change it to:
> `[NotificationChannelData(..., Presence = NotificationChannelDataPresenceMode.AnyDeclaredProperty)]`。
>
> If a property is only an optional read item (does not participate in existence determination), add `IgnoreWhenMatching = true` to it.
>
> If `Presence` is neither declared nor any matching attributes, **will be thrown during registration
> `AbpNotificationChannelDataConfigurationException`**. This is by design:
> Change "forgot to define existence" from silently dropping messages in the production environment to failing at startup.
>
> Why not use "abstract static method enforcement"? C# does not allow `static` members to be marked as `abstract` (CS0112),
> The base class cannot force the derived class to implement static members; and providing a "default return false" implementation will result in
> **Forgot implementer silently invalidates**. See [CHANNEL-DATA.md §3](./CHANNEL-DATA.md) for details.

### 4.2 Registration

The channel module declares the payload using `Configure<>` in its own `AbpModule`:

```csharp
using LINGYUN.Abp.Notifications;
using Volo.Abp.Modularity;

namespace MyProject.Notifications.DingTalk;

[DependsOn(typeof(AbpNotificationsModule))]
public class AbpNotificationsDingTalkModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpNotificationsPublishOptions>(options =>
        {
            options.PublishProviders.Add<DingTalkNotificationPublishProvider>();
        });

        // Declare the payload formats supported by this platform (the registration period is to verify the existence contract)
        Configure<AbpNotificationChannelDataOptions>(options =>
        {
            options.Add<DingTalkChannelData>();
        });
    }
}
```

> `AbpNotificationChannelDataOptions` is a pure data object. Existence verification during the registration period only relies on declaration metadata.
> Does not depend on any service, so `Add()` does not require any initialization sequence and does not require additional configurator classes.

### 4.3 Parsing in channel implementation

```csharp
public class DingTalkNotificationPublishProvider : NotificationPublishProvider
{
    public const string ProviderName = "DingTalk";
    public override string Name => ProviderName;

    protected INotificationChannelDataResolver ChannelDataResolver
        => ServiceProvider.LazyGetRequiredService<INotificationChannelDataResolver>();

    protected async override Task PublishAsync(
        NotificationPublishContext context,
        CancellationToken cancellationToken = default)
    {
        var data = context.Notification.Data;

        // ① The payload has been declared but parsing failed => the producer data is illegal, an error is reported clearly instead of silently falling back
        if (ChannelDataResolver.IsDeclared(data, typeof(DingTalkChannelData))
            && ChannelDataResolver.Resolve<DingTalkChannelData>(data) is not { } payload)
        {
            context.Cancel("The dingtalk payload can not be parsed, please check the notification data.");
            Logger.LogWarning(context.Reason);
            return;
        }

        // ② If not declared, the default behavior will be used
        var webhook = ChannelDataResolver.Resolve<DingTalkChannelData>(data)?.Webhook;
        if (webhook.IsNullOrWhiteSpace())
        {
            context.Cancel("Unable to send dingtalk messages because the webhook is not set.");
            return;
        }

        // ③ Use typed reading when scalar options are required (do not use as)
        var timeout = ChannelDataResolver.GetValue<int>(data, "dingtalk:timeout") ?? 30;

        await SendAsync(webhook, context, cancellationToken);
    }
}
```

### 4.4 Definition side (channel must be declared, otherwise the channel will not be called)

```csharp
public class MyNotificationDefinitionProvider : NotificationDefinitionProvider
{
    public override void Define(INotificationDefinitionContext context)
    {
        context.AddGroup("MyGroup", L("MyGroup"))
            .AddNotification("OrderShipped", L("OrderShipped"))
            .WithProviders(
                DingTalkNotificationPublishProvider.ProviderName,
                NotificationProviderNames.SignalR);
    }
}
```

### 4.5 Producer side

```csharp
var data = new NotificationData();
data.WriteStandardData("Order Shipped", "Your order has been shipped", Clock.Now, "system");

// Payload driven by type declaration (recommended)
channelDataSerializer.Write(data, new DingTalkChannelData
{
    Webhook = "https://oapi.dingtalk.com/robot/send?access_token=xxx",
    AtMobiles = "13800000000"
});

await _notificationSender.SendNofiterAsync("OrderShipped", data, user);
```

> **Don't add extension methods** (such as `WithDingTalkRobot`) to channels.
> This framework has removed all channel-specific write extension methods: payload type + serializer
> A unified entrance with prefix, verifiable and discoverable has been provided.
> Overlaying another layer of extension methods will only result in two sets of parallel writing conventions.

---

## 5. Scenario 5: Migrate from the old writing method

### 5.1 Channel-specific extension methods have all been removed

Earlier versions provided a set of write/read extension methods in the channel module:

| Removed | Replacement |
|---|---|
| `data.WithTextCardMessage(...)`、`WithTemplateCardMessage`、`WithMiniProgramMessage`、`WithMpNewMessage`、`WithVideoMessage`、`WithVoiceMessage`、`WithFileMessage` | `channelDataSerializer.Write(data, new WeChatWorkXxxChannelData { ... })` |
| `data.SetSafe(...)`、`SetEnableIdTrans`、`SetEnableDuplicateCheck`、`SetDuplicateCheckInterval` | `data.TrySetData(WeChatWorkNotificationDataKeys.Xxx, value)` |
| `data.SetParty(...)`、`SetTag(...)` | `data.TrySetData(WeChatWorkNotificationDataKeys.ToParty / ToTag, value)` |
| `data.SetUrl(...)` (WxPusher) | `data.TrySetData(WxPusherNotificationDataKeys.Url, value)` or `channelDataSerializer.Write(data, new WxPusherChannelData { ... })` |
| `data.SetWebhook(...)`、`SetCallbackUrl(...)` (PushPlus) | `data.TrySetData(PushPlusNotificationDataKeys.Webhook / CallbackUrl, value)` |
| `GetSafe()`, `GetUrlOrNull()`, `TryGetMessagePayload()` and other reading methods | `INotificationChannelDataResolver` |

**Key constants (`XxxNotificationDataKeys`) are all reserved** - they define the data contract,
Not a convenience API. The channel's `NotificationDefinition` extension method (e.g.
`WithAgentId`, `WithTag`) are **definition side** APIs, which have nothing to do with data keys and are also retained.

### 5.2 Channel Implementation: Replace Naked Conversion

```csharp
// ❌ Old way of writing - it will inevitably become invalid after persistence, and there will be no log
var safe = data.TryGetData("wx-work:safe") as byte? ?? 0;
var url = data.TryGetData("wx-pusher:url")?.ToString();

// ✅ New writing method (inject INotificationChannelDataResolver)
var safe = channelDataResolver.GetValue<bool>(data, WeChatWorkNotificationDataKeys.Safe);
var payload = channelDataResolver.Resolve<WxPusherChannelData>(data);
var url = payload?.Url;
```

### 5.3 List of breaking changes

| Changes | Impact |
|---|---|
| **All channel-specific extension methods have been deleted** | `NotificationDataWeChatWorkExtensions` and `NotificationDataExtensions` of WxPusher/PushPlus have been removed. For the delivery method, see §4.1 Comparison table |
| **`NotificationChannelValues` static class has been deleted** | All capabilities changed to instance services: `INotificationChannelDataAccessor` (conversion) and `INotificationChannelDataResolver` (channel reading). Static API is nowhere to be found and compilation fails |
| Payload no longer required (and should not be written) `IsSet` | Existence changed to `Presence` declarative; old `IsSet` will not be called |
| `NotificationChannelDataSerializer.AddJsonConverter` has been removed | Changed to `INotificationChannelDataJsonSerializer`; Converter for abstract payload registered to JSON option |
| Enterprise WeChat `agentId` data key | Key constants retained (`WeChatWorkNotificationDataKeys.AgentId`); only write extension methods are removed |

> **Reserved**: `XxxNotificationDataKeys` (data contract),
> Channel extension method of `NotificationDefinition` (definition-side API, such as `WithAgentId`, `WithTag`),
> `NotificationDefinitionProvider` for each channel.

### 5.4 Read the migration comparison table

| Old extension method | New way of writing |
|---|---|
| `data.GetSafe()` | `resolver.GetValue(data, WeChatWorkNotificationDataKeys.Safe, false)` |
| `data.GetEnableIdTrans()` / `GetEnableDuplicateCheck()` | `resolver.GetValue(data, WeChatWorkNotificationDataKeys.EnableIdTrans / EnableDuplicateCheck, false)` |
| `data.GetDuplicateCheckInterval()` | `resolver.GetValue(data, WeChatWorkNotificationDataKeys.DuplicateCheckInterval, 1800)` |
| `data.GetAgentIdOrNull()` / `GetTagOrNull()` / `GetPartyOrNull()` | `resolver.GetOrNull(data, WeChatWorkNotificationDataKeys.AgentId / ToTag / ToParty)` |
| `data.GetUrlOrNull()` (WxPusher) | `resolver.Resolve<WxPusherChannelData>(data)?.Url` |
| `data.GetWebhookOrNull()` (PushPlus) | `resolver.Resolve<PushPlusChannelData>(data)?.Webhook` |
| `data.GetCallbackUrlOrNull()` (PushPlus) | `resolver.Resolve<PushPlusChannelData>(data)?.CallbackUrl` |
| `data.TryGetMessagePayload<T>(key, out var m)` | `resolver.Resolve<TChannelData>(data)` |

---

## 6. FAQ

| Phenomenon | Cause | Treatment |
|---|---|---|
| The channel is not called | The notification definition does not declare the channel name | `WithProviders(...)` or `useProviders` is passed when sending |
| Payload "written but not effective" | Existence determination excludes the key (marked `IgnoreWhenMatching`, or `Presence` is not as expected) | Check `Presence` and `IgnoreWhenMatching` annotations |
| `AbpNotificationChannelDataConfigurationException` is thrown upon startup | `Presence` is not declared and there is no matching attribute; or the same channel `Priority` is repeated; or `[NotificationChannelData]` is missing | Correct according to the exception information |
| Received two enterprise WeChat messages at the same time | Multiple payloads are written, each will be sent | Only one payload is retained |
| Template card report `abstract class and cannot be instantiated` | The abstract type lacks a JSON converter | Automatically registered to `AbpSystemTextJsonSerializerOptions` by `AbpNotificationsWeChatWorkModule`; you need to register yourself when building a container manually |
| The production environment silently loses messages | The payload data is illegal (parsing failed) | Use the `IsDeclared` + `Resolve` combination on the channel side to clearly report the error; the failure key name will be output in the log |

---

## 7. Unit testing

### 7.1 Split by module

Test projects correspond to modules one-to-one. **Framework Contract** and **Specific Channel** are tested separately:

| Test Project | Coverage |
|---|---|
| `tests/LINGYUN.Abp.Notifications.ChannelData.Tests` | **Framework Contract**: Weak type conversion, existence determination (three modes), registration period verification, priority sorting, JSON round-trip. Use the stub payload in this file |
| `tests/LINGYUN.Abp.Notifications.WeChat.Work.Tests` | Enterprise WeChat **Application Message** Channel |
| `tests/LINGYUN.Abp.Notifications.Webhook.WeChat.Work.Tests` | Enterprise WeChat **Webhook** Channel |
| `tests/LINGYUN.Abp.Notifications.WeChat.MiniProgram.Tests` | WeChat mini program subscription message channel |
| `tests/LINGYUN.Abp.Notifications.WxPusher.Tests` | WxPusher channel |
| `tests/LINGYUN.Abp.Notifications.PushPlus.Tests` | PushPlus channel |

### 7.2 Unified test writing method

Each channel test follows the same path: **First new message object → serialize and write notification data through `IJsonSerializer` → channel side verification**.

```csharp
public class WeChatWorkChannelData_Tests : AbpNotificationsWeChatWorkTestsBase
{
    [Fact]
    public void Should_Resolve_TextCard_Message()
    {
        // 1. First new message object
        var message = new TextCardMessage(
            title: "Approval Passed",
            description: "Your leave application has been approved",
            url: "https://example.com/leave/123",
            buttonText: "Go and check");

        // 2. Serialize and write the payload key of the notification data via IJsonSerializer
        var data = WriteChannelData(new WeChatWorkTextCardChannelData { TextCard = message });

        // 3. Verify the restoration result on the channel side
        var payload = ChannelDataResolver.Resolve<WeChatWorkTextCardChannelData>(data);

        payload.ShouldNotBeNull();
        payload!.TextCard!.Title.ShouldBe(message.Title);
        payload.TextCard.Url.ShouldBe(message.Url);
    }
}
```

Public base class `NotificationChannelDataTestsBase<TStartupModule>` (located in
`tests/LINGYUN.Abp.Notifications.Tests`) provides:

| Members | Description |
|---|---|
| `JsonSerializer` | ABP's `IJsonSerializer` for asserting JSON contracts |
| `ChannelDataAccessor` | Production side writing (serialization) |
| `ChannelDataResolver` | Channel side analysis |
| `ChannelDataSerializer` | Producer-side serializer |
| `Serialize(payload)` | Serialization only, used to assert field name contracts |
| `WriteChannelData(channelData)` | new → serialize → write, return `NotificationData` |
| `MergeChannelData(data, channelData)` | Merge multiple payloads into the same notification data |

> The test base is a real ABP integration test (`AbpIntegratedTest`), so `IJsonSerializer`
> Just the one for the production environment - the converter that contains the global JSON policy and channel registration.
> The channel's **real send** is replaced with a capture-style fake implementation (`FakeSubscribeMessager` /
> `FakeWxPusherMessageSender` / `FakePushPlusMessageSender`)，
> This enables us to assert "the content of the message finally sent by the channel" without actually calling the third-party interface.

### 7.3 Run

```bash
# all
dotnet test aspnet-core/tests/LINGYUN.Abp.Notifications.ChannelData.Tests
dotnet test aspnet-core/tests/LINGYUN.Abp.Notifications.WeChat.Work.Tests
dotnet test aspnet-core/tests/LINGYUN.Abp.Notifications.Webhook.WeChat.Work.Tests
dotnet test aspnet-core/tests/LINGYUN.Abp.Notifications.WeChat.MiniProgram.Tests
dotnet test aspnet-core/tests/LINGYUN.Abp.Notifications.WxPusher.Tests
dotnet test aspnet-core/tests/LINGYUN.Abp.Notifications.PushPlus.Tests
```
