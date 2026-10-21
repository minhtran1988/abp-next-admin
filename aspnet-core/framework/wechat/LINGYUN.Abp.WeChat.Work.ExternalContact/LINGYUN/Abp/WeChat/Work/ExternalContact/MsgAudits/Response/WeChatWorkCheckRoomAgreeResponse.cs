using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.ExternalContact.MsgAudits.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.MsgAudits.Response;
/// <summary>
/// Get the group chat session consent response parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/91782" />
/// </remarks>
public class WeChatWorkCheckRoomAgreeResponse : WeChatWorkResponse
{
    /// <summary>
    /// Consent
    /// </summary>
    [NotNull]
    [JsonProperty("agreeinfo")]
    [JsonPropertyName("agreeinfo")]
    public RoomAgreeInfo[] AgreeInfo { get; set; } = default!;
}
