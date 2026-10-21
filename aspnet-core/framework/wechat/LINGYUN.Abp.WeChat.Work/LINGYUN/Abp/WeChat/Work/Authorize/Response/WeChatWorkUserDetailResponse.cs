using LINGYUN.Abp.WeChat.Work.Authorize.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Authorize.Response;
/// <summary>
/// Enterprise WeChat user details response
/// </summary>
public class WeChatWorkUserDetailResponse : WeChatWorkResponse
{
    /// <summary>
    /// Member UserID
    /// </summary>
    [JsonProperty("userid")]
    [JsonPropertyName("userid")]
    public string UserId { get; set; } = default!;
    /// <summary>
    /// Gender.
    /// 0 means undefined,
    /// 1 means male,
    /// 2 means female.
    /// Only returns the true value when the user agrees to snsapi_privateinfo authorization, otherwise returns 0
    /// </summary>
    [JsonProperty("gender")]
    [JsonPropertyName("gender")]
    public WeChatWorkGender Gender { get; set; }
    /// <summary>
    /// Avatar url.
    /// Only return the real avatar when the user agrees to snsapi_privateinfo authorization, otherwise return the default avatar
    /// </summary>
    [JsonProperty("avatar")]
    [JsonPropertyName("avatar")]
    public string? Avatar { get; set; }
    /// <summary>
    /// Employee personal QR code (scan to add as external contact)
    /// Only returned when the user agrees to snsapi_privateinfo authorization
    /// </summary>
    [JsonProperty("qr_code")]
    [JsonPropertyName("qr_code")]
    public string? QrCode { get; set; }
    /// <summary>
    /// mobile phone
    /// Only returned when the user agrees to snsapi_privateinfo authorization, and cannot be obtained by third-party applications
    /// </summary>
    [JsonProperty("mobile")]
    [JsonPropertyName("mobile")]
    public string? Mobile { get; set; }
    /// <summary>
    /// Email
    /// Only returned when the user agrees to snsapi_privateinfo authorization, and cannot be obtained by third-party applications
    /// </summary>
    [JsonProperty("email")]
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    /// <summary>
    /// Business email
    /// Only returned when the user agrees to snsapi_privateinfo authorization, and cannot be obtained by third-party applications
    /// </summary>
    [JsonProperty("biz_mail")]
    [JsonPropertyName("biz_mail")]
    public string? WorkEmail { get; set; }
    /// <summary>
    /// address
    /// Only returned when the user agrees to snsapi_privateinfo authorization, and cannot be obtained by third-party applications
    /// </summary>
    [JsonProperty("address")]
    [JsonPropertyName("address")]
    public string? Address { get; set; }
}
