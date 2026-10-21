using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Leave component-leave component value
/// </summary>
public class VacationControlValue : ControlValue
{
    /// <summary>
    /// Leave content, that is, the leave information selected by the applicant in this component
    /// </summary>
    [NotNull]
    [JsonProperty("vacation")]
    [JsonPropertyName("vacation")]
    public VacationValue Vacation { get; set; } = default!;
    public VacationControlValue()
    {

    }

    public VacationControlValue(VacationValue vacation)
    {
        Vacation = vacation;
    }
}

public class VacationValue
{
    /// <summary>
    /// Leave type, the selected option is associated with vacation management and is the vacation type in vacation management.
    /// </summary>
    [NotNull]
    [JsonProperty("selector")]
    [JsonPropertyName("selector")]
    public VacationSelector Selector { get; set; } = default!;
    /// <summary>
    /// Fake attendance component
    /// </summary>
    [NotNull]
    [JsonProperty("attendance")]
    [JsonPropertyName("attendance")]
    public AttendanceValue Attendance { get; set; } = default!;
    public VacationValue()
    {

    }

    public VacationValue(VacationSelector selector, AttendanceValue attendance)
    {
        Selector = selector;
        Attendance = attendance;
    }
}

public class VacationSelector
{
    /// <summary>
    /// Selection method: single-radio selection; multi-multiple selection, fixed to single selection in the leave and attendance control
    /// </summary>
    [NotNull]
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;
    /// <summary>
    /// User selected options
    /// </summary>
    [NotNull]
    [JsonProperty("options")]
    [JsonPropertyName("options")]
    public List<VacationSelectorOption> Options { get; set; } = default!;
    public VacationSelector()
    {

    }

    public VacationSelector(List<VacationSelectorOption> options)
    {
        Type = "single";
        Options = options;
    }
}

public class VacationSelectorOption
{
    /// <summary>
    /// Option key, the unique id of the option. The id value of the item in vacation_list can be obtained through the "Get Approval Template Details" interface.
    /// </summary>
    [NotNull]
    [JsonProperty("key")]
    [JsonPropertyName("key")]
    public string Key { get; set; } = default!;
    /// <summary>
    /// Option value, if multiple languages are configured, it will include Chinese and English option values.
    /// </summary>
    [NotNull]
    [JsonProperty("value")]
    [JsonPropertyName("value")]
    public VacationSelectorOptionValue Value { get; set; } = default!;
    public VacationSelectorOption()
    {

    }

    public VacationSelectorOption(string key, VacationSelectorOptionValue value)
    {
        Key = key;
        Value = value;
    }
}

public class VacationSelectorOptionValue
{
    /// <summary>
    /// option value
    /// </summary>
    [NotNull]
    [JsonProperty("text")]
    [JsonPropertyName("text")]
    public string Text { get; set; } = default!;
    /// <summary>
    /// Multi-language name
    /// </summary>
    [NotNull]
    [JsonProperty("lang")]
    [JsonPropertyName("lang")]
    public string Lang { get; set; } = default!;
    public VacationSelectorOptionValue()
    {

    }

    public VacationSelectorOptionValue(string text, string lang = "zh_CN")
    {
        Text = text;
        Lang = lang;
    }
}

