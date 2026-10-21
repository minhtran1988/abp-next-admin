using LINGYUN.Abp.WeChat.Work.Messages;

namespace LINGYUN.Abp.Notifications.Webhook.WeChat.Work;

/// <summary>
/// Enterprise WeChat Webhook message construction context
/// </summary>
public class WeChatWorkWebhookMessageFactoryContext
{
    /// <summary>
    /// Notification message
    /// </summary>
    public NotificationInfo Notification { get; }

    /// <summary>
    /// Notify standard data (localization and template rendering completed)
    /// </summary>
    public NotificationStandardData StandardData { get; }

    /// <summary>
    /// Channel data (load)
    /// </summary>
    public WeChatWorkWebhookChannelData ChannelData { get; }

    /// <summary>
    /// Channel options
    /// </summary>
    public AbpNotificationsWebhookWeChatWorkOptions Options { get; }

    public WeChatWorkWebhookMessageFactoryContext(
        NotificationInfo notification,
        NotificationStandardData standardData,
        WeChatWorkWebhookChannelData channelData,
        AbpNotificationsWebhookWeChatWorkOptions options)
    {
        Notification = notification;
        StandardData = standardData;
        ChannelData = channelData;
        Options = options;
    }
}
