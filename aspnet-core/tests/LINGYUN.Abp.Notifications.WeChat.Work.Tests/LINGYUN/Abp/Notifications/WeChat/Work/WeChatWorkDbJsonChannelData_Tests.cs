using LINGYUN.Abp.WeChat.Work.Messages.Models;
using Shouldly;
using Volo.Abp.Data;
using Volo.Abp.Json;
using Xunit;

namespace LINGYUN.Abp.Notifications.WeChat.Work;

/// <summary>
/// Parse channel data test from database JSON string
/// </summary>
/// <remarks>
/// <para>
/// The previous test is the same process path of "write in memory → read", and the persistence round trip cannot be verified.
/// This group of tests adds **real link**: directly gives the JSON string stored in the database,
/// After restoring to <see cref="NotificationData"/>, use channel side parsing and message factory.
/// Answer "Can the data be parsed into the channel message format normally after being retrieved from the database?"
/// </para>
/// <para>
/// Key point: The complex values in the property bag are **JSON strings** (not nested objects),
/// And the discriminator <c>card_type</c> of the template card must be retained, otherwise the abstract type cannot be restored.
/// </para>
/// </remarks>
public class WeChatWorkDbJsonChannelData_Tests
    : AbpNotificationsWeChatWorkTestsBase
{
    private const string Provider = NotificationProviderNames.WechatWork;

    // ---------- 1. Template card (abstract type + discriminator) ----------

    /// <summary>
    /// Template card JSON in the database (escaped form, consistent with EF storage)
    /// </summary>
    private const string TemplateCardDbJson =
        "{\"wx-work:template_card_message\":\"{" +
        "\\\"main_title\\\":{\\\"title\\\":\\\"Leave application notification\\\",\\\"desc\\\":\\\"Your leave application has been approved\\\"}," +
        "\\\"sub_title_text\\\":\\\"Please handle the subsequent handover matters in a timely manner\\\"," +
        "\\\"horizontal_content_list\\\":[{\\\"keyname\\\":\\\"single number\\\",\\\"value\\\":\\\"LV20251001\\\"}]," +
        "\\\"TemplateCardCardAction\\\":{\\\"type\\\":1,\\\"url\\\":\\\"https://example.com/approval/123\\\"}," +
        "\\\"card_type\\\":\\\"template_card\\\"}\"}";

    [Fact]
    public void Should_Restore_TemplateCard_From_Db_Json()
    {
        var data = RestoreFromDbJson(TemplateCardDbJson);

        var payload = ChannelDataResolver.Resolve<WeChatWorkTemplateCardChannelData>(data);

        payload.ShouldNotBeNull();
        payload!.TemplateCard.ShouldNotBeNull();

        // The abstract base class must be reducible to a concrete card type
        var card = payload.TemplateCard.ShouldBeOfType<TextTemplateCard>();
        card.MainTitle!.Title.ShouldBe("Leave notification");
        card.MainTitle!.Description.ShouldBe("Your leave application has been approved");
        card.SubTitle.ShouldBe("Please handle subsequent handover matters in a timely manner");
        card.CardAction.Url.ShouldBe("https://example.com/approval/123");

        // Nested lists must also be restored completely
        card.HorizontalContents.ShouldNotBeNull();
        card.HorizontalContents!.Count.ShouldBe(1);
        card.HorizontalContents[0].KeyName.ShouldBe("Order Number");
        card.HorizontalContents[0].Value.ShouldBe("LV20251001");
    }

    // ---------- 2. Text card (basic type) ----------

    [Fact]
    public void Should_Restore_TextCard_From_Db_Json()
    {
        var data = RestoreFromDbJson(
            "{\"wx-work:text_card_message\":\"{" +
            "\\\"title\\\":\\\"title\\\",\\\"description\\\":\\\"description\\\"," +
            "\\\"url\\\":\\\"https://example.com\\\",\\\"btntxt\\\":\\\"Details\\\"}\"}");

        var payload = ChannelDataResolver.Resolve<WeChatWorkTextCardChannelData>(data);

        payload.ShouldNotBeNull();
        payload!.TextCard.ShouldNotBeNull();
        payload.TextCard!.Title.ShouldBe("title");
        payload.TextCard!.Url.ShouldBe("https://example.com");
    }

    // ---------- 3. Delivery options and channel data coexist ----------

    [Fact]
    public void Should_Restore_Delivery_Options_Alongside_Channel_Data()
    {
        // The real library has both channel data and delivery options (safe / repeat check interval, etc.)
        var data = RestoreFromDbJson(
            "{\"wx-work:safe\":true,\"wx-work:duplicate_check_interval\":600," +
            "\"wx-work:text_card_message\":\"{" +
            "\\\"title\\\":\\\"title\\\",\\\"description\\\":\\\"description\\\",\\\"url\\\":\\\"https://example.com\\\"}\"}");

        ChannelDataResolver.Resolve<WeChatWorkTextCardChannelData>(data).ShouldNotBeNull();

        // The delivery option is the original type and needs to be restored correctly according to the declared type.
        ChannelDataResolver.GetValue(data, WeChatWorkNotificationDataKeys.Safe, false).ShouldBeTrue();
        ChannelDataResolver.GetValue(data, WeChatWorkNotificationDataKeys.DuplicateCheckInterval, 0).ShouldBe(600);
    }

    // ---------- 4. No misparsing when there is no channel data ----------

    [Fact]
    public void Should_Resolve_Nothing_When_Db_Json_Has_No_Channel_Data()
    {
        var data = RestoreFromDbJson(
            "{\"title\":\"title\",\"message\":\"content\",\"formUser\":\"ci\"}");

        ChannelDataResolver.Resolve(Provider, data).ShouldBeEmpty();
    }

    // ---------- 5. Behavior when the payload is illegal ----------

    [Fact]
    public void Should_Report_Declared_But_Resolve_Nothing_When_Db_Json_Payload_Is_Invalid()
    {
        // Complex channel data is stored in JSON strings, and content that cannot be deserialized is written here.
        var data = RestoreFromDbJson(
            "{\"wx-work:template_card_message\":\"{ this is not json }\"}");

        // Contract: The key does exist → IsDeclared is true; parsing fails → Resolve returns empty and does not throw an exception
        ChannelDataResolver.IsDeclared(data, typeof(WeChatWorkTemplateCardChannelData)).ShouldBeTrue();
        ChannelDataResolver.Resolve(Provider, data).ShouldBeEmpty();

        // The diagnostic information can still give the actual hit key, which is convenient for troubleshooting.
        ChannelDataResolver.GetKey(data, typeof(WeChatWorkTemplateCardChannelData))
            .ShouldBe(WeChatWorkNotificationDataKeys.TemplateCardMessage);
    }

    // ---------- Auxiliary ----------

    /// <summary>
    /// Restore notification data after simulating "retrieving JSON string from database"
    /// </summary>
    /// <remarks>
    /// Consistent with EF's property bag value converter: the property bag as a whole is a flat JSON object,
    /// Complex channel data is stored in <b>Escaped JSON string</b>.
    /// </remarks>
    private NotificationData RestoreFromDbJson(string dbJson)
    {
        var extraProperties = JsonSerializer.Deserialize<ExtraPropertyDictionary>(dbJson);

        return new NotificationData { ExtraProperties = extraProperties };
    }
}
