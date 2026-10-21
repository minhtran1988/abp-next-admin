using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Approval node details
/// </summary>
public class ApprovalSpRecordDetail
{
    /// <summary>
    /// Branch approver
    /// </summary>
    [NotNull]
    [JsonProperty("approver")]
    [JsonPropertyName("approver")]
    public ApprovalUser Approver { get; set; } = default!;
    /// <summary>
    /// Approval comments
    /// </summary>
    [NotNull]
    [JsonProperty("speech")]
    [JsonPropertyName("speech")]
    public string Speech { get; set; } = default!;
    /// <summary>
    /// Branch approver approval status
    /// </summary>
    [NotNull]
    [JsonProperty("sp_status")]
    [JsonPropertyName("sp_status")]
    public ApprovalSpRecordStatus SpStatus { get; set; }
    /// <summary>
    /// Node branch approver's approval operation timestamp, 0 means no operation
    /// </summary>
    [NotNull]
    [JsonProperty("sptime")]
    [JsonPropertyName("sptime")]
    public long SpTime { get; set; }
    /// <summary>
    /// Attachment of approval opinions of node branch approver, microdisk file cannot be obtained
    /// </summary>
    [CanBeNull]
    [JsonProperty("media_id")]
    [JsonPropertyName("media_id")]
    public List<string>? MediaId { get; set; }
}
