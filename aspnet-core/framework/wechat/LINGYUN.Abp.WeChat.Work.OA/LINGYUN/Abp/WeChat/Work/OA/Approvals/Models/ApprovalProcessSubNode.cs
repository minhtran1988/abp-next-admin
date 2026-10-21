using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Child node
/// </summary>
public class ApprovalProcessSubNode
{
    /// <summary>
    /// Processor information
    /// </summary>
    [NotNull]
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string UserId { get; set; } = default!;
    /// <summary>
    /// Approval/Processing Opinions
    /// </summary>
    [NotNull]
    [JsonProperty("speech")]
    [JsonPropertyName("speech")]
    public string Speech { get; set; } = default!;
    /// <summary>
    /// Sub-node status 1-under approval; 2-agree; 3-reject; 4-transfer; 11-return to designated approver; 12-sign; 13-agree and sign; 14-process; 15-transfer
    /// </summary>
    [NotNull]
    [JsonProperty("sp_yj")]
    [JsonPropertyName("sp_yj")]
    public ApprovalProcessNodeStatus Status { get; set; }
    /// <summary>
    /// Operation time
    /// </summary>
    [NotNull]
    [JsonProperty("sptime")]
    [JsonPropertyName("sptime")]
    public long SpTime { get; set; }
    /// <summary>
    /// Attachment, microdisk file cannot be obtained
    /// </summary>
    [CanBeNull]
    [JsonProperty("media_ids")]
    [JsonPropertyName("media_ids")]
    public List<string>? MediaIds { get; set; }
}
