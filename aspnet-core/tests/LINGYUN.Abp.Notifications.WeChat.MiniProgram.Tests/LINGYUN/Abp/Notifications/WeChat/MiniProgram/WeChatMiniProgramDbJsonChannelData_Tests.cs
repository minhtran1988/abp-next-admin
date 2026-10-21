using LINGYUN.Abp.WeChat.MiniProgram.Messages;
using Shouldly;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Json;
using Xunit;

namespace LINGYUN.Abp.Notifications.WeChat.MiniProgram;

/// <summary>
/// Parse channel data test from database JSON string
/// </summary>
/// <remarks>
/// <para>
/// The previous test is the same process path of "write in memory → read", and the persistence round trip cannot be verified.
/// This group of tests adds **real link**: directly gives the JSON string stored in the database,
/// After restoring to <see cref="NotificationData"/>, proceed to channel side analysis and actual release.
/// Answer "Can the data be parsed into the channel message format normally after being retrieved from the database?"
/// </para>
/// <para>
/// Mini program channel data are all scalars, so the values in the library are primitive types (not JSON strings).
/// </para>
/// </remarks>
public class WeChatMiniProgramDbJsonChannelData_Tests
    : AbpNotificationsWeChatMiniProgramTestsBase
{
    private const string Provider = WeChatMiniProgramNotificationPublishProvider.ProviderName;

    private FakeSubscribeMessager Messager
        => (FakeSubscribeMessager)GetRequiredService<ISubscribeMessager>();

    // ---------- 1. Key with prefix ----------

    [Fact]
    public void Should_Restore_Channel_Data_From_Db_Json()
    {
        var data = RestoreFromDbJson(
            "{\"title\":\"Order status\",\"message\":\"Your order has been shipped\"," +
            "\"wx-mp:template_id\":\"TPL_DB\"," +
            "\"wx-mp:redirect_page\":\"pages/order/detail\"," +
            "\"wx-mp:weapp_state\":\"developer\"," +
            "\"wx-mp:weapp_language\":\"en_US\"}");

        var payload = ChannelDataResolver.Resolve<WeChatMiniProgramSubscribeChannelData>(data);

        payload.ShouldNotBeNull();
        payload!.TemplateId.ShouldBe("TPL_DB");
        payload.RedirectPage.ShouldBe("pages/order/detail");
        payload.WeAppState.ShouldBe("developer");
        payload.WeAppLanguage.ShouldBe("en_US");
    }

    // ---------- 2. Send JSON from the database all the way to the channel (end-to-end) ----------

    [Fact]
    public async Task Should_Send_With_Data_Restored_From_Db_Json()
    {
        Messager.Clear();

        var data = RestoreFromDbJson(
            "{\"wx-mp:template_id\":\"TPL_DB_SEND\"," +
            "\"wx-mp:redirect_page\":\"pages/a/b\"," +
            "\"wx-mp:weapp_state\":\"formal\"}");

        await PublishAsync(data, NewUser());

        // The data restored from the library must be actually sent out
        Messager.SentTemplates.Count.ShouldBe(1);

        var sent = Messager.SentTemplates[0];
        sent.TemplateId.ShouldBe("TPL_DB_SEND");
        sent.Page.ShouldBe("pages/a/b");
        sent.State.ShouldBe("formal");
    }

    // ---------- 3. No prefix key left over from history ----------

    /// <remarks>
    /// <para>
    /// The historical key is not within the matching range of the channel data parser, but is read back by the channel Provider during parsing.
    /// Therefore, the real release path must be verified here.
    /// </para>
    /// <para>
    /// The key name is written in <b>JSON literal</b> (rather than referencing the deprecated
    /// <c>WeChatMiniProgramNotificationDataKeys.Legacy</c>):
    /// It not only avoids referencing deprecated members, but also is closer to the fact that "these strings are stored in the database".
    /// </para>
    /// </remarks>
    [Fact]
    public async Task Should_Restore_Legacy_Keys_From_Db_Json()
    {
        Messager.Clear();

        // Unprefixed keys written by earlier versions, installed producers may still be writing
        var data = RestoreFromDbJson(
            "{\"TemplateId\":\"TPL_LEGACY\",\"RedirectPage\":\"pages/legacy\"}");

        await PublishAsync(data, NewUser());

        Messager.SentTemplates.Count.ShouldBe(1);
        Messager.SentTemplates[0].TemplateId.ShouldBe("TPL_LEGACY");
        Messager.SentTemplates[0].Page.ShouldBe("pages/legacy");
    }

    [Fact]
    public async Task Should_Prefer_Prefixed_Key_Over_Legacy_Key_From_Db_Json()
    {
        Messager.Clear();

        var data = RestoreFromDbJson(
            "{\"TemplateId\":\"TPL_LEGACY\",\"wx-mp:template_id\":\"TPL_NEW\"}");

        // The channel data parser only recognizes keys with prefixes
        ChannelDataResolver.Resolve<WeChatMiniProgramSubscribeChannelData>(data)!
            .TemplateId.ShouldBe("TPL_NEW");

        // Keys with prefixes must take precedence when publishing.
        await PublishAsync(data, NewUser());

        Messager.SentTemplates[0].TemplateId.ShouldBe("TPL_NEW");
    }

    // ---------- 4. No misparsing when there is no channel data ----------

    [Fact]
    public void Should_Resolve_Nothing_When_Db_Json_Has_No_Channel_Data()
    {
        var data = RestoreFromDbJson(
            "{\"title\":\"Order status\",\"message\":\"Your order has been shipped\"}");

        ChannelDataResolver.Resolve(Provider, data).ShouldBeEmpty();
    }

    // ---------- Auxiliary ----------

    /// <summary>
    /// Restore notification data after simulating "retrieving JSON string from database"
    /// </summary>
    /// <remarks>
    /// Consistent with EF's property bag value converter: the property bag as a whole is a flat JSON object.
    /// </remarks>
    private NotificationData RestoreFromDbJson(string dbJson)
    {
        var extraProperties = JsonSerializer.Deserialize<ExtraPropertyDictionary>(dbJson);

        return new NotificationData { ExtraProperties = extraProperties };
    }

    private static UserIdentifier NewUser() => new(Guid.NewGuid(), "tester");

    private async Task PublishAsync(NotificationData data, UserIdentifier user)
    {
        var provider = GetRequiredService<WeChatMiniProgramNotificationPublishProvider>();

        var notification = new NotificationInfo
        {
            Name = "Test",
            ContentType = NotificationContentType.Text,
            Data = data
        };
        notification.SetId(1);

        await provider.PublishAsync(new NotificationPublishContext(notification, new[] { user }));
    }
}
