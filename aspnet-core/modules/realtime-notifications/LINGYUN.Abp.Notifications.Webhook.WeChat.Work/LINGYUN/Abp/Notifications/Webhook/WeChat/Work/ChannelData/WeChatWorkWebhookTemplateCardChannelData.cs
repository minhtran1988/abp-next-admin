using LINGYUN.Abp.Notifications;
using LINGYUN.Abp.WeChat.Work.Messages.Models;

namespace LINGYUN.Abp.Notifications.Webhook.WeChat.Work;

/// <summary>
/// Template card message payload
/// </summary>
/// <remarks>
/// Two concrete implementations of abstract type <see cref="WebhookTemplateCardMessage"/>
/// (<see cref="WebhookTextNoticeCardMessage"/> / <see cref="WebhookNewsNoticeCardMessage"/>)
/// Both require JSON polymorphic converters to round-trip notification data, registered by the channel module.
/// </remarks>
[NotificationChannelData(WeChatWorkWebhookNotificationProvider.ProviderName, Priority = 800)]
public class WeChatWorkWebhookTemplateCardChannelData : WeChatWorkWebhookChannelData
{
    public override WeChatWorkWebhookMessageKind Kind => WeChatWorkWebhookMessageKind.TemplateCard;

    /// <summary>
    /// Template card message body
    /// </summary>
    [NotificationChannelProperty(WeChatWorkWebhookNotificationDataKeys.TemplateCard)]
    public virtual WebhookTemplateCardMessage? TemplateCard { get; set; }
}
