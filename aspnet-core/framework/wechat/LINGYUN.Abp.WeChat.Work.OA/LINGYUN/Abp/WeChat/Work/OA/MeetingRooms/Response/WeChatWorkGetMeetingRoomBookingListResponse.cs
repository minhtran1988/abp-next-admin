using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.OA.MeetingRooms.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.MeetingRooms.Response;
/// <summary>
/// Query the reservation information response parameters of the conference room
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/93620#%E6%9F%A5%E8%AF%A2%E4%BC%9A%E8%AE%AE%E5%AE%A4%E7%9A%84%E9%A2%84%E5%AE%9A%E4%BF%A1%E6%81%AF"/>
/// </remarks>
public class WeChatWorkGetMeetingRoomBookingListResponse : WeChatWorkResponse
{
    /// <summary>
    /// Meeting room reservation information list
    /// </summary>
    [NotNull]
    [JsonProperty("booking_list")]
    [JsonPropertyName("booking_list")]
    public MeetingRoomBookingInfo[] BookingList { get; set; } = default!;
}
