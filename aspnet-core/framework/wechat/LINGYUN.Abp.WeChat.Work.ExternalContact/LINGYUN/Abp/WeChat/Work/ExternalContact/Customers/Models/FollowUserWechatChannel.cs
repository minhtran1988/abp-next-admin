using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Customers.Models;
/// <summary>
/// Video number information
/// </summary>
public class FollowUserWechatChannel
{
    /// <summary>
    /// Video account name
    /// </summary>
    [NotNull]
    [JsonProperty("nickname")]
    [JsonPropertyName("nickname")]
    public string NickName { get; set; } = default!;
    /// <summary>
    /// Add scene to video number
    /// </summary>
    [NotNull]
    [JsonProperty("source")]
    [JsonPropertyName("source")]
    public FollowUserWechatChannelSource Source { get; set; }
}
