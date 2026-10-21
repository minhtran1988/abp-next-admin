using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Request;
/// <summary>
/// Delete calendar request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/97718"/>
/// </remarks>
public class WeChatWorkDeleteCalendarRequest : WeChatWorkRequest
{
    /// <summary>
    /// Calendar ID
    /// </summary>
    [NotNull]
    [JsonProperty("cal_id")]
    [JsonPropertyName("cal_id")]
    public string CalId { get; }
    public WeChatWorkDeleteCalendarRequest(string calId)
    {
        CalId = Check.NotNullOrWhiteSpace(calId, nameof(calId));
    }
}
