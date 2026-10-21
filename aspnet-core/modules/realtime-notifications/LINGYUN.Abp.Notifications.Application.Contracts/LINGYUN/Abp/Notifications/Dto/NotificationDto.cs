namespace LINGYUN.Abp.Notifications;

public class NotificationDto
{
    /// <summary>
    /// Notification name
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// Display name
    /// </summary>
    public string? DisplayName { get; set; }
    /// <summary>
    /// Description
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// Survival type
    /// </summary>
    public NotificationLifetime Lifetime { get; set; }
    /// <summary>
    /// Notification type
    /// </summary>
    public NotificationType Type { get; set; }
    /// <summary>
    /// Notification content type
    /// </summary>
    public NotificationContentType ContentType { get; set; }
}
