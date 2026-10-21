using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Approval application remark information
/// </summary>
public class ApprovalComment
{
    /// <summary>
    /// Note person information
    /// </summary>
    [CanBeNull]
    [JsonProperty("commentUserInfo")]
    [JsonPropertyName("commentUserInfo")]
    public ApprovalUser? CommentUserInfo { get; set; }
    /// <summary>
    /// Approval application submission time, Unix timestamp
    /// </summary>
    [NotNull]
    [JsonProperty("commenttime")]
    [JsonPropertyName("commenttime")]
    public long CommentTime { get; set; }
    /// <summary>
    /// Remark text content
    /// </summary>
    [NotNull]
    [JsonProperty("commentcontent")]
    [JsonPropertyName("commentcontent")]
    public string CommentContent { get; set; } = default!;
    /// <summary>
    /// Remark id
    /// </summary>
    [NotNull]
    [JsonProperty("commentid")]
    [JsonPropertyName("commentid")]
    public string CommentId { get; set; } = default!;
    /// <summary>
    /// Note attachment id, there may be multiple, the microdisk file cannot be obtained
    /// </summary>
    [CanBeNull]
    [JsonProperty("media_id")]
    [JsonPropertyName("media_id")]
    public List<string>? MediaId { get; set; }
}
