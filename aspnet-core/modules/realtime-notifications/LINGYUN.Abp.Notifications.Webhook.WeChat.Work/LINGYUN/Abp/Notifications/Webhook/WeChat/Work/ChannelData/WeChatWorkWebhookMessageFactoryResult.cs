using LINGYUN.Abp.WeChat.Work.Messages;

namespace LINGYUN.Abp.Notifications.Webhook.WeChat.Work;

/// <summary>
/// Enterprise WeChat Webhook message construction result
/// </summary>
public class WeChatWorkWebhookMessageFactoryResult
{
    /// <summary>
    /// Message carrier
    /// </summary>
    public WeChatWorkWebhookMessage Message { get; }

    /// <summary>
    /// Message carrier type, used for logging and troubleshooting
    /// </summary>
    public WeChatWorkWebhookMessageKind Kind { get; }

    public WeChatWorkWebhookMessageFactoryResult(
        WeChatWorkWebhookMessage message,
        WeChatWorkWebhookMessageKind kind)
    {
        Message = message;
        Kind = kind;
    }

    public static WeChatWorkWebhookMessageFactoryResult Create(
        WeChatWorkWebhookMessage message,
        WeChatWorkWebhookMessageKind kind)
    {
        return new WeChatWorkWebhookMessageFactoryResult(message, kind);
    }
}
