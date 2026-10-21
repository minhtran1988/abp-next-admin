using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.MeetingRooms.Models;
/// <summary>
/// Meeting room reservation information
/// </summary>
public class MeetingRoomBookingInfo
{
    /// <summary>
    /// Conference room id
    /// </summary>
    [NotNull]
    [JsonProperty("meetingroom_id")]
    [JsonPropertyName("meetingroom_id")]
    public int MeetingRoomId { get; set; }
    /// <summary>
    /// Query the reservation status of this conference room within the time period
    /// </summary>
    [NotNull]
    [JsonProperty("schedule")]
    [JsonPropertyName("schedule")]
    public MeetingRoomSchedule[] Schedule { get; set; } = default!;
}
