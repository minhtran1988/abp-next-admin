using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.OA.Schedules.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Request;

public abstract class WeChatWorkScheduleChangeAttendeesRequest : WeChatWorkRequest
{
    private List<ScheduleAttendee> _attendees = new List<ScheduleAttendee>();
    /// <summary>
    /// Schedule ID.
    /// </summary>
    /// <remarks>
    /// ID returned when creating schedule
    /// </remarks>
    [NotNull]
    [JsonProperty("schedule_id")]
    [JsonPropertyName("schedule_id")]
    public string ScheduleId { get; }
    /// <summary>
    /// Schedule participant list
    /// </summary>
    /// <remarks>
    /// Supports up to 1,000 people in total
    /// </remarks>
    [CanBeNull]
    [JsonProperty("attendees")]
    [JsonPropertyName("attendees")]
    public ScheduleAttendee[]? Attendees {
        get {
            if (_attendees.Count > 0)
            {
                return _attendees.ToArray();
            }
            return null;
        }
    }
    protected WeChatWorkScheduleChangeAttendeesRequest(string scheduleId)
    {
        ScheduleId = Check.NotNullOrWhiteSpace(scheduleId, nameof(scheduleId));
    }

    /// <summary>
    /// Add a schedule participant
    /// </summary>
    /// <param name="userId"></param>
    public void AddAttendee(string userId)
    {
        _attendees.Add(new ScheduleAttendee(userId));
    }
    /// <summary>
    /// Remove a schedule participant
    /// </summary>
    /// <param name="userId"></param>
    public void RemoveAttendee(string userId)
    {
        _attendees.RemoveAll(x => x.UserId == userId);
    }
    /// <summary>
    /// Clear schedule participants
    /// </summary>
    public void ClearAttendee()
    {
        _attendees.Clear();
    }
}
