using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.ExternalContact.MsgAudits.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.MsgAudits.Response;
/// <summary>
/// Get the session content archive internal group information response parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/91782" />
/// </remarks>
public class WeChatWorkGetGroupChatResponse : WeChatWorkResponse
{
    /// <summary>
    /// The group name corresponding to roomid
    /// </summary>
    [NotNull]
    [JsonProperty("roomname")]
    [JsonPropertyName("roomname")]
    public string RoomName { get; set; } = default!;
    /// <summary>
    /// The group creator corresponding to roomid, userid
    /// </summary>
    [NotNull]
    [JsonProperty("creator")]
    [JsonPropertyName("creator")]
    public string Creator { get; set; } = default!;
    /// <summary>
    /// The group creation time corresponding to roomid
    /// </summary>
    [NotNull]
    [JsonProperty("room_create_time")]
    [JsonPropertyName("room_create_time")]
    public long RoomCreateTime { get; set; }
    /// <summary>
    /// Group announcement corresponding to roomid
    /// </summary>
    [CanBeNull]
    [JsonProperty("notice")]
    [JsonPropertyName("notice")]
    public string? Notice { get; set; }
    /// <summary>
    /// List of group members corresponding to roomid
    /// </summary>
    [NotNull]
    [JsonProperty("members")]
    [JsonPropertyName("members")]
    public RoomMember[] Members { get; set; } = default!;
}
