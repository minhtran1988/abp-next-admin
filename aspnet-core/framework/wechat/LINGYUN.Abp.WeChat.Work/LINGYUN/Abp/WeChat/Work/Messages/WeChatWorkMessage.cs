using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages;
/// <summary>
/// Enterprise WeChat message
/// </summary>
public abstract class WeChatWorkMessage : WeChatWorkRequest
{
    /// <summary>
    /// Specify the member to receive the message, member ID list (multiple receivers are separated by '|', up to 1000 are supported).
    /// Special case: Specify "@all" to send to all members of the enterprise application
    /// </summary>
    [JsonProperty("touser")]
    [JsonPropertyName("touser")]
    public virtual string? ToUser { get; set; }
    /// <summary>
    /// Specify the department to receive the message, department ID list, multiple recipients separated by '|', up to 100 are supported.
    /// This parameter is ignored when touser is "@all"
    /// </summary>
    [JsonProperty("toparty")]
    [JsonPropertyName("toparty")]
    public virtual string? ToParty { get; set; }
    /// <summary>
    /// Specify the tag to receive the message, tag ID list, multiple receivers separated by '|', up to 100 are supported.
    /// This parameter is ignored when touser is "@all"
    /// </summary>
    [JsonProperty("totag")]
    [JsonPropertyName("totag")]
    public virtual string? ToTag { get; set; }
    /// <summary>
    /// Message type
    /// </summary>
    [NotNull]
    [JsonProperty("msgtype")]
    [JsonPropertyName("msgtype")]
    public virtual string MsgType { get; protected set; }
    /// <summary>
    /// The id of the enterprise application, integer type.
    /// Developed internally by the company, it can be viewed on the application’s settings page;
    /// Third-party service providers can obtain enterprise authorization information through the interface to obtain this parameter value.
    /// </summary>
    [NotNull]
    [JsonProperty("agentid")]
    [JsonPropertyName("agentid")]
    public virtual string AgentId { get; protected set; }
    protected WeChatWorkMessage(
        string agentId,
        string msgType,
        string toUser = "",
        string toParty = "",
        string toTag = "")
    {
        AgentId = agentId;
        MsgType = msgType;
        ToUser = toUser;
        ToParty = toParty;
        ToTag = toTag;
    }
}
