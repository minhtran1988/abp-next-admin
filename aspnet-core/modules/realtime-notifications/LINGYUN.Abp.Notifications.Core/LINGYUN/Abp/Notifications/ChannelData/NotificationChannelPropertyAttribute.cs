using System;

namespace LINGYUN.Abp.Notifications;

/// <summary>
/// Mark the attributes of notification channel data (payload) to participate in typed reading and writing
/// </summary>
/// <remarks>
/// <para>
/// The name of the attribute participating in reading and writing is the attribute name by default, or it can be specified explicitly through <see cref="Name"/>.
/// In order to be compatible with historical notification data keys (such as unprefixed keys such as <c>TemplateId</c>).
/// </para>
/// <para>
/// The reading and writing process will perform weak type conversion, so no matter how many times the data has been
/// JSON serialization/deserialization (persistence, failure retry, cross-process delivery) will not be lost.
/// </para>
/// </remarks>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class NotificationChannelPropertyAttribute : Attribute
{
    /// <summary>
    /// The key name in the notification data, if it is empty, take the attribute name
    /// </summary>
    public string? Name { get; }

    /// <summary>
    /// Whether to ignore this attribute when matching the payload
    /// </summary>
    /// <remarks>
    /// Applicable to attributes such as "delivery options" that do not determine the payload form.
    /// For example, the recipient of Enterprise WeChat (to_user/toparty/totag).
    /// The presence of a delivery option alone does not mean that a certain message payload exists.
    /// </remarks>
    public bool IgnoreWhenMatching { get; set; }

    public NotificationChannelPropertyAttribute()
    {
    }

    public NotificationChannelPropertyAttribute(string name)
    {
        Name = name;
    }
}
