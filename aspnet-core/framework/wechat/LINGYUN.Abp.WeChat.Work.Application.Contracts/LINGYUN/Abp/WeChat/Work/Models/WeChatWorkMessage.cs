using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Models;
public class WeChatWorkMessage
{
    /// <summary>
    /// Enterprise WeChat encrypted signature,
    /// msg_signature calculation combines the token filled in by the enterprise, the timestamp, nonce in the request, and the encrypted message body
    /// </summary>
    /// <remarks>
    /// Signature calculation method reference: https://developer.work.weixin.qq.com/document/path/90930#12976/%E6%B6%88%E6%81%AF%E4%BD%93%E7%AD%BE%E5%90%8D%E6%A0%A1%E9%AA%8C
    /// </remarks>
    [Required]
    [JsonPropertyName("msg_signature")]
    public string MsgSignature { get; set; } = default!;
    /// <summary>
    /// Timestamp. Used in conjunction with nonce to prevent request replay attacks.
    /// </summary>
    [Required]
    [JsonPropertyName("timestamp")]
    public int TimeStamp { get; set; }
    /// <summary>
    /// Random number. Used in conjunction with timestamp to prevent request replay attacks.
    /// </summary>
    [Required]
    [JsonPropertyName("nonce")]
    public string Nonce { get; set; } = default!;
}
