using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Formula control value
/// </summary>
public class FormulaControlValue : ControlValue
{
    /// <summary>
    /// formula
    /// </summary>
    [NotNull]
    [JsonProperty("formula")]
    [JsonPropertyName("formula")]
    public FormulaValue Formula { get; set; } = default!;
    public FormulaControlValue()
    {

    }

    public FormulaControlValue(FormulaValue formula)
    {
        Formula = formula;
    }
}

public class FormulaValue
{
    /// <summary>
    /// The value of the formula does not need to be filled in when submitting the form. It is automatically calculated in the background.
    /// </summary>
    [NotNull]
    [JsonProperty("value")]
    [JsonPropertyName("value")]
    public string Value { get; set; } = default!;
    public FormulaValue()
    {

    }

    public FormulaValue(string value)
    {
        Value = value;
    }
}