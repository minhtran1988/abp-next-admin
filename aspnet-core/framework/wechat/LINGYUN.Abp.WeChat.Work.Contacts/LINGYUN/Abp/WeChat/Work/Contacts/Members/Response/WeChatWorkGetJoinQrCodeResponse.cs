using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Members.Response;
/// <summary>
/// Get the response parameters of joining the enterprise QR code
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/91714" />
/// </remarks>
public class WeChatWorkGetJoinQrCodeResponse : WeChatWorkResponse
{
    /// <summary>
    /// QR code link, valid for 7 days
    /// </summary>
    [NotNull]
    [JsonProperty("join_qrcode")]
    [JsonPropertyName("join_qrcode")]
    public string JoinQrcCode { get; set; } = default!;
}
