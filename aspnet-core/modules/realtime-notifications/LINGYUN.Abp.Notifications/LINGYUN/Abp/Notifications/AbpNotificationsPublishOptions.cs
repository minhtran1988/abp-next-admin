using System;
using Volo.Abp.Collections;

namespace LINGYUN.Abp.Notifications;

public class AbpNotificationsPublishOptions
{
    public const string DefaultDateTimeFormat = "yyyy-MM-dd HH:mm:ss";
    /// <summary>
    /// Custom notification publishing provider collection
    /// </summary>
    public ITypeList<INotificationPublishProvider> PublishProviders { get; }
    /// <summary>
    /// Expiration time
    /// Default 60 days
    /// </summary>
    public TimeSpan ExpirationTime { get; set; }
    /// <summary>
    /// Default time and date serialization format
    /// </summary>
    /// <remarks>
    /// Default: yyyy-MM-dd HH:mm:ss
    /// </remarks>
    public string DateTimeFormat { get; set; }
    public AbpNotificationsPublishOptions()
    {
        PublishProviders = new TypeList<INotificationPublishProvider>();

        ExpirationTime = TimeSpan.FromDays(60);
        DateTimeFormat = DefaultDateTimeFormat;
    }
}
