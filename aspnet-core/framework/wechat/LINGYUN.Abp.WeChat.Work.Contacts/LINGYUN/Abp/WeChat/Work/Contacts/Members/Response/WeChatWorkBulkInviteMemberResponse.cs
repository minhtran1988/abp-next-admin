using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Members.Response;
/// <summary>
/// Invite member response parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/90975" />
/// </remarks>
public class WeChatWorkBulkInviteMemberResponse : WeChatWorkResponse
{
    /// <summary>
    /// Illegal member list
    /// </summary>
    [NotNull]
    [JsonProperty("invaliduser")]
    [JsonPropertyName("invaliduser")]
    public string[] InvalidUser { get; set; } = default!;
    /// <summary>
    /// List of illegal departments
    /// </summary>
    [NotNull]
    [JsonProperty("invalidparty")]
    [JsonPropertyName("invalidparty")]
    public int[] InvalidParty { get; set; } = default!;
    /// <summary>
    /// Illegal tag list
    /// </summary>
    [NotNull]
    [JsonProperty("invalidtag")]
    [JsonPropertyName("invalidtag")]
    public int[] InvalidTag { get; set; } = default!;
}