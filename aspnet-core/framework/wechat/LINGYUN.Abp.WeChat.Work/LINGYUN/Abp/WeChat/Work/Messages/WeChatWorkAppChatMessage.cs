using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages;
/// <summary>
/// Enterprise WeChat group chat message
/// </summary>
public abstract class WeChatWorkAppChatMessage : WeChatWorkRequest
{
    protected WeChatWorkAppChatMessage(
        string agentId,
        string chatId,
        string msgType)
    {
        AgentId = agentId;
        ChatId = chatId;
        MsgType = msgType;
    }

    /// <summary>
    /// Group chat id
    /// </summary>
    [NotNull]
    [JsonProperty("chatid")]
    [JsonPropertyName("chatid")]
    public virtual string ChatId { get; protected set; }
    /// <summary>
    /// Message type
    /// </summary>
    [NotNull]
    [JsonProperty("msgtype")]
    [JsonPropertyName("msgtype")]
    public virtual string MsgType { get; protected set; }
    /// <summary>
    /// The id of the enterprise application
    /// </summary>
    /// <remarks>
    /// Since token needs to be exchanged, the application ID needs to be passed
    /// </remarks>
    [NotNull]
    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual string AgentId { get; protected set; }
}
