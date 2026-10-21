using System.ComponentModel;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Customers.Models;
/// <summary>
/// Add scene to video number
/// </summary>
[Description("Add scene to video number")]
public enum FollowUserWechatChannelSource
{
    /// <summary>
    /// Unknown
    /// </summary>
    [Description("Unknown")]
    None = 0,
    /// <summary>
    /// Video account homepage
    /// </summary>
    [Description("Video account homepage")]
    Home = 1,
    /// <summary>
    /// Video account live room
    /// </summary>
    [Description("Video Account Live Room")]
    LiveRoom = 2,
    /// <summary>
    /// Video account capital retention service
    /// </summary>
    [Description("Video Account Retention Service")]
    RetentionService = 3
}
