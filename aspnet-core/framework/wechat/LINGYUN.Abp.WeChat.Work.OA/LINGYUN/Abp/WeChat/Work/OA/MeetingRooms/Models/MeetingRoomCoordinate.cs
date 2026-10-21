using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.MeetingRooms.Models;
/// <summary>
/// Conference room coordinates
/// </summary>
public class MeetingRoomCoordinate
{
    /// <summary>
    /// The latitude of the building where the conference room is located
    /// </summary>
    [NotNull]
    [JsonProperty("latitude")]
    [JsonPropertyName("latitude")]
    public string Latitude { get; }
    /// <summary>
    /// The longitude of the building where the conference room is located
    /// </summary>
    [NotNull]
    [JsonProperty("longitude")]
    [JsonPropertyName("longitude")]
    public string Longitude { get; }
    public MeetingRoomCoordinate(decimal latitude, decimal longitude)
    {
        Latitude = latitude.ToString(); 
        Longitude = longitude.ToString();
    }
}
