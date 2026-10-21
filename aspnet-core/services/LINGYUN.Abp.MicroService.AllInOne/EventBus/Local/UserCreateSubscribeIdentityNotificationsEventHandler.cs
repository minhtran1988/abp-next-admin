using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.Users;

namespace LINGYUN.Abp.MicroService.AllInOne.EventBus.Local;

public class UserCreateSubscribeIdentityNotificationsEventHandler : ILocalEventHandler<EntityCreatedEventData<UserEto>>, ITransientDependency
{
    private readonly INotificationSubscriptionManager _notificationSubscriptionManager;
    public UserCreateSubscribeIdentityNotificationsEventHandler(
        INotificationSubscriptionManager notificationSubscriptionManager
        )
    {
        _notificationSubscriptionManager = notificationSubscriptionManager;
    }

    public async virtual Task HandleEventAsync(EntityCreatedEventData<UserEto> eventData)
    {
        var userIdentifer = new UserIdentifier(eventData.Entity.Id, eventData.Entity.UserName);
        // Subscribe to user session expiration notifications
        await _notificationSubscriptionManager
            .SubscribeAsync(
                eventData.Entity.TenantId,
                userIdentifer,
                IdentityNotificationNames.Session.ExpirationSession);
        // Subscribe to user account inactivity notification
        await _notificationSubscriptionManager
            .SubscribeAsync(
                eventData.Entity.TenantId,
                userIdentifer,
                IdentityNotificationNames.IdentityUser.InactiveUserReminderNotifier);
        // Subscribe to user account deactivation notification
        await _notificationSubscriptionManager
            .SubscribeAsync(
                eventData.Entity.TenantId,
                userIdentifer,
                IdentityNotificationNames.IdentityUser.InactiveUserDeactivationNotifier);
    }
}
