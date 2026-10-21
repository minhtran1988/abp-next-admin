using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Members.Request;
/// <summary>
/// Get member ID list request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/96067" />
/// </remarks>
public class WeChatWorkGetUserIdListRequest : WeChatWorkRequest
{
    /// <summary>
    /// Cursor used for paging query, string type, returned by the previous call, not filled in for the first call
    /// </summary>
    [CanBeNull]
    [JsonProperty("cursor")]
    [JsonPropertyName("cursor")]
    public string? Cursor { get; set; }
    /// <summary>
    /// Paging, expected requested data amount, value range 1 ~ 10000
    /// </summary>
    [CanBeNull]
    [JsonProperty("limit")]
    [JsonPropertyName("limit")]
    public int? Limit { get; set; }
    public WeChatWorkGetUserIdListRequest(string? cursor = null, int? limit = 100)
    {
        Cursor = cursor;
        Limit = limit;
    }

    protected override void Validate()
    {
        if (Limit.HasValue)
        {
            Check.Range(Limit.Value, nameof(Limit), 1, 10000);
        }
    }
}
