using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Response;
/// <summary>
/// Create schedule response parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/93648"/>
/// </remarks>
public class WeChatWorkCreateScheduleResponse : WeChatWorkResponse
{
    /// <summary>
    /// Schedule ID
    /// </summary>
    [NotNull]
    [JsonProperty("schedule_id")]
    [JsonPropertyName("schedule_id")]
    public string ScheduleId { get; set; } = default!;
}
