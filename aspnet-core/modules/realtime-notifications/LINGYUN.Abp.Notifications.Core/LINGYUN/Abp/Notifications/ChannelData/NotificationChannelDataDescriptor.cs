using System;

namespace LINGYUN.Abp.Notifications;

/// <summary>
/// Notification channel data (load) descriptor
/// </summary>
public sealed class NotificationChannelDataDescriptor
{
    /// <summary>
    /// Channel name
    /// </summary>
    public string ProviderName { get; }

    /// <summary>
    /// Payload type
    /// </summary>
    public Type PayloadType { get; }

    /// <summary>
    /// Matching order, the smaller the value, the first to match
    /// </summary>
    public int Priority { get; }

    /// <summary>
    /// Existence judgment mode
    /// </summary>
    public NotificationChannelDataPresenceMode Presence { get; }

    /// <summary>
    /// Determine whether this payload exists in the notification data
    /// </summary>
    public Func<NotificationData, bool> IsPresent { get; }

    public NotificationChannelDataDescriptor(
        string providerName,
        Type payloadType,
        int priority,
        NotificationChannelDataPresenceMode presence,
        Func<NotificationData, bool> isPresent)
    {
        ProviderName = providerName;
        PayloadType = payloadType;
        Priority = priority;
        Presence = presence;
        IsPresent = isPresent;
    }

    public override string ToString()
    {
        return $"{ProviderName}:{PayloadType.Name}(Priority={Priority}, Presence={Presence})";
    }
}
