using LINGYUN.Abp.Notifications;

namespace LINGYUN.Abp.Notifications.Webhook.WeChat.Work;

/// <summary>
/// markdown_v2 message payload
/// </summary>
/// <remarks>
/// markdown_v2 supports richer syntax such as tables, code blocks, etc.
/// For details, see https://developer.work.weixin.qq.com/document/path/99110#markdown-v2%E7%B1%BB%E5%9E%8B
/// </remarks>
[NotificationChannelData(WeChatWorkWebhookNotificationProvider.ProviderName, Priority = 300)]
public class WeChatWorkWebhookMarkdownV2ChannelData : WeChatWorkWebhookChannelData
{
    public override WeChatWorkWebhookMessageKind Kind => WeChatWorkWebhookMessageKind.MarkdownV2;

    /// <summary>
    /// markdown_v2 content
    /// </summary>
    [NotificationChannelProperty(WeChatWorkWebhookNotificationDataKeys.MarkdownV2)]
    public virtual string? Content { get; set; }
}
