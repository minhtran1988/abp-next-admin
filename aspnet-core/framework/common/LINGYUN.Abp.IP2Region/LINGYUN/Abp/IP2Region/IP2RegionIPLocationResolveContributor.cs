using IP2Region.Net.Abstractions;
using LINGYUN.Abp.IP.Location;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace LINGYUN.Abp.IP2Region;
public class IP2RegionIPLocationResolveContributor : IPLocationResolveContributorBase
{
    public const string ContributorName = "IP2Region";
    public override string Name => ContributorName;

    public override Task ResolveAsync(IIPLocationResolveContext context)
    {
        // Illegal addresses, local loopbacks and unspecified addresses are handled uniformly by IIPLocationResolver and will not be judged here.
        var searcher = context.ServiceProvider.GetRequiredService<ISearcher>();
        var region = searcher.Search(context.IpAddress);

        if (string.IsNullOrWhiteSpace(region))
        {
            return Task.CompletedTask;
        }

        var regions = region!.Split('|');
        // |    0    |      1      |  2 |  3   |   4  | 5 | 6 |
        // 39.128.0.0|39.128.31.255|China|Yunnan Province|Kunming City|Mobile|CN
        // regions:
        // China | Yunnan Province | Kunming City | Mobile | CN

        // Special address segments such as intranet, link local, multicast, reserved addresses (offline library returns Reserved|Reserved|Reserved|0|0):
        // Do not return country/province/city, Remarks is empty
        if (string.Equals(regions[0], "Reserved", StringComparison.OrdinalIgnoreCase))
        {
            context.Location = new IPLocation();

            return Task.CompletedTask;
        }

        context.Location = CreateIPLocation(
            context,
            regions.Length >= 1 && !string.Equals(regions[0], "0") ? regions[0] : null,
            regions.Length >= 2 && !string.Equals(regions[1], "0") ? regions[1] : null,
            regions.Length >= 3 && !string.Equals(regions[2], "0") ? regions[2] : null);

        return Task.CompletedTask;
    }
}
