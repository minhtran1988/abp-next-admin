using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Template name definition
/// </summary>
public class TemplateName
{
    /// <summary>
    /// Template name
    /// </summary>
    [NotNull]
    [JsonProperty("text")]
    [JsonPropertyName("text")]
    public string Text {  get; set; } = default!;
    /// <summary>
    /// Multi-language name
    /// </summary>
    [NotNull]
    [JsonProperty("lang")]
    [JsonPropertyName("lang")]
    public string Lang { get; set; } = default!;
    public TemplateName()
    {

    }

    public TemplateName(string text, string lang = "zh_CN")
    {
        Text = text;
        Lang = lang;
    }
}
