using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Numeric control value
/// </summary>
public class NumberControlValue : ControlValue
{
    /// <summary>
    /// Numeric content, fill in the input value of the numeric control here
    /// </summary>
    [NotNull]
    [JsonProperty("new_number")]
    [JsonPropertyName("new_number")]
    public string NewMumber { get; set; } = default!;
    public NumberControlValue()
    {

    }

    public NumberControlValue(decimal newMumber)
    {
        NewMumber = newMumber.ToString();
    }
}
