using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LINGYUN.Abp.Notifications.WxPusher;

/// <summary>
/// WxPusher channel data test
/// </summary>
/// <remarks>
/// Unified writing method: first new channel data → serialize and write notification data through <c>IJsonSerializer</c> →
/// Let the channel actually publish and assert the captured message parameters.
/// </remarks>
public class WxPusherChannelData_Tests : AbpNotificationsWxPusherTestBase
{
    private const string Provider = WxPusherNotificationPublishProvider.ProviderName;

    private FakeWxPusherMessageSender Sender
        => (FakeWxPusherMessageSender)GetRequiredService<LINGYUN.Abp.WxPusher.Messages.IWxPusherMessageSender>();

    [Fact]
    public void Should_Resolve_Url()
    {
        var data = WriteChannelData(new WxPusherChannelData
        {
            Url = "https://example.com/detail/123"
        });

        var payload = ChannelDataResolver.Resolve<WxPusherChannelData>(data);

        payload.ShouldNotBeNull();
        payload!.Url.ShouldBe("https://example.com/detail/123");
    }

    [Fact]
    public void Should_Not_Declare_Payload_When_Url_Absent()
    {
        ChannelDataResolver.IsDeclared(new NotificationData(), typeof(WxPusherChannelData))
            .ShouldBeFalse();
    }

    [Fact]
    public void Should_Match_Key_Case_Insensitively()
    {
        var data = new NotificationData();
        data.TrySetData(WxPusherNotificationDataKeys.Url, "https://example.com/a");

        ChannelDataResolver.Resolve<WxPusherChannelData>(data)!.Url
            .ShouldBe("https://example.com/a");

        var upper = new NotificationData();
        upper.TrySetData("WX-PUSHER:URL", "https://example.com/b");

        ChannelDataResolver.Resolve<WxPusherChannelData>(upper)!.Url
            .ShouldBe("https://example.com/b");
    }

    [Fact]
    public async Task Should_Send_With_Resolved_Url()
    {
        Sender.Clear();

        var data = WriteChannelData(new WxPusherChannelData
        {
            Url = "https://example.com/from-channel-data"
        });
        data.WriteStandardData("title", "content", DateTime.Now, "system");

        await PublishAsync(data);

        Sender.SentMessages.Count.ShouldBe(1);
        Sender.SentMessages[0].Url.ShouldBe("https://example.com/from-channel-data");
        Sender.SentMessages[0].Content.ShouldBe("content");
        Sender.SentMessages[0].Summary.ShouldBe("title");
    }

    [Fact]
    public async Task Should_Send_Without_Url_When_Not_Provided()
    {
        Sender.Clear();

        var data = new NotificationData();
        data.WriteStandardData("title", "content", DateTime.Now, "system");

        await PublishAsync(data);

        Sender.SentMessages.Count.ShouldBe(1);
        Sender.SentMessages[0].Url.ShouldBeNull();
    }

    private async Task PublishAsync(NotificationData data)
    {
        var provider = GetRequiredService<WxPusherNotificationPublishProvider>();

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
