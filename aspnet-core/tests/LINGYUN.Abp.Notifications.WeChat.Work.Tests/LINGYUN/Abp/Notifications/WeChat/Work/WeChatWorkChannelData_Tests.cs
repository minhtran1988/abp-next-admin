using LINGYUN.Abp.WeChat.Work.Messages.Models;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace LINGYUN.Abp.Notifications.WeChat.Work;

/// <summary>
/// Enterprise WeChat application message channel data test
/// </summary>
/// <remarks>
/// Unified writing method: first new message object → serialize and write notification data through <c>IJsonSerializer</c> →
/// Then use the channel side parser to verify the restoration result.
/// </remarks>
public class WeChatWorkChannelData_Tests : AbpNotificationsWeChatWorkTestsBase
{
    // ---------- Text card ----------

    [Fact]
    public void Should_Resolve_TextCard_Message()
    {
        var message = new TextCardMessage(
            title: "Approval Passed",
            description: "Your leave application has been approved",
            url: "https://example.com/leave/123",
            buttonText: "Go and check");

        var payload = ChannelDataResolver.Resolve<WeChatWorkTextCardChannelData>(
            WriteChannelData(new WeChatWorkTextCardChannelData { TextCard = message }));

        payload.ShouldNotBeNull();
        payload!.TextCard.ShouldNotBeNull();

        // The message restored on the channel side must be consistent field by field with the object produced by the producer new
        payload.TextCard!.Title.ShouldBe(message.Title);
        payload.TextCard.Description.ShouldBe(message.Description);
        payload.TextCard.Url.ShouldBe(message.Url);
        payload.TextCard.ButtonText.ShouldBe(message.ButtonText);
    }

    [Fact]
    public void Should_Serialize_TextCard_By_Json_Contract()
    {
        var json = Serialize(new TextCardMessage("Title", "Description", "https://example.com"));

        // Serialization contract: The field name must be the snake_case required by Enterprise WeChat
        json.ShouldContain("\"title\"");
        json.ShouldContain("\"description\"");
        json.ShouldContain("\"url\"");
    }

    // ---------- Mini program notification ----------

    [Fact]
    public void Should_Resolve_MiniProgram_Message()
    {
        var message = new MiniProgramMessage(
            appId: "wx1234567890",
            title: "Order has been shipped",
            page: "pages/order/detail?id=123",
            description: "Click to view logistics details")
        {
            EmphasisFirstItem = true,
            ContentItems = new List<MiniProgramContent>
            {
                new MiniProgramContent("Order Number", "SO20251001001")
            }
        };

        var payload = ChannelDataResolver.Resolve<WeChatWorkMiniProgramNoticeChannelData>(
            WriteChannelData(new WeChatWorkMiniProgramNoticeChannelData { MiniProgram = message }));

        payload.ShouldNotBeNull();
        payload!.MiniProgram.ShouldNotBeNull();
        payload.MiniProgram!.AppId.ShouldBe("wx1234567890");
        payload.MiniProgram.Title.ShouldBe("Order has been shipped");
        payload.MiniProgram.Page.ShouldBe("pages/order/detail?id=123");
        payload.MiniProgram.EmphasisFirstItem.ShouldBe(true);
        payload.MiniProgram.ContentItems.ShouldNotBeNull();
        payload.MiniProgram.ContentItems!.Count.ShouldBe(1);
        payload.MiniProgram.ContentItems[0].Key.ShouldBe("Order Number");
        payload.MiniProgram.ContentItems[0].Value.ShouldBe("SO20251001001");
    }

    // ---------- Graphic message ----------

    [Fact]
    public void Should_Resolve_MpNew_Message()
    {
        var message = new MpNewMessagePayload(new List<MpNewMessage>
        {
            new MpNewMessage(
                title: "Monthly Report",
                thumbMediaId: "MEDIA_ID",
                content: "<p>Report text</p>",
                author: "Operations Department",
                contentSourceUrl: "https://example.com/report",
                description: "Monthly Business Report for October 2025")
        });

        var payload = ChannelDataResolver.Resolve<WeChatWorkMpNewChannelData>(
            WriteChannelData(new WeChatWorkMpNewChannelData { MpNewMessage = message }));

        payload.ShouldNotBeNull();
        payload!.MpNewMessage.ShouldNotBeNull();
        payload.MpNewMessage!.Articles.Count.ShouldBe(1);

        var article = payload.MpNewMessage.Articles[0];
        article.Title.ShouldBe("Monthly Report");
        article.ThumbMediaId.ShouldBe("MEDIA_ID");
        article.Content.ShouldBe("<p>Report text</p>");
        article.Author.ShouldBe("Operations Department");
        article.ContentSourceUrl.ShouldBe("https://example.com/report");
        article.Description.ShouldBe("Monthly Business Report for October 2025");
    }

    // ---------- Video / Voice / File ----------

    [Fact]
    public void Should_Resolve_Video_Message()
    {
        var message = new VideoMessage("MEDIA_ID", "Training Video", "Newcomer Induction Training");

        var payload = ChannelDataResolver.Resolve<WeChatWorkVideoChannelData>(
            WriteChannelData(new WeChatWorkVideoChannelData { Video = message }));

        payload.ShouldNotBeNull();
        payload!.Video.ShouldNotBeNull();
        payload.Video!.MediaId.ShouldBe("MEDIA_ID");
        payload.Video.Title.ShouldBe("Training Video");
        payload.Video.Description.ShouldBe("Newcomer Induction Training");
    }

