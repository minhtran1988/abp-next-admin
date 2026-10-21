using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Video message
/// </summary>
public class VideoMessage
{
    [Newtonsoft.Json.JsonConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    public VideoMessage(
        string mediaId,
        string title = "",
        string description = "")
    {
        Title = title;
        Description = description;
        MediaId = mediaId;
    }

    /// <summary>
    /// The title of the video message should not exceed 128 bytes. If it exceeds 128 bytes, it will be automatically truncated.
    /// </summary>
    [CanBeNull]
    [JsonProperty("title")]
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    /// <summary>
    /// Description of the video message, no more than 512 bytes, it will be automatically truncated if it exceeds
    /// </summary>
    [CanBeNull]
    [JsonProperty("description")]
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    /// <summary>
    /// Video media file id, which can be obtained by calling the upload temporary material interface.
    /// </summary>
    [NotNull]
    [JsonProperty("media_id")]
    [JsonPropertyName("media_id")]
    public string MediaId { get; set; }
}
