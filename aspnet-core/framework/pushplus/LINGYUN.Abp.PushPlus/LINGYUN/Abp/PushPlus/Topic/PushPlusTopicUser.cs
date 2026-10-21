using LINGYUN.Abp.PushPlus.User;
using Newtonsoft.Json;

namespace LINGYUN.Abp.PushPlus.Topic;

public class PushPlusTopicUser
{
    /// <summary>
    /// User number; can be used to delete users
    /// </summary>
    [JsonProperty("id")]
    public int Id { get; set; }
    /// <summary>
    /// Nickname
    /// </summary>
    [JsonProperty("nickName")]
    public string? NickName { get; set; }
    /// <summary>
    /// User WeChat openId
    /// </summary>
    [JsonProperty("openId")]
    public string? OpenId { get; set; }
    /// <summary>
    /// Avatar url address
    /// </summary>
    [JsonProperty("headImgUrl")]
    public string? HeadImgUrl { get; set; }
    /// <summary>
    /// Avatar url address
    /// </summary>
    [JsonProperty("userSex")]
    public PushPlusUserSex? UserSex { get; set; }
    /// <summary>
    /// Whether to bind the mobile phone; 0-not bound, 1-bound
    /// </summary>
    [JsonProperty("havePhone")]
    public PushPlusUserPhoneBindStatus? HavePhone { get; set; }
    /// <summary>
    /// Whether to follow the WeChat official account; 0-not following, 1-already following
    /// </summary>
    [JsonProperty("isFollow")]
    public PushPlusUserFollowStatus? IsFollow { get; set; }
    /// <summary>
    /// Email verification status; 0-not verified, 1-to be verified, 2-verified
    /// </summary>
    [JsonProperty("emailStatus")]
    public PushPlusUserEmailStatus? EmailStatus { get; set; }
}
