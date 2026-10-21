using System;
using System.Collections.Generic;

namespace LINGYUN.Abp.Notifications;

/// <summary>
/// Notification channel data (payload) parser
/// </summary>
public interface INotificationChannelDataResolver
{
    /// <summary>
    /// Parse the payload of the specified channel in matching order
    /// </summary>
    /// <remarks>
    /// Only "written and parsable" payloads are returned; when illegal data is passed in, it will be skipped and logged.
    /// If the channel needs to distinguish this situation, it should be used with <see cref="IsDeclared"/>.
    /// </remarks>
    IReadOnlyList<NotificationChannelData> Resolve(string providerName, NotificationData notificationData);

    /// <summary>
    /// Parse the specified type of channel data
    /// </summary>
    /// <returns>Payload instance, returns null if it is not written or cannot be parsed</returns>
    TChannelData? Resolve<TChannelData>(NotificationData notificationData)
        where TChannelData : NotificationChannelData;

    /// <summary>
    /// Parse the specified type of channel data
    /// </summary>
    NotificationChannelData? Resolve(Type payloadType, NotificationData notificationData);

    /// <summary>
    /// Determine whether the producer declares the specified type of channel data
    /// </summary>
    /// <remarks>
    /// Used in conjunction with parsing results, it can be distinguished
    /// "The producer did not write the payload" and "The payload was written but could not be parsed (the data is illegal)".
    /// The latter is a producer error, and the channel should report the error clearly instead of silently retreating.
    /// </remarks>
    bool IsDeclared(NotificationData notificationData, Type payloadType);

    /// <summary>
    /// Get the payload type collection of the specified channel
    /// </summary>
    IReadOnlyList<Type> GetPayloadTypes(string providerName);

    /// <summary>
    /// Get the identification key of the payload
    /// </summary>
    /// <remarks>
    /// Returns the first attribute key "participating in existence determination" of the payload (the actual key name in the data),
    /// For diagnostic information (e.g. "which key failed to parse"). Returns null if no matchable properties have been declared.
    /// </remarks>
    string? GetKey(NotificationData notificationData, Type payloadType);

    /// <summary>
    /// Read weakly type and convert to target type
    /// </summary>
    object? Get(NotificationData notificationData, string key, Type targetType);

    /// <summary>
    /// Typed reading channel data key
    /// </summary>
    /// <remarks>
    /// Used to read "scattered scalar options" (such as enterprise WeChat's safe, enable_id_trans),
    /// This type of value does not require an independent load type, but must also undergo weak type conversion.
    /// </remarks>
    TValue? GetValue<TValue>(NotificationData notificationData, string key)
        where TValue : struct;

    /// <summary>
    /// Typed reading channel data key, returns the default value if not set
    /// </summary>
    TValue GetValue<TValue>(NotificationData notificationData, string key, TValue defaultValue)
        where TValue : struct;

    /// <summary>
    /// Read channel data keys case-insensitively
    /// </summary>
    /// <remarks>
    /// Used to be compatible with case-sensitive legacy keys in historical channel implementations (such as <c>TemplateId</c>),
    /// Prevent producers from silently failing due to case differences.
    /// </remarks>
    string? GetOrNull(NotificationData notificationData, params string[] keys);
}
