using System.ComponentModel;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Node status
/// </summary>
public enum ApprovalProcessNodeStatus : byte
{
    /// <summary>
    /// Under approval
    /// </summary>
    [Description("Under Approval")]
    UnderApproval = 1,
    /// <summary>
    /// Agree
    /// </summary>
    [Description("Agree")]
    Agree = 2,
    /// <summary>
    /// Reject
    /// </summary>
    [Description("Reject")]
    Rejection = 3,
    /// <summary>
    /// Transfer
    /// </summary>
    [Description("Transfer")]
    TransferForReview = 4,
    /// <summary>
    /// Return to designated approver
    /// </summary>
    [Description("Return to designated approver")]
    ReturnToApprover = 11,
    /// <summary>
    /// Add signature
    /// </summary>
    [Description("signature")]
    Additional = 12,
    /// <summary>
    /// Agree and sign
    /// </summary>
    [Description("Agree and sign")]
    AgreeAndSign = 13,
    /// <summary>
    /// Process
    /// </summary>
    [Description("handle")]
    Processing = 14,
    /// <summary>
    /// Transfer
    /// </summary>
    [Description("transfer")]
    Transfer = 15,
}
