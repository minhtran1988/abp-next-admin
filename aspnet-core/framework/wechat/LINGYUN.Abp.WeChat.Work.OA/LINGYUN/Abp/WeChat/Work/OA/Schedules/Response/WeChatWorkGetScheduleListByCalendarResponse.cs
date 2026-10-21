using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.OA.Schedules.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Response;
/// <summary>
/// Get the schedule list response parameters under the calendar
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/97723"/>
/// </remarks>
public class WeChatWorkGetScheduleListByCalendarResponse : WeChatWorkResponse
{
    /// <summary>
    /// Schedule list
    /// </summary>
    [NotNull]
    [JsonProperty("schedule_list")]
    [JsonPropertyName("schedule_list")]
    public CalendarScheduleInfo[] ScheduleList { get; set; } = default!;
}
