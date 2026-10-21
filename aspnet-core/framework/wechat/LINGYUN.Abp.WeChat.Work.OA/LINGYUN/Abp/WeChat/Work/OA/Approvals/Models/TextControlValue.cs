using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Text control value
/// </summary>
public class TextControlValue : ControlValue
{
    /// <summary>
    /// Text content, fill in the input value of the text/multiline text control here. The Text content of the text control does not support line breaks.
    /// </summary>
    [NotNull]
    [JsonProperty("text")]
    [JsonPropertyName("text")]
    public string Text { get; set; } = default!;
    public TextControlValue()
    {

    }

    public TextControlValue(string text)
    {
        Text = text;
    }
}