    [Fact]
    public void Should_Resolve_Voice_Message()
    {
        var message = new MediaMessage("VOICE_MEDIA_ID");

        var payload = ChannelDataResolver.Resolve<WeChatWorkVoiceChannelData>(
            WriteChannelData(new WeChatWorkVoiceChannelData { Voice = message }));

        payload.ShouldNotBeNull();
        payload!.Voice.ShouldNotBeNull();
        payload.Voice!.MediaId.ShouldBe("VOICE_MEDIA_ID");
    }

    [Fact]
    public void Should_Resolve_File_Message()
    {
        var message = new MediaMessage("FILE_MEDIA_ID");

        var payload = ChannelDataResolver.Resolve<WeChatWorkFileChannelData>(
            WriteChannelData(new WeChatWorkFileChannelData { File = message }));

        payload.ShouldNotBeNull();
        payload!.File.ShouldNotBeNull();
        payload.File!.MediaId.ShouldBe("FILE_MEDIA_ID");
    }

    // ---------- Template card (abstract type polymorphism) ----------

    [Fact]
    public void Should_Resolve_TemplateCard_Message()
    {
        var message = new TextTemplateCard(
            new TemplateCardCardAction("https://example.com/approval/123"))
        {
            MainTitle = new TemplateCardMainTitle("Leave application notification", "Your leave application has been approved"),
            SubTitle = "Please handle follow-up handover matters in a timely manner"
        };

        var payload = ChannelDataResolver.Resolve<WeChatWorkTemplateCardChannelData>(
            WriteChannelData(new WeChatWorkTemplateCardChannelData { TemplateCard = message }));

        payload.ShouldNotBeNull();
        payload!.TemplateCard.ShouldNotBeNull();

        // The abstract base class must be reducible to a concrete card type
        payload.TemplateCard.ShouldBeOfType<TextTemplateCard>();

        var card = (TextTemplateCard)payload.TemplateCard!;
        card.MainTitle!.Title.ShouldBe("Leave application notification");
        card.SubTitle.ShouldBe("Please handle follow-up handover matters in a timely manner");
        card.CardAction.Url.ShouldBe("https://example.com/approval/123");
    }

    // ---------- Delivery options and payloads coexist ----------

    [Fact]
    public void Should_Resolve_Payload_Along_With_Delivery_Options()
    {
        var data = WriteChannelData(new WeChatWorkTextCardChannelData
        {
            TextCard = new TextCardMessage("Title", "Description", "https://example.com")
        });

        data.TrySetData(WeChatWorkNotificationDataKeys.Safe, true);
        data.TrySetData(WeChatWorkNotificationDataKeys.ToParty, "1|2");
        data.TrySetData(WeChatWorkNotificationDataKeys.DuplicateCheckInterval, 600);

        // payload
        ChannelDataResolver.Resolve<WeChatWorkTextCardChannelData>(data).ShouldNotBeNull();

        // scalar options
        ChannelDataResolver.GetValue(data, WeChatWorkNotificationDataKeys.Safe, false).ShouldBeTrue();
        ChannelDataResolver.GetValue(data, WeChatWorkNotificationDataKeys.DuplicateCheckInterval, 1800)
            .ShouldBe(600);
        ChannelDataResolver.GetOrNull(data, WeChatWorkNotificationDataKeys.ToParty).ShouldBe("1|2");
    }

    [Fact]
    public void Should_Require_Only_Its_Own_Key_To_Be_Declared()
    {
        // Only write one payload key and do not hit other payload types by mistake
        var data = WriteChannelData(new WeChatWorkVideoChannelData
        {
            Video = new VideoMessage("MEDIA_ID")
        });

        ChannelDataResolver.IsDeclared(data, typeof(WeChatWorkVideoChannelData)).ShouldBeTrue();
        ChannelDataResolver.IsDeclared(data, typeof(WeChatWorkTextCardChannelData)).ShouldBeFalse();
        ChannelDataResolver.IsDeclared(data, typeof(WeChatWorkFileChannelData)).ShouldBeFalse();
        ChannelDataResolver.IsDeclared(data, typeof(WeChatWorkTemplateCardChannelData)).ShouldBeFalse();
    }

    // ---------- Multiple payloads coexist: Sort by Priority ----------

    [Fact]
    public void Should_Resolve_All_Payloads_In_Priority_Order()
    {
        var data = WriteChannelData(new WeChatWorkTextCardChannelData
        {
            TextCard = new TextCardMessage("Title", "Description", "https://example.com")
        });
        // Merge another payload (different keys, no conflict with each other)
        MergeChannelData(data, new WeChatWorkMiniProgramNoticeChannelData
        {
            MiniProgram = new MiniProgramMessage("appid", "Title")
        });

        var payloads = ChannelDataResolver.Resolve(
            WeChatWorkNotificationPublishProvider.ProviderName,
            data);

        payloads.Count.ShouldBe(2);
        // Priority: Mini Program Notification 100 < Text Card 400
        payloads[0].ShouldBeOfType<WeChatWorkMiniProgramNoticeChannelData>();
        payloads[1].ShouldBeOfType<WeChatWorkTextCardChannelData>();
    }

    [Fact]
    public void Should_Declare_Every_Registered_Payload_Kind()
    {
        var types = ChannelDataResolver.GetPayloadTypes(WeChatWorkNotificationPublishProvider.ProviderName);

        types.Count.ShouldBe(7);
        types.ShouldContain(typeof(WeChatWorkMiniProgramNoticeChannelData));
        types.ShouldContain(typeof(WeChatWorkMpNewChannelData));
        types.ShouldContain(typeof(WeChatWorkTemplateCardChannelData));
        types.ShouldContain(typeof(WeChatWorkTextCardChannelData));
        types.ShouldContain(typeof(WeChatWorkVideoChannelData));
        types.ShouldContain(typeof(WeChatWorkVoiceChannelData));
        types.ShouldContain(typeof(WeChatWorkFileChannelData));
    }
}
