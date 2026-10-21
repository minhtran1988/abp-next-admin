using LINGYUN.Abp.IP.Location;
using LINGYUN.Abp.Tests;
using Volo.Abp.Modularity;

namespace LINGYUN.Abp.IP2Region;

[DependsOn(
    typeof(AbpIP2RegionModule),
    typeof(AbpTestsBaseModule))]
public class AbpIP2RegionTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpIPLocationResolveOptions>(options =>
        {
            // China IP does not show country (removed from remarks)
            options.RemoveCountry = new[] { "\u4e2d\u56fd" };
        });
    }
}