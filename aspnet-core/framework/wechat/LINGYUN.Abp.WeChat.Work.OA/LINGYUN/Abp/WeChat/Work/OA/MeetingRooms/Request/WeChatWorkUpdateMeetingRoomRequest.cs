using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.OA.MeetingRooms.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.OA.MeetingRooms.Request;
/// <summary>
/// Edit conference room request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/93619#%E7%BC%96%E8%BE%91%E4%BC%9A%E8%AE%AE%E5%AE%A4"/>
/// </remarks>
public class WeChatWorkUpdateMeetingRoomRequest : WeChatWorkRequest
{
    /// <summary>
    /// Conference room id
    /// </summary>
    [NotNull]
    [JsonProperty("meetingroom_id")]
    [JsonPropertyName("meetingroom_id")]
    public int MeetingRoomId { get; }
    /// <summary>
    /// Conference room name
    /// </summary>
    [CanBeNull]
    [JsonProperty("name")]
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    /// <summary>
    /// Meeting room capacity
    /// </summary>
    [CanBeNull]
    [JsonProperty("capacity")]
    [JsonPropertyName("capacity")]
    public int Capacity { get; set; }
    /// <summary>
    /// The city where the meeting room is located
    /// </summary>
    [CanBeNull]
    [JsonProperty("city")]
    [JsonPropertyName("city")]
    public string? City { get; private set; }
    /// <summary>
    /// The building where the conference room is located
    /// </summary>
    [CanBeNull]
    [JsonProperty("building")]
    [JsonPropertyName("building")]
    public string? Building { get; private set; }
    /// <summary>
    /// The floor where the conference room is located
    /// </summary>
    [CanBeNull]
    [JsonProperty("floor")]
    [JsonPropertyName("floor")]
    public string? Floor { get; private set; }
    /// <summary>
    /// List of devices supported by the conference room
    /// </summary>
    private readonly List<int> _equipment;
    /// <summary>
    /// List of devices supported by the conference room
    /// </summary>
    [NotNull]
    [JsonProperty("equipment")]
    [JsonPropertyName("equipment")]
    public int[] Equipment => _equipment.ToArray();
    /// <summary>
    /// Conference room coordinates
    /// </summary>
    [CanBeNull]
    [JsonProperty("coordinate")]
    [JsonPropertyName("coordinate")]
    public MeetingRoomCoordinate? Coordinate { get; set; }
    /// <summary>
    /// Meeting room usage range
    /// </summary>
    [CanBeNull]
    [JsonProperty("range")]
    [JsonPropertyName("range")]
    public MeetingRoomRange? Range { get; set; }
    public WeChatWorkUpdateMeetingRoomRequest(int meetingRoomId)
    {
        MeetingRoomId = Check.NotDefaultOrNull<int>(meetingRoomId, nameof(meetingRoomId));

        _equipment = new List<int>();
    }
    /// <summary>
    /// Set location information
    /// </summary>
    /// <param name="city"></param>
    /// <param name="building"></param>
    /// <param name="floor"></param>
    public void SetLocation(string city, string building, string floor)
    {
        City = Check.NotNullOrWhiteSpace(city, nameof(city));
        Building = Check.NotNullOrWhiteSpace(building, nameof(building));
        Floor = Check.NotNullOrWhiteSpace(floor, nameof(floor));
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
    protected override void Validate()
    {
        Name = Check.Length(Name, nameof(Name), 30);
    }
}
