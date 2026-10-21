using LINGYUN.Abp.WeChat.Work.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Message;
public class MessageValidationInput : WeChatWorkMessage
{
    /// <summary>
    /// Encrypted string. It needs to be decrypted to obtain the plain text of the message content. After decryption, there are four fields: random, msg_len, msg, and receiveid, where msg is the plain text of the message content.
    /// </summary>
    [Required]
    [JsonPropertyName("echostr")]
    public string EchoStr { get; set; } = default!;
}
