using LINGYUN.Abp.Notifications;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using PackageName.CompanyName.ProjectName.AIO.Host.BackgroundJobs;
using PackageName.CompanyName.ProjectName.AIO.Host.MultiTenancy;
using System.Globalization;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Json;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;
using Volo.Abp.TextTemplating;
using Volo.Abp.Uow;

namespace PackageName.CompanyName.ProjectName.AIO.Host.EventBus.Distributed
{
    /// <summary>
    /// Subscribe to notification publishing events and publish messages uniformly
    /// </summary>
    /// <remarks>
    /// The purpose is that the SignalR client will only establish a connection with one server.
    /// Only the SignlR server can actually publish messages to the client.
    /// </remarks>
    public class NotificationEventHandler :
        IDistributedEventHandler<NotificationEto<NotificationData>>,
        IDistributedEventHandler<NotificationEto<NotificationTemplate>>,
        ITransientDependency
    {
        /// <summary>
        /// Reference to <see cref="ILogger<DefaultNotificationDispatcher>"/>.
        /// </summary>
        public ILogger<NotificationEventHandler> Logger { get; set; }
        /// <summary>
        /// Reference to <see cref="AbpNotificationsPublishOptions"/>.
        /// </summary>
        protected AbpNotificationsPublishOptions Options { get; }
        /// <summary>
        /// Reference to <see cref="ICurrentTenant"/>.
        /// </summary>
        protected ICurrentTenant CurrentTenant { get; }
        /// <summary>
        /// Reference to <see cref="ITenantConfigurationCache"/>.
        /// </summary>
        protected ITenantConfigurationCache TenantConfigurationCache { get; }
        /// <summary>
        /// Reference to <see cref="IJsonSerializer"/>.
        /// </summary>
        protected IJsonSerializer JsonSerializer { get; }
        /// <summary>
        /// Reference to <see cref="IBackgroundJobManager"/>.
        /// </summary>
        protected IBackgroundJobManager BackgroundJobManager { get; }
        /// <summary>
        /// Reference to <see cref="ITemplateRenderer"/>.
        /// </summary>
        protected ITemplateRenderer TemplateRenderer { get; }
        /// <summary>
        /// Reference to <see cref="INotificationStore"/>.
        /// </summary>
        protected INotificationStore NotificationStore { get; }
        /// <summary>
        /// Reference to <see cref="IStringLocalizerFactory"/>.
        /// </summary>
        protected IStringLocalizerFactory StringLocalizerFactory { get; }
        /// <summary>
        /// Reference to <see cref="INotificationDataSerializer"/>.
        /// </summary>
        protected INotificationDataSerializer NotificationDataSerializer { get; }
        /// <summary>
        /// Reference to <see cref="INotificationDefinitionManager"/>.
        /// </summary>
        protected INotificationDefinitionManager NotificationDefinitionManager { get; }
        /// <summary>
        /// Reference to <see cref="INotificationSubscriptionManager"/>.
        /// </summary>
        protected INotificationSubscriptionManager NotificationSubscriptionManager { get; }
        /// <summary>
        /// Reference to <see cref="INotificationPublishProviderManager"/>.
        /// </summary>
        protected INotificationPublishProviderManager NotificationPublishProviderManager { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationEventHandler"/> class.
        /// </summary>
        public NotificationEventHandler(
            ICurrentTenant currentTenant,
            ITenantConfigurationCache tenantConfigurationCache,
            IJsonSerializer jsonSerializer,
            ITemplateRenderer templateRenderer,
            IBackgroundJobManager backgroundJobManager,
            IStringLocalizerFactory stringLocalizerFactory,
            IOptions<AbpNotificationsPublishOptions> options,
            INotificationStore notificationStore,
            INotificationDataSerializer notificationDataSerializer,
            INotificationDefinitionManager notificationDefinitionManager,
            INotificationSubscriptionManager notificationSubscriptionManager,
            INotificationPublishProviderManager notificationPublishProviderManager)
        {
            Options = options.Value;
            TenantConfigurationCache = tenantConfigurationCache;
            CurrentTenant = currentTenant;
            JsonSerializer = jsonSerializer;
            TemplateRenderer = templateRenderer;
            BackgroundJobManager = backgroundJobManager;
            StringLocalizerFactory = stringLocalizerFactory;
            NotificationStore = notificationStore;
            NotificationDataSerializer = notificationDataSerializer;
            NotificationDefinitionManager = notificationDefinitionManager;
            NotificationSubscriptionManager = notificationSubscriptionManager;
            NotificationPublishProviderManager = notificationPublishProviderManager;

            Logger = NullLogger<NotificationEventHandler>.Instance;
        }

        [UnitOfWork]
        public async virtual Task HandleEventAsync(NotificationEto<NotificationTemplate> eventData)
        {
            var notification = await NotificationDefinitionManager.GetOrNullAsync(eventData.Name);
            if (notification == null)
            {
                return;
            }

            var culture = eventData.Data.Culture;
            if (culture.IsNullOrWhiteSpace())
            {
                culture = CultureInfo.CurrentCulture.Name;
            }
            using (CultureHelper.Use(culture, culture))
            {
                if (notification.NotificationType == NotificationType.System)
                {
                    using (CurrentTenant.Change(null))
                    {
                        await SendToTenantAsync(null, notification, eventData);

                        var allActiveTenants = await TenantConfigurationCache.GetTenantsAsync();

                        foreach (var activeTenant in allActiveTenants)
                        {
                            await SendToTenantAsync(activeTenant.Id, notification, eventData);
                        }
                    }
                }
                else
                {
                    await SendToTenantAsync(eventData.TenantId, notification, eventData);
                }
            }
        }

        [UnitOfWork]
        public async virtual Task HandleEventAsync(NotificationEto<NotificationData> eventData)
        {
            var notification = await NotificationDefinitionManager.GetOrNullAsync(eventData.Name);
            if (notification == null)
            {
                return;
            }

            if (notification.NotificationType == NotificationType.System)
            {
                using (CurrentTenant.Change(null))
                {
                    await SendToTenantAsync(null, notification, eventData);

                    var allActiveTenants = await TenantConfigurationCache.GetTenantsAsync();

                    foreach (var activeTenant in allActiveTenants)
                    {
                        await SendToTenantAsync(activeTenant.Id, notification, eventData);
                    }
                }
            }
            else
            {
                await SendToTenantAsync(eventData.TenantId, notification, eventData);
            }
        }

        protected async virtual Task SendToTenantAsync(
            Guid? tenantId,
            NotificationDefinition notification,
            NotificationEto<NotificationTemplate> eventData)
        {
            using (CurrentTenant.Change(tenantId))
            {
                var providers = Enumerable.Reverse(NotificationPublishProviderManager.Providers);

                // Filter user-specified providers
                if (eventData.UseProviders.Any())
                {
                    providers = providers.Where(p => eventData.UseProviders.Contains(p.Name));
                }
                else if (notification.Providers.Any())
                {
                    providers = providers.Where(p => notification.Providers.Contains(p.Name));
                }

                var notificationInfo = new NotificationInfo
                {
                    Name = notification.Name,
                    TenantId = tenantId,
                    Severity = eventData.Severity,
                    Type = notification.NotificationType,
                    ContentType = notification.ContentType,
                    CreationTime = eventData.CreationTime,
                    Lifetime = notification.NotificationLifetime,
                };
                notificationInfo.SetId(eventData.Id);

                var title = notification.DisplayName.Localize(StringLocalizerFactory);
                var message = "";

                try
                {
                    // Since template notifications are affected by the tenant, messages that fail to be formatted will be discarded.
                    message = await TemplateRenderer.RenderAsync(
                        templateName: eventData.Data.Name,
                        model: eventData.Data.ExtraProperties,
                        cultureName: eventData.Data.Culture,
                        globalContext: new Dictionary<string, object>
                        {
                            // The template does not support the $ character, change it to a normal keyword
                            { NotificationKeywords.Name, notification.Name },
                            { NotificationKeywords.FormUser, eventData.Data.FormUser },
                            { NotificationKeywords.Id, eventData.Id },
                            { NotificationKeywords.Title, title.ToString() },
                            { NotificationKeywords.CreationTime, eventData.CreationTime.ToString(Options.DateTimeFormat) },
                        });
                }
                catch(Exception ex)
                {
                    Logger.LogWarning("Formatting template notification failed, message will be discarded, cause :{message}", ex.Message);
                    return;
                }

                var notificationData = new NotificationData();
                notificationData.WriteStandardData(
                    title: title.ToString(),
                    message: message,
                    createTime: eventData.CreationTime,
                    formUser: eventData.Data.FormUser);
                notificationData.ExtraProperties.AddIfNotContains(eventData.Data.ExtraProperties);

                notificationInfo.Data = notificationData;

                var subscriptionUsers = await GerSubscriptionUsersAsync(
                    notificationInfo.Name,
                    eventData.Users,
                    tenantId);

                await PersistentNotificationAsync(
                    notificationInfo,
                    subscriptionUsers,
                    providers);

                if (subscriptionUsers.Any())
                {
                    // Publish notification
                    foreach (var provider in providers)
                    {
                        await PublishToSubscriberAsync(provider, notificationInfo, subscriptionUsers);
                    }
                }
            }
        }

        protected async virtual Task SendToTenantAsync(
            Guid? tenantId,
            NotificationDefinition notification,
            NotificationEto<NotificationData> eventData)
        {
            using (CurrentTenant.Change(tenantId))
            {
                var providers = Enumerable.Reverse(NotificationPublishProviderManager.Providers);

                // Filter user-specified providers
                if (eventData.UseProviders.Any())
                {
                    providers = providers.Where(p => eventData.UseProviders.Contains(p.Name));
                }
                else if (notification.Providers.Any())
                {
                    providers = providers.Where(p => notification.Providers.Contains(p.Name));
                }

                var notificationInfo = new NotificationInfo
                {
                    Name = notification.Name,
                    CreationTime = eventData.CreationTime,
                    Data = eventData.Data,
                    Severity = eventData.Severity,
                    Lifetime = notification.NotificationLifetime,
                    TenantId = tenantId,
                    Type = notification.NotificationType,
                    ContentType = notification.ContentType,
                };
                notificationInfo.SetId(eventData.Id);

                notificationInfo.Data = NotificationDataSerializer.Serialize(notificationInfo.Data);

                // Get user subscription
                var subscriptionUsers = await GerSubscriptionUsersAsync(
                    notificationInfo.Name,
                    eventData.Users,
                    tenantId);

                // Persistence notification
                await PersistentNotificationAsync(
                    notificationInfo,
                    subscriptionUsers,
                    providers);

                if (subscriptionUsers.Any())
                {
                    // Publish subscription notification
                    foreach (var provider in providers)
                    {
                        await PublishToSubscriberAsync(provider, notificationInfo, subscriptionUsers);
                    }
                }
            }
        }
        /// <summary>
        /// Get user subscription list
        /// </summary>
        /// <param name="notificationName">Notification name</param>
        /// <param name="sendToUsers">Receive user list</param>
        /// <param name="tenantId">Tenant ID</param>
        /// <returns>User subscription list</returns>
        protected async Task<IEnumerable<UserIdentifier>> GerSubscriptionUsersAsync(
            string notificationName,
            IEnumerable<UserIdentifier> sendToUsers,
            Guid? tenantId = null)
        {
            try
            {
                // Get user subscription list
                var userSubscriptions = await NotificationSubscriptionManager.GetUsersSubscriptionsAsync(
                    tenantId,
                    notificationName,
                    sendToUsers);

                return userSubscriptions.Select(us => new UserIdentifier(us.UserId, us.UserName));
            }
            catch(Exception ex)
            {
                Logger.LogWarning("Failed to get user subscription, message will not be received by the user, reason: {message}", ex.Message);
            }

            return new List<UserIdentifier>();
        }
        /// <summary>
        /// Persist the notification and return the subscriber list
        /// </summary>
        /// <param name="notificationInfo">Notification entity</param>
        /// <param name="subscriptionUsers">Subscription user list</param>
        /// <param name="sendToProviders">Notify sending providers</param>
        /// <returns>Return the subscriber list</returns>
        protected async Task PersistentNotificationAsync(
            NotificationInfo notificationInfo,
            IEnumerable<UserIdentifier> subscriptionUsers,
            IEnumerable<INotificationPublishProvider> sendToProviders)
        {
            try
            {
                // Persistence notification
                await NotificationStore.InsertNotificationAsync(notificationInfo);

                if (!subscriptionUsers.Any())
                {
                    return;
                }

                // Persistent user notification
                await NotificationStore.InsertUserNotificationsAsync(notificationInfo, subscriptionUsers.Select(u => u.UserId));

                if (notificationInfo.Lifetime == NotificationLifetime.OnlyOne)
                {
                    // One-time notification to cancel user subscription
                    await NotificationStore.DeleteUserSubscriptionAsync(
                        notificationInfo.TenantId,
                        subscriptionUsers,
                        notificationInfo.Name);
                }
            }
            catch (Exception ex)
            {
                Logger.LogWarning("Failed to persistent notification failed, reason: {message}", ex.Message);

                foreach (var provider in sendToProviders)
                {
                    // Process persistence failure and enter the background queue
                    await ProcessingFailedToQueueAsync(provider, notificationInfo, subscriptionUsers);
                }
            }
        }
        /// <summary>
        /// Publish subscriber notification
        /// </summary>
        /// <param name="provider">Notify publisher</param>
        /// <param name="notificationInfo">Notification information</param>
        /// <param name="subscriptionUserIdentifiers">Subscription user list</param>
        /// <returns></returns>
        protected async Task PublishToSubscriberAsync(
            INotificationPublishProvider provider,
            NotificationInfo notificationInfo,
            IEnumerable<UserIdentifier> subscriptionUsers)
        {
            try
            {
                Logger.LogDebug($"Sending notification with provider {provider.Name}");

                // 2024-10-10: The framework level should cancel notification data conversion and instead leave it to the provider to implement
                // var notifacationDataMapping = Options.NotificationDataMappings
                //        .GetMapItemOrDefault(provider.Name, notificationInfo.Name);
                // if (notifacationDataMapping != null)
                //{
                //    notificationInfo.Data = notifacationDataMapping.MappingFunc(notificationInfo.Data);
                //}

                // publish
                await provider.PublishAsync(notificationInfo, subscriptionUsers);

                Logger.LogDebug($"Send notification {notificationInfo.Name} with provider {provider.Name} was successful");
            }
            catch (Exception ex)
            {
                Logger.LogWarning($"Send notification error with provider {provider.Name}");
                Logger.LogWarning($"Error message:{ex.Message}");
                Logger.LogDebug($"Failed to send notification {notificationInfo.Name}. Try to push notification to background job");
                // Messages that failed to be sent enter the background queue
                await ProcessingFailedToQueueAsync(provider, notificationInfo, subscriptionUsers);
            }
        }
        /// <summary>
        /// Messages that failed to be processed enter the background queue
        /// </summary>
        /// <remarks>
        /// Note: If enqueueing fails, the message will be discarded.
        /// </remarks>
        /// <param name="provider"></param>
        /// <param name="notificationInfo"></param>
        /// <param name="subscriptionUsers"></param>
        /// <returns></returns>
        protected async Task ProcessingFailedToQueueAsync(
            INotificationPublishProvider provider,
            NotificationInfo notificationInfo,
            IEnumerable<UserIdentifier> subscriptionUsers)
        {
            try
            {
                // Messages that failed to be sent enter the background queue
                await BackgroundJobManager.EnqueueAsync(
                    new NotificationPublishJobArgs(
                        notificationInfo.GetId(),
                        provider.GetType().AssemblyQualifiedName,
                        subscriptionUsers.ToList(),
                        notificationInfo.TenantId));
            }
            catch(Exception ex)
            {
                Logger.LogWarning("Failed to push to background job, notification will be discarded, error cause: {message}", ex.Message);
            }
        }
    }
}
