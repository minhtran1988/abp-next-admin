# Notification channel data structure analysis

Strongly typed payload (channel data) framework for **release channels**.

Core problem solved: When producers use weakly typed attribute packages to express channel-specific semantics,
"The key is written but the channel can never read it", "The configuration fails silently after being persisted", "The payload type is guessed based on the field detection sequence".

---

## 1. Problem background

The bottom layer of notification data `NotificationData` is `ExtraPropertyDictionary` (weak type property bag),
A JSON round trip must be experienced in the following links:

| link | location |
|---|---|
| Read back after persistence | `NotificationStore.InsertNotificationAsync` → `Notification.SetData` |
| Retry on failure | `NotificationPublishJob` reads from storage and republishes |
| Cross-process delivery | `NotificationEto` via EventBus |

The runtime type of the value changes after the JSON round trip:

```
byte 1  ->  long 1 / JsonElement(Number)
int 1   ->  long 1 / JsonElement(Number)
```

So a naked conversion in the old implementation would fail forever without any logs:

```csharp
// Write int, read byte? —— as operator always returns null for boxed int
notificationData.TrySetData(Safe, safe ? 1 : 0);
var safe = notificationData.TryGetData(Safe) as byte?; // always null
```

Affected options: Enterprise WeChat `safe`, `enable_id_trans`, `enable_duplicate_check`.

---

## 2. Design principles

1. **All obtained through dependency injection, no static API provided**
   ——Conversion relies on JSON configuration and logs. Static entry will force the framework to maintain a set of "statically available downgrade implementations".
   Increase the implicit coupling of state and initialization order.
2. **JSON is abstracted through a dedicated interface**, and the ABP global configuration is reused by default.
3. **Existence declarative expression + mandatory verification during registration**, allowing configuration errors to be exposed during the startup phase.
4. **The conversion logic shares the same implementation on both reading and writing sides** (`INotificationChannelDataAccessor`),
   Avoid "the rules of the writer and the reader are inconsistent".

---

## 3. Framework composition

### 3.1 Contract (`LINGYUN.Abp.Notifications.Core`, `ChannelData/` subdirectory)

| File | Type | Function |
|---|---|---|
| `NotificationChannelData.cs` | `NotificationChannelData` | Payload base class (empty tag, see §4) |
| `NotificationChannelDataAttribute.cs` | `NotificationChannelDataAttribute` | Declaration channel, matching order `Priority`, presence mode `Presence` |
| `NotificationChannelDataPresenceMode.cs` | `NotificationChannelDataPresenceMode` | Presence determination mode (None / DeclaredProperties / AnyDeclaredProperty) |
| `NotificationChannelPropertyAttribute.cs` | `NotificationChannelPropertyAttribute` | Declare the attributes that participate in reading and writing; `Name` is compatible with history keys; `IgnoreWhenMatching` excludes optional reading items |
| `NotificationChannelDataFormat.cs` | `NotificationChannelDataFormat` | Property storage format (Auto / Json) |
| `NotificationChannelDataDescriptor.cs` | `NotificationChannelDataDescriptor` | Payload descriptor (read-only metadata) |

### 3.2 Implementation (`LINGYUN.Abp.Notifications`, `ChannelData/` subdirectory)

Each type has a separate file (interface and implementation files).

| Interface | Default implementation | Function |
|---|---|---|
| `INotificationChannelDataJsonSerializer` | `AbpNotificationChannelDataJsonSerializer` | JSON abstraction, default delegate ABP `IJsonSerializer` |
| `INotificationChannelDataAccessor` | `NotificationChannelDataAccessor` | **Weak type conversion + the only implementation of JSON**, read and write shared |
| `INotificationChannelDataPresenceProvider` | `NotificationChannelDataPresenceProvider` | Creation and caching of existence determination delegate (including registration period verification) |
| `INotificationChannelDataSerializer` | `NotificationChannelDataSerializer` | Producer side writing entry |
| `INotificationChannelDataResolver` | `NotificationChannelDataResolver` | Channel side parsing entrance |

