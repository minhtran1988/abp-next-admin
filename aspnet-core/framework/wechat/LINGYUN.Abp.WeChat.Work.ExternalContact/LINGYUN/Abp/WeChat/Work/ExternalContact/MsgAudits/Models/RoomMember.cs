using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.MsgAudits.Models;
/// <summary>
/// Group chat members
/// </summary>
public class RoomMember
{
    /// <summary>
    /// roomid group member's id, userid
    /// </summary>
    [NotNull]
    [JsonProperty("memberid")]
    [JsonPropertyName("memberid")]
    public string MemberId { get; set; } = default!;
    /// <summary>
    /// The time the roomid group members joined the group
    /// </summary>
    [NotNull]
    [JsonProperty("jointime")]
    [JsonPropertyName("jointime")]
    public long JoinTime { get; set; }
}
