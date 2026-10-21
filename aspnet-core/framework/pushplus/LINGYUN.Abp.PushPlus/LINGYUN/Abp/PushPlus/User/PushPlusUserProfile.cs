using Newtonsoft.Json;
using System;

namespace LINGYUN.Abp.PushPlus.User;

public class PushPlusUserProfile
{
    /// <summary>
    /// The openId of user WeChat
    /// </summary>
    [JsonProperty("openId")]
    public string? OpenId { get; set; }
    /// <summary>
    /// UnionId of user WeChat
    /// </summary>
    [JsonProperty("unionId")]
    public string? UnionId { get; set; }
    /// <summary>
    /// Nickname
    /// </summary>
    [JsonProperty("nickName")]
    public string? NickName { get; set; }
    /// <summary>
    /// Avatar
    /// </summary>
    [JsonProperty("headImgUrl")]
    public string? HeadImgUrl { get; set; }
    /// <summary>
    /// Gender;
    /// 0 - not set,
    /// 1-Male,
    /// 2-Female
    /// </summary>
    [JsonProperty("userSex")]
    public PushPlusUserSex? Sex { get; set; }
    /// <summary>
    /// User token
    /// </summary>
    [JsonProperty("token")]
    public string? Token { get; set; }
    /// <summary>
    /// Mobile phone number
    /// </summary>
    [JsonProperty("phoneNumber")]
    public string? PhoneNumber { get; set; }
    /// <summary>
    /// Email
    /// </summary>
    [JsonProperty("email")]
    public string? Email { get; set; }
    /// <summary>
    /// Email verification status;
    /// 0 - not verified,
    /// 1-To be verified,
    /// 2-Verified
    /// </summary>
    [JsonProperty("emailStatus")]
    public PushPlusUserEmailStatus? EmailStatus { get; set; }
    /// <summary>
    /// birthday
    /// </summary>
    [JsonProperty("birthday")]
    public DateTime? Birthday { get; set; }
    /// <summary>
    /// User points
    /// </summary>
    [JsonProperty("points")]
    public int? Points { get; set; }
}
