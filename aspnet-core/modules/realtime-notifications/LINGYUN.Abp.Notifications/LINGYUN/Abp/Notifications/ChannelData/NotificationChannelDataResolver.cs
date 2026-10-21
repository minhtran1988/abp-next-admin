using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace LINGYUN.Abp.Notifications;

/// <summary>
/// Notification channel data (payload) parser
/// </summary>
public class NotificationChannelDataResolver : INotificationChannelDataResolver, ISingletonDependency
{
    protected IServiceProvider ServiceProvider { get; }
    protected INotificationChannelDataAccessor Accessor { get; }
    protected INotificationChannelDataPresenceProvider PresenceProvider { get; }
    protected ILogger<NotificationChannelDataResolver> Logger { get; }

    private readonly IOptions<AbpNotificationChannelDataOptions> _options;
    private readonly Dictionary<string, List<ChannelDataRegistration>> _registrations;
    private readonly Dictionary<Type, ChannelDataRegistration> _registrationsByType;
    private readonly object _syncLock = new();

    public NotificationChannelDataResolver(
        IServiceProvider serviceProvider,
        IOptions<AbpNotificationChannelDataOptions> options,
        INotificationChannelDataAccessor accessor,
        INotificationChannelDataPresenceProvider presenceProvider,
        ILogger<NotificationChannelDataResolver>? logger = null)
    {
        ServiceProvider = serviceProvider;
        _options = options;
        Accessor = accessor;
        PresenceProvider = presenceProvider;
        Logger = logger ?? NullLogger<NotificationChannelDataResolver>.Instance;
        _registrations = new Dictionary<string, List<ChannelDataRegistration>>(StringComparer.OrdinalIgnoreCase);
        _registrationsByType = new Dictionary<Type, ChannelDataRegistration>();
    }

    public virtual IReadOnlyList<NotificationChannelData> Resolve(string providerName, NotificationData notificationData)
    {
        Check.NotNull(providerName, nameof(providerName));
        Check.NotNull(notificationData, nameof(notificationData));

        var result = new List<NotificationChannelData>();

        foreach (var registration in GetRegistrations(providerName))
        {
            if (!registration.IsPresent(notificationData))
            {
                continue;
            }

            var channelData = CreateChannelData(registration, notificationData);
            if (channelData != null)
            {
                result.Add(channelData);
            }
        }

        return result;
    }

    public virtual TChannelData? Resolve<TChannelData>(NotificationData notificationData)
        where TChannelData : NotificationChannelData
    {
        return Resolve(typeof(TChannelData), notificationData) as TChannelData;
    }

    public virtual NotificationChannelData? Resolve(Type payloadType, NotificationData notificationData)
    {
        Check.NotNull(payloadType, nameof(payloadType));
        Check.NotNull(notificationData, nameof(notificationData));

        var registration = GetRegistration(payloadType);

        return registration.IsPresent(notificationData)
            ? CreateChannelData(registration, notificationData)
            : null;
    }

    public virtual bool IsDeclared(NotificationData notificationData, Type payloadType)
    {
        Check.NotNull(notificationData, nameof(notificationData));
        Check.NotNull(payloadType, nameof(payloadType));

        return GetRegistration(payloadType).IsPresent(notificationData);
    }

    public virtual IReadOnlyList<Type> GetPayloadTypes(string providerName)
    {
        return GetRegistrations(providerName)
            .Select(x => x.Descriptor.PayloadType)
            .ToArray();
    }

    public virtual string? GetKey(NotificationData notificationData, Type payloadType)
    {
        Check.NotNull(payloadType, nameof(payloadType));

        // Return the actual hit payload identification key in the data (the case may be different from the declaration), used for diagnostic information
        foreach (var key in PresenceProvider.GetMatchingKeys(payloadType))
        {
            if (Accessor.TryGetRawValue(notificationData, key, out _, out var matchedKey))
            {
                return matchedKey ?? key;
            }
        }

        // Return the declared identification key if it does not exist in the data
        return PresenceProvider.GetMatchingKeys(payloadType).FirstOrDefault();
    }

    public virtual object? Get(NotificationData notificationData, string key, Type targetType)
    {
        return Accessor.Get(notificationData, key, targetType);
    }

    public virtual TValue? GetValue<TValue>(NotificationData notificationData, string key)
        where TValue : struct
    {
        return Accessor.GetValue<TValue>(notificationData, key);
    }

    public virtual TValue GetValue<TValue>(NotificationData notificationData, string key, TValue defaultValue)
        where TValue : struct
    {
        return Accessor.GetValue(notificationData, key, defaultValue);
    }

