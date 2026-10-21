using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Graphic message (mp) carrier
/// </summary>
public class MpNewMessagePayload
{
    /// <summary>
    /// Graphic message (mp) list
    /// </summary>
    [NotNull]
    [JsonProperty("articles")]
    [JsonPropertyName("articles")]
    public List<MpNewMessage> Articles { get; set; }

    [Newtonsoft.Json.JsonConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    public MpNewMessagePayload(List<MpNewMessage> articles)
    {
        Articles = articles;
    }
}
/// <summary>
/// Graphic message (mp)
/// </summary>
public class MpNewMessage
{
    [Newtonsoft.Json.JsonConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    public MpNewMessage(
        string title,
        string thumbMediaId,
        string content,
        string author = "",
        string contentSourceUrl = "",
        string description = "")
    {
        Title = title;
        ThumbMediaId = thumbMediaId;
        Author = author;
        ContentSourceUrl = contentSourceUrl;
        Content = content;
        Description = description;
    }

    /// <summary>
    /// Title, no more than 128 bytes, will be automatically truncated if exceeded (supports id translation)
    /// </summary>
    [NotNull]
    [JsonProperty("title")]
    [JsonPropertyName("title")]
    public string Title { get; set; } = default!;
    /// <summary>
    /// The media_id of the graphic message thumbnail can be obtained through the material management interface. Here thumb_media_id is the media_id returned by the upload interface
    /// </summary>
    [NotNull]
    [JsonProperty("thumb_media_id")]
    [JsonPropertyName("thumb_media_id")]
    public string ThumbMediaId { get; set; } = default!;
    /// <summary>
    /// The author of the graphic message, no more than 64 bytes
    /// </summary>
    [CanBeNull]
    [JsonProperty("author")]
    [JsonPropertyName("author")]
    public string? Author { get; set; }
    /// <summary>
    /// The page link after clicking "Read the original text" in the graphic message
    /// </summary>
    [CanBeNull]
    [JsonProperty("content_source_url")]
    [JsonPropertyName("content_source_url")]
    public string? ContentSourceUrl { get; set; }
    /// <summary>
    /// The content of the graphic message supports html tags and does not exceed 666 K bytes (supports id translation)
    /// </summary>
    [NotNull]
    [JsonProperty("content")]
    [JsonPropertyName("content")]
    public string Content { get; set; } = default!;
    /// <summary>
    /// Description of graphic message, no more than 512 bytes, will be automatically truncated if exceeded (supports id translation)
    /// </summary>
    [CanBeNull]
    [JsonProperty("digest")]
    [JsonPropertyName("digest")]
    public string? Description { get; set; }
}
