namespace LINGYUN.Abp.Notifications;

/// <summary>
/// Notification channel data (load) base class
/// </summary>
/// <remarks>
/// <para>
/// Channel data is a "strongly typed data structure that the publishing channel can understand". Producers use it to express channel-specific semantics.
/// The channel implementation uses it to parse out the specific message carrier.
/// </para>
/// <para>
/// Derived classes need to do two things:
/// <list type="number">
/// <item>
/// Use <see cref="NotificationChannelDataAttribute"/> to declare the channel, matching order and
/// <see cref="NotificationChannelDataAttribute.Presence"/> Existence determination mode;
/// </item>
/// <item>
/// Use <see cref="NotificationChannelPropertyAttribute"/> to mark the attributes involved in reading and writing.
/// </item>
/// </list>
/// </para>
/// </remarks>
public abstract class NotificationChannelData
{
}
