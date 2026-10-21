using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Tags.Request;
/// <summary>
/// Tag member change request parameters
/// </summary>
public class WeChatWorkTagChangeMemberRequest : WeChatWorkRequest
{
    /// <summary>
    /// Tag id, non-negative integer type. When this parameter is specified, the new tag will generate a corresponding tag id. If not specified, the current largest id will be incremented.
    /// </summary>
    [NotNull]
    [JsonProperty("tagid")]
    [JsonPropertyName("tagid")]
    public int TagId { get; set; }
    /// <summary>
    /// Enterprise member ID list, note: userlist and partylist cannot be empty at the same time, and the number of single requests does not exceed 1000
    /// </summary>
    [CanBeNull]
    [JsonProperty("userlist")]
    [JsonPropertyName("userlist")]
    public List<string>? Users { get; set; }
    /// <summary>
    /// Enterprise department ID list, note: userlist and partylist cannot be empty at the same time, and the number of single requests does not exceed 100
    /// </summary>
    [CanBeNull]
    [JsonProperty("partylist")]
    [JsonPropertyName("partylist")]
    public List<int>? Parts { get; set; }
    public WeChatWorkTagChangeMemberRequest(
        int tagId,
        List<string>? users = null,
        List<int>? parts = null)
    {
        TagId = Check.Positive(tagId, nameof(tagId));
        Users = users;
        Parts = parts;

        if (users == null && parts == null)
        {
            throw new ArgumentNullException("users/parts", "userlist and partylist cannot be empty at the same time!");
        }
        if (users?.Count > 1000)
        {
            throw new ArgumentOutOfRangeException(nameof(users), "The number of single requests for the enterprise member ID list does not exceed 1000!");
        }
        if (parts?.Count > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(users), "The number of single requests for the enterprise department ID list does not exceed 100!");
        }
    }
}
