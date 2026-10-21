using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.MeetingRooms.Request;
/// <summary>
/// Query conference room request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/93619#%E6%9F%A5%E8%AF%A2%E4%BC%9A%E8%AE%AE%E5%AE%A4"/>
/// </remarks>
public class WeChatWorkGetMeetingRoomListRequest : WeChatWorkRequest
{
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
    /// List of devices supported by the conference room
    /// </summary>
    private readonly List<int> _equipment;
    /// <summary>
    /// List of devices supported by the conference room
    /// </summary>
    [CanBeNull]
    [JsonProperty("equipment")]
    [JsonPropertyName("equipment")]
    public int[] Equipment => _equipment.ToArray();
    public WeChatWorkGetMeetingRoomListRequest()
    {
        _equipment = new List<int>();
    }
    /// <summary>
    /// Allow TV devices
    /// </summary>
    /// <returns></returns>
    public void WithTv()
    {
        _equipment.AddIfNotContains(1);
    }
    /// <summary>
    /// Allow phone devices
    /// </summary>
    /// <returns></returns>
    public void WithPhone()
    {
        _equipment.AddIfNotContains(2);

    }
    /// <summary>
    /// Allow projection devices
    /// </summary>
    /// <returns></returns>
    public void WithProjection()
    {
        _equipment.AddIfNotContains(3);
    }
    /// <summary>
    /// Allow whiteboard devices
    /// </summary>
    /// <returns></returns>
    public void WithWhiteboard()
    {
        _equipment.AddIfNotContains(4);
    }
    /// <summary>
    /// Allow video devices
    /// </summary>
    /// <returns></returns>
    public void WithVideo()
    {
        _equipment.AddIfNotContains(5);
    }
}
