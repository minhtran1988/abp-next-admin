using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Customers.Models;
/// <summary>
/// Rule group management scope
/// </summary>
public class CustomerStrategyRange
{
    /// <summary>
    /// Node type
    /// </summary>
    [NotNull]
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public CustomerStrategyRangeType Type { get; set; }
    /// <summary>
    /// The member userid configured within the management scope, returned only when Type is <see cref="CustomerStrategyRangeType.Member"/>
    /// </summary>
    [CanBeNull]
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string? UserId { get; set; }
    /// <summary>
    /// Department partyid configured within the management scope, returned only when Type is <see cref="CustomerStrategyRangeType.Part"/>
    /// </summary>
    [CanBeNull]
    [JsonProperty("partyid")]
    [JsonPropertyName("partyid")]
    public int? PartyId { get; set; }

    public CustomerStrategyRange()
    {

    }

    private CustomerStrategyRange(
        CustomerStrategyRangeType type, 
        string? userId = null, 
        int? partyId = null)
    {
        Type = type;
        UserId = userId;
        PartyId = partyId;
    }

    public static CustomerStrategyRange Member(string userId)
    {
        Check.NotNullOrWhiteSpace(userId, nameof(userId));

        return new CustomerStrategyRange(CustomerStrategyRangeType.Member, userId);
    }
    public static CustomerStrategyRange Party(int partyId)
    {
        Check.NotDefaultOrNull<int>(partyId, nameof(partyId));

        return new CustomerStrategyRange(CustomerStrategyRangeType.Part, partyId: partyId);
    }
}