Auxiliary type: `AbpNotificationChannelDataOptions` (payload registration),
`NotificationChannelDataDescriptorList`、`AbpNotificationChannelDataConfigurationException`、
`NotificationChannelDataPresence` (existence determination, purely static),
`NotificationChannelDataProperty` / `NotificationChannelDataPropertyCache` (property metadata),
`NotificationChannelDataValueTypes` (value type determination).

### 3.3 Registration method: module `PostConfigure<>`

The channel module declares the payload in its own `AbpModule.ConfigureServices`:

```csharp
[DependsOn(typeof(AbpNotificationsModule))]
public class AbpNotificationsMyChannelModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpNotificationsPublishOptions>(options =>
        {
            options.PublishProviders.Add<MyNotificationPublishProvider>();
        });

        // Declare the payload formats supported by this platform (the registration period is to verify the existence contract)
        Configure<AbpNotificationChannelDataOptions>(options =>
        {
            options.Add<MyChannelData>();
        });
    }
}
```

**Why no additional configurator classes are needed and there are no ordering issues**:
`AbpNotificationChannelDataOptions` is a **pure data** object - the existence check during the registration period only relies on
Declaration metadata on payload type (`[NotificationChannelData]` / `[NotificationChannelProperty]`),
Not dependent on any service. The decision logic is located in the static `NotificationChannelDataPresence`,
Therefore `options.Add(...)` will work properly if called before the container is built, in any module order.

Correspondingly, the previously introduced "resolving existence provider"
`NotificationChannelDataPresenceConfigurer` has been removed, on the options object
The `PresenceProvider` attribute has also been removed. `INotificationChannelDataPresenceProvider`
Reserved as a thin wrapper in dependency injection form (delegated to static implementation) for use by parsers and serializers.

---

## 4. Data key naming convention

### 4.1 This channel prefix must be included

All channel-owned keys use the form `<channel prefix><key name>` to avoid keys from different channels in the same
Conflicting notification data property packs:

| channel | prefix | key |
|---|---|---|
| Enterprise WeChat | `wx-work:` | `wx-work:text_card_message`, `wx-work:template_card_message`, `wx-work:safe` … |
| Enterprise WeChat Webhook | `wx-work-hook:` | `wx-work-hook:text`, `wx-work-hook:markdown_v2`, `wx-work-hook:template_card` … |
| WeChat applet | `wx-mp:` | `wx-mp:template_id`, `wx-mp:redirect_page`, `wx-mp:weapp_state`, `wx-mp:weapp_language` |
| WxPusher | `wx-pusher:` | `wx-pusher:url` |
| PushPlus | `push-plus:` | `push-plus:webhook`、`push-plus:callback` |

> **Channel registry keys and key prefixes are independent namespaces**. Enterprise WeChat application messages and Webhook are two transmission methods.
> Two sets of carrier models, so registry keys (`WeChat.Work` / `WeChat.Work.Webhook`) and key prefixes
> (`wx-work:` / `wx-work-hook:`) must be separated ——
> Otherwise, when the same notification is subscribed by two channels at the same time, the payload will be parsed by both parties and one message will be sent each.

Each channel exposes constants as `XxxNotificationDataKeys`, and gives a `Prefix`:

```csharp
public static class MyChannelNotificationDataKeys
{
    public const string Prefix = "my-channel:";

    public const string TemplateId = Prefix + "template_id";
    public const string RedirectPage = Prefix + "redirect_page";
}
```

> The only exception is the keys for **notification standard data** (`title` / `message` / `description` /
> `formUser` / `createTime`), which are defined by `NotificationData.WriteStandardData`,
> It is a common convention shared by all channels, without prefix.

### 4.2 Reading is not case sensitive

