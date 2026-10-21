using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Models;

public abstract class CreateOrUpdateCalendar
{
    /// <summary>
    /// Calendar title. 1 ~ 128 characters
    /// </summary>
    [NotNull]
    [JsonProperty("summary")]
    [JsonPropertyName("summary")]
    public string Summary { get; }
    /// <summary>
    /// The color of the calendar displayed on the terminal, RGB color coding hexadecimal representation, for example: "#0000FF" represents pure blue
    /// </summary>
    [NotNull]
    [JsonProperty("color")]
    [JsonPropertyName("color")]
    public string Color { get; }
    /// <summary>
    /// Calendar description. 0 ~ 512 characters
    /// </summary>
    [CanBeNull]
    [JsonProperty("description")]
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    /// <summary>
    /// Calendar administrator userid list
    /// </summary>
    [CanBeNull]
    [JsonProperty("admins")]
    [JsonPropertyName("admins")]
    public string[]? Admins { get; set; }
    /// <summary>
    /// Whether to set this calendar as the default calendar of the application corresponding to access_token.
    /// </summary>
    [NotNull]
    [JsonProperty("set_as_default")]
    [JsonPropertyName("set_as_default")]
    [Newtonsoft.Json.JsonConverter(typeof(IntToBoolConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(IntToBoolConverter))]
    public bool SetAsDefault { get; set; }
    /// <summary>
    /// Whether the calendar is public. 0-no; 1-yes
    /// </summary>
    [NotNull]
    [JsonProperty("is_public")]
    [JsonPropertyName("is_public")]
    [Newtonsoft.Json.JsonConverter(typeof(IntToBoolConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(IntToBoolConverter))]
    public bool IsPublic { get; private set; }
    /// <summary>
    /// Public scope. Only valid if it is a public calendar
    /// </summary>
    [CanBeNull]
    [JsonProperty("public_range")]
    [JsonPropertyName("public_range")]
    public CalendarPublicRange? PublicRange { get; private set; }
    /// <summary>
    /// Whether to have a calendar for all employees. 0-no; 1-yes
    /// </summary>
    [NotNull]
    [JsonProperty("is_corp_calendar")]
    [JsonPropertyName("is_corp_calendar")]
    [Newtonsoft.Json.JsonConverter(typeof(IntToBoolConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(IntToBoolConverter))]
    public bool IsCorpCalendar { get; private set; }
    /// <summary>
    /// Calendar notification scope member list. Up to 2000 people
    /// </summary>
    [CanBeNull]
    [JsonProperty("shares")]
    [JsonPropertyName("shares")]
    public CalendarShare[]? Shares { get; private set; }
    protected CreateOrUpdateCalendar(string summary, string color, string? description)
    {
        Summary = Check.NotNullOrWhiteSpace(summary, nameof(summary), 128, 1);
        Color = Check.NotNullOrWhiteSpace(color, nameof(color));
        Description = Check.Length(description, nameof(description), 512);
    }
    /// <summary>
    /// Set the public scope
    /// </summary>
    /// <param name="publicRange"></param>
    public void WithPublic(CalendarPublicRange publicRange)
    {
        IsPublic = true;
        PublicRange = publicRange;
    }
    /// <summary>
    /// Set everyone's calendar
    /// </summary>
    /// <param name="shares"></param>
    public void WithCorpCalendar(CalendarShare[] shares)
    {
        IsCorpCalendar = true;
        Shares = shares;
    }
    /// <summary>
    /// Validate input
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public void Validate()
    {
        Check.Length(Description, nameof(Description), 512);

        if (IsPublic && PublicRange == null)
        {
            throw new ArgumentException("The public range of the public calendar cannot be null!", nameof(PublicRange));
        }
        if (IsCorpCalendar)
        {
            if (Shares == null)
            {
                throw new ArgumentException("The shares of the corp calendar cannot be null!", nameof(IsCorpCalendar));
            }
            if (Shares.Length > 2000)
            {
                throw new ArgumentException("Calendar shares member list. Up to 2,000!", nameof(Shares));
            }
        }
    }
}
