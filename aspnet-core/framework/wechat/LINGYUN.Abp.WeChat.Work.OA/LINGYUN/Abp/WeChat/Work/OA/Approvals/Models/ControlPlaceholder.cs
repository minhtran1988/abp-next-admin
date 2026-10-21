using JetBrains.Annotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Control description
/// </summary>
public class ControlPlaceholder
{
    /// <summary>
    /// Control description. The following conditions must be met: the length must not exceed 80 characters.
    /// </summary>
    [NotNull]
    [StringLength(80)]
    [JsonProperty("text")]
    [JsonPropertyName("text")]
    public string Text { get; set; } = default!;
    /// <summary>
    /// Display language, Chinese: zh_CN (note not zh-CN); if text is filled in, this item is required
    /// </summary>
    [NotNull]
    [JsonProperty("lang")]
    [JsonPropertyName("lang")]
    public string Lang { get; set; } = default!;
    public ControlPlaceholder()
    {

    }

    public ControlPlaceholder(string text, string lang = "zh_CN")
    {
        Text = text;
        Lang = lang;
    }
}
