using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Volo.Abp;

namespace LINGYUN.Abp.Notifications;

/// <summary>
/// Notification channel data (payload) options
/// </summary>
public class AbpNotificationChannelDataOptions
{
    private readonly Dictionary<string, NotificationChannelDataDescriptorList> _providers
        = new(StringComparer.OrdinalIgnoreCase);

    private readonly List<Type> _payloadTypes = new();

    /// <summary>
    /// Registered payload type
    /// </summary>
    public IReadOnlyList<Type> PayloadTypes => _payloadTypes;

    /// <summary>
    /// Get the payload collection of the specified channel (arranged in matching order)
    /// </summary>
    public NotificationChannelDataDescriptorList GetProviderData(string providerName)
    {
        if (!_providers.TryGetValue(providerName, out var list))
        {
            list = new NotificationChannelDataDescriptorList();
            _providers[providerName] = list;
        }

        return list;
    }

    /// <summary>
    /// Register channel data (load)
    /// </summary>
    /// <remarks>
    /// The channel name, matching order and existence determination mode come from
    /// <see cref="NotificationChannelDataAttribute"/>。
    /// </remarks>
    public AbpNotificationChannelDataOptions Add<TChannelData>()
        where TChannelData : NotificationChannelData
    {
        return Add(typeof(TChannelData));
    }

    /// <summary>
    /// Register channel data (load)
    /// </summary>
    /// <exception cref="AbpNotificationChannelDataConfigurationException">
    /// Missing <see cref="NotificationChannelDataAttribute"/>,
    /// The existence rule is not declared, or it conflicts with the channel priority.
    /// </exception>
    public AbpNotificationChannelDataOptions Add(Type channelDataType)
    {
        Check.NotNull(channelDataType, nameof(channelDataType));

        if (_payloadTypes.Contains(channelDataType))
        {
            return this;
        }

        var attribute = channelDataType.GetCustomAttribute<NotificationChannelDataAttribute>(inherit: false);
        if (attribute == null)
        {
            throw new AbpNotificationChannelDataConfigurationException(
                $"The channel data type '{channelDataType.FullName}' must be annotated with '{nameof(NotificationChannelDataAttribute)}'.");
        }

        // The registration period verifies the existence statement, allowing configuration errors to be exposed during the startup phase.
        // Verification only relies on declaration metadata, so there is no need to parse any services.
        NotificationChannelDataPresence.Validate(channelDataType);

        var descriptors = GetProviderData(attribute.ProviderName);

        if (descriptors.Any(x => x.Priority == attribute.Priority))
        {
            var conflict = descriptors.First(x => x.Priority == attribute.Priority);
            throw new AbpNotificationChannelDataConfigurationException(
                $"The channel data '{channelDataType.FullName}' and '{conflict.PayloadType.FullName}' of provider '{attribute.ProviderName}' declare the same priority '{attribute.Priority}', the matching order would be ambiguous.");
        }

        descriptors.Add(new NotificationChannelDataDescriptor(
            attribute.ProviderName,
            channelDataType,
            attribute.Priority,
            attribute.Presence,
            NotificationChannelDataPresence.Create(channelDataType)));

        descriptors.Sort((left, right) => left.Priority.CompareTo(right.Priority));

        _payloadTypes.Add(channelDataType);

        return this;
    }
}
