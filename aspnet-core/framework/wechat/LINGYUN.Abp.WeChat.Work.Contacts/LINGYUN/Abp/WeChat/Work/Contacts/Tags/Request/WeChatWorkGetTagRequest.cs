using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Tags.Request;
/// <summary>
/// Get tag request parameters
/// </summary>
public class WeChatWorkGetTagRequest
{
    /// <summary>
    /// tag id
    /// </summary>
    [NotNull]
    [JsonProperty("tagid")]
    [JsonPropertyName("tagid")]
    public int TagId { get; set; }
    public WeChatWorkGetTagRequest(int tagId)
    {
        TagId = Check.Positive(tagId, nameof(tagId));
    }
}
