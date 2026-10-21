using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Approval application data
/// </summary>
public class ApprovalControlValue
{
    /// <summary>
    /// Text content, that is, the text content filled in by the applicant in this control
    /// </summary>
    [CanBeNull]
    [JsonProperty("text")]
    [JsonPropertyName("text")]
    public string? Text { get; set; }
    /// <summary>
    /// Digital content, that is, the digital content filled in by the applicant in this control
    /// </summary>
    [CanBeNull]
    [JsonProperty("new_number")]
    [JsonPropertyName("new_number")]
    public decimal? NewNumber { get; set; }
    /// <summary>
    /// Amount content, that is, the amount content filled in by the applicant in this control
    /// </summary>
    [CanBeNull]
    [JsonProperty("new_money")]
    [JsonPropertyName("new_money")]
    public decimal? NewMoney { get; set; }
    /// <summary>
    /// Selected member content, that is, the members selected by the applicant in this control, there can be multiple in multi-select mode
    /// </summary>
    [NotNull]
    [JsonProperty("members")]
    [JsonPropertyName("members")]
    public List<MemberValue> Members { get; set; } = default!;
    /// <summary>
    /// The content of the selected department, that is, the department selected by the applicant in this control, there may be multiple in multi-select mode
    /// </summary>
    [NotNull]
    [JsonProperty("departments")]
    [JsonPropertyName("departments")]
    public List<DepartmentValue> Departments { get; set; } = default!;
    /// <summary>
    /// Attachment list
    /// </summary>
    [NotNull]
    [JsonProperty("files")]
    [JsonPropertyName("files")]
    public List<FileValue> Files { get; set; } = default!;
    /// <summary>
    /// Detail content, a detail control may contain multiple sub-details
    /// </summary>
    [NotNull]
    [JsonProperty("children")]
    [JsonPropertyName("children")]
    public List<ApprovalDataChildrenValue> Children { get; set; } = default!;
    /// <summary>
    /// Select content
    /// </summary>
    [CanBeNull]
    [JsonProperty("selector")]
    [JsonPropertyName("selector")]
    public SelectorValue? Selector { get; set; }
    /// <summary>
    /// Associated approval form
    /// </summary>
    [NotNull]
    [JsonProperty("related_approval")]
    [JsonPropertyName("related_approval")]
    public List<RelatedApprovalValue> RelatedApproval { get; set; } = default!;
}

public class ApprovalDataChildrenValue
{
    /// <summary>
    /// Detail content, a detail control may contain multiple sub-details
    /// </summary>
    [NotNull]
    [JsonProperty("list")]
    [JsonPropertyName("list")]
    public List<ApprovalControlData> List { get; set; } = default!;
}