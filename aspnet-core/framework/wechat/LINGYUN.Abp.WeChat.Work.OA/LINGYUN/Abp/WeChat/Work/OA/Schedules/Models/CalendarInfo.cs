using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Models;
/// <summary>
/// Calendar details
/// </summary>
public class CalendarInfo
{
    /// <summary>
    /// Calendar ID
    /// </summary>
    [NotNull]
    [JsonProperty("cal_id")]
    [JsonPropertyName("cal_id")]
    public string CalId { get; set; } = default!;
    /// <summary>
    /// Calendar administrator userid list
    /// </summary>
    [NotNull]
    [JsonProperty("admins")]
    [JsonPropertyName("admins")]
    public string[] Admins { get; set; } = default!;
    /// <summary>
    /// Calendar title. 1 ~ 128 characters
    /// </summary>
    [NotNull]
    [JsonProperty("summary")]
    [JsonPropertyName("summary")]
    public string Summary { get; set; } = default!;
    /// <summary>
    /// Calendar color, RGB color coding hexadecimal representation, for example: "#0000FF" represents pure blue
    /// </summary>
    [NotNull]
    [JsonProperty("color")]
    [JsonPropertyName("color")]
    public string Color { get; set; } = default!;
    /// <summary>
    /// Calendar description. 0 ~ 512 characters
    /// </summary>
    [CanBeNull]
    [JsonProperty("description")]
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    /// <summary>
    /// Whether the calendar is public.
    /// </summary>
    [NotNull]
    [JsonProperty("is_public")]
    [JsonPropertyName("is_public")]
    [Newtonsoft.Json.JsonConverter(typeof(IntToBoolConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(IntToBoolConverter))]
    public bool IsPublic { get; set; }
    /// <summary>
    /// Public scope. Only valid if it is a public calendar
    /// </summary>
    [CanBeNull]
    [JsonProperty("public_range")]
    [JsonPropertyName("public_range")]
    public CalendarPublicRange? PublicRange { get; set; }
    /// <summary>
    /// Whether to have a calendar for all employees.
    /// </summary>
    [NotNull]
    [JsonProperty("is_corp_calendar")]
    [JsonPropertyName("is_corp_calendar")]
    [Newtonsoft.Json.JsonConverter(typeof(IntToBoolConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(IntToBoolConverter))]
    public bool IsCorpCalendar { get; set; }
    /// <summary>
    /// Calendar notification scope member list. Up to 2000 people
    /// </summary>
    [NotNull]
    [JsonProperty("shares")]
    [JsonPropertyName("shares")]
    public CalendarShare[] Shares { get; set; } = default!;
}
