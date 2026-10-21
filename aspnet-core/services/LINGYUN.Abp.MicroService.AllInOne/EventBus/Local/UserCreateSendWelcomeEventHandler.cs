using LINGYUN.Abp.Notifications;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;
using Volo.Abp.Users;

namespace LINGYUN.Abp.MicroService.AllInOne.EventBus
{
    public class UserCreateSendWelcomeEventHandler : ILocalEventHandler<EntityCreatedEventData<UserEto>>, ITransientDependency
    {
        private readonly INotificationSender _notificationSender;
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;
        public UserCreateSendWelcomeEventHandler(
            INotificationSender notificationSender,
            INotificationSubscriptionManager notificationSubscriptionManager
            )
        {
            _notificationSender = notificationSender;
            _notificationSubscriptionManager = notificationSubscriptionManager;
        }

        public async Task HandleEventAsync(EntityCreatedEventData<UserEto> eventData)
        {
            var userIdentifer = new UserIdentifier(eventData.Entity.Id, eventData.Entity.UserName);
            // Subscribe to user welcome message
            await SubscribeInternalNotifers(userIdentifer, eventData.Entity.TenantId);

            await _notificationSender.SendNofiterAsync(
                    UserNotificationNames.WelcomeToApplication,
                    new NotificationTemplate(
                        UserNotificationNames.WelcomeToApplication,
                        formUser: eventData.Entity.UserName,
                        data: new Dictionary<string, object?>
                        {
                            { "name", eventData.Entity.UserName },
                        }),
                    userIdentifer,
                    eventData.Entity.TenantId,
                    NotificationSeverity.Info);
        }

        private async Task SubscribeInternalNotifers(UserIdentifier userIdentifer, Guid? tenantId = null)
        {
            // Subscribe to built-in notifications
            await _notificationSubscriptionManager
                .SubscribeAsync(
                    tenantId,
                    userIdentifer,
                    DefaultNotifications.SystemNotice);
            await _notificationSubscriptionManager
                .SubscribeAsync(
                    tenantId,
                    userIdentifer,
                    DefaultNotifications.OnsideNotice);
            await _notificationSubscriptionManager
                .SubscribeAsync(
                    tenantId,
                    userIdentifer,
                    DefaultNotifications.ActivityNotice);

            // Subscribe to user welcome message
            await _notificationSubscriptionManager
                .SubscribeAsync(
                    tenantId,
                    userIdentifer,
                    UserNotificationNames.WelcomeToApplication);
        }
    }
}
