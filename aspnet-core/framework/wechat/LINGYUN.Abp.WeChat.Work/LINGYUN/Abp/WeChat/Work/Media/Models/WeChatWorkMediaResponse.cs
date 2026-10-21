using Newtonsoft.Json;

namespace LINGYUN.Abp.WeChat.Work.Media.Models;
public class WeChatWorkMediaResponse : WeChatWorkResponse
{
    /// <summary>
    /// Media file type
    /// </summary>
    /// <remarks>
    /// Picture (image)
    /// Voice (voice)
    /// Video (video)
    /// Ordinary file (file)
    /// </remarks>
    [JsonProperty("type")]
    public string Type { get; set; } = default!;
    /// <summary>
    /// The unique identifier obtained after the media file is uploaded, valid within 3 days
    /// </summary>
    [JsonProperty("media_id")]
    public string MediaId { get; set; } = default!;
    /// <summary>
    /// Media file upload timestamp
    /// </summary>
    [JsonProperty("created_at")]
    public string CreatedAt { get; set; } = default!;
}
