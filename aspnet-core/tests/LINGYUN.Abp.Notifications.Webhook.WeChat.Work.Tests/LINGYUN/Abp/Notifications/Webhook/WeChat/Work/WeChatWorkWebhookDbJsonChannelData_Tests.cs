using LINGYUN.Abp.WeChat.Work.Messages;
using LINGYUN.Abp.WeChat.Work.Messages.Models;
using Microsoft.Extensions.Options;
using Shouldly;
using System;
using System.Collections.Generic;
using Volo.Abp.Data;
using Xunit;

namespace LINGYUN.Abp.Notifications.Webhook.WeChat.Work;

/// <summary>
/// Parse channel data test from database JSON string
/// </summary>
/// <remarks>
/// <para>
/// The previous tests are all the same process path of "write in memory → read", and the persistence round trip cannot be verified.
/// This group of tests adds **real link**: directly gives the JSON string stored in the database,
/// After restoring to <see cref="NotificationData"/>, use channel side parsing and message factory.
/// Answer "Can the data be parsed into the channel message format normally after being retrieved from the database?"
/// </para>
/// <para>
/// Key point: The value in the property bag is a **JSON string** (not a nested object) after persistence,
/// And the discriminator <c>card_type</c> of the template card must be retained, otherwise the abstract type cannot be restored.
/// </para>
/// </remarks>
public class WeChatWorkWebhookDbJsonChannelData_Tests
    : AbpNotificationsWebhookWeChatWorkTestsBase
{
    private const string Provider = WeChatWorkWebhookNotificationProvider.ProviderName;

    private AbpNotificationsWebhookWeChatWorkMessageOptions MessageOptions
        => GetRequiredService<IOptions<AbpNotificationsWebhookWeChatWorkMessageOptions>>().Value;

    private AbpNotificationsWebhookWeChatWorkOptions ChannelOptions
        => GetRequiredService<IOptions<AbpNotificationsWebhookWeChatWorkOptions>>().Value;

    // ---------- 1. Template card (abstract type + discriminator) ----------

    /// <summary>
    /// Template card JSON in the database (escaped form, consistent with EF storage)
    /// </summary>
    private const string TemplateCardDbJson =
        "{" +
        "\"wx-work-hook:template_card\":\"{\\\"sub_title_text\\\":\\\"Please check the log\\\"," +
        "\\\"card_type\\\":\\\"text_notice\\\"," +
        "\\\"main_title\\\":{\\\"title\\\":\\\"Build failed\\\",\\\"desc\\\":\\\"Build #1234 failed\\\"}," +
        "\\\"horizontal_content_list\\\":[{\\\"keyname\\\":\\\"branch\\\",\\\"value\\\":\\\"main\\\"}]," +
        "\\\"card_action\\\":{\\\"type\\\":1,\\\"url\\\":\\\"https://example.com/build/1\\\"}}\"" +
        "}";

    [Fact]
    public void Should_Restore_TemplateCard_From_Db_Json()
    {
        var data = RestoreFromDbJson(TemplateCardDbJson);

        var payload = ChannelDataResolver.Resolve<WeChatWorkWebhookTemplateCardChannelData>(data);

        payload.ShouldNotBeNull();
        payload!.TemplateCard.ShouldNotBeNull();

        // Abstract type is restored to concrete type according to card_type
        var card = payload.TemplateCard.ShouldBeOfType<WebhookTextNoticeCardMessage>();
        card.CardType.ShouldBe("text_notice");
        card.MainTitle!.Title.ShouldBe("Build failed");
        card.MainTitle!.Description.ShouldBe("Build #1234 failed");
        card.SubTitleText.ShouldBe("Please check the log");
        card.Action!.Url.ShouldBe("https://example.com/build/1");

        // Nested lists must also be restored completely
        card.HorizontalContents.ShouldNotBeNull();
        card.HorizontalContents!.Count.ShouldBe(1);
        card.HorizontalContents[0].KeyName.ShouldBe("branch");
        card.HorizontalContents[0].Value.ShouldBe("main");
    }

    [Fact]
    public void Should_Build_TemplateCard_Message_From_Db_Json()
    {
        var data = RestoreFromDbJson(TemplateCardDbJson);

        var message = BuildMessage(Provider, data);

        message.MsgType.ShouldBe("template_card");

        var card = message.ShouldBeOfType<WeChatWorkWebhookTemplateCardMessage>();
        card.TemplateCard.ShouldBeOfType<WebhookTextNoticeCardMessage>()
            .MainTitle!.Title.ShouldBe("Build failed");
    }

    // ---------- 2. Scalar load ----------

    private const string TextDbJson =
        "{\"wx-work-hook:text\":\"Build #1234 failed\"}";

    [Fact]
    public void Should_Restore_Text_From_Db_Json()
    {
        var data = RestoreFromDbJson(TextDbJson);

        var payload = ChannelDataResolver.Resolve<WeChatWorkWebhookTextChannelData>(data);

        payload.ShouldNotBeNull();
        payload!.Content.ShouldBe("Build #1234 failed");

        BuildMessage(Provider, data)
            .ShouldBeOfType<WeChatWorkWebhookTextMessage>()
            .Text.Content.ShouldBe("Build #1234 failed");
    }

    // ---------- 3. Basic parameters and channel data coexist ----------

    [Fact]
    public void Should_Restore_Standard_Data_Alongside_Channel_Data()
    {
        // The real library has both basic notification parameters and channel data.
        var data = RestoreFromDbJson(
            "{\"title\":\"Build failed\",\"message\":\"Build #1234 failed\",\"formUser\":\"ci\"," +
            "\"wx-work-hook:text\":\"fallback text\"}");

        data.TryGetData("title").ShouldBe("Build failed");
        data.TryGetData("formUser").ShouldBe("ci");

        var payload = ChannelDataResolver.Resolve<WeChatWorkWebhookTextChannelData>(data);
        payload.ShouldNotBeNull();
        payload!.Content.ShouldBe("fallback text");
    }

    // ---------- 4. No misparsing when there is no channel data ----------

    [Fact]
    public void Should_Resolve_Nothing_When_Db_Json_Has_No_Channel_Data()
    {
        var data = RestoreFromDbJson(
            "{\"title\":\"Build failed\",\"message\":\"Build #1234 failed\"}");

        ChannelDataResolver.Resolve(Provider, data).ShouldBeEmpty();
    }

    // ---------- 5. Behavior when the payload is illegal ----------

    [Fact]
    public void Should_Report_Declared_But_Resolve_Nothing_When_Db_Json_Payload_Is_Invalid()
    {
        // The template card JSON lacks the discriminator card_type, and the abstract type cannot be restored.
        var data = RestoreFromDbJson(
            "{\"wx-work-hook:template_card\":\"{\\\"main_title\\\":{\\\"title\\\":\\\"t\\\"}}\"}");

        // Contract: The producer did write the key → IsDeclared is true,
        // But the parsing failed → Resolve returns empty (only logs), no exception is thrown
        ChannelDataResolver.IsDeclared(data, typeof(WeChatWorkWebhookTemplateCardChannelData)).ShouldBeTrue();
        ChannelDataResolver.Resolve(Provider, data).ShouldBeEmpty();

        // The diagnostic information can still give the actual hit key, which is convenient for troubleshooting.
        ChannelDataResolver.GetKey(data, typeof(WeChatWorkWebhookTemplateCardChannelData))
            .ShouldBe(WeChatWorkWebhookNotificationDataKeys.TemplateCard);
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

    private WeChatWorkWebhookMessage BuildMessage(string provider, NotificationData data)
    {
        var payload = ChannelDataResolver.Resolve(provider, data);
        payload.ShouldNotBeEmpty();

        return MessageOptions.Create(new WeChatWorkWebhookMessageFactoryContext(
            new NotificationInfo { Name = "Test", ContentType = NotificationContentType.Text },
            new NotificationStandardData("Title", "Content"),
            (WeChatWorkWebhookChannelData)payload[0],
            ChannelOptions)).Message;
    }
}
