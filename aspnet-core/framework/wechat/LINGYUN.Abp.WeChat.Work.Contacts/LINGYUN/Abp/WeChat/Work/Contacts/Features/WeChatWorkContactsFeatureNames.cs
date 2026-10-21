using LINGYUN.Abp.WeChat.Work.Features;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Features;
/// <summary>
/// Enterprise WeChat address book module function
/// </summary>
public static class WeChatWorkContactsFeatureNames
{
    public const string GroupName = WeChatWorkFeatureNames.GroupName + ".Contacts";
    /// <summary>
    /// Enable corporate WeChat address book
    /// </summary>
    public const string Enable = GroupName + ".Enable";
}
