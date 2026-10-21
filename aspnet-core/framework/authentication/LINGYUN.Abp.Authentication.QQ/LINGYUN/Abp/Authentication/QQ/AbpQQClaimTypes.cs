namespace LINGYUN.Abp.Authentication.QQ;

/// <summary>
/// QQ Internet identity type can be configured by yourself like <see cref="Volo.Abp.Security.Claims.AbpClaimTypes"/>
/// <br />
/// See: <see cref="https://wiki.connect.qq.com/get_user_info"/>
/// </summary>
public class AbpQQClaimTypes
{
    /// <summary>
    /// The user's unique identifier
    /// </summary>
    public static string OpenId { get; set; } = "qq-openid"; // Can be changed
    /// <summary>
    /// User nickname
    /// </summary>
    public static string NickName { get; set; } = "nickname";
    /// <summary>
    /// Gender. If it cannot be obtained, it will return "male" by default.
    /// </summary>
    public static string Gender { get; set; } = "gender";
    /// <summary>
    /// User avatar, taken from field: figureurl_qq_1
    /// </summary>
    /// <remarks>
    /// According to QQ Internet documents, 40x40 avatars will definitely exist, only 40x40 avatars are taken.
    /// see: https://wiki.connect.qq.com/get_user_info
    /// </remarks>
    public static string AvatarUrl { get; set; } = "avatar";
}
