using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Corporate WeChat group chat text and graphic messages
/// </summary>
public class WeChatWorkAppChatNewMessage : WeChatWorkAppChatMessage
{
    public WeChatWorkAppChatNewMessage(
        string agentId,
        string chatId,
        NewMessagePayload news) : base(agentId, chatId, "news")
    {
        News = news;
    }
    /// <summary>
    /// Graphic message
    /// </summary>
    [NotNull]
    [JsonProperty("news")]
    [JsonPropertyName("news")]
    public NewMessagePayload News { get; set; }
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
