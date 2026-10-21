using System;

namespace LINGYUN.Abp.Notifications;

/// <summary>
/// Channel data attribute metadata
/// </summary>
/// <remarks>
/// Derived from <see cref="NotificationChannelPropertyAttribute"/>,
/// Describe "which key is written/read, what type it is, and whether it participates in existence determination".
/// </remarks>
public sealed class NotificationChannelDataProperty
{
    /// <summary>
    /// Data key name
    /// </summary>
    public string Key { get; }

    /// <summary>
    /// Property declaration type
    /// </summary>
    public Type PropertyType { get; }

    /// <summary>
    /// Storage format
    /// </summary>
    public NotificationChannelDataFormat Format { get; }

    /// <summary>
    /// Whether to ignore this attribute when determining existence
    /// </summary>
    public bool IgnoreWhenMatching { get; }

    /// <summary>
    /// Read delegate
    /// </summary>
    public Func<object, object?> GetValue { get; }

    /// <summary>
    /// Write delegate
    /// </summary>
    public Action<object, object?> SetValue { get; }

    public NotificationChannelDataProperty(
        string key,
        Type propertyType,
        NotificationChannelDataFormat format,
        bool ignoreWhenMatching,
        Func<object, object?> getValue,
        Action<object, object?> setValue)
    {
        Key = key;
        PropertyType = propertyType;
        Format = format;
        IgnoreWhenMatching = ignoreWhenMatching;
        GetValue = getValue;
        SetValue = setValue;
    }
}
