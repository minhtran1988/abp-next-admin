using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Official.Models;
public class WeChatMessage
{
    /// <summary>
    /// WeChat encrypted signature,
    /// The signature combines the token parameter filled in by the developer with the timestamp parameter and nonce parameter in the request.
    /// </summary>
    /// <remarks>
    /// Signature calculation method reference: https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/Before_Develop/Message_encryption_and_decryption.html
    /// </remarks>
    [JsonPropertyName("signature")]
    public string Signature { get; set; } = default!;
    /// <summary>
    /// Timestamp. Used in conjunction with nonce to prevent request replay attacks.
    /// </summary>
    [JsonPropertyName("timestamp")]
    public int TimeStamp { get; set; }
    /// <summary>
    /// Random number. Used in conjunction with timestamp to prevent request replay attacks.
    /// </summary>
    [JsonPropertyName("nonce")]
    public string Nonce { get; set; } = default!;
}
