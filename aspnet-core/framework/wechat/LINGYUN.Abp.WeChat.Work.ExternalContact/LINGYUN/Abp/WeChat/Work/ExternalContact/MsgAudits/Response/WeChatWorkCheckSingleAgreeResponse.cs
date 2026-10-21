using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.ExternalContact.MsgAudits.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.MsgAudits.Response;
/// <summary>
/// Get the response parameters of single chat session consent status
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/91782" />
/// </remarks>
public class WeChatWorkCheckSingleAgreeResponse : WeChatWorkResponse
{
    /// <summary>
    /// Consent
    /// </summary>
    [NotNull]
    [JsonProperty("agreeinfo")]
    [JsonPropertyName("agreeinfo")]
    public UserAgreeInfo[] AgreeInfo { get; set; } = default!;
}
