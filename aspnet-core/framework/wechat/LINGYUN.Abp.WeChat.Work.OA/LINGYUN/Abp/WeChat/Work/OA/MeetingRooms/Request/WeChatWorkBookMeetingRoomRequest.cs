using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.OA.MeetingRooms.Request;
/// <summary>
/// Reservation meeting room request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/93620#%E9%A2%84%E5%AE%9A%E4%BC%9A%E8%AE%AE%E5%AE%A4"/>
/// </remarks>
public class WeChatWorkBookMeetingRoomRequest : WeChatWorkRequest
{
    /// <summary>
    /// List of userids of participants
    /// </summary>
    private readonly static List<string> _attendees = new List<string>();
    /// <summary>
    /// Conference room id
    /// </summary>
    [NotNull]
    [JsonProperty("meetingroom_id")]
    [JsonPropertyName("meetingroom_id")]
    public int MeetingRoomId { get; }
    /// <summary>
    /// Scheduled start time
    /// </summary>
    [NotNull]
    [JsonProperty("start_time")]
    [JsonPropertyName("start_time")]
    public long StartTime { get; }
    /// <summary>
    /// Scheduled end time
    /// </summary>
    [NotNull]
    [JsonProperty("end_time")]
    [JsonPropertyName("end_time")]
    public long EndTime { get; }
    /// <summary>
    /// The userid of the reservation person
    /// </summary>
    [NotNull]
    [JsonProperty("booker")]
    [JsonPropertyName("booker")]
    public string Booker { get; }
    /// <summary>
    /// Conference theme
    /// </summary>
    [CanBeNull]
    [JsonProperty("subject")]
    [JsonPropertyName("subject")]
    public string? Subject { get; }
    /// <summary>
    /// List of userids of participants
    /// </summary>
    [NotNull]
    [JsonProperty("attendees")]
    [JsonPropertyName("attendees")]
    public string[] Attendees => _attendees.ToArray();
    public WeChatWorkBookMeetingRoomRequest(
        int meetingRoomId,
        DateTime startTime,
        DateTime endTime, 
        string booker,
        string? subject = null)
    {
        MeetingRoomId = Check.NotDefaultOrNull<int>(meetingRoomId, nameof(meetingRoomId));
        StartTime = startTime.GetUnixTimeSeconds();
        EndTime = endTime.GetUnixTimeSeconds();
        Booker = Check.NotNullOrWhiteSpace(booker, nameof(booker));
        Subject = subject;
    }
    /// <summary>
    /// Add a participant
    /// </summary>
    /// <param name="userId"></param>
    public void AddAttendee(string userId)
    {
        _attendees.AddIfNotContains(userId);
    }
    /// <summary>
    /// Remove a participant
    /// </summary>
    /// <param name="userId"></param>
    public void RemoveAttendee(string userId)
    {
        _attendees.RemoveAll(attendee => string.Equals(attendee, userId));
    }
    /// <summary>
    /// Clear participants
    /// </summary>
    public void ClearAttendee()
    {
        _attendees.Clear();
    }
}
