using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Attachments.Response;
/// <summary>
/// Upload attachment resource response result
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/95098" />
/// </remarks>
public class WeChatWorkUploadAttachmentResponse : WeChatWorkResponse
{
    /// <summary>
    /// Media file types, including image (image), voice (voice), video (video), and ordinary file (file)
    /// </summary>
    [NotNull]
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public string MediaType { get; set; } = default!;
    /// <summary>
    /// The unique identifier obtained after the media file is uploaded. It is valid for three days and can be obtained by using the temporary material acquisition interface.
    /// </summary>
    [NotNull]
    [JsonProperty("media_id")]
    [JsonPropertyName("media_id")]
    public string MediaId { get; set; } = default!;
    /// <summary>
    /// Media file upload timestamp
    /// </summary>
    [NotNull]
    [JsonProperty("created_at")]
    [JsonPropertyName("created_at")]
    public long CreatedAt { get; set; }
}
