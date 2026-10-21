using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.MeetingRooms.Response;
/// <summary>
/// Add conference room response parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/93619#%E6%B7%BB%E5%8A%A0%E4%BC%9A%E8%AE%AE%E5%AE%A4"/>
/// </remarks>
public class WeChatWorkCreateMeetingRoomResponse : WeChatWorkResponse
{
    /// <summary>
    /// Conference room id
    /// </summary>
    [NotNull]
    [JsonProperty("meetingroom_id")]
    [JsonPropertyName("meetingroom_id")]
    public int MeetingRoomId { get; set; }
}
