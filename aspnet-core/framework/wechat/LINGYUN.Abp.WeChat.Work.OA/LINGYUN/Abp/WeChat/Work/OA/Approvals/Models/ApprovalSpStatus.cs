using System.ComponentModel;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Application status
/// </summary>
public enum ApprovalSpStatus : byte
{
    /// <summary>
    /// Under approval
    /// </summary>
    [Description("Under Approval")]
    UnderApproval = 1,
    /// <summary>
    /// Passed
    /// </summary>
    [Description("Passed")]
    Passed = 2,
    /// <summary>
    /// Rejected
    /// </summary>
    [Description("Rejected")]
    Rejected = 3,
    /// <summary>
    /// Canceled
    /// </summary>
    [Description("Revoked")]
    Revoked = 4,
    /// <summary>
    /// Cancel after passing
    /// </summary>
    [Description("Cancel after passing")]
    RevokeAfterApproval = 6,
    /// <summary>
    /// Deleted
    /// </summary>
    [Description("Deleted")]
    Deleted = 7,
    /// <summary>
    /// Paid
    /// </summary>
    [Description("Paid")]
    Paid = 10,
}
