using System;

namespace LINGYUN.Abp.Notifications;

/// <summary>
/// Notification channel data (load) accessor
/// </summary>
/// <remarks>
/// <para>
/// Notification data must undergo JSON round-trips in links such as persistence, failure retry, and cross-process delivery.
/// After the round trip the values in <c>ExtraPropertyDictionary</c> are JSON nodes instead of raw CLR instances:
/// <c>byte 1</c> will become <c>long 1</c> / <c>JsonElement(Number)</c>,
/// The original <c>x as byte?</c> will always return null.
/// </para>
/// <para>
/// This service is the only implementation of channel data "weak type attribute package ↔ strong type value" conversion.
/// By <see cref="INotificationChannelDataSerializer"/> and
/// <see cref="INotificationChannelDataResolver"/> shared,
/// and guaranteed to be independent of the JSON implementation used.
/// </para>
/// </remarks>
public interface INotificationChannelDataAccessor
{
    /// <summary>
    /// Get the data type key of the payload
    /// </summary>
    /// <returns>Key name, returns null if the type does not have a full name</returns>
    string? GetDataTypeKey(Type payloadType);

    /// <summary>
    /// Whether the key exists (case insensitive)
    /// </summary>
    bool IsPresent(NotificationData notificationData, string key);

    /// <summary>
    /// Read the original value (case insensitive)
    /// </summary>
    /// <param name="notificationData">Notification data</param>
    /// <param name="key">Key name</param>
    /// <param name="value">Original value</param>
    /// <param name="matchedKey">The actual hit key name (may be different from the case of the input parameter)</param>
    bool TryGetRawValue(NotificationData notificationData, string key, out object? value, out string? matchedKey);

    /// <summary>
    /// Read and convert to target type
    /// </summary>
    /// <returns>The converted value; returns null when the key does not exist or cannot be converted</returns>
    object? Get(NotificationData notificationData, string key, Type targetType);

    /// <summary>
    /// Typed read (returns null when not set, to distinguish "not set" from "explicit false/0")
    /// </summary>
    TValue? GetValue<TValue>(NotificationData notificationData, string key)
        where TValue : struct;

    /// <summary>
    /// Typed reading, returns default value if not set
    /// </summary>
    TValue GetValue<TValue>(NotificationData notificationData, string key, TValue defaultValue)
        where TValue : struct;

    /// <summary>
    /// Read the first non-empty string value in order (case insensitive)
    /// </summary>
    string? GetOrNull(NotificationData notificationData, params string[] keys);

    /// <summary>
    /// Write values, complex types are stored in JSON form
    /// </summary>
    void Set(NotificationData notificationData, string key, Type valueType, object? value, NotificationChannelDataFormat format);
}
