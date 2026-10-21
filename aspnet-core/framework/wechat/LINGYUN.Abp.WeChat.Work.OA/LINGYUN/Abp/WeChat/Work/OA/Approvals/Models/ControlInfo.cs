using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Template control properties
/// </summary>
public class ControlInfo
{
    /// <summary>
    /// Control id. 1-The control ID in the template must be unique; 2-The control ID format: control-number, such as "Text-01"
    /// </summary>
    [NotNull]
    [JsonProperty("id")]
    [JsonPropertyName("id")]
    public string Id { get; set; } = default!;
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
    /// DateRange-Duration<br />
    /// PhoneNumber-phone number<br />
    /// Vacation-vacation<br />
    /// Attendance-outing/business trip/working overtime<br />
    /// BankAccount-receiving account<br />
    /// The above are the currently supported control types.
    /// </remarks>
    [NotNull]
    [JsonProperty("control")]
    [JsonPropertyName("control")]
    public string Control { get; set; } = default!;
    /// <summary>
    /// Control name
    /// </summary>
    [NotNull]
    [JsonProperty("title")]
    [JsonPropertyName("title")]
    public List<ControlTtile> Title { get; set; } = default!;
    /// <summary>
    /// Control description, vacation components (Vacation, Attendance) do not support setting yet
    /// </summary>
    [CanBeNull]
    [JsonProperty("placeholder")]
    [JsonPropertyName("placeholder")]
    public List<ControlPlaceholder>? Placeholder { get; set; }
    /// <summary>
    /// Whether the control is required. 0-non-required; 1-required; the default is 0; vacation components (Vacation, Attendance) do not support setting non-required
    /// </summary>
    [CanBeNull]
    [JsonProperty("require")]
    [JsonPropertyName("require")]
    public byte Require { get; set; }
    /// <summary>
    /// Whether the control is printable. 0-printable; 1-non-printable; the default is 0; vacation components (Vacation, Attendance) do not support the setting of non-printable
    /// </summary>
    [CanBeNull]
    [JsonProperty("un_print")]
    [JsonPropertyName("un_print")]
    public byte UnPrint { get; set; }
    /// <summary>
    /// un_replace
    /// </summary>
    [CanBeNull]
    [JsonProperty("un_replace")]
    [JsonPropertyName("un_replace")]
    public byte UnReplace { get; set; }
    /// <summary>
    /// inner_id
    /// </summary>
    [CanBeNull]
    [JsonProperty("inner_id")]
    [JsonPropertyName("inner_id")]
    public string? InnerId { get; set; }
    /// <summary>
    /// Whether the control is hidden
    /// </summary>
    [CanBeNull]
    [JsonProperty("display")]
    [JsonPropertyName("display")]
    public byte Display { get; set; }
}
