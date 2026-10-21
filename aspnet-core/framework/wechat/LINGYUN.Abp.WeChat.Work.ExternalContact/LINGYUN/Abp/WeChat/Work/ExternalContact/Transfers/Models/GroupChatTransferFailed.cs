using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Transfers.Models;
/// <summary>
/// Group details that failed to be inherited successfully
/// </summary>
public class GroupChatTransferFailed
{
    /// <summary>
    /// Group ID that failed to be inherited successfully
    /// </summary>
    [NotNull]
    [JsonProperty("chat_id")]
    [JsonPropertyName("chat_id")]
    public string ChatId { get; set; } = default!;
    /// <summary>
    /// Group that failed to inherit successfully, error code
    /// </summary>
    [NotNull]
    [JsonProperty("errcode")]
    [JsonPropertyName("errcode")]
    public int ErrCode { get; set; }
    /// <summary>
    /// Group that failed to be successfully inherited, error description
    /// </summary>
    [NotNull]
    [JsonProperty("errmsg")]
    [JsonPropertyName("errmsg")]
    public string ErrMsg { get; set; } = default!;
}
