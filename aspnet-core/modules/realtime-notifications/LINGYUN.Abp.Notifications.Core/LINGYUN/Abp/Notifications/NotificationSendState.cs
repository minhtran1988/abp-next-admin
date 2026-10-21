namespace LINGYUN.Abp.Notifications;
/// <summary>
/// Send status
/// </summary>
public enum NotificationSendState
{
    /// <summary>
    /// Not sent
    /// </summary>
    None,
    /// <summary>
    /// Provider disabled
    /// </summary>
    Disabled,
    /// <summary>
    /// Sent
    /// </summary>
    Sent,
    /// <summary>
    /// Sending failed
    /// </summary>
    Failed
}
