using LINGYUN.Abp.Notifications;
using LINGYUN.Abp.WeChat.Work.Messages.Models;

namespace LINGYUN.Abp.Notifications.Webhook.WeChat.Work;

/// <summary>
/// Image message payload
/// </summary>
[NotificationChannelData(WeChatWorkWebhookNotificationProvider.ProviderName, Priority = 400)]
public class WeChatWorkWebhookImageChannelData : WeChatWorkWebhookChannelData
{
    public override WeChatWorkWebhookMessageKind Kind => WeChatWorkWebhookMessageKind.Image;

    /// <summary>
    /// Image content (base64 encoding, no more than 2MB)
    /// </summary>
    [NotificationChannelProperty(WeChatWorkWebhookNotificationDataKeys.Image)]
    public virtual WebhookImageMessage? Image { get; set; }
}
