using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.MsgAudits.Request;
/// <summary>
/// Get session content archive internal group information request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/92951" />
/// </remarks>
public class WeChatWorkGetGroupChatRequest : WeChatWorkRequest
{
    /// <summary>
    /// The roomid to be queried
    /// </summary>
    [NotNull]
    [JsonProperty("roomid")]
    [JsonPropertyName("roomid")]
    public string RoomId { get; }
    public WeChatWorkGetGroupChatRequest(string roomId)
    {
        RoomId = roomId;
    }
}
