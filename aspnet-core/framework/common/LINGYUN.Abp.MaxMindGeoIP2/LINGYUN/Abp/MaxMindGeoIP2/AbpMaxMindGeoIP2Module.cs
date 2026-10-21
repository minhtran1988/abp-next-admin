using LINGYUN.Abp.IP.Location;
using MaxMind.GeoIP2;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.Modularity;

namespace LINGYUN.Abp.MaxMindGeoIP2;

/// <summary>
/// MaxMind GeoIP2 module
/// </summary>
/// <remarks>
/// The offline library (GeoLite2-City.mmdb) is subject to the MaxMind license and is not distributed by this module:
/// The user needs to specify the file path through <c>MaxMindGeoIP2:DatabaseFile</c>.
/// Use <see cref="EmptyGeoIP2DatabaseReader"/> when the file does not exist and log a warning
/// </remarks>
[DependsOn(typeof(AbpIPLocationModule))]
public class AbpMaxMindGeoIP2Module : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        Configure<AbpMaxMindGeoIP2Options>(configuration.GetSection("MaxMindGeoIP2"));

        Configure<AbpIPLocationResolveOptions>(options =>
        {
            options.IPLocationResolvers.Add(new MaxMindGeoIP2IPLocationResolveContributor());
        });

        context.Services.AddSingleton<IGeoIP2DatabaseReader>((serviceProvider) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<AbpMaxMindGeoIP2Options>>().Value;
            var logger = serviceProvider.GetService<ILogger<AbpMaxMindGeoIP2Module>>();

            if (options.DatabaseFileExists())
            {
                logger?.LogInformation(
                    "The MaxMind GeoIP2 database was loaded from {DatabaseFile} with {FileAccessMode}.",
                    options.DatabaseFile,
                    options.FileAccessMode);

                return new DatabaseReader(options.DatabaseFile, options.FileAccessMode);
            }

            // Use an empty interpreter when no offline library is provided (or the file does not exist) to avoid blocking the business
            logger?.LogWarning(
                "The MaxMind GeoIP2 database file was not found: {DatabaseFile}, " +
                "IP location resolution will return empty results. " +
                "Download a GeoLite2 database from MaxMind (its license applies) " +
                "and set MaxMindGeoIP2:DatabaseFile.",
                options.DatabaseFile);

            return new EmptyGeoIP2DatabaseReader();
        });
    }
}
