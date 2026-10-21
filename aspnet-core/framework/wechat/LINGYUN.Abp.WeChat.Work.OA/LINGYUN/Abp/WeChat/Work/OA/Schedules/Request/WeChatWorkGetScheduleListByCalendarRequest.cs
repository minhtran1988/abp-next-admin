using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Request;
/// <summary>
/// Get the schedule list request parameters under the calendar
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/97723"/>
/// </remarks>
public class WeChatWorkGetScheduleListByCalendarRequest : WeChatWorkRequest
{
    /// <summary>
    /// Calendar ID
    /// </summary>
    [NotNull]
    [JsonProperty("cal_id")]
    [JsonPropertyName("cal_id")]
    public string CalId { get; }
    /// <summary>
    /// Paging, offset, default is 0
    /// </summary>
    [NotNull]
    [JsonProperty("offset")]
    [JsonPropertyName("offset")]
    public int Offset { get; }
    /// <summary>
    /// Paging, the expected amount of requested data, the default is 500, the value range is 1 ~ 1000
    /// </summary>
    [NotNull]
    [JsonProperty("limit")]
    [JsonPropertyName("limit")]
    public int Limit { get; }
    public WeChatWorkGetScheduleListByCalendarRequest(string calId, int offset = 0, int limit = 500)
    {
        CalId = Check.NotNullOrWhiteSpace(calId, nameof(calId));
        Offset = offset;
        Limit = limit;

        if (Limit <= 0)
        {
            Limit = 1;
        }
        if (Limit > 1000)
        {
            Limit = 1000;
        }
    }
}
