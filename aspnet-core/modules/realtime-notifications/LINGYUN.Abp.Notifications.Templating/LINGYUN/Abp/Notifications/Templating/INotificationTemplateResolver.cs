using JetBrains.Annotations;
using System.Threading.Tasks;

namespace LINGYUN.Abp.Notifications.Templating;
/// <summary>
/// Notification template model parsing interface
/// </summary>
public interface INotificationTemplateResolver
{
    /// <summary>
    /// Parse template data
    /// </summary>
    /// <param name="template"></param>
    /// <returns></returns>
    [NotNull]
    Task<NotificationTemplateResolveResult> ResolveAsync(NotificationTemplate template);
}
