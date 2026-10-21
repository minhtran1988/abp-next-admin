namespace LINGYUN.Abp.Notifications;

/// <summary>
/// Severity level
/// </summary>
public enum NotificationSeverity : sbyte
{
    /// <summary>
    /// Success
    /// </summary>
    Success = 0,
    /// <summary>
    /// information
    /// </summary>
    Info = 10,
    /// <summary>
    /// warning
    /// </summary>
    Warn = 20,
    /// <summary>
    /// Error
    /// </summary>
    Error = 30,
    /// <summary>
    /// Fatal error
    /// </summary>
    Fatal = 40
}
