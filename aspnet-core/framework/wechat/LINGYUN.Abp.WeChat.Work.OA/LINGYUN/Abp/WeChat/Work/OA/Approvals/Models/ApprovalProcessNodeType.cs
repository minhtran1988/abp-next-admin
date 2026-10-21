using System.ComponentModel;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Node type
/// </summary>
public enum ApprovalProcessNodeType : byte
{
    /// <summary>
    /// Approver
    /// </summary>
    [Description("Approver")]
    Approver = 1,
    /// <summary>
    /// CC person
    /// </summary>
    [Description("Cc")]
    CC = 2,
    /// <summary>
    /// handler
    /// </summary>
    [Description("handler")]
    Handler = 3,
}
