using System;

namespace LINGYUN.Abp.Notifications;

public class AbpNotificationsManagementOptions
{
    /// <summary>
    /// Default: true.
    /// </summary>
    public bool SaveStaticNotificationsToDatabase { get; set; }
    /// <summary>
    /// Default: false.
    /// </summary>
    public bool IsDynamicNotificationsStoreEnabled { get; set; }
    /// <summary>
    /// Cache refresh time
    /// default: 30 seconds
    /// </summary>
    public TimeSpan NotificationsCacheRefreshInterval { get; set; }
    /// <summary>
    /// Application timestamp timeout
    /// default: 2 minutes
    /// </summary>
    public TimeSpan NotificationsCacheStampTimeOut { get; set; }
    /// <summary>
    /// Timestamp expiration time
    /// default: 30 days
    /// </summary>
    public TimeSpan NotificationsCacheStampExpiration { get; set; }
    public AbpNotificationsManagementOptions()
    {
        SaveStaticNotificationsToDatabase = true;

        NotificationsCacheRefreshInterval = TimeSpan.FromSeconds(30);
        NotificationsCacheStampTimeOut = TimeSpan.FromMinutes(2);
        // Refresh cache after 30 days expiration
        NotificationsCacheStampExpiration = TimeSpan.FromDays(30);
    }
}
