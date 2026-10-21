using System.ComponentModel;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.GroupChats.Models;
/// <summary>
/// Customer group follow-up status filtering
/// </summary>
[Description("Customer group follow-up status filtering")]
public enum StatusFilter
{
    /// <summary>
    /// All lists
    /// </summary>
    [Description("All Lists")]
    All = 0,
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
