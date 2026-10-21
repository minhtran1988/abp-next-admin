namespace LINGYUN.Abp.Notifications;

/// <summary>
/// Storage format of notification channel data (load) attribute
/// </summary>
public enum NotificationChannelDataFormat
{
    /// <summary>
    /// Automatic recognition (default)
    /// </summary>
    /// <remarks>
    /// Direct conversion of simple types, serialization of complex types into JSON strings,
    /// When reading, it is also compatible with the two forms of "object instance" and "JSON string".
    /// </remarks>
    Auto = 0,
    /// <summary>
    /// Always stored as JSON string
    /// </summary>
    Json = 1
}
