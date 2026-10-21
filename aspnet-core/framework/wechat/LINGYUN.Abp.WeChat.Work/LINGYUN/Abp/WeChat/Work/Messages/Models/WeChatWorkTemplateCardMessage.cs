using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Enterprise WeChat template card message
/// </summary>
public class WeChatWorkTemplateCardMessage : WeChatWorkMessage
{
    public WeChatWorkTemplateCardMessage(
        string agentId,
        TemplateCard template,
        string toUser = "",
        string toParty = "",
        string toTag = "") : base(agentId, "template_card", toUser, toParty, toTag)
    {
        Template = template;
    }
    /// <summary>
    /// Template card message
    /// </summary>
    [NotNull]
    [JsonProperty("template_card")]
    [JsonPropertyName("template_card")]
    public TemplateCard Template { get; set; }
    /// <summary>
    /// Indicates whether to enable id translation, 0 means no, 1 means yes, and the default is 0.
    /// Only needed for third-party applications
    /// Enterprise self-built applications can be ignored.
    /// </summary>
    [JsonProperty("enable_id_trans")]
    [JsonPropertyName("enable_id_trans")]
    public byte EnableIdTrans { get; set; }
    /// <summary>
    /// Indicates whether to enable duplicate message checking, 0 means no, 1 means yes, the default is 0
    /// </summary>
    [JsonProperty("enable_duplicate_check")]
    [JsonPropertyName("enable_duplicate_check")]
    public byte EnableDuplicateCheck { get; set; }
    /// <summary>
    /// Indicates whether to repeat the message check time interval, the default is 1800s, the maximum is no more than 4 hours
    /// </summary>
    [JsonProperty("duplicate_check_interval")]
    [JsonPropertyName("duplicate_check_interval")]
    public int DuplicateCheckInterval { get; set; } = 1800;
}
