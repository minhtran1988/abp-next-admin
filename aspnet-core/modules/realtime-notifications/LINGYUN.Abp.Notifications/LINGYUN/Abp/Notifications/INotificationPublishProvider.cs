using System.Collections.Generic;
using System.Threading.Tasks;

namespace LINGYUN.Abp.Notifications;

/// <summary>
/// Notification publishing provider interface
/// </summary>
public interface INotificationPublishProvider
{
    /// <summary>
    /// Name
    /// </summary>
    string Name { get; }
    /// <summary>
    /// Whether notifications can be issued
    /// </summary>
    /// <param name="notification"></param>
    /// <returns></returns>
    Task<bool> CanPublishAsync(
        NotificationInfo notification);
    /// <summary>
    /// Publish notification
    /// </summary>
    /// <param name="context">Notification sends context information</param>
    /// <returns></returns>
    Task PublishAsync(NotificationPublishContext context);
}
