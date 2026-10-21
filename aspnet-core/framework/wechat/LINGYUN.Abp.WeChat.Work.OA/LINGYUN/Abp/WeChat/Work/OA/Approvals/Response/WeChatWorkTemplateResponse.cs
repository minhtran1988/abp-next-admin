using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Response;
/// <summary>
/// Approval template response parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/91982"/>
/// </remarks>
public class WeChatWorkTemplateResponse : WeChatWorkResponse
{
    /// <summary>
    /// Template name array
    /// </summary>
    [NotNull]
    [JsonProperty("template_names")]
    [JsonPropertyName("template_names")]
    public List<TemplateName> TemplateNames { get; set; } = default!;
    /// <summary>
    /// Approval template control settings, consisting of multiple form controls and their contents, including information that needs to be assigned to the controls
    /// </summary>
    [NotNull]
    [JsonProperty("template_content")]
    [JsonPropertyName("template_content")]
    public TemplateContent TemplateContent { get; set; } = default!;
    public WeChatWorkTemplateResponse()
    {

    }

    public WeChatWorkTemplateResponse(List<TemplateName> templateNames, TemplateContent templateContent)
    {
        TemplateNames = templateNames;
        TemplateContent = templateContent;
    }
}
