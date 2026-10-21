using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.OA.Schedules.Models;
using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Request;
/// <summary>
/// Cancel schedule request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/97725"/>
/// </remarks>
public class WeChatWorkDeleteScheduleRequest : WeChatWorkRequest
{
    /// <summary>
    /// Schedule ID
    /// </summary>
    [NotNull]
    [JsonProperty("schedule_id")]
    [JsonPropertyName("schedule_id")]
    public string ScheduleId { get; }
    /// <summary>
    /// Operation mode.
    /// </summary>
    /// <remarks>
    /// Valid when repeating schedule.
    /// </remarks>
    [CanBeNull]
    [JsonProperty("op_mode")]
    [JsonPropertyName("op_mode")]
    public ScheduleDeleteMode? OpMode { get; private set; }
    /// <summary>
    /// Operation start time.
    /// </summary>
    /// <remarks>
    /// Only valid when the operation mode is DeleteThisSchedule or DeleteAllFutureSchedules.
    /// This time must be a start time of the repeating schedule.
    /// </remarks>
    [CanBeNull]
    [JsonProperty("op_start_time")]
    [JsonPropertyName("op_start_time")]
    public long? OpStartTime { get; private set; }
    public WeChatWorkDeleteScheduleRequest(string scheduleId)
    {
        ScheduleId = Check.NotNullOrWhiteSpace(scheduleId, nameof(scheduleId));
    }
    /// <summary>
    /// Delete the start time of this schedule
    /// </summary>
    /// <param name="startTime"></param>
    public void DeleteThisSchedule(DateTime startTime)
    {
        OpMode = ScheduleDeleteMode.DeleteThisSchedule;
        OpStartTime = startTime.GetUnixTimeSeconds();
    }
    /// <summary>
    /// Delete all schedule start times
    /// </summary>
    /// <param name="startTime"></param>
    public void DeleteAllFutureSchedules(DateTime startTime)
    {
        OpMode = ScheduleDeleteMode.DeleteAllFutureSchedules;
        OpStartTime = startTime.GetUnixTimeSeconds();
    }
}
