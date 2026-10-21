using LINGYUN.Abp.Notifications;

namespace LINGYUN.Abp.Notifications.Webhook.WeChat.Work;

/// <summary>
/// Text message payload
/// </summary>
[NotificationChannelData(WeChatWorkWebhookNotificationProvider.ProviderName, Priority = 100)]
public class WeChatWorkWebhookTextChannelData : WeChatWorkWebhookChannelData
{
    public override WeChatWorkWebhookMessageKind Kind => WeChatWorkWebhookMessageKind.Text;

    /// <summary>
    /// Text content
    /// </summary>
    [NotificationChannelProperty(WeChatWorkWebhookNotificationDataKeys.Text)]
    public virtual string? Content { get; set; }
}
