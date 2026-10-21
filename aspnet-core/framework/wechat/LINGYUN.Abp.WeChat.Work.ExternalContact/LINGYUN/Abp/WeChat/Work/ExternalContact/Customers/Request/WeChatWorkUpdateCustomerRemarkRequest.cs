using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Customers.Request;
/// <summary>
/// Modify customer remark information request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/92115" />
/// </remarks>
public class WeChatWorkUpdateCustomerRemarkRequest : WeChatWorkRequest
{
    /// <summary>
    /// The userid of the enterprise member
    /// </summary>
    [NotNull]
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string UserId { get; }
    /// <summary>
    /// External contact userid
    /// </summary>
    [NotNull]
    [JsonProperty("external_userid")]
    [JsonPropertyName("external_userid")]
    public string ExternalUserId { get; }
    /// <summary>
    /// This user's remarks on external contacts, up to 20 characters
    /// </summary>
    [CanBeNull]
    [StringLength(20)]
    [JsonProperty("remark")]
    [JsonPropertyName("remark")]
    public string? Remark { get; }
    /// <summary>
    /// This user's description of the external contact, up to 150 characters
    /// </summary>
    [CanBeNull]
    [StringLength(150)]
    [JsonProperty("description")]
    [JsonPropertyName("description")]
    public string? Description { get; }
    /// <summary>
    /// The name of the company this user notes for external contacts, up to 20 characters
    /// </summary>
    [CanBeNull]
    [StringLength(20)]
    [JsonProperty("remark_company")]
    [JsonPropertyName("remark_company")]
    public string? RemarkCompany { get; }
    /// <summary>
    /// The mobile phone number this user noted for external contacts
    /// </summary>
    [CanBeNull]
    [JsonProperty("remark_mobiles")]
    [JsonPropertyName("remark_mobiles")]
    public List<string>? RemarkMobiles { get; }
    /// <summary>
    /// The mediaid of the note image
    /// </summary>
    [CanBeNull]
    [JsonProperty("remark_pic_mediaid")]
    [JsonPropertyName("remark_pic_mediaid")]
    public string? RemarkPictureMediaId { get; }
    public WeChatWorkUpdateCustomerRemarkRequest(
        string userId, 
        string externalUserId, 
        string? remark = null, 
        string? description = null, 
        string? remarkCompany = null,
        List<string>? remarkMobiles = null, 
        string? remarkPictureMediaId = null)
    {
        UserId = Check.NotNullOrWhiteSpace(userId, nameof(userId));
        ExternalUserId = Check.NotNullOrWhiteSpace(externalUserId, nameof(externalUserId));
        Remark = Check.Length(remark, nameof(remark), 20);
        Description = Check.Length(description, nameof(description), 150);
        RemarkCompany = Check.Length(remarkCompany, nameof(remarkCompany), 20);
        RemarkMobiles = remarkMobiles;
        RemarkPictureMediaId = remarkPictureMediaId;
    }
}
