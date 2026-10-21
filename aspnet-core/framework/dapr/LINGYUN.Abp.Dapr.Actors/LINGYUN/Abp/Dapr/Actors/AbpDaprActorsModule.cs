using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace LINGYUN.Abp.Dapr.Actors;

[DependsOn(
    typeof(AbpHttpClientModule)
    )]
public class AbpDaprActorsModule : AbpModule
{
    /// <summary>
    /// Integrate with AbpHttpClient and create a named HttpClient
    /// </summary>
    internal const string DaprHttpClient = "_AbpDaprActorsClient";

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClient(DaprHttpClient);
    }
}
