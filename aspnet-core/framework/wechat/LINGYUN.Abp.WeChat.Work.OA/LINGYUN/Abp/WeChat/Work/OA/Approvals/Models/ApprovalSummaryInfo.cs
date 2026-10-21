using JetBrains.Annotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Summary information
/// </summary>
public class ApprovalSummaryInfo
{
    /// <summary>
    /// The summary line displays text, which is used to display record lists and message notifications. It should not exceed 20 characters.
    /// </summary>
    [NotNull]
    [StringLength(20)]
    [JsonProperty("text")]
    [JsonPropertyName("text")]
    public string Text { get; set; } = default!;
    /// <summary>
    /// Summary line display language, Chinese: zh_CN (note not zh-CN), English: en.
    /// </summary>
    [NotNull]
    [StringLength(30)]
    [JsonProperty("lang")]
    [JsonPropertyName("lang")]
    public string Lang { get; set; } = default!;
    public ApprovalSummaryInfo()
    {

    }

    public ApprovalSummaryInfo(string text, string lang = "zh_CN")
    {
        Text = text;
        Lang = lang;
    }
}
