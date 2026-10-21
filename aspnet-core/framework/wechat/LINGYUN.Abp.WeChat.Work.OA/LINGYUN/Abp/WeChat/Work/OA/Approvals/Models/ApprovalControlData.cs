using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Approval application form control data
/// </summary>
public class ApprovalControlData
{
    /// <summary>
    /// Control type
    /// </summary>
    /// <remarks>
    /// Text-text<br />
    /// Textarea-multiline text<br />
    /// Number-Number<br />
    /// Money-amount<br />
    /// Date-date/date+time<br />
    /// Selector-single selection/multiple selection<br />
    /// Contact-Member/Department<br />
    /// Tips-Explanation text<br />
    /// File-Attachment<br />
    /// Table-Details<br />
    /// Location-location<br />
    /// RelatedApproval-Related approval form<br />
    /// Formula-formula<br />
    /// DateRange-Duration
    /// </remarks>
    [NotNull]
    [JsonProperty("control")]
    [JsonPropertyName("control")]
    public string Control { get; set; } = default!;
    /// <summary>
    /// Control id: The unique id of the control, which can be obtained through the "Get Approval Template Details" interface
    /// </summary>
    [NotNull]
    [JsonProperty("id")]
    [JsonPropertyName("id")]
    public string Id { get; set; } = default!;
    /// <summary>
    /// Control value, the applicant needs to fill in the content in each control here. Different controls have different assignment parameters.
    /// </summary>
    /// <remarks>
    /// When the control attribute configured in the template is required, the corresponding value must have a value.
    /// </remarks>
    [NotNull]
    [JsonProperty("value")]
    [JsonPropertyName("value")]
    public ApprovalControlValue Value { get; set; } = default!;
    /// <summary>
    /// Control hidden flag, 1 means the control is hidden
    /// </summary>
    [CanBeNull]
    [JsonProperty("display")]
    [JsonPropertyName("display")]
    public int? Display { get; set; }
    /// <summary>
    /// The control must lose the flag. If it is 1, it means the control must lose.
    /// </summary>
    [CanBeNull]
    [JsonProperty("require")]
    [JsonPropertyName("require")]
    public int? Require { get; set; }
}
