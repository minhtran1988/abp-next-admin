using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Transfers.Request;
/// <summary>
/// Assign client request parameters to active members
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/92125" />
/// </remarks>
public class WeChatWorkTransferCustomerRequest : WeChatWorkRequest
{
    /// <summary>
    /// The userid of the original follow-up member
    /// </summary>
    [NotNull]
    [JsonProperty("handover_userid")]
    [JsonPropertyName("handover_userid")]
    public string HandoverUserId { get; }
    /// <summary>
    /// The userid of the replacement member
    /// </summary>
    [NotNull]
    [JsonProperty("takeover_userid")]
    [JsonPropertyName("takeover_userid")]
    public string TakeoverUserId { get; }
    /// <summary>
    /// Customer's external_userid list, up to 100 customers can be allocated each time
    /// </summary>
    [NotNull]
    [JsonProperty("external_userid")]
    [JsonPropertyName("external_userid")]
    public string[] ExternalUserId { get; }
    /// <summary>
    /// The message sent to the customer after the transfer is successful, up to 200 characters, if not filled in, the default copy will be used
    /// </summary>
    [CanBeNull]
    [JsonProperty("transfer_success_msg")]
    [JsonPropertyName("transfer_success_msg")]
    public string? TransferSuccessMsg { get; set; }
    public WeChatWorkTransferCustomerRequest(string handoverUserId, string takeoverUserId, string[] externalUserId, string? transferSuccessMsg = null)
    {
        Check.NotNullOrWhiteSpace(handoverUserId, nameof(handoverUserId));
        Check.NotNullOrWhiteSpace(takeoverUserId, nameof(takeoverUserId));
        Check.NotNullOrEmpty(externalUserId, nameof(externalUserId));
        Check.Length(transferSuccessMsg, nameof(transferSuccessMsg), 200);

        HandoverUserId = handoverUserId;
        TakeoverUserId = takeoverUserId;
        ExternalUserId = externalUserId;
        TransferSuccessMsg = transferSuccessMsg;
    }

    protected override void Validate()
    {
        Check.Length(TransferSuccessMsg, nameof(TransferSuccessMsg), 200);
    }
}
