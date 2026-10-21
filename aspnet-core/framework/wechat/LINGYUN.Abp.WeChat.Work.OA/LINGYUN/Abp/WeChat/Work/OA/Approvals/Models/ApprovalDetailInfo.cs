using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Approval application details
/// </summary>
public class ApprovalDetailInfo
{
    /// <summary>
    /// Approval number
    /// </summary>
    [NotNull]
    [JsonProperty("sp_no")]
    [JsonPropertyName("sp_no")]
    public string SpNo { get; set; } = default!;
    /// <summary>
    /// Approval application type name (approval template name)
    /// </summary>
    [NotNull]
    [JsonProperty("sp_name")]
    [JsonPropertyName("sp_name")]
    public string SpName { get; set; } = default!;
    /// <summary>
    /// Application status
    /// </summary>
    [NotNull]
    [JsonProperty("sp_status")]
    [JsonPropertyName("sp_status")]
    public ApprovalSpStatus SpStatus { get; set; }
    /// <summary>
    /// Approval template id. It can be obtained in "Get Approval Application Details", "Approval Status Change Callback Notification", or in the template editing page link of the approval template.
    /// </summary>
    [NotNull]
    [JsonProperty("template_id")]
    [JsonPropertyName("template_id")]
    public string TemplateId { get; set; } = default!;
    /// <summary>
    /// Approval application submission time, Unix timestamp
    /// </summary>
    [NotNull]
    [JsonProperty("apply_time")]
    [JsonPropertyName("apply_time")]
    public long ApplyTime { get; set; }
    /// <summary>
    /// Applicant information
    /// </summary>
    [CanBeNull]
    [JsonProperty("applyer")]
    [JsonPropertyName("applyer")]
    public ApprovalApplyer? Applyer { get; set; }
    /// <summary>
    /// Batch applicant information (mutually exclusive with the applyer field)
    /// </summary>
    [CanBeNull]
    [JsonProperty("batch_applyer")]
    [JsonPropertyName("batch_applyer")]
    public List<ApprovalUser>? BatchApplyer { get; set; }
    /// <summary>
    /// Approval process information, there may be multiple approval nodes
    /// </summary>
    [NotNull]
    [JsonProperty("sp_record")]
    [JsonPropertyName("sp_record")]
    public List<ApprovalSpRecord> SpRecord { get; set; } = default!;
    /// <summary>
    /// CC information, there may be multiple CC nodes
    /// </summary>
    [CanBeNull]
    [JsonProperty("notifyer")]
    [JsonPropertyName("notifyer")]
    public List<ApprovalUser>? Notifyer { get; set; }
    /// <summary>
    /// Approval application data
    /// </summary>
    [NotNull]
    [JsonProperty("apply_data")]
    [JsonPropertyName("apply_data")]
    public ApprovalData ApplyData { get; set; } = default!;
    /// <summary>
    /// Approval application remark information, there may be multiple remark nodes
    /// </summary>
    [NotNull]
    [JsonProperty("comments")]
    [JsonPropertyName("comments")]
    public List<ApprovalComment> Comments { get; set; } = default!;
    /// <summary>
    /// Approval process list
    /// </summary>
    [NotNull]
    [JsonProperty("process_list")]
    [JsonPropertyName("process_list")]
    public ApprovalProcess Process { get; set; } = default!;
}
