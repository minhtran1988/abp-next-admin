using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Volo.Abp;

namespace LINGYUN.Abp.Notifications;
public static class INotificationSenderExtensions
{
    /// <summary>
    /// Send notification
    /// </summary>
    /// <param name="sender">Sending interface</param>
    /// <param name="name">Name</param>
    /// <param name="data">data</param>
    /// <param name="user">User, the empty ID is sent to all subscribing users</param>
    /// <param name="tenantId">Tenant</param>
    /// <param name="severity">Severity level</param>
    /// <returns>Notification ID</returns>
    public async static Task<string> SendNofiterAsync(
        [NotNull] this INotificationSender sender,
        [NotNull] string name,
        [NotNull] NotificationData data,
        UserIdentifier? user = null,
        Guid? tenantId = null,
        NotificationSeverity severity = NotificationSeverity.Info)
    {
        Check.NotNull(sender, nameof(sender));
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.NotNull(data, nameof(data));

        if (user == null)
        {
            return await sender.SendNofiterAsync(name, data, tenantId: tenantId, severity: severity);
        }
        return await sender.SendNofiterAsync(name, data, new[] { user },tenantId: tenantId, severity: severity);
    }

    /// <summary>
    /// Send template notification
    /// </summary>
    /// <param name="sender">Sending interface</param>
    /// <param name="name">Name</param>
    /// <param name="template">Template object</param>
    /// <param name="user">User, if empty, send to all subscribed users</param>
    /// <param name="tenantId">Tenant</param>
    /// <param name="severity">Severity level</param>
    /// <returns>Notification ID</returns>
    public async static Task<string> SendNofiterAsync(
        [NotNull] this INotificationSender sender,
        [NotNull] string name,
        [NotNull] NotificationTemplate template,
        UserIdentifier? user = null,
        Guid? tenantId = null,
        NotificationSeverity severity = NotificationSeverity.Info)
    {
        Check.NotNull(sender, nameof(sender));
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.NotNull(template, nameof(template));

        if (user == null)
        {
            return await sender.SendNofiterAsync(name, template, tenantId: tenantId, severity: severity);
        }
        return await sender.SendNofiterAsync(name, template, new[] { user }, tenantId: tenantId, severity: severity);
    }
}
