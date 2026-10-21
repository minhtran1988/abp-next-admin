using LINGYUN.Abp.Notifications;

namespace LINGYUN.Abp.Notifications.PushPlus;

/// <summary>
/// PushPlus channel data (payload)
/// </summary>
/// <remarks>
/// webhook and callback are "optional parameters" rather than mutually exclusive "payload identifiers":
/// Writing any one of them means that the producer wants to use the PushPlus channel.
/// Therefore the presence determination mode is declared as <see cref="NotificationChannelDataPresenceMode.AnyDeclaredProperty"/>.
/// </remarks>
[NotificationChannelData(
    PushPlusNotificationPublishProvider.ProviderName,
    Priority = 100,
    Presence = NotificationChannelDataPresenceMode.AnyDeclaredProperty)]
public class PushPlusChannelData : NotificationChannelData
{
    /// <summary>
    /// webhook address
    /// </summary>
    [NotificationChannelProperty(PushPlusNotificationDataKeys.Webhook)]
    public virtual string? Webhook { get; set; }

    /// <summary>
    /// Callback address
    /// </summary>
    [NotificationChannelProperty(PushPlusNotificationDataKeys.CallbackUrl)]
    public virtual string? CallbackUrl { get; set; }
}
