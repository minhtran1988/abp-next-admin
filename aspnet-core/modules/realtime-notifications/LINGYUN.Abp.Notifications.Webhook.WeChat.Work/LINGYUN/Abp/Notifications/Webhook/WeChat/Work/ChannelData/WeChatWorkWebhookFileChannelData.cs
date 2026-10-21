using LINGYUN.Abp.Notifications;
using LINGYUN.Abp.WeChat.Work.Messages.Models;

namespace LINGYUN.Abp.Notifications.Webhook.WeChat.Work;

/// <summary>
/// File message payload
/// </summary>
[NotificationChannelData(WeChatWorkWebhookNotificationProvider.ProviderName, Priority = 600)]
public class WeChatWorkWebhookFileChannelData : WeChatWorkWebhookChannelData
{
    public override WeChatWorkWebhookMessageKind Kind => WeChatWorkWebhookMessageKind.File;

    /// <summary>
    /// File message body (material ID)
    /// </summary>
    [NotificationChannelProperty(WeChatWorkWebhookNotificationDataKeys.File)]
    public virtual WebhookFileMessage? File { get; set; }
}
