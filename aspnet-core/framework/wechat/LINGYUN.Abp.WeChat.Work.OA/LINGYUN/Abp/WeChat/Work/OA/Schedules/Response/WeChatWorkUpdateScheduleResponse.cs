using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Response;
/// <summary>
/// Update schedule response parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/97720"/>
/// </remarks>
public class WeChatWorkUpdateScheduleResponse : WeChatWorkResponse
{
    /// <summary>
    /// Modify the newly generated schedule ID of the repeated schedule.
    /// </summary>
    /// <remarks>
    /// For repeated schedules, if all periods are not modified, the original repeated schedule will be trimmed and a new repeated schedule will be generated. At this time, the ID of the new schedule will be returned.
    /// </remarks>
    [NotNull]
    [JsonProperty("schedule_id")]
    [JsonPropertyName("schedule_id")]
    public string ScheduleId { get; set; } = default!;
}
