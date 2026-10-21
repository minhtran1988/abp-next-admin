using LINGYUN.Abp.WeChat.Official.Models;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Official.Message;
public class MessageValidationInput : WeChatMessage
{
    /// <summary>
    /// Encrypted string. It needs to be decrypted to obtain the plain text of the message content. After decryption, there are four fields: random, msg_len, msg, and receiveid, where msg is the plain text of the message content.
    /// </summary>
    [JsonPropertyName("echostr")]
    public string EchoStr { get; set; } = default!;
}
