using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.Contacts.Tags.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Tags.Response;
/// <summary>
/// Get the tag list response parameters
/// </summary>
/// <remarks>
/// For details, see: https://developer.work.weixin.qq.com/document/path/90216
/// </remarks>
public class WeChatWorkTagListResponse : WeChatWorkResponse
{
    /// <summary>
    /// Tag list
    /// </summary>
    [NotNull]
    [JsonProperty("taglist")]
    [JsonPropertyName("taglist")]
    public TagInfo[] Tags { get; set; } = default!;
}