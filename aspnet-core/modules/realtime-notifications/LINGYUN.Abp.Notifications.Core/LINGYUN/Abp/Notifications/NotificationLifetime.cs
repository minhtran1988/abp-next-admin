namespace LINGYUN.Abp.Notifications;

/// <summary>
/// Notification survival time
/// Cancel user subscription after sending, similar to WeChat applet
/// </summary>
public enum NotificationLifetime
{
    /// <summary>
    /// Persistence
    /// </summary>
    Persistent = 0,
    /// <summary>
    /// Disposable
    /// </summary>
    OnlyOne = 1
}
