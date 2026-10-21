using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.OA.MeetingRooms.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.MeetingRooms.Response;
/// <summary>
/// Query reservation details response parameters based on conference room reservation ID
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/93620#%E6%A0%B9%E6%8D%AE%E4%BC%9A%E 8%AE%AE%E5%AE%A4%E9%A2%84%E5%AE%9Aid%E6%9F%A5%E8%AF%A2%E9%A2%84%E5%AE%9A%E8%AF%A6%E6%83%85"/>
/// </remarks>
public class WeChatWorkGetMeetingRoomBookResponse : WeChatWorkResponse
{
    /// <summary>
    /// Reservation status of this conference room
    /// </summary>
    [NotNull]
    [JsonProperty("schedule")]
    [JsonPropertyName("schedule")]
    public MeetingRoomSchedule Schedule { get; set; } = default!;
}
