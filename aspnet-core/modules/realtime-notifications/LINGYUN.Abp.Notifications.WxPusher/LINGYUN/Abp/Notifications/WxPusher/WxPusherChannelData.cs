using LINGYUN.Abp.Notifications;

namespace LINGYUN.Abp.Notifications.WxPusher;

/// <summary>
/// WxPusher channel data (payload)
/// </summary>
/// <remarks>
/// Existence is deduced from the url key (default Presence = DeclaredProperties), no need to implement the determination method.
/// </remarks>
[NotificationChannelData(WxPusherNotificationPublishProvider.ProviderName, Priority = 100)]
public class WxPusherChannelData : NotificationChannelData
{
    /// <summary>
    /// The jump address after the message is clicked
    /// </summary>
    [NotificationChannelProperty(WxPusherNotificationDataKeys.Url)]
    public virtual string? Url { get; set; }
}
