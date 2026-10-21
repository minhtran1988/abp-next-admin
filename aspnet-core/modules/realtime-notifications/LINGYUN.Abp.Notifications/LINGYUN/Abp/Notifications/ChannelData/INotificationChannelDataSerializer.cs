using System;

namespace LINGYUN.Abp.Notifications;

/// <summary>
/// Notification channel data (payload) serializer
/// </summary>
/// <remarks>
/// The unified entrance on the producer side is responsible for writing strong type payloads into <see cref="NotificationData"/>.
/// </remarks>
public interface INotificationChannelDataSerializer
{
    /// <summary>
    /// Write channel data (load)
    /// </summary>
    /// <typeparam name="TChannelData">Payload type</typeparam>
    /// <param name="notificationData">Notification data</param>
    /// <param name="channelData">Payload instance</param>
    NotificationData Write<TChannelData>(NotificationData notificationData, TChannelData channelData)
        where TChannelData : class;

    /// <summary>
    /// Write channel data (load)
    /// </summary>
    /// <param name="notificationData">Notification data</param>
    /// <param name="channelData">Payload instance</param>
    NotificationData Write(NotificationData notificationData, NotificationChannelData channelData);

    /// <summary>
    /// Clear channel data by payload type
    /// </summary>
    NotificationData Clear<TChannelData>(NotificationData notificationData)
        where TChannelData : class;

    /// <summary>
    /// Determine whether the specified payload exists in the notification data
    /// </summary>
    bool Has<TPayload>(NotificationData notificationData)
        where TPayload : class;
}
