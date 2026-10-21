using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.MeetingRooms.Request;
/// <summary>
/// Query the reservation information request parameters of the conference room
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/93620#%E6%9F%A5%E8%AF%A2%E4%BC%9A%E8%AE%AE%E5%AE%A4%E7%9A%84%E9%A2%84%E5%AE%9A%E4%BF%A1%E6%81%AF"/>
/// </remarks>
public class WeChatWorkGetMeetingRoomBookingListRequest : WeChatWorkRequest
{
    private readonly static DateTime _beginUnixTime = new DateTime(1970, 1, 1);
    /// <summary>
    /// Conference room id
    /// </summary>
    [CanBeNull]
    [JsonProperty("meetingroom_id")]
    [JsonPropertyName("meetingroom_id")]
    public int? MeetingRoomId { get; set; }
    /// <summary>
    /// The city where the meeting room is located
    /// </summary>
    [CanBeNull]
    [JsonProperty("city")]
    [JsonPropertyName("city")]
    public string? City { get; set; }
    /// <summary>
    /// The building where the conference room is located
    /// </summary>
    [CanBeNull]
    [JsonProperty("building")]
    [JsonPropertyName("building")]
    public string? Building { get; set; }
    /// <summary>
    /// The floor where the conference room is located
    /// </summary>
    [CanBeNull]
    [JsonProperty("floor")]
    [JsonPropertyName("floor")]
    public string? Floor { get; set; }
    /// <summary>
    /// Query the scheduled starting time. If empty, the default is the current time.
    /// </summary>
    [CanBeNull]
    [JsonProperty("start_time")]
    [JsonPropertyName("start_time")]
    public long? StartTime { get; private set; }
    /// <summary>
    /// Query the scheduled end time. If it is empty, it defaults to 0:00 tomorrow.
    /// </summary>
    [CanBeNull]
    [JsonProperty("end_time")]
    [JsonPropertyName("end_time")]
    public long? EndTime { get; private set; }
    /// <summary>
    /// Set the query scheduled time range
    /// </summary>
    /// <param name="startTime">Start time</param>
    /// <param name="endTime">End time</param>
    public void Beetween(DateTime startTime, DateTime endTime)
    {
        StartTime = (long)(startTime - _beginUnixTime).TotalSeconds;
        EndTime = (long)(endTime - _beginUnixTime).TotalSeconds;
    }
}
