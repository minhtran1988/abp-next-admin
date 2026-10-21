using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Request;
/// <summary>
/// Get approval application details request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/91983"/>
/// </remarks>
public class WeChatWorkGetApprovalDetailRequest : WeChatWorkRequest
{
    /// <summary>
    /// Approval order number
    /// </summary>
    [NotNull]
    [JsonProperty("sp_no")]
    [JsonPropertyName("sp_no")]
    public string SpNo { get; set; }
    public WeChatWorkGetApprovalDetailRequest(string spNo)
    {
        SpNo = spNo;
    }
}
