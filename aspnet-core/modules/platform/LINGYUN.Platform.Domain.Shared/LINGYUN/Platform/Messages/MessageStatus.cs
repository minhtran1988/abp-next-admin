namespace LINGYUN.Platform.Messages;
public enum MessageStatus
{
    /// <summary>
    /// Not sent
    /// </summary>
    Pending = -1,
    /// <summary>
    /// Sent
    /// </summary>
    Sent = 0,
    /// <summary>
    /// Sending failed
    /// </summary>
    Failed = 10
}
