using System.ComponentModel;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Node approval method
/// </summary>
public enum ApprovalApproverAttr : byte
{
    /// <summary>
    /// Or sign
    /// </summary>
    [Description("or signature")]
    OrSign = 1,
    /// <summary>
    /// Countersign
    /// </summary>
    [Description("Countersign")]
    CoSign = 2,
}
