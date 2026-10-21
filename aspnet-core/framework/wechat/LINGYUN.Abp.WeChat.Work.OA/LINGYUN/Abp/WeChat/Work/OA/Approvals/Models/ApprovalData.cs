using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;

/// <summary>
/// Approval application data
/// </summary>
public class ApprovalData
{
    /// <summary>
    /// Approval application details, consisting of multiple form controls and their contents, including information that needs to be assigned to the controls
    /// </summary>
    [NotNull]
    [JsonProperty("contents")]
    [JsonPropertyName("contents")]
    public List<ApprovalControlData> Contents { get; set; } = default!;
    public ApprovalData()
    {

    }

    public ApprovalData(List<ApprovalControlData> contents)
    {
        Contents = contents;
    }
}

