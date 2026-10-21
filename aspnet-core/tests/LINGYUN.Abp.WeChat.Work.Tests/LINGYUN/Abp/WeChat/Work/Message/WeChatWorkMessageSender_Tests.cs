using LINGYUN.Abp.WeChat.Work.Messages;
using LINGYUN.Abp.WeChat.Work.Messages.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LINGYUN.Abp.WeChat.Work.Message;
public class WeChatWorkMessageSender_Tests : AbpWeChatWorkTestBase
{
    protected IWeChatWorkMessageSender Sender { get; }
    public WeChatWorkMessageSender_Tests()
    {
        Sender = GetRequiredService<IWeChatWorkMessageSender>();
    }

    [Theory]
    [InlineData("1000002")]
    public async Task Send_Text_Message_Test(string agentId)
    {
        var text = new TextMessage("Your express delivery has arrived, please bring your work card to the mail center to pick it up.\nBefore departure, you can view <a href=\"http://work.weixin.qq.com\">Mail Center video live broadcast</a> to avoid queues smartly.");
        var message = new WeChatWorkTextMessage(agentId, text)
        {
            ToUser = "@all"
        };

        var response = await Sender.SendAsync(message);
        response.IsSuccessed.ShouldBeTrue();
    }

    [Theory]
    [InlineData("1000002", "wr0vTBJwAAFY2xtHp76qFMRDcVXu42qw")]
    public async Task Send_AppChat_Text_Message_Test(string agentId, string chatId)
    {
        var text = new TextMessage("Your express delivery has arrived, please bring your work card to the mail center to pick it up.\nBefore departure, you can view <a href=\"http://work.weixin.qq.com\">Mail Center video live broadcast</a> to avoid queues smartly.");
        var message = new WeChatWorkAppChatTextMessage(agentId, chatId, text);

        var response = await Sender.SendAsync(message);
        response.IsSuccessed.ShouldBeTrue();
    }

    [Theory]
    [InlineData("1000002")]
    public async Task Send_Markdown_Message_Test(string agentId)
    {
        var markdown = new MarkdownMessage("Your meeting room has been reserved and will be synchronized to `mailbox` later \n>**Item details** \n>Item: <font color=\"info\">Meeting</font> \n>Organizer: @miglioguan \n>Participants: @miglioguan, @kunliu, @jamdeezhou, @kanexiong, @kisonwang \n> \n>Conference room: <font color=\"info\">Room 301, 1st Floor, Guangzhou TIT</font> \n>Date: <font color=\"warning\">May 18, 2018</font> \n>Time: <font color=\"comment\">9:00-11:00 am</font> \n> \n>Please attend the meeting on time. \n>If you need to modify the meeting information, please click: [Modify Meeting Information](https://work.weixin.qq.com)");
        var message = new WeChatWorkMarkdownMessage(agentId, markdown)
        {
            ToUser = "@all"
        };

        var response = await Sender.SendAsync(message);
        response.IsSuccessed.ShouldBeTrue();
    }

    [Theory]
    [InlineData("1000002", "wr0vTBJwAAFY2xtHp76qFMRDcVXu42qw")]
    public async Task Send_AppChat_Markdown_Message_Test(string agentId, string chatId)
    {
        var markdown = new MarkdownMessage("Your meeting room has been reserved and will be synchronized to `mailbox` later \n>**Item details** \n>Item: <font color=\"info\">Meeting</font> \n>Organizer: @miglioguan \n>Participants: @miglioguan, @kunliu, @jamdeezhou, @kanexiong, @kisonwang \n> \n>Conference room: <font color=\"info\">Room 301, 1st Floor, Guangzhou TIT</font> \n>Date: <font color=\"warning\">May 18, 2018</font> \n>Time: <font color=\"comment\">9:00-11:00 am</font> \n> \n>Please attend the meeting on time. \n>If you need to modify the meeting information, please click: [Modify Meeting Information](https://work.weixin.qq.com)");
        var message = new WeChatWorkAppChatMarkdownMessage(agentId, chatId, markdown);

        var response = await Sender.SendAsync(message);
        response.IsSuccessed.ShouldBeTrue();
    }