The key matching rules are **Exact matching takes precedence, and case is ignored if there is no hit**,
Therefore producers use `wx-work:text_card_message`, `WX-WORK:TEXT_CARD_MESSAGE`
Or `Wx-Work:Text_Card_Message` can all hit.

This rule is implemented uniformly in `INotificationChannelDataAccessor.TryGetRawValue`,
`NotificationChannelDataPresence.ContainsKey` uses the same rule,
It is guaranteed that there will be no rule inconsistency between "existence determination" and "actual reading".

### 4.3 Historical unprefixed keys

The WeChat mini program channel used unprefixed keys (`TemplateId`, etc.) in early versions.
To allow for a smooth upgrade of installed producers, these keys remain in
`WeChatMiniProgramNotificationDataKeys.Legacy`, **read-only fallback** when parsed by the channel
(prefixed keys take precedence), and log warnings when legacy keys are hit to drive migrations.

Please always use prefixed constants in new code.

### 4.4 Keys of multiple channels can coexist in one data object

Different prefixes do not conflict with each other, so the same notification can drive multiple channels at the same time:

```csharp
data.TrySetData(WeChatWorkNotificationDataKeys.ToParty, "1|2"); // Application message
data.TrySetData(WeChatWorkWebhookNotificationDataKeys.Markdown, "# alert"); // Webhook
```

Each channel only parses the payload of its own prefix and will not interfere with each other (see §9).

---

## 5. Existence determination

**Declarative existence + mandatory verification during registration**:

```csharp
public enum NotificationChannelDataPresenceMode
{
    None = 0, // always exists (does not rely on notification data)
    DeclaredProperties = 1, // All properties involved in matching exist (default)
    AnyDeclaredProperty = 2 // Any one of the properties participating in the matching exists
}
```

Verification rules (executed during registration, i.e. startup phase):

| Situation | Result |
|---|---|
| Key press determination is declared, but there is no matching attribute | Throwing `AbpNotificationChannelDataConfigurationException` |
| Explicitly declare `Presence = None` | Legal, payload is always present |
| Missing `[NotificationChannelData]` | Throws `AbpNotificationChannelDataConfigurationException` |
| Duplicate same channel `Priority` | Throw `AbpNotificationChannelDataConfigurationException` |

In this way, "forgetting to define existence" changes from **losing messages silently** to **failure at startup**.

---

## 6. JSON serialization abstraction

```csharp
public interface INotificationChannelDataJsonSerializer
{
    string Serialize(object value, Type valueType);
    object? Deserialize(string json, Type targetType);
}

[Dependency(TryRegister = true)]
public class AbpNotificationChannelDataJsonSerializer
    : INotificationChannelDataJsonSerializer, ISingletonDependency
{
    protected IJsonSerializer JsonSerializer { get; }

    public string Serialize(object value, Type valueType) => JsonSerializer.Serialize(value);

    public object? Deserialize(string json, Type targetType)
        => json.IsNullOrWhiteSpace() ? null : JsonSerializer.Deserialize(targetType, json);
}
```

Reused global capabilities: camel case strategy, time format, enumeration conversion,
`AbpSystemTextJsonSerializerModifiersOptions` modifier, custom Converter.

When encryption/compression/private format is required, just replace the interface implementation.

> **Note**: The JSON integration used by the project is `Volo.Abp.Json.SystemTextJson`,
> Therefore the `JsonConverter` for complex payloads must implement the System.Text.Json version
> (see `TemplateCardSystemTextJsonConverter`).

---

## 7. Decoupling from JSON implementation

`NotificationChannelDataAccessor` will **restore the JSON intermediate representation** before conversion:

| JSON implementation | Values in property bags |
|---|---|
| System.Text.Json | `JsonElement` |
| Newtonsoft.Json | `JValue` / raw CLR value |

Both will first be restored to the CLR original value (the number is restored to the smallest available numeric type), and then weak type conversion will be performed.
Therefore, the same logic behaves consistently under both JSON implementations.

---

## 8. Producer usage

### 8.1 Declaring payload

