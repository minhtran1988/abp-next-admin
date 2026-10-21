using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.ExternalContact.Contacts.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Contacts.Response;
/// <summary>
/// Get the serviced external contact response result
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/92994" />
/// </remarks>
public class WeChatWorkGetExternalContactListResponse : WeChatWorkResponse
{
    /// <summary>
    /// External contact list
    /// </summary>
    [NotNull]
    [JsonProperty("info_list")]
    [JsonPropertyName("info_list")]
    public ExternalContactInfo[] InfoList { get; set; } = default!;
    /// <summary>
    /// Paging cursor, fill it in during the next request to obtain subsequent paging records. If there is no more data, it will return empty. The validity period is 4 hours.
    /// </summary>
    [CanBeNull]
    [JsonProperty("next_cursor")]
    [JsonPropertyName("next_cursor")]
    public string? NextCursor { get; set; }
}
