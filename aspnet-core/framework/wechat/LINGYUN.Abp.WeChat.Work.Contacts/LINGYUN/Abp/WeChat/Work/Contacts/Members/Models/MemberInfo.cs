using JetBrains.Annotations;
using LINGYUN.Abp.WeChat.Work.Common.Members.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Members.Models;
/// <summary>
/// Member details
/// </summary>
public class MemberInfo
{
    /// <summary>
    /// Globally unique
    /// </summary>
    [NotNull]
    [JsonProperty("open_userid")]
    [JsonPropertyName("open_userid")]
    public string OpenUserId { get; set; } = default!;
    /// <summary>
    /// Member UserID
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
    /// <summary>
    /// Mobile phone number
    /// </summary>
    [CanBeNull]
    [JsonProperty("mobile")]
    [JsonPropertyName("mobile")]
    public string? Mobile { get; set; }
    /// <summary>
    /// List of department IDs to which members belong
    /// </summary>
    [NotNull]
    [JsonProperty("department")]
    [JsonPropertyName("department")]
    public int[] Department { get; set; } = default!;
    /// <summary>
    /// Main department
    /// </summary>
    [CanBeNull]
    [JsonProperty("main_department")]
    [JsonPropertyName("main_department")]
    public int? MainDepartment { get; set; }
    /// <summary>
    /// The sorting value within the department, the default is 0. The quantity must be consistent with the department. The larger the value, the higher the order. The value range is [0, 2^32)
    /// </summary>
    [NotNull]
    [JsonProperty("order")]
    [JsonPropertyName("order")]
    public int[]? Order { get; set; }
    /// <summary>
    /// Employee personal QR code
    /// </summary>
    [CanBeNull]
    [JsonProperty("qr_code")]
    [JsonPropertyName("qr_code")]
    public string? QrCode { get; set; }
    /// <summary>
    /// Job information
    /// </summary>
    [CanBeNull]
    [JsonProperty("position")]
    [JsonPropertyName("position")]
    public string? Position { get; set; }
    /// <summary>
    /// Gender
    /// </summary>
    [CanBeNull]
    [JsonProperty("gender")]
    [JsonPropertyName("gender")]
    public Gender? Gender { get; set; }
    /// <summary>
    /// Email
    /// </summary>
    [CanBeNull]
    [JsonProperty("email")]
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    /// <summary>
    /// Business email
    /// </summary>
    [CanBeNull]
    [JsonProperty("biz_mail")]
    [JsonPropertyName("biz_mail")]
    public string? BizEmail { get; set; }
    /// <summary>
    /// Indicates whether you are the head of the department in your department, the number is consistent with department
    /// </summary>
    [CanBeNull]
    [JsonProperty("is_leader_in_dept")]
    [JsonPropertyName("is_leader_in_dept")]
    public int[]? IsLeaderInDept { get; set; }
    /// <summary>
    /// Direct superior UserID, returns the list of direct superiors within the visible range of the application, there is at most 1 direct superior
    /// </summary>
    [CanBeNull]
    [JsonProperty("direct_leader")]
    [JsonPropertyName("direct_leader")]
    public string[]? DirectLeader { get; set; }
    /// <summary>
    /// Avatar url
    /// </summary>
    [CanBeNull]
    [JsonProperty("avatar")]
    [JsonPropertyName("avatar")]
    public string? Avatar { get; set; }
    /// <summary>
    /// Avatar thumbnail url
    /// </summary>
    [CanBeNull]
    [JsonProperty("thumb_avatar")]
    [JsonPropertyName("thumb_avatar")]
    public string? ThumbAvatar { get; set; }
    /// <summary>
    /// Landline
    /// </summary>
    [CanBeNull]
    [JsonProperty("telephone")]
    [JsonPropertyName("telephone")]
    public string? TelePhone { get; set; }
    /// <summary>
    /// Alias
    /// </summary>
    [CanBeNull]
    [JsonProperty("alias")]
    [JsonPropertyName("alias")]
    public string? Alias { get; set; }
    /// <summary>
    /// address
    /// </summary>
    [CanBeNull]
    [JsonProperty("address")]
    [JsonPropertyName("address")]
    public string? Address { get; set; }
    /// <summary>
    /// Activation status
    /// </summary>
    [NotNull]
    [JsonProperty("status")]
    [JsonPropertyName("status")]
    public MemberStatus Status { get; set; }
    /// <summary>
    /// Extended attributes
    /// </summary>
    [CanBeNull]
    [JsonProperty("extattr")]
    [JsonPropertyName("extattr")]
    public MemberExternalAttribute? ExternalAttribute { get; set; }
    /// <summary>
    /// External position, if this value is set, this will be used as the position displayed externally, otherwise it will be displayed as position.
    /// </summary>
    [CanBeNull]
    [JsonProperty("external_position")]
    [JsonPropertyName("external_position")]
    public string? ExternalPosition { get; set; }
    /// <summary>
    /// Customized display information for external contacts
    /// </summary>
    [CanBeNull]
    [JsonProperty("external_profile")]
    [JsonPropertyName("external_profile")]
    public ExternalProfile? ExternalProfile { get; set; }
}
