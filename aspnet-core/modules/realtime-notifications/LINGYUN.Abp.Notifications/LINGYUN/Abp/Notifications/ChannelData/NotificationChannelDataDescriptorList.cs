using System;
using System.Collections.Generic;

namespace LINGYUN.Abp.Notifications;

/// <summary>
/// Notification channel data (load) type collection
/// </summary>
/// <remarks>
/// Payloads in the same channel are sorted by <see cref="NotificationChannelDataAttribute.Priority"/>,
/// The matching order must be explicitly declared, and reliance on registration order or field detection order is not allowed.
/// </remarks>
public sealed class NotificationChannelDataDescriptorList : List<NotificationChannelDataDescriptor>
{
}
