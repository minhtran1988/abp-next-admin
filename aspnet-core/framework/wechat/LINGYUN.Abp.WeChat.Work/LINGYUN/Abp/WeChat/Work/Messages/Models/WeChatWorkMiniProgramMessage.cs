using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Messages.Models;
/// <summary>
/// Enterprise WeChat applet notification message
/// </summary>
public class WeChatWorkMiniProgramMessage : WeChatWorkMessage
{
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
    /// <summary>
    /// Message content
    /// </summary>
    [NotNull]
    [JsonProperty("miniprogram_notice")]
    [JsonPropertyName("miniprogram_notice")]
    public MiniProgramMessage MiniProgram { get; set; }
    public WeChatWorkMiniProgramMessage(
        MiniProgramMessage miniProgram,
        string toUser = "",
        string toParty = "",
        string toTag = "",
        byte enableIdTrans = 0,
        byte enableDuplicateCheck = 0,
        int duplicateCheckInterval = 1800)
        : base("", "miniprogram_notice", toUser, toParty, toTag)
    {
        MiniProgram = miniProgram;
        EnableIdTrans = enableIdTrans;
        EnableDuplicateCheck = enableDuplicateCheck;
        DuplicateCheckInterval = duplicateCheckInterval;
    }
}
