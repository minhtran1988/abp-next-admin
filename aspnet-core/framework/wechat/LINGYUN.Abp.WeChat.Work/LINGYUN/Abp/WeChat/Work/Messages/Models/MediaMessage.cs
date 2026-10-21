using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Media file message
/// </summary>
public class MediaMessage
{
    /// <summary>
    /// Media file id, which can be obtained by calling the upload temporary material interface.
    /// </summary>
    [NotNull]
    [JsonProperty("media_id")]
    [JsonPropertyName("media_id")]
    public string MediaId { get; set; }

    [Newtonsoft.Json.JsonConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    public MediaMessage(string mediaId)
    {
        MediaId = mediaId;
    }
}
