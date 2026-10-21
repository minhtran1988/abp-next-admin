using System;

namespace LINGYUN.Abp.OpenApi;

public class AbpOpenApiOptions
{
    /// <summary>
    /// Enable Api signature checking
    /// </summary>
    /// <remarks>
    /// Default: true
    /// </remarks>
    public bool IsEnabled { get; set; }
    /// <summary>
    /// Request random number expiration time
    /// </summary>
    /// <remarks>
    /// Default: 10 minutes
    /// </remarks>
    public TimeSpan RequestNonceExpireIn { get; set; }

    public AbpOpenApiOptions()
    {
        IsEnabled = true;

        RequestNonceExpireIn = TimeSpan.FromMinutes(10);
    }
}
