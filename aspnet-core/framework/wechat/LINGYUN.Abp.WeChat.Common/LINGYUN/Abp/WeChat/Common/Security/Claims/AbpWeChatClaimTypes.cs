namespace LINGYUN.Abp.WeChat.Common.Security.Claims;

/// <summary>
/// WeChat authentication identity type can be configured by yourself like <see cref="Volo.Abp.Security.Claims.AbpClaimTypes"/>
/// <br />
/// See: <see cref="https://developers.weixin.qq.com/doc/offiaccount/OA_Web_Apps/Wechat_webpage_authorization.html"/>
/// </summary>
public class AbpWeChatClaimTypes
{
    /// <summary>
    /// The user's unique identifier
    /// </summary>
    public static string OpenId { get; set; } = "wx-openid"; // Can be changed
    /// <summary>
    /// This field will only appear after the user binds the official account to the WeChat open platform account.
    /// </summary>
    public static string UnionId { get; set; } = "wx-unionid"; //can be changed
    /// <summary>
    /// User nickname
    /// </summary>
    public static string NickName { get; set; } = "nickname";
    /// <summary>
    /// The gender of the user. When the value is 1, it is male, when the value is 2, it is female, and when the value is 0, it is unknown.
    /// </summary>
    public static string Sex { get; set; } = "sex";
    /// <summary>
    /// Country, such as China is CN
    /// </summary>
    public static string Country { get; set; } = "country";
    /// <summary>
    /// The province where the user’s personal information is filled in
    /// </summary>
    public static string Province { get; set; } = "province";
    /// <summary>
    /// The city where ordinary users fill in their personal data
    /// </summary>
    public static string City { get; set; } = "city";
    /// <summary>
    /// User avatar, the last value represents the square avatar size (0, 46, 64, 96, 132 values are available, 0 represents a 640*640 square avatar), this item is empty when the user does not have an avatar.
    /// If the user changes the avatar, the original avatar URL will be invalid.
    /// </summary>
    public static string AvatarUrl { get; set; } = "avatar";
    /// <summary>
    /// User privilege information, json array, such as WeChat Woka user is (chinaunicom)
    /// </summary>
    public static string Privilege { get; set; } = "privilege";
}
