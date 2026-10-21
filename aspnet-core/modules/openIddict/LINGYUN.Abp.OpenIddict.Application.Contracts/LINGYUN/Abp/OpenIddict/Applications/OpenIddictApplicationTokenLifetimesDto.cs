using Volo.Abp.Validation;

namespace LINGYUN.Abp.OpenIddict.Applications;
public class OpenIddictApplicationTokenLifetimesDto
{
    /// <summary>
    /// Access token life cycle(s)
    /// </summary>
    [DynamicRange(
        typeof(OpenIddictApplicationTokenLifetimeConsts),
        typeof(long),
        nameof(OpenIddictApplicationTokenLifetimeConsts.MinAccessTokenValue),
        nameof(OpenIddictApplicationTokenLifetimeConsts.MaxAccessTokenValue))]
    public long? AccessToken { get; set; }
    /// <summary>
    /// Authorization code life cycle (s)
    /// </summary>
    [DynamicRange(
        typeof(OpenIddictApplicationTokenLifetimeConsts),
        typeof(long),
        nameof(OpenIddictApplicationTokenLifetimeConsts.MinAuthorizationCodeValue),
        nameof(OpenIddictApplicationTokenLifetimeConsts.MaxAuthorizationCodeValue))]
    public long? AuthorizationCode { get; set; }
    /// <summary>
    /// Device code life cycle (s)
    /// </summary>
    [DynamicRange(
        typeof(OpenIddictApplicationTokenLifetimeConsts),
        typeof(long),
        nameof(OpenIddictApplicationTokenLifetimeConsts.MinDeviceCodeValue),
        nameof(OpenIddictApplicationTokenLifetimeConsts.MaxDeviceCodeValue))]
    public long? DeviceCode { get; set; }
    /// <summary>
    /// Identity token life cycle(s)
    /// </summary>
    [DynamicRange(
        typeof(OpenIddictApplicationTokenLifetimeConsts),
        typeof(long),
        nameof(OpenIddictApplicationTokenLifetimeConsts.MinIdentityTokenValue),
        nameof(OpenIddictApplicationTokenLifetimeConsts.MaxIdentityTokenValue))]
    public long? IdentityToken { get; set; }
    /// <summary>
    /// Refresh token life cycle (s)
    /// </summary>
    [DynamicRange(
        typeof(OpenIddictApplicationTokenLifetimeConsts),
        typeof(long),
        nameof(OpenIddictApplicationTokenLifetimeConsts.MinRefreshTokenValue),
        nameof(OpenIddictApplicationTokenLifetimeConsts.MaxRefreshTokenValue))]
    public long? RefreshToken { get; set; }
    /// <summary>
    /// User code life cycle(s)
    /// </summary>
    [DynamicRange(
        typeof(OpenIddictApplicationTokenLifetimeConsts),
        typeof(long),
        nameof(OpenIddictApplicationTokenLifetimeConsts.MinUserCodeValue),
        nameof(OpenIddictApplicationTokenLifetimeConsts.MaxUserCodeValue))]
    public long? UserCode { get; set; }
}
