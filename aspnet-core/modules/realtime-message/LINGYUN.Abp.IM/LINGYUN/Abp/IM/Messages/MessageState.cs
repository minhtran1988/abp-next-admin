namespace LINGYUN.Abp.IM.Messages;

/// <summary>
/// Message status
/// </summary>
public enum MessageState : sbyte
{
    /// <summary>
    /// Sent
    /// </summary>
    Send = 0,
    /// <summary>
    /// Read
    /// </summary>
    Read = 1,
    /// <summary>
    /// Withdraw
    /// </summary>
    ReCall = 10,
    /// <summary>
    /// Sending failed
    /// </summary>
    Failed = 50,
    /// <summary>
    /// Return
    /// </summary>
    BackTo = 100
}
