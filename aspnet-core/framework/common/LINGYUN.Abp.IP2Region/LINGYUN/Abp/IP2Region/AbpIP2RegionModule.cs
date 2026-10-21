using IP2Region.Net.Abstractions;
using LINGYUN.Abp.IP.Location;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using Volo.Abp;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace LINGYUN.Abp.IP2Region;

[DependsOn(typeof(AbpIPLocationModule))]
[DependsOn(typeof(AbpVirtualFileSystemModule))]
public class AbpIP2RegionModule : AbpModule
{
    /// <summary>
    /// Built-in IPv4 offline library (virtual file system path)
    /// </summary>
    public const string DefaultIPv4DatabaseFile = "/LINGYUN/Abp/IP2Region/Resources/ip2region_v4.xdb";

    /// <summary>
    /// Built-in IPv6 offline library (virtual file system path)
    /// </summary>
    public const string DefaultIPv6DatabaseFile = "/LINGYUN/Abp/IP2Region/Resources/ip2region_v6.xdb";

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        Configure<AbpIP2RegionOptions>(configuration.GetSection("IP2Region"));

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpIP2RegionModule>();
        });

        context.Services.AddSingleton<ISearcher>((serviceProvider) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<AbpIP2RegionOptions>>().Value;

            // Physical files are given priority to facilitate replacement/update of offline libraries; otherwise, built-in resources are used
            var ipv4XdbStream = CreateDatabaseStream(serviceProvider, options.IPv4DatabaseFile, true);

            // The IPv6 data file is large and optional (the size can be reduced by excluding the resource when packaging). It will not be loaded if it does not exist. IPv6 query returns null.
            var ipv6XdbStream = CreateDatabaseStream(serviceProvider, options.IPv6DatabaseFile, false);

            // Automatically select data files by address family
            return new AbpSearcher(options.CachePolicy, ipv4XdbStream!, ipv6XdbStream);
        });

        Configure<AbpIPLocationResolveOptions>(options =>
        {
            options.IPLocationResolvers.Add(new IP2RegionIPLocationResolveContributor());
        });
    }

    /// <summary>
    /// Create offline library data stream
    /// </summary>
    /// <remarks>
    /// Physical files take priority, followed by virtual file system paths (built-in resources)
    /// </remarks>
    private static Stream? CreateDatabaseStream(IServiceProvider serviceProvider, string databaseFile, bool required)
    {
        if (File.Exists(databaseFile))
        {
            return File.OpenRead(databaseFile);
        }

        var virtualFileProvider = serviceProvider.GetRequiredService<IVirtualFileProvider>();
        var databaseFileInfo = virtualFileProvider.GetFileInfo(databaseFile);
        if (databaseFileInfo.Exists)
        {
            return databaseFileInfo.CreateReadStream();
        }

        if (required)
        {
            throw new AbpException(
                $"IP2Region database file not found: {databaseFile}. " +
                "Please check the IP2Region:IPv4DatabaseFile option or the embedded ip2region_v4.xdb resource.");
        }

        return null;
    }
}
