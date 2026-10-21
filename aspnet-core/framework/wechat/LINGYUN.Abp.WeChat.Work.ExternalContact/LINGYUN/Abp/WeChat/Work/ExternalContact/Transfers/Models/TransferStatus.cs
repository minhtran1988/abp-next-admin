using System.ComponentModel;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Transfers.Models;
/// <summary>
/// Take over status
/// </summary>
[Description("Replacement status")]
public enum TransferStatus
{
    /// <summary>
    /// Succession completed
    /// </summary>
    [Description("Replacement completed")]
    Completed = 1,
    /// <summary>
    /// Waiting for replacement
    /// </summary>
    [Description("Waiting for replacement")]
    Pending = 2,
    /// <summary>
    /// Customer rejects
    /// </summary>
    [Description("Customer Rejected")]
    CustomerReject = 3,
    /// <summary>
    /// Replacement member customers reach the upper limit
    /// </summary>
    [Description("Replacement member customers have reached the upper limit")]
    ReachesLimit = 4
}
