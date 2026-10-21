using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Text card message
/// </summary>
public class TextCardMessage
{
    [Newtonsoft.Json.JsonConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    public TextCardMessage(
        string title,
        string description,
        string url,
        string buttonText = "")
    {
        Title = title;
        Description = description;
        Url = url;
        ButtonText = buttonText;
    }

    /// <summary>
    /// Title, no more than 128 bytes, will be automatically truncated if exceeded (supports id translation)
    /// </summary>
    [NotNull]
    [JsonProperty("title")]
    [JsonPropertyName("title")]
    public string Title { get; set; }
    /// <summary>
    /// Description, no more than 512 bytes, will be automatically truncated if exceeded (supports id translation)
    /// </summary>
    [NotNull]
    [JsonProperty("description")]
    [JsonPropertyName("description")]
    public string Description { get; set; }
    /// <summary>
    /// The link that jumps after clicking. The maximum length is 2048 bytes, please make sure to include the protocol header (http/https)
    /// </summary>
    [NotNull]
    [JsonProperty("url")]
    [JsonPropertyName("url")]
    public string Url { get; set; }
    /// <summary>
    /// Button text. The default is "Details", no more than 4 characters, and it will be automatically truncated if it exceeds 4 characters.
    /// </summary>
    [CanBeNull]
    [JsonProperty("btntxt")]
    [JsonPropertyName("btntxt")]
    public string ButtonText { get; set; }
}
