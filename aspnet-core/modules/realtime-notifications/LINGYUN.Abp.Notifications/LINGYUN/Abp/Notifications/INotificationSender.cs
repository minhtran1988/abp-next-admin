using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LINGYUN.Abp.Notifications;

/// <summary>
/// Send notification interface
/// </summary>
public interface INotificationSender
{
    /// <summary>
    /// Send notification
    /// </summary>
    /// <param name="name">Name</param>
    /// <param name="data">data</param>
    /// <param name="users">User list, empty ID is sent to all subscribed users</param>
    /// <param name="tenantId">Tenant</param>
    /// <param name="severity">Severity level</param>
    /// <param name="useProviders">Use notification providers</param>
    /// <returns>Notification ID</returns>
    Task<string> SendNofiterAsync(
        string name,
        NotificationData data,
        IEnumerable<UserIdentifier>? users = null,
        Guid? tenantId = null,
        NotificationSeverity severity = NotificationSeverity.Info,
        IEnumerable<string>? useProviders = null);
    /// <summary>
    /// Send template notification
    /// </summary>
    /// <param name="name">Name</param>
    /// <param name="template">Template object</param>
    /// <param name="users">User list, empty ID is sent to all subscribed users</param>
    /// <param name="tenantId">Tenant</param>
    /// <param name="severity">Severity level</param>
    /// <param name="useProviders">Use notification providers</param>
    /// <returns></returns>
    Task<string> SendNofiterAsync(
        string name,
        NotificationTemplate template,
        IEnumerable<UserIdentifier>? users = null,
        Guid? tenantId = null,
        NotificationSeverity severity = NotificationSeverity.Info,
        IEnumerable<string>? useProviders = null);
}
