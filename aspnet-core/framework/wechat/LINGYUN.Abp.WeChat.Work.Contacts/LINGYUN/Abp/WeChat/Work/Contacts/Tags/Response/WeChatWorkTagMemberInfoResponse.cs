using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.Contacts.Tags.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Tags.Response;
/// <summary>
/// Tag member response parameters
/// </summary>
/// <remarks>
/// For details, see: https://developer.work.weixin.qq.com/document/path/90213
/// </remarks>
public class WeChatWorkTagMemberInfoResponse : WeChatWorkResponse
{
    /// <summary>
    /// Tag name
    /// </summary>
    [NotNull]
    [JsonProperty("tagname")]
    [JsonPropertyName("tagname")]
    public string TagName { get; set; } = default!;
    /// <summary>
    /// List of members contained in the tag
    /// </summary>
    [NotNull]
    [JsonProperty("userlist")]
    [JsonPropertyName("userlist")]
    public TagUserInfo[] Users { get; set; } = default!;
    /// <summary>
    /// List of department ids contained in the tag
    /// </summary>
    [NotNull]
    [JsonProperty("partylist")]
    [JsonPropertyName("partylist")]
    public int[] Parts { get; set; } = default!;
}
