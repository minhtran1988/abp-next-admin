using System.Collections.Generic;

namespace LINGYUN.Abp.Notifications.Templating;
public class NotificationTemplateResolveResult
{
    /// <summary>
    /// Template data
    /// </summary>
    public object? Model { get; set; }

    public List<string> AppliedResolvers { get; }
    public NotificationTemplateResolveResult()
    {
        AppliedResolvers = new List<string>();
    }
}
