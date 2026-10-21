using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.ExternalContact.GroupChats.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.GroupChats.Request;
/// <summary>
/// Get the customer group list request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/92120" />
/// </remarks>
public class WeChatWorkGetGroupChatListRequest : WeChatWorkRequest
{
    /// <summary>
    /// Customer group follow-up status filtering
    /// </summary>
    [CanBeNull]
    [JsonProperty("status_filter")]
    [JsonPropertyName("status_filter")]
    public StatusFilter? StatusFilter { get; set; }
    /// <summary>
    /// Customer group follow-up status filtering
    /// </summary>
    [CanBeNull]
    [JsonProperty("owner_filter")]
    [JsonPropertyName("owner_filter")]
    public OwnerFilter[]? OwnerFilter { get; set; }
    /// <summary>
    /// Cursor used for paging query, string type, returned by the previous call, not filled in for the first call
    /// </summary>
    [CanBeNull]
    [JsonProperty("cursor")]
    [JsonPropertyName("cursor")]
    public string? Cursor { get; set; }
    /// <summary>
    /// Paging, expected requested data amount, value range 1 ~ 1000
    /// </summary>
    [CanBeNull]
    [JsonProperty("limit")]
    [JsonPropertyName("limit")]
    public int Limit { get; }
    public WeChatWorkGetGroupChatListRequest(int limit = 1000)
    {
        Check.Range(limit, nameof(limit), 1, 1000);

        Limit = limit;
    }
}
