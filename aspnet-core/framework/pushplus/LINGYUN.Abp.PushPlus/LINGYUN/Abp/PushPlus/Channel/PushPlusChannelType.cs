namespace LINGYUN.Abp.PushPlus.Channel;
/// <summary>
/// Sending channel (channel) enumeration
/// </summary>
public enum PushPlusChannelType
{
    /// <summary>
    /// WeChat public account
    /// </summary>
    WeChat = 0,
    /// <summary>
    /// Third-party webhook
    /// </summary>
    Webhook = 1,
    /// <summary>
    /// Enterprise WeChat application
    /// </summary>
    WeWork = 2,
    /// <summary>
    /// Email
    /// </summary>
    Email = 3,
    /// <summary>
    /// SMS
    /// </summary>
    Sms = 4
}
