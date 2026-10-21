using System;

namespace LINGYUN.Abp.Notifications;

/// <summary>
/// JSON serialization abstraction for notification channel data (payload)
/// </summary>
public interface INotificationChannelDataJsonSerializer
{
    /// <summary>
    /// Serialization
    /// </summary>
    string Serialize(object value, Type valueType);

    /// <summary>
    /// Deserialization
    /// </summary>
    /// <param name="json">JSON string</param>
    /// <param name="targetType">Target type</param>
    /// <returns>Instance, returns null if the type is not supported</returns>
    object? Deserialize(string json, Type targetType);
}