    [Theory]
    [InlineData("1000002")]
    public async Task Send_Text_Template_Card_Message_Test(string agentId)
    {
        var cardAction = new TemplateCardCardAction("https://work.weixin.qq.com");
        var template = new TextTemplateCard(cardAction, Guid.NewGuid().ToString("D"))
        {
            Source = new TemplateCardSource("URL of the picture", "URL of the picture", DescriptionColor.Black),
            ActionMenu = new TemplateCardActionMenu(
                new List<TemplateCardAction>
                {
                    new TemplateCardAction("A","Accept Push"),
                    new TemplateCardAction("B","No more pushing"),
                },
                "Card Vice Interaction Auxiliary Text Description"),
            MainTitle = new TemplateCardMainTitle("Welcome to Enterprise WeChat", "Your friend is inviting you to join Enterprise WeChat"),
            QuoteArea = new TemplateCardQuoteArea("https://work.weixin.qq.com", "Quote style of Enterprise WeChat", "Enterprise WeChat is so easy to use, so easy to use"),
            EmphasisContent = new TemplateCardEmphasisContent("100", "Core Data"),
            SubTitle = "Download Enterprise WeChat and grab red envelopes!",
            HorizontalContents = new List<TemplateCardHorizontalContent>
            {
                TemplateCardHorizontalContent.None("Inviter","Zhang San"),
                TemplateCardHorizontalContent.FromUrl("Enterprise WeChat official website", "https://work.weixin.qq.com", "Click to visit"),
                // TemplateCardHorizontalContent.FromMedia("Enterprise WeChat Download", "File media_id", "Enterprise WeChat.apk"),
                // TemplateCardHorizontalContent.FromUser("Employee Information", "Click to View", "Zhang San"),
            },
            Jumps = new List<TemplateCardJump>
            {
                new TemplateCardJump("Enterprise WeChat official website","https://work.weixin.qq.com"),
                new TemplateCardJump("Jump applet","Jump applet","/index.html"),
            },
        };
        var message = new WeChatWorkTemplateCardMessage(agentId, template)
        {
            ToUser = "@all"
        };

        var response = await Sender.SendAsync(message);
        response.IsSuccessed.ShouldBeTrue();
    }

    [Theory]
    [InlineData("cee71c5b-6e6a-478a-aafe-fe612198ffbd")]
    public async Task Send_Webhook_Text_Template_Card_Message_Test(string webhookKey)
    {
        var message = new WeChatWorkWebhookTemplateCardMessage(
            new WebhookTextNoticeCardMessage(
                WebhookTemplateCardAction.Link("https://developer.work.weixin.qq.com/document/path/99110"),
                new WebhookTemplateCardMainTitle("Leave application approval notification", "Your leave application has been approved, please go and check it!"),
                source: WebhookTemplateCardSource.Black("https://wwcdn.weixin.qq.com/node/wework/images/wecom-logo.a61830413b.svg", "Enterprise WeChat"),
                horizontalContents: new List<WebhookTemplateCardHorizontalContent>
                {
                    WebhookTemplateCardHorizontalContent.Default("Approval Number", "QJ20251000000136"),
                    WebhookTemplateCardHorizontalContent.Default("Leave Date", "2025/10/01-2025/10/10"),
                    WebhookTemplateCardHorizontalContent.Default("Passed time", "2025-10-01 15:30:00"),
                    WebhookTemplateCardHorizontalContent.Default("Approval Notes", "Do a good job in attendance and handover matters"),
                },
                jumps: new List<WebhookTemplateCardJump>
                {
                    WebhookTemplateCardJump.Link("Go to OA to view", "https://developer.work.weixin.qq.com/document/path/99110")
                }));

        var response = await Sender.SendAsync(webhookKey, message);
        response.IsSuccessed.ShouldBeTrue();
    }
}
