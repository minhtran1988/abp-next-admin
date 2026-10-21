namespace LINGYUN.Abp.Notifications.Webhook.WeChat.Work;

/// <summary>
/// Enterprise WeChat Webhook channel data (load) base class
/// </summary>
/// <remarks>
/// Each payload corresponds to a Webhook message carrier, and the form is explicitly declared through <see cref="Kind"/>.
/// The channel side uses the factory dictionary to do O(1) dispatch.
/// </remarks>
public abstract class WeChatWorkWebhookChannelData : NotificationChannelData
{
    /// <summary>
    /// Message carrier type
    /// </summary>
    public abstract WeChatWorkWebhookMessageKind Kind { get; }
}
