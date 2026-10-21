using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Request;
/// <summary>
/// Get approval template request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/91982"/>
/// </remarks>
public class WeChatWorkGetTemplateRequest : WeChatWorkRequest
{
    /// <summary>
    /// template id
    /// </summary>
    [NotNull]
    [JsonProperty("template_id")]
    [JsonPropertyName("template_id")]
    public string TemplateId { get; set; }
    public WeChatWorkGetTemplateRequest(string templateId)
    {
        TemplateId = templateId;
    }
}
