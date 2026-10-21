using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Transfers.Request;
public class WeChatWorkGetUnassignedListRequest : WeChatWorkRequest
{
    /// <summary>
    /// Paging query cursor, string type, suitable for large amounts of data. If you use this parameter, you do not need to fill in the page_id. This parameter is returned by the previous call.
    /// </summary>
    [CanBeNull]
    [JsonProperty("cursor")]
    [JsonPropertyName("cursor")]
    public string? Cursor { get; set; }
    /// <summary>
    /// The maximum number of records returned each time, the default is 1000, the maximum value is 1000
    /// </summary>
    [CanBeNull]
    [JsonProperty("page_size")]
    [JsonPropertyName("page_size")]
    public int? PageSize { get; set; }
    public WeChatWorkGetUnassignedListRequest(string? cursor = null, int? pageSize = 1000)
    {
        Cursor = cursor;
        PageSize = pageSize;
    }
}
