namespace LINGYUN.Abp.AspNetCore.Mvc.Client;

public class AbpAspNetCoreMvcClientCacheOptions
{
    /// <summary>
    /// User cache expiration time, unit: seconds
    /// Default: 300
    /// </summary>
    public int UserCacheExpirationSeconds { get; set; } = 300;
    /// <summary>
    /// Anonymous user cache expiration time, unit: seconds
    /// Default: 300
    /// </summary>
    public int AnonymousCacheExpirationSeconds { get; set; } = 300;
}
