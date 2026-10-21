namespace LINGYUN.Abp.PushPlus.Message;

public enum PushPlusMessageStatus
{
    /// <summary>
    /// Not sent
    /// </summary>
    NotSend = 0,
    /// <summary>
    /// Sending
    /// </summary>
    Sending = 1,
    /// <summary>
    /// Sent
    /// </summary>
    Successed = 2,
    /// <summary>
    /// Sending failed
    /// </summary>
    Failed = 3,
}
