using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// markdown message
/// </summary>
public class MarkdownMessage
{
    /// <summary>
    /// markdown content, up to 2048 bytes, must be utf8 encoded
    /// </summary>
    [NotNull]
    [JsonProperty("content")]
    [JsonPropertyName("content")]
    public string Content { get; set; }
    public MarkdownMessage(string content)
    {
        Content = content;
    }
}
