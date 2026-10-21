using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.MsgAudits.Request;
/// <summary>
/// Get the group chat session consent request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/91782" />
/// </remarks>
public class WeChatWorkCheckRoomAgreeRequest : WeChatWorkRequest
{
    /// <summary>
    /// The roomid to be queried
    /// </summary>
    [NotNull]
    [JsonProperty("roomid")]
    [JsonPropertyName("roomid")]
    public string RoomId { get; }
    public WeChatWorkCheckRoomAgreeRequest(string roomId)
    {
        RoomId = roomId;
    }
}
