using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Process node
/// </summary>
public class ApprovalProcessNode
{
    /// <summary>
    /// Node type 1: Approver 2: Copy person 3: Handler
    /// </summary>
    [NotNull]
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public ApprovalProcessNodeType Type { get; set; }
    /// <summary>
    /// Node status 1-approval; 2-agree; 3-reject; 4-transfer; 11-return to designated approver; 12-sign; 13-agree and sign; 14-process; 15-transfer
    /// </summary>
    [NotNull]
    [JsonProperty("sp_status")]
    [JsonPropertyName("sp_status")]
    public ApprovalProcessNodeStatus Status { get; set; }
    /// <summary>
    /// Multi-person approval method 1-countersign; 2-or sign 3-approval in sequence
    /// </summary>
    [CanBeNull]
    [JsonProperty("apv_rel")]
    [JsonPropertyName("apv_rel")]
    public byte? ApvRel { get; set; }
    /// <summary>
    /// Child node list
    /// </summary>
    [CanBeNull]
    [JsonProperty("sub_node_list")]
    [JsonPropertyName("sub_node_list")]
    public List<ApprovalProcessSubNode>? SubNodes { get; set; }
}
