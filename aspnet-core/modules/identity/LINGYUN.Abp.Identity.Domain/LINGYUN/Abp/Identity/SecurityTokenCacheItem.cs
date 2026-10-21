using System;

namespace LINGYUN.Abp.Identity;

/// <summary>
/// Security token verification cache
/// </summary>
public class SecurityTokenCacheItem
{
    /// <summary>
    /// Token used for verification
    /// </summary>
    public string Token { get; set; } = default!;
    /// <summary>
    /// User ID used for verification
    /// </summary>
    public Guid UserId { get; set; }
    /// <summary>
    /// Security token used for verification
    /// </summary>
    public string SecurityToken { get; set; } = default!;

    public SecurityTokenCacheItem()
    {

    }

    public SecurityTokenCacheItem(string token, Guid userId, string securityToken)
    {
        Token = token;
        UserId = userId;
        SecurityToken = securityToken;
    }

    /// <summary>
    /// Generate query Key
    /// </summary>
    /// <param name="phoneNumber">Mobile phone number</param>
    /// <param name="purpose">Security token purpose</param>
    /// <returns></returns>
    public static string CalculateSmsCacheKey(string phoneNumber, string purpose)
    {
        return "Totp:" + purpose + ";p:" + phoneNumber;
    }

    /// <summary>
    /// Generate query Key
    /// </summary>
    /// <param name="email">Email address</param>
    /// <param name="purpose">Security token purpose</param>
    /// <returns></returns>
    public static string CalculateEmailCacheKey(string email, string purpose)
    {
        return "Totp:" + purpose + ";e:" + email;
    }
}
