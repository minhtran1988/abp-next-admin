namespace LINGYUN.Abp.Notifications;

/// <summary>
/// Notification type
/// </summary>
public enum NotificationType
{
    /// <summary>
    /// Apply (only to current tenant)
    /// </summary>
    Application = 0,
    /// <summary>
    /// System notification (global release)
    /// </summary>
    System = 10,
    /// <summary>
    /// User (corresponding to user, controlled by tenant)
    /// </summary>
    User = 20,
    /// <summary>
    /// Server callback, user should not process it
    /// </summary>
    ServiceCallback = 30,
}
