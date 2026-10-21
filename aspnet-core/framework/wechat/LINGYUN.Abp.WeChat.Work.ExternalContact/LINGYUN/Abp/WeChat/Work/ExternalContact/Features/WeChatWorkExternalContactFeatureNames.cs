using LINGYUN.Abp.WeChat.Work.Features;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Features;
/// <summary>
/// Enterprise WeChat customer contact module function
/// </summary>
public static class WeChatWorkExternalContactFeatureNames
{
    public const string GroupName = WeChatWorkFeatureNames.GroupName + ".ExternalContact";
    /// <summary>
    /// Enable enterprise WeChat customer contact
    /// </summary>
    public const string Enable = GroupName + ".Enable";
}
