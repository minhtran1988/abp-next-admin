using System;
using System.Collections.Generic;

namespace LINGYUN.Abp.Notifications;

/// <summary>
/// Channel data existence determination provider
/// </summary>
public interface INotificationChannelDataPresenceProvider
{
    /// <summary>
    /// Get the existence determination mode of the payload declaration
    /// </summary>
    NotificationChannelDataPresenceMode GetPresenceMode(Type channelDataType);

    /// <summary>
    /// Get the attribute keys participating in the match
    /// </summary>
    IReadOnlyList<string> GetMatchingKeys(Type channelDataType);

    /// <summary>
    /// Create an existence determination delegate
    /// </summary>
    /// <exception cref="AbpNotificationChannelDataConfigurationException">
    /// The key press determination is declared, but there are no attributes that can participate in the matching.
    /// </exception>
    Func<NotificationData, bool> Create(Type channelDataType);
}
