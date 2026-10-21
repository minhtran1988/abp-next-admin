using System;

namespace LINGYUN.Abp.Notifications;

/// <summary>
/// Value type determination of channel data
/// </summary>
/// <remarks>
/// Used to distinguish simple types that "can be stored directly in property bags" and complex types that "need to be serialized".
/// </remarks>
public static class NotificationChannelDataValueTypes
{
    /// <summary>
    /// Determine whether it is a simple type that can be stored directly
    /// </summary>
    public static bool IsPrimitive(Type type)
    {
        return type.IsPrimitive
            || type.IsEnum
            || type == typeof(string)
            || type == typeof(decimal)
            || type == typeof(DateTime)
            || type == typeof(DateTimeOffset)
            || type == typeof(TimeSpan)
            || type == typeof(Guid);
    }
}
