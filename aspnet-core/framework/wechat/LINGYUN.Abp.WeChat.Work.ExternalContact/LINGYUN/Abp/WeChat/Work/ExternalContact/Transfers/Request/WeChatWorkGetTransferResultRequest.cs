using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Transfers.Request;
/// <summary>
/// Query client replacement status request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/94088" />
/// </remarks>
public class WeChatWorkGetTransferResultRequest : WeChatWorkRequest
{
    /// <summary>
    /// The userid of the original follow-up member
    /// </summary>
    [NotNull]
    [JsonProperty("handover_userid")]
    [JsonPropertyName("handover_userid")]
    public string HandOverUserId { get; }
    /// <summary>
    /// The userid of the replacement member
    /// </summary>
    [NotNull]
    [JsonProperty("takeover_userid")]
    [JsonPropertyName("takeover_userid")]
    public string TakeOverUserId { get; }
    /// <summary>
    /// Cursor for paging query, the data returned by each page will not exceed 1000; leaving it blank or empty means getting the first page
    /// </summary>
    [CanBeNull]
    [JsonProperty("cursor")]
    [JsonPropertyName("cursor")]
    public string? Cursor { get; }
    public WeChatWorkGetTransferResultRequest(
        string handOverUserId, 
        string takeOverUserId, 
        string? cursor = null)
    {
        Check.NotNullOrWhiteSpace(handOverUserId, nameof(handOverUserId));
        Check.NotNullOrWhiteSpace(takeOverUserId, nameof(takeOverUserId));

        HandOverUserId = handOverUserId;
        TakeOverUserId = takeOverUserId;
        Cursor = cursor;
    }
}
