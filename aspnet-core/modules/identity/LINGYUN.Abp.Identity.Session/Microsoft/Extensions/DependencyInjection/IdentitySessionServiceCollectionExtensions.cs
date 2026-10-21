using LINGYUN.Abp.Identity.Session;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection;
public static class IdentitySessionServiceCollectionExtensions
{
    /// <summary>
    /// Allow any session
    /// </summary>
    /// <remarks>
    /// In some scenarios, the cache configuration may be different, so you do not need to check the session cache.
    /// </remarks>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddAlwaysAllowSession(this IServiceCollection services)
    {
        services.Replace(
            ServiceDescriptor.Singleton<IIdentitySessionChecker, AllowAnonymousIdentitySessionChecker>());

        return services;
    }
}
