using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Summary information
/// </summary>
public class ApprovalSummary
{
    /// <summary>
    /// Summary line information, used to define the content displayed in a certain line summary
    /// </summary>
    [NotNull]
    [JsonProperty("summary_info")]
    [JsonPropertyName("summary_info")]
    public List<ApprovalSummaryInfo> SummaryInfo { get; set; } = default!;
    public ApprovalSummary()
    {

    }

    public ApprovalSummary(List<ApprovalSummaryInfo> summaryInfo)
    {
        SummaryInfo = summaryInfo;
    }
}
