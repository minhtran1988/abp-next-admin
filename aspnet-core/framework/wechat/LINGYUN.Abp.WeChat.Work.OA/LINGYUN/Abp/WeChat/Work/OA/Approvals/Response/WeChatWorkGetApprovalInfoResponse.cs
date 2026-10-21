using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Response;
/// <summary>
/// Get approval order number response parameters in batches
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/91816"/>
/// </remarks>
public class WeChatWorkGetApprovalInfoResponse : WeChatWorkResponse
{
    /// <summary>
    /// Approval order number list, including approval applications that meet the conditions
    /// </summary>
    [NotNull]
    [JsonProperty("sp_no_list")]
    [JsonPropertyName("sp_no_list")]
    public List<string> SpNos { get; set; } = default!;
    /// <summary>
    /// The cursor for subsequent request queries. When the returned result does not have this field, it means that the approval form has been fetched.
    /// </summary>
    [CanBeNull]
    [JsonProperty("new_next_cursor")]
    [JsonPropertyName("new_next_cursor")]
    public string? NewNextCursor { get; set; }
}
