using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LINGYUN.Abp.Notifications.PushPlus;

/// <summary>
/// PushPlus channel data test
/// </summary>
/// <remarks>
/// Unified writing method: first new channel data → serialize and write notification data through <c>IJsonSerializer</c> →
/// Let the channel actually publish and assert the captured message parameters.
/// </remarks>
public class PushPlusChannelData_Tests : AbpNotificationsPushPlusTestsBase
{
    private const string Provider = PushPlusNotificationPublishProvider.ProviderName;

    private FakePushPlusMessageSender Sender
        => (FakePushPlusMessageSender)GetRequiredService<LINGYUN.Abp.PushPlus.Message.IPushPlusMessageSender>();

    // ---------- 1. Parse channel data ----------

    [Fact]
    public void Should_Resolve_Both_Optional_Keys()
    {
        var data = WriteChannelData(new PushPlusChannelData
        {
            Webhook = "https://example.com/hook",
            CallbackUrl = "https://example.com/callback"
        });

        var payload = ChannelDataResolver.Resolve<PushPlusChannelData>(data);

        payload.ShouldNotBeNull();
        payload!.Webhook.ShouldBe("https://example.com/hook");
        payload.CallbackUrl.ShouldBe("https://example.com/callback");
    }

    [Fact]
    public void Should_Declare_Payload_When_Only_One_Key_Written()
    {
        // Presence = AnyDeclaredProperty: writing any one is considered a declaration
        var webhookOnly = WriteChannelData(new PushPlusChannelData
        {
            Webhook = "https://example.com/hook"
        });
        ChannelDataResolver.IsDeclared(webhookOnly, typeof(PushPlusChannelData)).ShouldBeTrue();

        var callbackOnly = WriteChannelData(new PushPlusChannelData
        {
            CallbackUrl = "https://example.com/callback"
        });
        ChannelDataResolver.IsDeclared(callbackOnly, typeof(PushPlusChannelData)).ShouldBeTrue();
    }

    [Fact]
    public void Should_Not_Declare_Payload_When_Nothing_Written()
    {
        ChannelDataResolver.IsDeclared(new NotificationData(), typeof(PushPlusChannelData))
            .ShouldBeFalse();
    }

    [Fact]
    public void Should_Match_Keys_Case_Insensitively()
    {
        var data = new NotificationData();
        data.TrySetData(PushPlusNotificationDataKeys.Webhook, "https://example.com/hook");

        ChannelDataResolver.Resolve<PushPlusChannelData>(data)!.Webhook
            .ShouldBe("https://example.com/hook");

        var upper = new NotificationData();
        upper.TrySetData("PUSH-PLUS:WEBHOOK", "https://example.com/HOOK");

        ChannelDataResolver.Resolve<PushPlusChannelData>(upper)!.Webhook
            .ShouldBe("https://example.com/HOOK");
    }

    // ---------- 2. Actual release of channel (end-to-end) ----------

    [Fact]
    public async Task Should_Send_With_Resolved_Webhook_And_CallbackUrl()
    {
        Sender.Clear();

        var data = WriteChannelData(new PushPlusChannelData
        {
            Webhook = "https://example.com/from-channel-data",
            CallbackUrl = "https://example.com/callback"
        });
        data.WriteStandardData("title", "content", DateTime.Now, "system");

        await PublishAsync(data);

        Sender.SentMessages.Count.ShouldBe(1);
        Sender.SentMessages[0].Webhook.ShouldBe("https://example.com/from-channel-data");
        Sender.SentMessages[0].CallbackUrl.ShouldBe("https://example.com/callback");
        Sender.SentMessages[0].Title.ShouldBe("title");
        Sender.SentMessages[0].Content.ShouldBe("content");
    }

    [Fact]
    public async Task Should_Send_With_Empty_Options_When_Nothing_Written()
    {
        Sender.Clear();

        var data = new NotificationData();
        data.WriteStandardData("title", "content", DateTime.Now, "system");

        await PublishAsync(data);

        Sender.SentMessages.Count.ShouldBe(1);
        Sender.SentMessages[0].Webhook.ShouldBeEmpty();
        Sender.SentMessages[0].CallbackUrl.ShouldBeEmpty();
    }

    private async Task PublishAsync(NotificationData data)
    {
        var provider = GetRequiredService<PushPlusNotificationPublishProvider>();

        var notification = new NotificationInfo
        {
            Name = "Test",
            ContentType = NotificationContentType.Text,
            Data = data
        };
        notification.SetId(1);

        var context = new NotificationPublishContext(
            notification,
            new[] { new UserIdentifier(Guid.NewGuid(), "tester") });

        await provider.PublishAsync(context);
    }
}
