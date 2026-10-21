using LINGYUN.Abp.Notifications;
using Volo.Abp.Identity.Localization;
using Volo.Abp.Localization;

namespace LINGYUN.Abp.Identity.Notifications;
public class IdentityNotificationDefinitionProvider : NotificationDefinitionProvider
{
    public override void Define(INotificationDefinitionContext context)
    {
        var group = context.AddGroup(
            IdentityNotificationNames.GroupName,
            L("Notifications:AbpIdentity"),
            L("Notifications:AbpIdentity"));

        group.AddNotification(
            IdentityNotificationNames.Session.ExpirationSession,
            L("Notifications:SessionExpiration"),
            L("Notifications:SessionExpiration"),
            NotificationType.ServiceCallback,
            NotificationLifetime.Persistent,
            NotificationContentType.Json,
            allowSubscriptionToClients: false) // Client disabled, all new users must subscribe to this notification
            .WithProviders(
                NotificationProviderNames.SignalR, // Real-time notification processing session expiration event
                NotificationProviderNames.Emailing); // Email notification session expires

        // Account remains active notification
        group.AddNotification(
            IdentityNotificationNames.IdentityUser.InactiveUserReminderNotifier,
            L("InactiveUserReminderNotifier"),
            L("InactiveUserReminderNotifier"),
            NotificationType.User,
            NotificationLifetime.Persistent,
            NotificationContentType.Html,
            allowSubscriptionToClients: false) // Client disabled, all new users must subscribe to this notification
            .WithProviders(NotificationProviderNames.Emailing);
        // Account deactivation notification
        group.AddNotification(
            IdentityNotificationNames.IdentityUser.InactiveUserDeactivationNotifier,
            L("InactiveUserDeactivationNotifier"),
            L("InactiveUserDeactivationNotifier"),
            NotificationType.User,
            NotificationLifetime.Persistent,
            NotificationContentType.Html,
            allowSubscriptionToClients: false) // Client disabled, all new users must subscribe to this notification
            .WithProviders(NotificationProviderNames.Emailing);
        // There is no need to add the account deletion notification, just send the email notification directly, because the user has been deleted when the notification is posted.
    }

    private static ILocalizableString L(string name)
    {
        return LocalizableString.Create<IdentityResource>(name);
    }
}
