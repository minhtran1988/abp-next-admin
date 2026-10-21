using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.MeetingRooms.Models;
/// <summary>
/// Meeting room details
/// </summary>
public class MeetingRoomInfo
{
    /// <summary>
    /// Conference room id
    /// </summary>
    [NotNull]
    [JsonProperty("meetingroom_id")]
    [JsonPropertyName("meetingroom_id")]
    public int MeetingRoomId { get; set; }
    /// <summary>
    /// Conference room name
    /// </summary>
    [NotNull]
    [JsonProperty("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    /// <summary>
    /// Meeting room capacity
    /// </summary>
    [NotNull]
    [JsonProperty("capacity")]
    [JsonPropertyName("capacity")]
    public int Capacity { get; set; }
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
    [NotNull]
    [JsonProperty("equipment")]
    [JsonPropertyName("equipment")]
    public int[] Equipment { get; set; } = default!;
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
    /// <summary>
    /// Whether approval is required 0-No approval required 1-Approval required
    /// </summary>
    [NotNull]
    [JsonProperty("need_approval")]
    [JsonPropertyName("need_approval")]
    [Newtonsoft.Json.JsonConverter(typeof(IntToBoolConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(IntToBoolConverter))]
    public bool NeedApproval { get; set; }
}
