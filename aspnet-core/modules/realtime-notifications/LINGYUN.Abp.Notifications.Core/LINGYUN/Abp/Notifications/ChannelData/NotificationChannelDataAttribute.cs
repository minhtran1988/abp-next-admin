using System;

namespace LINGYUN.Abp.Notifications;

/// <summary>
/// Notification channel data contract
/// </summary>
/// <remarks>
/// <para>
/// Channel data (load) is the data structure agreed between the publishing channel and the producer.
/// The producer writes the payload through <see cref="INotificationChannelDataSerializer"/>,
/// The channel resolves the payload via <see cref="INotificationChannelDataResolver"/>.
/// </para>
/// <para>
/// The presence of the payload is expressed declaratively by <see cref="Presence"/>, combined with
/// The key definition of <see cref="NotificationChannelPropertyAttribute"/> is automatically deduced,
/// There is no need to implement the determination method in the payload type.
/// </para>
/// <para>
/// If <see cref="Presence"/> is not declared and there are no attributes that can participate in matching,
/// <see cref="AbpNotificationChannelDataConfigurationException"/> will be thrown when registering:
/// Make "forgot to define existence" exposed during startup, rather than silently discarding notifications in production.
/// </para>
/// </remarks>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class NotificationChannelDataAttribute : Attribute
{
    /// <summary>
    /// Channel name, corresponding to <see cref="INotificationPublishProvider.Name"/>
    /// </summary>
    public string ProviderName { get; }

    /// <summary>
    /// The matching order of payloads in the same channel. The smaller the value, the first to match.
    /// </summary>
    /// <remarks>
    /// Used to express explicit rules within the channel, the matching order must be declared,
    /// Dependence on registration order or field probing order is not allowed.
    /// </remarks>
    public int Priority { get; set; }

    /// <summary>
    /// Existence judgment mode, the default judgment is "all attributes participating in the matching exist"
    /// </summary>
    public NotificationChannelDataPresenceMode Presence { get; set; } = NotificationChannelDataPresenceMode.DeclaredProperties;

    public NotificationChannelDataAttribute(string providerName)
    {
        if (providerName.IsNullOrWhiteSpace())
        {
            throw new ArgumentException("The provider name of the channel data can not be null or whitespace.", nameof(providerName));
        }

        ProviderName = providerName;
    }
}
