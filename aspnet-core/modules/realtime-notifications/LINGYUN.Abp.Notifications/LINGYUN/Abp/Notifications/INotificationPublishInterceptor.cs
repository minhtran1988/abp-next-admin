using System.Threading.Tasks;

namespace LINGYUN.Abp.Notifications;
/// <summary>
/// Notification release interceptor, implement this interface to add custom checks before release
/// </summary>
public interface INotificationPublishInterceptor
{
    /// <summary>
    /// Interception priority, the smaller the value, the higher the priority.
    /// </summary>
    int Priority { get; }

    /// <summary>
    /// Perform checks before notification is posted
    /// </summary>
    /// <param name="notification">Notification information</param>
    /// <returns>true-allow publishing, false-prevent publishing</returns>
    Task<bool> CanPublishAsync(NotificationInfo notification);
}
