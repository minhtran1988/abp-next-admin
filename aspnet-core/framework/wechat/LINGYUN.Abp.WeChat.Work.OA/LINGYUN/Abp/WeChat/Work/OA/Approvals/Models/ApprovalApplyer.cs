using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Applicant information
/// </summary>
public class ApprovalApplyer : ApprovalUser
{
    /// <summary>
    /// The applicant’s department id
    /// </summary>
    [NotNull]
    [JsonProperty("partyid")]
    [JsonPropertyName("partyid")]
    public int PartyId { get; set; }
}
