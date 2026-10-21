using System.ComponentModel;

namespace LINGYUN.Abp.WeChat.Work.Common.Members.Models;
/// <summary>
/// Video account status
/// </summary>
[Description("Video account status")]
public enum WechatChannelStatus
{
    /// <summary>
    /// Confirmed
    /// </summary>
    [Description("Confirmed")]
    Confirmed = 0,
    /// <summary>
    /// To be confirmed
    /// </summary>
    [Description("To be confirmed")]
    UnConfirmed = 1,
}
