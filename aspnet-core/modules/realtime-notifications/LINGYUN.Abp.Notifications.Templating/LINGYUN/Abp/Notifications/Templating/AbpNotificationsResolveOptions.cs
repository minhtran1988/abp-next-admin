using JetBrains.Annotations;
using System.Collections.Generic;

namespace LINGYUN.Abp.Notifications.Templating;
public class AbpNotificationsResolveOptions
{
    /// <summary>
    /// Template parsing provider list
    /// </summary>
    [NotNull]
    public List<INotificationTemplateResolveContributor> TemplateResolvers { get; }

    public AbpNotificationsResolveOptions()
    {
        TemplateResolvers = new List<INotificationTemplateResolveContributor>();
    }
}
