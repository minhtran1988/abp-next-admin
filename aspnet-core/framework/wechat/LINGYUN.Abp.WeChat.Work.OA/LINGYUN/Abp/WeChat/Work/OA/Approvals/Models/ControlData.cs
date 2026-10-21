using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Approval application details
/// </summary>
public class ControlData
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
    [CanBeNull]
    [JsonProperty("value")]
    [JsonPropertyName("value")]
    public ControlValue? Value { get; set; }
    public ControlData()
    {

    }

    private ControlData(string id, string control, ControlValue? value = null)
    {
        Id = id;
        Control = control;
        Value = value;
    }

    /// <summary>
    /// Create a Text control
    /// </summary>
    /// <param name="id">ControlId</param>
    /// <param name="value">Control value</param>
    /// <returns></returns>
    public static ControlData Text(string id, TextControlValue value)
    {
        return new ControlData(id, "Text", value);
    }
    /// <summary>
    /// Create a Textarea control
    /// </summary>
    /// <param name="id">ControlId</param>
    /// <param name="value">Control value</param>
    /// <returns></returns>
    public static ControlData Textarea(string id, TextareaControlValue value)
    {
        return new ControlData(id, "Textarea", value);
    }
    /// <summary>
    /// Create a Number control
    /// </summary>
    /// <param name="id">ControlId</param>
    /// <param name="value">Control value</param>
    /// <returns></returns>
    public static ControlData Number(string id, NumberControlValue value)
    {
        return new ControlData(id, "Number", value);
    }
    /// <summary>
    /// Create a Money control
    /// </summary>
    /// <param name="id">ControlId</param>
    /// <param name="value">Control value</param>
    /// <returns></returns>
    public static ControlData Money(string id, MoneyControlValue value)
    {
        return new ControlData(id, "Money", value);
    }
    /// <summary>
    /// Create a Date control
    /// </summary>
    /// <param name="id">ControlId</param>
    /// <param name="value">Control value</param>
    /// <returns></returns>
    public static ControlData Date(string id, DateControlValue value)
    {
        return new ControlData(id, "Date", value);
    }
    /// <summary>
    /// Create a Selector control
    /// </summary>
    /// <param name="id">ControlId</param>
    /// <param name="value">Control value</param>
    /// <returns></returns>
    public static ControlData Selector(string id, SelectorControlValue value)
    {
        return new ControlData(id, "Selector", value);
    }
    /// <summary>
    /// Create a Contact control
    /// </summary>
    /// <param name="id">ControlId</param>
    /// <param name="value">Control value</param>
    /// <returns></returns>
    public static ControlData Contact(string id, ContactControlValue value)
    {
        return new ControlData(id, "Contact", value);
    }
    /// <summary>
    /// Create a Tips control
    /// </summary>
    /// <param name="id">ControlId</param>
    /// <returns></returns>
    public static ControlData Tips(string id)
    {
        return new ControlData(id, "Tips", null);
    }
    /// <summary>
    /// Create a File control
    /// </summary>
    /// <param name="id">ControlId</param>
    /// <param name="value">Control value</param>
    /// <returns></returns>
    public static ControlData File(string id, FileControlValue value)
    {
        return new ControlData(id, "File", value);
    }
    /// <summary>
    /// Create a Table control
    /// </summary>
    /// <param name="id">ControlId</param>
    /// <param name="value">Control value</param>
    /// <returns></returns>
    public static ControlData Table(string id, TableControlValue value)
    {
        return new ControlData(id, "Table", value);
    }
    /// <summary>
    /// Create a Location control
    /// </summary>
    /// <param name="id">ControlId</param>
    /// <param name="value">Control value</param>
    /// <returns></returns>
    public static ControlData Location(string id, LocationControlValue value)
    {
        return new ControlData(id, "Location", value);
    }
    /// <summary>
    /// Create a RelatedApproval control
    /// </summary>
    /// <param name="id">ControlId</param>
    /// <param name="value">Control value</param>
    /// <returns></returns>
    public static ControlData RelatedApproval(string id, RelatedApprovalControlValue value)
    {
        return new ControlData(id, "RelatedApproval", value);
    }
    /// <summary>
    /// Create a Formula control
    /// </summary>
    /// <param name="id">ControlId</param>
    /// <param name="value">Control value</param>
    /// <returns></returns>
    public static ControlData Formula(string id, FormulaControlValue value)
    {
        return new ControlData(id, "Formula", value);
    }
    /// <summary>
    /// Create a DateRange control
    /// </summary>
    /// <param name="id">ControlId</param>
    /// <param name="value">Control value</param>
    /// <returns></returns>
    public static ControlData DateRange(string id, DateRangeControlValue value)
    {
        return new ControlData(id, "DateRange", value);
    }
}
