using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Text message
/// </summary>
public class TextMessage
{
    public TextMessage(string content)
    {
        Content = Check.NotNullOrWhiteSpace(content, nameof(content), 2048);
    }

    /// <summary>
    /// Message content, up to 2048 bytes, will be truncated if exceeded (supports id translation)
    /// </summary>
    [NotNull]
    [JsonProperty("content")]
    [JsonPropertyName("content")]
    public string Content { get; set; }
}
