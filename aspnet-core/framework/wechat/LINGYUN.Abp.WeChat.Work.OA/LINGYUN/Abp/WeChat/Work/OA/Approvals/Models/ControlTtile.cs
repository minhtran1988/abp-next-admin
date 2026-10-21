using JetBrains.Annotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Control name
/// </summary>
public class ControlTtile
{
    /// <summary>
    /// Control name. The following conditions must be met: 1-The control name must not be the same as the existing control name; 2-The length must not exceed 40 characters. 3-Attendance-Outing/business trip/overtime control title is fixed to outing/business trip/overtime, and does not support customization for the time being.
    /// </summary>
    [NotNull]
    [StringLength(40)]
    [JsonProperty("text")]
    [JsonPropertyName("text")]
    public string Text { get; set; } = default!;
    /// <summary>
    /// Display language, Chinese: zh_CN (note not zh-CN)
    /// </summary>
    [NotNull]
    [JsonProperty("lang")]
    [JsonPropertyName("lang")]
    public string Lang { get; set; } = default!;
    public ControlTtile()
    {

    }

    public ControlTtile(string text, string lang = "zh_CN")
    {
        Text = text;
        Lang = lang;
    }
}
