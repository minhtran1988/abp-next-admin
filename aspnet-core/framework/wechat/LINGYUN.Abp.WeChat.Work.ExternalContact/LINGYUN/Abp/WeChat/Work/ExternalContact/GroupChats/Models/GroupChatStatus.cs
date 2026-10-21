using System.ComponentModel;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.GroupChats.Models;
/// <summary>
/// Customer group follow-up status
/// </summary>
[Description("Customer group follow-up status")]
public enum GroupChatStatus
{
    /// <summary>
    /// Following people is normal
    /// </summary>
    [Description("Following up people is normal")]
    Normal = 0,
    /// <summary>
    /// Follow up on the person’s resignation
    /// </summary>
    [Description("Follow up person's resignation")]
    Leaves = 1,
    /// <summary>
    /// Inheritance after resignation
    /// </summary>
    [Description("Resignation and inheritance")]
    Resiging = 2,
    /// <summary>
    /// Resignation inheritance completed
    /// </summary>
    [Description("Resignation inheritance completed")]
    ResignCompleted = 3,
}
