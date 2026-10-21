using LINGYUN.Abp.Notifications;

namespace LINGYUN.Abp.Notifications.Webhook.WeChat.Work;

/// <summary>
/// markdown message payload
/// </summary>
[NotificationChannelData(WeChatWorkWebhookNotificationProvider.ProviderName, Priority = 200)]
public class WeChatWorkWebhookMarkdownChannelData : WeChatWorkWebhookChannelData
{
    public override WeChatWorkWebhookMessageKind Kind => WeChatWorkWebhookMessageKind.Markdown;

    /// <summary>
    /// markdown content
    /// </summary>
    [NotificationChannelProperty(WeChatWorkWebhookNotificationDataKeys.Markdown)]
    public virtual string? Content { get; set; }
}
