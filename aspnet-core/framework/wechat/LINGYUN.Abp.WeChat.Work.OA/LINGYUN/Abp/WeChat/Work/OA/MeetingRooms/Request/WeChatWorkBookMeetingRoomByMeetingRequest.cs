using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.OA.MeetingRooms.Request;
/// <summary>
/// Reservation of meeting room request parameters via meeting
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/93620#%E9%80%9A%E8%BF%87%E4%BC%9A%E8%AE%AE%E9%A2%84%E5%AE%9A%E4%BC%9A%E8%AE%AE%E5%AE%A4"/>
/// </remarks>
public class WeChatWorkBookMeetingRoomByMeetingRequest : WeChatWorkRequest
{
    /// <summary>
    /// Conference room id
    /// </summary>
    [NotNull]
    [JsonProperty("meetingroom_id")]
    [JsonPropertyName("meetingroom_id")]
    public int MeetingRoomId { get; }
    /// <summary>
    /// The userid of the reservation person
    /// </summary>
    [NotNull]
    [JsonProperty("booker")]
    [JsonPropertyName("booker")]
    public string Booker { get; }
    /// <summary>
    /// Meeting id, only meetings created with the same application can be used
    /// </summary>
    [NotNull]
    [JsonProperty("meetingid")]
    [JsonPropertyName("meetingid")]
    public string MeetingId { get; }
    public WeChatWorkBookMeetingRoomByMeetingRequest(
        int meetingRoomId,
        string booker,
        string meetingId)
    {
        MeetingRoomId = Check.NotDefaultOrNull<int>(meetingRoomId, nameof(meetingRoomId));
        Booker = Check.NotNullOrWhiteSpace(booker, nameof(booker));
        MeetingId = Check.NotNullOrWhiteSpace(meetingId, nameof(meetingId));
    }
}