```csharp
[NotificationChannelData(MyProvider.ProviderName, Priority = 100)]
public class MyChannelData : NotificationChannelData
{
    [NotificationChannelProperty("my:template_id")]
    public string? TemplateId { get; set; }

    // Optional reading item: does not participate in existence determination
    [NotificationChannelProperty("my:page", IgnoreWhenMatching = true)]
    public string? Page { get; set; }
}
```

### 8.2 Register in the module

```csharp
[DependsOn(typeof(AbpNotificationsModule))]
public class AbpNotificationsMyChannelModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpNotificationsPublishOptions>(options =>
        {
            options.PublishProviders.Add<MyNotificationPublishProvider>();
        });

        Configure<AbpNotificationChannelDataOptions>(options =>
        {
            options.Add<MyChannelData>();
        });
    }
}
```

### 8.3 Write notification data

```csharp
public class MyProducer
{
    private readonly INotificationChannelDataSerializer _channelDataSerializer;

    public MyProducer(INotificationChannelDataSerializer channelDataSerializer)
    {
        _channelDataSerializer = channelDataSerializer;
    }

    public void Build(NotificationData data)
    {
        _channelDataSerializer.Write(data, new MyChannelData { TemplateId = "tpl-1" });
    }
}
```

---

## 9. Channel usage

```csharp
public class MyProvider : NotificationPublishProvider
{
    protected INotificationChannelDataResolver ChannelDataResolver
        => ServiceProvider.LazyGetRequiredService<INotificationChannelDataResolver>();

    protected override Task PublishAsync(NotificationPublishContext context, CancellationToken ct)
    {
        var data = context.Notification.Data;

        // parse by type
        var payload = ChannelDataResolver.Resolve<MyChannelData>(data);

        // Distinguish between "not written" and "written but illegal data"
        if (ChannelDataResolver.IsDeclared(data, typeof(MyChannelData)) && payload == null)
        {
            context.Cancel("The payload can not be parsed.");
            return Task.CompletedTask;
        }

        // scalar options
        var safe = ChannelDataResolver.GetValue(data, "my:safe", false);

        return Task.CompletedTask;
    }
}
```

---

## 10. Behavior Guarantee

| Scene | Behavior |
|---|---|
| Unset values | Return `null` (rather than being swallowed by the default value), distinguishing "unset" from "explicit false/0" |
| After JSON round trip | `byte`/`int`/`long`/`string`/`bool`/`JsonElement` can be converted correctly |
| Key case inconsistency | Case-insensitive matching, compatible with historical legacy keys (such as `TemplateId`) |
| Illegal data | No exception is thrown; `Resolve` returns `null` and records a warning log, `IsDeclared` is still `true` |
| Existence rule not declared | `AbpNotificationChannelDataConfigurationException` thrown during registration |
| Missing channel features | `AbpNotificationChannelDataConfigurationException` thrown during registration |
| Priority conflict with the same channel | `AbpNotificationChannelDataConfigurationException` thrown during registration |
| The payload type is not mapped to the message carrier | `AbpNotificationChannelDataConfigurationException` is thrown during construction (fast failure) |

---

## 11. Checklist for scaling into new channels

1. Define the payload type and mark it as `[NotificationChannelData]`;
2. Mark at least one "identification property" with `[NotificationChannelProperty]` (optional read item plus `IgnoreWhenMatching = true`);
3. When you need "any key", set `Presence` to `AnyDeclaredProperty`; when you need "always present", set it to `None`;
4. Register the payload in `Configure<AbpNotificationChannelDataOptions>` of this module;
5. Don’t use naked conversions like `as byte?` / `as int?` in channel implementation. Use `INotificationChannelDataResolver` instead;
6. When you need to distinguish between "undeclared" and "illegal data", use `IsDeclared` and `Resolve` in combination;
7. Abstract type payload needs to register a JSON type converter (note JSON implementation: STJ or Newtonsoft).
