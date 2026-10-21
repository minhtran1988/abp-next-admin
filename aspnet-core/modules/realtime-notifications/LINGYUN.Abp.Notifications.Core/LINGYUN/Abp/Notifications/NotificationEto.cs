using LINGYUN.Abp.RealTime;
using System;
using System.Collections.Generic;
using Volo.Abp.EventBus;
using Volo.Abp.MultiTenancy;

namespace LINGYUN.Abp.Notifications;

[Serializable]
[GenericEventName(Prefix = "abp.realtime.")]
public class NotificationEto<T> : RealTimeEto<T>, IMultiTenant
{
    /// <summary>
    /// Notification ID
    /// Automatic calculation
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// Tenant
    /// </summary>
    public Guid? TenantId { get; set; }
    /// <summary>
    /// Notification name
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// Creation time
    /// </summary>
    public DateTime CreationTime { get; set; }
    /// <summary>
    /// Emergency level
    /// </summary>
    public NotificationSeverity Severity { get; set; }
    /// <summary>
    /// Specified receiving user information collection
    /// </summary>
    /// <remarks>
    /// Note:<br/>
    /// If a user list is specified, subscribed users should be filtered through this collection in the event subscriber<br/>
    /// If no user list is specified, all users who subscribe to this notification should be filtered in the event subscriber
    /// </remarks>
    public List<UserIdentifier> Users { get; set; } = new List<UserIdentifier>();
    /// <summary>
    /// List of user-specified notification providers
    /// </summary>
    /// <remarks>
    /// Note:<br/>
    /// If a notification provider is specified, only the specific provider will be used to publish notifications
    /// </remarks>
    public List<string> UseProviders { get; set; } = new List<string>();
    public NotificationEto() : base()
    {
    }

    public NotificationEto(T data) : base(data)
    {
    }
}
