using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Approval process information
/// </summary>
public class ApprovalSpRecord
{
    /// <summary>
    /// Approval node status
    /// </summary>
    [NotNull]
    [JsonProperty("sp_status")]
    [JsonPropertyName("sp_status")]
    public ApprovalSpRecordStatus SpStatus { get; set; }
    /// <summary>
    /// Node approval method
    /// </summary>
    [NotNull]
    [JsonProperty("approverattr")]
    [JsonPropertyName("approverattr")]
    public ApprovalApproverAttr ApproverAttr { get; set; }
    /// <summary>
    /// Approval node details, one approval node has multiple approvers
    /// </summary>
    [NotNull]
    [JsonProperty("details")]
    [JsonPropertyName("details")]
    public List<ApprovalSpRecordDetail> Details { get; set; } = default!;
}