    public virtual string? GetOrNull(NotificationData notificationData, params string[] keys)
    {
        return Accessor.GetOrNull(notificationData, keys);
    }

    protected virtual ChannelDataRegistration GetRegistration(Type payloadType)
    {
        if (_registrationsByType.TryGetValue(payloadType, out var registration))
        {
            return registration;
        }

        // Unregistered types are read directly according to the agreement to avoid coupling producers and channels to the registration order.
        return new ChannelDataRegistration(
            new NotificationChannelDataDescriptor(
                string.Empty,
                payloadType,
                0,
                PresenceProvider.GetPresenceMode(payloadType),
                PresenceProvider.Create(payloadType)),
            serviceProvider => ActivatorUtilities.CreateInstance(serviceProvider, payloadType));
    }

    protected virtual NotificationChannelData? CreateChannelData(
        ChannelDataRegistration registration,
        NotificationData notificationData)
    {
        object? instance;
        try
        {
            instance = registration.Factory(ServiceProvider);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex,
                "Failed to create the channel data '{ChannelData}' for provider '{Provider}'.",
                registration.Descriptor.PayloadType.FullName, registration.Descriptor.ProviderName);
            return null;
        }

        if (instance is not NotificationChannelData channelData)
        {
            Logger.LogError(
                "The channel data '{ChannelData}' for provider '{Provider}' is not assignable to '{BaseType}'.",
                registration.Descriptor.PayloadType.FullName, registration.Descriptor.ProviderName, typeof(NotificationChannelData).FullName);
            return null;
        }

        foreach (var property in NotificationChannelDataPropertyCache.GetProperties(registration.Descriptor.PayloadType))
        {
            // Locate the actual key name in a case-insensitive manner, compatible with data written by producers using different cases.
            if (!Accessor.TryGetRawValue(notificationData, property.Key, out var rawValue, out var matchedKey))
            {
                continue;
            }

            if (rawValue == null)
            {
                continue;
            }

            var value = ConvertPropertyValue(notificationData, property, matchedKey);

            if (value == null)
            {
                // The value exists but cannot be parsed/the type does not match, indicating that the payload written by the producer is illegal
                Logger.LogWarning(
                    "The channel data '{ChannelData}' property '{Property}' of provider '{Provider}' can not be resolved from the key '{Key}'.",
                    registration.Descriptor.PayloadType.FullName, property.Key, registration.Descriptor.ProviderName, matchedKey ?? property.Key);
                return null;
            }

            property.SetValue(channelData, value);
        }

        return channelData;
    }

    /// <summary>
    /// Convert payload attribute value
    /// </summary>
    /// <returns>Conversion result; null is returned when conversion cannot be performed or the type does not match</returns>
    protected virtual object? ConvertPropertyValue(
        NotificationData notificationData,
        NotificationChannelDataProperty property,
        string? matchedKey)
    {
        var value = Accessor.Get(notificationData, matchedKey ?? property.Key, property.PropertyType);

        if (value == null)
        {
            return null;
        }

        // The runtime type of the value must be compatible with the property declaration type
        var underlyingType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

        return underlyingType.IsInstanceOfType(value) ? value : null;
    }

    protected virtual IReadOnlyList<ChannelDataRegistration> GetRegistrations(string providerName)
    {
        lock (_syncLock)
        {
            if (_registrations.TryGetValue(providerName, out var registrations))
            {
                return registrations;
            }

            registrations = _options.Value
                .GetProviderData(providerName)
                .Select(CreateRegistration)
                .ToList();

            _registrations[providerName] = registrations;

            return registrations;
        }
    }

    protected virtual ChannelDataRegistration CreateRegistration(NotificationChannelDataDescriptor descriptor)
    {
        return new ChannelDataRegistration(
            descriptor,
            serviceProvider => ActivatorUtilities.CreateInstance(serviceProvider, descriptor.PayloadType));
    }

    /// <summary>
    /// Channel data registration item
    /// </summary>
    protected sealed class ChannelDataRegistration
    {
        public NotificationChannelDataDescriptor Descriptor { get; }
        public Func<IServiceProvider, object> Factory { get; }
        public Func<NotificationData, bool> IsPresent => Descriptor.IsPresent;

        public ChannelDataRegistration(
            NotificationChannelDataDescriptor descriptor,
            Func<IServiceProvider, object> factory)
        {
            Descriptor = descriptor;
            Factory = factory;
        }
    }
}
