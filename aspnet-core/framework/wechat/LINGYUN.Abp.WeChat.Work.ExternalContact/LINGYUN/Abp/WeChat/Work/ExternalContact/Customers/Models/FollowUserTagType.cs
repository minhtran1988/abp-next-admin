using System.ComponentModel;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Customers.Models;
/// <summary>
/// Tag type
/// </summary>
[Description("Tag Type")]
public enum FollowUserTagType
{
    /// <summary>
    /// Enterprise settings
    /// </summary>
    [Description("Enterprise Settings")]
    EnterpriseSettings = 1,
    /// <summary>
    /// User-defined
    /// </summary>
    [Description("User-defined")]
    UserCustom = 2,
    /// <summary>
    /// Rule group label
    /// </summary>
    [Description("Rule Group Label")]
    RuleGroupTags = 3,
}
