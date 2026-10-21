using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Card operation button
/// </summary>
public class TemplateCardAction
{
    public TemplateCardAction(string key, string text)
    {
        Key = key;
        Text = text;
    }

    /// <summary>
    /// Manipulate the key value. After the user clicks, a callback event will be generated and this parameter will be returned as EventKey. The callback event will bring the key value. The maximum length is 1024 bytes and cannot be repeated.
    /// </summary>
    [NotNull]
    [JsonProperty("key")]
    [JsonPropertyName("key")]
    public string Key { get; set; }
    /// <summary>
    /// Description copy of the operation
    /// </summary>
    [NotNull]
    [JsonProperty("text")]
    [JsonPropertyName("text")]
    public string Text { get; set; }
}