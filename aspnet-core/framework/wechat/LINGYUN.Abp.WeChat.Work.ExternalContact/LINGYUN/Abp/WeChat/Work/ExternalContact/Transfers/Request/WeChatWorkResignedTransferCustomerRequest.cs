using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Transfers.Request;
/// <summary>
/// Assign the customer request parameters of the resigned member
/// </summary>
public class WeChatWorkResignedTransferCustomerRequest : WeChatWorkRequest
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
    public WeChatWorkResignedTransferCustomerRequest(string handoverUserId, string takeoverUserId, string[] externalUserId)
    {
        Check.NotNullOrWhiteSpace(handoverUserId, nameof(handoverUserId));
        Check.NotNullOrWhiteSpace(takeoverUserId, nameof(takeoverUserId));
        Check.NotNullOrEmpty(externalUserId, nameof(externalUserId));

        HandoverUserId = handoverUserId;
        TakeoverUserId = takeoverUserId;
        ExternalUserId = externalUserId;
    }

    protected override void Validate()
    {
        if (ExternalUserId.Length > 100)
        {
            throw new ArgumentException("Transfer a maximum of 100 customers at a time!");
        }
    }
}
