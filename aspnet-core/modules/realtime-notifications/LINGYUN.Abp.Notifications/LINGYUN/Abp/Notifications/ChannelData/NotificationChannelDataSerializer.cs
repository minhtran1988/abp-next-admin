using System;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace LINGYUN.Abp.Notifications;

/// <summary>
/// Notification channel data (payload) serializer
/// </summary>
/// <remarks>
/// <para>
/// The unified entrance on the producer side is responsible for writing strong type payloads into <see cref="NotificationData"/>.
/// </para>
/// <para>
/// The writing of the payload is "declarative": <see cref="NotificationChannelPropertyAttribute"/> on the payload type
/// Decide which keys to write and how to convert them, so the producer and channel implementation share the same contract definition,
/// There will be no situation where "one party writes the key, but the other party can never read it".
/// </para>
/// </remarks>
public class NotificationChannelDataSerializer : INotificationChannelDataSerializer, ISingletonDependency
{
    protected INotificationChannelDataAccessor Accessor { get; }
    protected INotificationChannelDataPresenceProvider PresenceProvider { get; }

    public NotificationChannelDataSerializer(
        INotificationChannelDataAccessor accessor,
        INotificationChannelDataPresenceProvider presenceProvider)
    {
        Accessor = accessor;
        PresenceProvider = presenceProvider;
    }

    public virtual NotificationData Write<TChannelData>(NotificationData notificationData, TChannelData channelData)
        where TChannelData : class
    {
        Check.NotNull(notificationData, nameof(notificationData));
        Check.NotNull(channelData, nameof(channelData));

        return Write(notificationData, (NotificationChannelData)(object)channelData);
    }

    public virtual NotificationData Write(NotificationData notificationData, NotificationChannelData channelData)
    {
        Check.NotNull(notificationData, nameof(notificationData));
        Check.NotNull(channelData, nameof(channelData));

        foreach (var property in NotificationChannelDataPropertyCache.GetProperties(channelData.GetType()))
        {
            var value = property.GetValue(channelData);
            if (value == null)
            {
                continue;
            }

            Accessor.Set(notificationData, property.Key, property.PropertyType, value, property.Format);
        }

        return notificationData;
    }

    public virtual NotificationData Clear<TChannelData>(NotificationData notificationData)
        where TChannelData : class
    {
        Check.NotNull(notificationData, nameof(notificationData));

        return Clear(notificationData, typeof(TChannelData));
    }

    public virtual NotificationData Clear(NotificationData notificationData, Type channelDataType)
    {
        Check.NotNull(notificationData, nameof(notificationData));
        Check.NotNull(channelDataType, nameof(channelDataType));

        foreach (var property in NotificationChannelDataPropertyCache.GetProperties(channelDataType))
        {
            notificationData.ExtraProperties.Remove(property.Key);
        }

        return notificationData;
    }

    public virtual bool Has<TPayload>(NotificationData notificationData)
        where TPayload : class
    {
        Check.NotNull(notificationData, nameof(notificationData));

        return PresenceProvider.Create(typeof(TPayload))(notificationData);
    }
}
