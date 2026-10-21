using Volo.Abp.Modularity;

namespace LINGYUN.Abp.Notifications.Webhook.WeChat.Work;

/// <summary>
/// Enterprise WeChat Webhook notification channel name
/// </summary>
/// <remarks>
/// <para>
/// The **publish** of Webhook still goes through <c>WebhookNotificationPublishProvider</c> (channel name <c>Webhook</c>),
/// This constant is only used as a **payload registry key**:
/// Enterprise WeChat application messages (channel name <c>WeChat.Work</c>) and Webhook are two transmission methods.
/// There are two sets of carrier models, so different registry keys must be used to avoid misparsing the payload by the other party.
/// </para>
/// <para>
/// Producer side: The notification definition will only be published if <c>ProviderName</c> is still declared as <c>"Webhook"</c>.
/// The payload is written using the data key of this channel (prefix <c>wx-work-hook:</c>).
/// </para>
/// </remarks>
public static class WeChatWorkWebhookNotificationProvider
{
    /// <summary>
    /// Payload registry key for this channel
    /// </summary>
    public const string ProviderName = "WeChat.Work.Webhook";
}
