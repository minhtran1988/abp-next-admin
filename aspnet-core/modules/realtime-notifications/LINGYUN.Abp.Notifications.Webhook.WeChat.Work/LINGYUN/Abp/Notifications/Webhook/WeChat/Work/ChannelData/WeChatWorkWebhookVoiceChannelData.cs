using LINGYUN.Abp.Notifications;
using LINGYUN.Abp.WeChat.Work.Messages.Models;

namespace LINGYUN.Abp.Notifications.Webhook.WeChat.Work;

/// <summary>
/// Voice message payload
/// </summary>
[NotificationChannelData(WeChatWorkWebhookNotificationProvider.ProviderName, Priority = 700)]
public class WeChatWorkWebhookVoiceChannelData : WeChatWorkWebhookChannelData
{
    public override WeChatWorkWebhookMessageKind Kind => WeChatWorkWebhookMessageKind.Voice;

    /// <summary>
    /// Voice message body (material ID)
    /// </summary>
    [NotificationChannelProperty(WeChatWorkWebhookNotificationDataKeys.Voice)]
    public virtual WebhookVoiceMessage? Voice { get; set; }
}
