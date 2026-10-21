namespace LINGYUN.Abp.PushPlus.Setting;
/// <summary>
/// Send limit;
/// 0 - no limit,
/// 1-Disable sending through all channels,
/// 2-Restrict WeChat channels,
/// 3-Restrict email channels
/// </summary>
public enum PushPlusChannelSendLimit
{
    /// <summary>
    /// Unlimited
    /// </summary>
    None = 0,
    /// <summary>
    /// Disable sending through all channels
    /// </summary>
    ForbidAllChannel = 1,
    /// <summary>
    /// Restrict WeChat channels
    /// </summary>
    ForbidWeChatChannel = 2,
    /// <summary>
    /// Restrict email channels
    /// </summary>
    ForbidEmailChannel = 3,
}
