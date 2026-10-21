using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Response;
/// <summary>
/// Get approval application details response parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/91983"/>
/// </remarks>
public class WeChatWorkGetApprovalDetailResponse : WeChatWorkResponse
{
    /// <summary>
    /// Approval application details
    /// </summary>
    [NotNull]
    [JsonProperty("info")]
    [JsonPropertyName("info")]
    public ApprovalDetailInfo Info { get; set; } = default!;
}
