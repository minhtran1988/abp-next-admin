using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Request;
/// <summary>
/// Create approval template request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/97437"/>
/// </remarks>
public class WeChatWorkCreateTemplateRequest : WeChatWorkRequest
{
    /// <summary>
    /// Template name array
    /// </summary>
    [NotNull]
    [JsonProperty("template_name")]
    [JsonPropertyName("template_name")]
    public List<TemplateName> TemplateName {  get; set; }
    /// <summary>
    /// Approval template control settings, consisting of multiple form controls and their contents, including information that needs to be assigned to the controls
    /// </summary>
    [NotNull]
    [JsonProperty("template_content")]
    [JsonPropertyName("template_content")]
    public TemplateContent TemplateContent { get; set; }

    public WeChatWorkCreateTemplateRequest(List<TemplateName> templateName, TemplateContent templateContent)
    {
        TemplateName = templateName;
        TemplateContent = templateContent;
    }
}
