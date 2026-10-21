using System.Threading.Tasks;

namespace LINGYUN.Abp.Notifications.Templating;
public abstract class NotificationTemplateResolveContributorBase : INotificationTemplateResolveContributor
{
    public abstract string Name { get; }
    /// <summary>
    /// Implement this interface to process template data
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public abstract Task ResolveAsync(INotificationTemplateResolveContext context);
}
