using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Response;
/// <summary>
/// Create approval template response parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/97437"/>
/// </remarks>
public class WeChatWorkCreateTemplateResponse : WeChatWorkResponse
{
    /// <summary>
    /// The template id returned after the template is successfully created
    /// </summary>
    [NotNull]
    [JsonProperty("template_id")]
    [JsonPropertyName("template_id")]
    public string TemplateId { get; set; } = default!;
}
