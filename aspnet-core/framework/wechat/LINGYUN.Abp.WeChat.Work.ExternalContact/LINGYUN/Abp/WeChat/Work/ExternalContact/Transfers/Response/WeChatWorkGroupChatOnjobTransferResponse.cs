using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.ExternalContact.Transfers.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Transfers.Response;
/// <summary>
/// Assign the customer group response results of current members
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/95703" />
/// </remarks>
public class WeChatWorkGroupChatOnjobTransferResponse : WeChatWorkResponse
{
    /// <summary>
    /// Group that failed to inherit successfully
    /// </summary>
    [NotNull]
    [JsonProperty("failed_chat_list")]
    [JsonPropertyName("failed_chat_list")]
    public GroupChatTransferFailed[] FailedChatList { get; set; } = default!;
}
