using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals.Models;
/// <summary>
/// Member control configuration
/// </summary>
public class ContactControlConfig : ControlConfig
{
    /// <summary>
    /// Member and department controls
    /// </summary>
    [NotNull]
    [JsonProperty("contact")]
    [JsonPropertyName("contact")]
    public ContactConfig Contact { get; set; } = default!;
    public ContactControlConfig()
    {

    }

    public ContactControlConfig(ContactConfig contact)
    {
        Contact = contact;
    }
}

public class ContactConfig
{
    /// <summary>
    /// single-single selection, multi-multiple selection
    /// </summary>
    [NotNull]
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;
    /// <summary>
    /// user-member, department-department
    /// </summary>
    [NotNull]
    [JsonProperty("mode")]
    [JsonPropertyName("mode")]
    public string Mode { get; set; } = default!;
    public ContactConfig()
    {

    }

    private ContactConfig(string type, string mode)
    {
        Type = type;
        Mode = mode;
    }

    public static ContactConfig SingleUser()
    {
        return new ContactConfig("single", "user");
    }

    public static ContactConfig SingleDepartment()
    {
        return new ContactConfig("single", "department");
    }

    public static ContactConfig MultipleUser()
    {
        return new ContactConfig("multi", "user");
    }

    public static ContactConfig MultipleDepartment()
    {
        return new ContactConfig("multi", "department");
    }
}
