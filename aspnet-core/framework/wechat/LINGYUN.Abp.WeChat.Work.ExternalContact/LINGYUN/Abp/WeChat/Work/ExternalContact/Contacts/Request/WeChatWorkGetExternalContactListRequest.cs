using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Contacts.Request;
/// <summary>
/// Get the serviced external contact request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/99434" />
/// </remarks>
public class WeChatWorkGetExternalContactListRequest : WeChatWorkRequest
{
    /// <summary>
    /// The cursor used for paging query, string type, returned by the last call, can be left blank for the first call
    /// </summary>
    /// <remarks>
    /// The cursor has a validity period, please do not cache it before using it.
    /// </remarks>
    [CanBeNull]
    [JsonProperty("cursor")]
    [JsonPropertyName("cursor")]
    public string? Cursor { get; }
    /// <summary>
    /// The maximum number of records returned, integer, default is 1000
    /// </summary>
    [CanBeNull]
    [JsonProperty("limit")]
    [JsonPropertyName("limit")]
    public int? Limit { get; }
    public WeChatWorkGetExternalContactListRequest(string? cursor = null, int? limit = 1000)
    {
        Cursor = cursor;
        Limit = limit;
    }
}
