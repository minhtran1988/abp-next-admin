using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Corporate WeChat group chat text and graphic messages
/// </summary>
public class WeChatWorkAppChatMpNewMessage : WeChatWorkAppChatMessage
{
    public WeChatWorkAppChatMpNewMessage(
        string agentId,
        string chatId,
        MpNewMessagePayload mpnews) : base(agentId, chatId, "mpnews")
    {
        News = mpnews;
    }
    /// <summary>
    /// Graphic message (mp)
    /// </summary>
    [NotNull]
    [JsonProperty("mpnews")]
    [JsonPropertyName("mpnews")]
    public MpNewMessagePayload News { get; set; }
    /// <summary>
    /// Indicates whether it is confidential information,
    /// 0 is not confidential
    /// 1Confidential
    /// Default is 0
    /// </summary>
    [JsonProperty("safe")]
    [JsonPropertyName("safe")]
    public byte Safe { get; set; }
}
