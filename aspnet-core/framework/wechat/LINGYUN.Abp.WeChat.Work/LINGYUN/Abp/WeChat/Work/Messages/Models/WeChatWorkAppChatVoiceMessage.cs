using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Enterprise WeChat group chat language message
/// </summary>
public class WeChatWorkAppChatVoiceMessage : WeChatWorkAppChatMessage
{
    public WeChatWorkAppChatVoiceMessage(
        string agentId,
        string chatId,
        MediaMessage voice) : base(agentId, chatId, "voice")
    {
        Voice = voice;
    }
    /// <summary>
    /// Voice media file
    /// </summary>
    [NotNull]
    [JsonProperty("voice")]
    [JsonPropertyName("voice")]
    public MediaMessage Voice { get; set; }
}
