using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.Notifications;

/// <summary>
/// Notification subscription manager
/// </summary>
public interface INotificationSubscriptionManager
{
    /// <summary>
    /// Whether you have subscribed
    /// </summary>
    /// <param name="tenantId">Tenant</param>
    /// <param name="userId">User ID</param>
    /// <param name="notificationName">Notification name</param>
    /// <returns></returns>
    Task<bool> IsSubscribedAsync(
        Guid? tenantId, 
        Guid userId, 
        string notificationName,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Subscribe to notifications
    /// </summary>
    /// <param name="tenantId">Tenant</param>
    /// <param name="identifier">User ID</param>
    /// <param name="notificationName">Notification name</param>
    /// <returns></returns>
    Task SubscribeAsync(
        Guid? tenantId,
        UserIdentifier identifier, 
        string notificationName,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Subscribe to notifications
    /// </summary>
    /// <param name="tenantId">Tenant</param>
    /// <param name="identifiers">User ID list</param>
    /// <param name="notificationName">Notification name</param>
    /// <returns></returns>
    Task SubscribeAsync(
        Guid? tenantId, 
        IEnumerable<UserIdentifier> identifiers,
        string notificationName,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Cancel all user subscriptions
    /// </summary>
    /// <param name="tenantId">Tenant</param>
    /// <param name="notificationName">Notification name</param>
    /// <returns></returns>
    Task UnsubscribeAllAsync(
        Guid? tenantId, 
        string notificationName,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Unsubscribe
    /// </summary>
    /// <param name="tenantId">Tenant</param>
    /// <param name="identifier">User ID</param>
    /// <param name="notificationName">Notification name</param>
    /// <returns></returns>
    Task UnsubscribeAsync(
        Guid? tenantId, 
        UserIdentifier identifier, 
        string notificationName,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Unsubscribe
    /// </summary>
    /// <param name="tenantId">Tenant</param>
    /// <param name="identifiers">User ID list</param>
    /// <param name="notificationName">Notification name</param>
    /// <returns></returns>
    Task UnsubscribeAsync(
        Guid? tenantId, 
        IEnumerable<UserIdentifier> identifiers,
        string notificationName,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get the list of users who are subscribed to the notification
    /// </summary>
    /// <param name="tenantId">Tenant</param>
    /// <param name="notificationName">Notification name</param>
    /// <param name="identifiers">List of users to be checked</param>
    /// <returns></returns>
    Task<List<NotificationSubscriptionInfo>> GetUsersSubscriptionsAsync(
        Guid? tenantId, 
        string notificationName, 
        IEnumerable<UserIdentifier>? identifiers = null,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get user subscription list
    /// </summary>
    /// <param name="tenantId">Tenant</param>
    /// <param name="userId">User ID</param>
    /// <returns></returns>
    Task<List<NotificationSubscriptionInfo>> GetUserSubscriptionsAsync(
        Guid? tenantId, 
        Guid userId,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get user subscription list
    /// </summary>
    /// <param name="tenantId">Tenant</param>
    /// <param name="userName">Username</param>
    /// <returns></returns>
    Task<List<NotificationSubscriptionInfo>> GetUserSubscriptionsAsync(
        Guid? tenantId, 
        string userName,
        CancellationToken cancellationToken = default);
}
