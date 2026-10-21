using JetBrains.Annotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Tags.Request;
/// <summary>
/// Create tag request parameters
/// </summary>
public class WeChatWorkTagCreateRequest : WeChatWorkRequest
{
    /// <summary>
    /// Tag id, non-negative integer type. When this parameter is specified, the new tag will generate a corresponding tag id. If not specified, the current largest id will be incremented.
    /// </summary>
    [NotNull]
    [JsonProperty("tagid")]
    [JsonPropertyName("tagid")]
    public int? TagId { get; set; }
    /// <summary>
    /// Tag name, the length is limited to 32 characters (Chinese characters or English letters), the tag name cannot have the same name as other tags.
    /// </summary>
    [NotNull]
    [StringLength(32)]
    [JsonProperty("tagname")]
    [JsonPropertyName("tagname")]
    public string TagName { get; set; }
    public WeChatWorkTagCreateRequest(string tagName, int? tagId = null)
    {
        TagName = Check.NotNullOrWhiteSpace(tagName, nameof(tagName), 32);
        TagId = tagId;
    }
}
