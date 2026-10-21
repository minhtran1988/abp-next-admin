using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Member control value
/// </summary>
public class MemberControlValue : ContactControlValue
{
    /// <summary>
    /// Selected member content, that is, the members selected by the applicant in this control, there can be multiple in multi-select mode
    /// </summary>
    [NotNull]
    [JsonProperty("members")]
    [JsonPropertyName("members")]
    public List<MemberValue> Members { get; set; } = default!;
    public MemberControlValue()
    {

    }

    private MemberControlValue(List<MemberValue> members)
    {
        Members = members;
    }

    public static MemberControlValue Single(MemberValue member)
    {
        return new MemberControlValue(new List<MemberValue> { member });
    }

    public static MemberControlValue Multiple(List<MemberValue> members)
    {
        return new MemberControlValue(members);
    }
}

public class MemberValue
{
    /// <summary>
    /// The userid of the selected member
    /// </summary>
    [NotNull]
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string UserId { get; set; } = default!;
    /// <summary>
    /// Member name
    /// </summary>
    [NotNull]
    [JsonProperty("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    public MemberValue()
    {

    }

    public MemberValue(string userId, string name)
    {
        UserId = userId;
        Name = name;
    }
}
