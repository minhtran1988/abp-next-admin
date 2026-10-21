using LINGYUN.Abp.OpenObserve;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace System.Net.Http;

internal static class HttpClientOpenObserveExtensions
{
    private const string HttpClientName = "__Abp_OpenObserve_Client";

    public static IServiceCollection AddOpenObserveClient(this IServiceCollection services)
    {
        services.AddHttpClient<OpenObserveClient>(
            HttpClientName,
            (sp, client) =>
            {
                var options = sp.GetRequiredService<IOptions<AbpOpenObserveOptions>>().Value;

                client.BaseAddress = new Uri(options.Endpoint, UriKind.Absolute);
                // The default timeout of the search interface is 600 seconds, and the default value of HttpClient is 100 seconds, which will interrupt the request.
                client.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);

                var authenticationHeader = options.CreateAuthorizationHeader();
                if (authenticationHeader != null)
                {
                    client.DefaultRequestHeaders.Authorization = authenticationHeader;
                }
            });

        return services;
    }
}
