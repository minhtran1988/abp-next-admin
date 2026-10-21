using LINGYUN.Abp.WeChat.Work.Messages;

namespace LINGYUN.Abp.Notifications.WeChat.Work;

/// <summary>
/// Enterprise WeChat message construction context
/// </summary>
public class WeChatWorkMessageFactoryContext
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
    public WeChatWorkChannelData ChannelData { get; }

    /// <summary>
    /// Message receiver
    /// </summary>
    public WeChatWorkNotificationContact Contact { get; }

    /// <summary>
    /// Message public options
    /// </summary>
    public WeChatWorkMessageOptions Options { get; }

    public WeChatWorkMessageFactoryContext(
        NotificationInfo notification,
        NotificationStandardData standardData,
        WeChatWorkChannelData channelData,
        WeChatWorkNotificationContact contact,
        WeChatWorkMessageOptions options)
    {
        Notification = notification;
        StandardData = standardData;
        ChannelData = channelData;
        Contact = contact;
        Options = options;
    }
}
