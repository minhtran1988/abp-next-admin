using System;
using System.Collections.Generic;
using Volo.Abp.MultiTenancy;

namespace LINGYUN.Abp.Notifications;

public class NotificationEventData : IMultiTenant
{
    /// <summary>
    /// Tenant
    /// </summary>
    public Guid? TenantId { get; set; }
    /// <summary>
    /// Notification name
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// Used to identify an application
    /// </summary>
    /// <remarks>
    /// tips: You can use this to specify application boundaries
    /// </remarks>
    public string Application { get; set; }
    /// <summary>
    /// data
    /// </summary>
    public NotificationData Data { get; set; } = default!;
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
    public List<UserIdentifier> Users { get; set; }
    public NotificationEventData()
    {
        Application = "Abp";

        Users = new List<UserIdentifier>();
    }
}
