using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.OA.Schedules.Models;
using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Request;
/// <summary>
/// Update schedule request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/97720"/>
/// </remarks>
public class WeChatWorkUpdateScheduleRequest : WeChatWorkRequest
{
    /// <summary>
    /// Schedule information
    /// </summary>
    [NotNull]
    [JsonProperty("schedule")]
    [JsonPropertyName("schedule")]
    public UpdateSchedule Schedule { get; }
    /// <summary>
    /// Whether not to update participants
    /// </summary>
    [CanBeNull]
    [JsonProperty("skip_attendees")]
    [JsonPropertyName("skip_attendees")]
    public uint? SkipAttendees { get; }
    /// <summary>
    /// Operation mode.
    /// </summary>
    /// <remarks>
    /// Valid when repeating schedule.
    /// </remarks>
    [CanBeNull]
    [JsonProperty("op_mode")]
    [JsonPropertyName("op_mode")]
    public ScheduleUpdateMode? OpMode { get; private set; }
    /// <summary>
    /// Operation start time.
    /// </summary>
    /// <remarks>
    /// Only valid when the operation mode is OnlyModifyThisSchedule or ModifyAllFutureSchedules.
    /// This time must be a start time of the repeating schedule.
    /// For example:
    /// If the schedule start time start_time is 1661990950 (2022-09-01 08:09:10), and the recurrence type is weekly, then op_start_time can be: 1661990950 (2022-09-01 08:09:10), 1662595750 (2022-09-08) 08:09:10), 1663200550 (2022-09-15 08:09:10)......
    /// </remarks>
    [CanBeNull]
    [JsonProperty("op_start_time")]
    [JsonPropertyName("op_start_time")]
    public long? OpStartTime { get; private set; }
    public WeChatWorkUpdateScheduleRequest(UpdateSchedule schedule, bool skipAttendees = false)
    {
        Schedule = schedule;
        SkipAttendees = skipAttendees ? 1u : 0u;
    }
    /// <summary>
    /// Modify the start time of this schedule
    /// </summary>
    /// <param name="startTime"></param>
    public void ModifyThisSchedule(DateTime startTime)
    {
        OpMode = ScheduleUpdateMode.ModifyThisSchedule;
        OpStartTime = startTime.GetUnixTimeSeconds();
    }
    /// <summary>
    /// Modify all schedule start times
    /// </summary>
    /// <param name="startTime"></param>
    public void ModifyAllFutureSchedules(DateTime startTime)
    {
        OpMode = ScheduleUpdateMode.ModifyAllFutureSchedules;
        OpStartTime = startTime.GetUnixTimeSeconds();
    }

    protected override void Validate()
    {
        Check.NotNull(Schedule, nameof(Schedule));

        Schedule.Validate();
    }
}
