using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Tags.Response;
/// <summary>
/// Create tag response parameters
/// </summary>
/// <remarks>
/// For details, see: https://developer.work.weixin.qq.com/document/path/90210
/// </remarks>
public class WeChatWorkTagCreateResponse : WeChatWorkResponse
{
    /// <summary>
    /// tag id
    /// </summary>
    [NotNull]
    [JsonProperty("tagid")]
    [JsonPropertyName("tagid")]
    public int TagId { get; set; }
}
