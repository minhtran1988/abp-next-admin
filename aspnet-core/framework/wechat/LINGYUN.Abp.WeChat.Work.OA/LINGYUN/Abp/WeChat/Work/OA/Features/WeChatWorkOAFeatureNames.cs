using LINGYUN.Abp.WeChat.Work.Features;

namespace LINGYUN.Abp.WeChat.Work.OA.Features;
/// <summary>
/// Enterprise WeChat office module function
/// </summary>
public static class WeChatWorkOAFeatureNames
{
    public const string GroupName = WeChatWorkFeatureNames.GroupName + ".OA";
    /// <summary>
    /// Enable corporate WeChat office
    /// </summary>
    public const string Enable = GroupName + ".Enable";
}
