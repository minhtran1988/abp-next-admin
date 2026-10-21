using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Response;
/// <summary>
/// Submit approval application response parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/91853"/>
/// </remarks>
public class WeChatWorkApplyEventResponse : WeChatWorkResponse
{
    /// <summary>
    /// After the form is submitted successfully, the form number returned
    /// </summary>
    [NotNull]
    [JsonProperty("sp_no")]
    [JsonPropertyName("sp_no")]
    public string SpNo { get; set; } = default!;
}
