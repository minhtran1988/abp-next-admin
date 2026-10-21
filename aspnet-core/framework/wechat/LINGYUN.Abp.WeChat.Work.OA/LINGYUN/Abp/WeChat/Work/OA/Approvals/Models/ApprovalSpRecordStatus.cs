using System.ComponentModel;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Approval node status
/// </summary>
public enum ApprovalSpRecordStatus : byte
{
    /// <summary>
    /// Under approval
    /// </summary>
    [Description("Under Approval")]
    UnderApproval = 1,
    /// <summary>
    /// Agreed
    /// </summary>
    [Description("Agreed")]
    Passed = 2,
    /// <summary>
    /// Rejected
    /// </summary>
    [Description("Rejected")]
    Rejected = 3,
    /// <summary>
    /// Referred for review
    /// </summary>
    [Description("Referred for review")]
    Transferred = 4,
    /// <summary>
    /// Returned
    /// </summary>
    [Description("Returned")]
    Returned = 11,
    /// <summary>
    /// Signed
    /// </summary>
    [Description("signed")]
    Signed = 12,
    /// <summary>
    /// Agreed and signed
    /// </summary>
    [Description("Agreed and signed")]
    AgreedAndSigned = 13,
}
