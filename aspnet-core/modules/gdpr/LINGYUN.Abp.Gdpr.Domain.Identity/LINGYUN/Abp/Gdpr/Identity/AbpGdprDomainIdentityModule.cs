using LINGYUN.Abp.Gdpr.Localization;
using LINGYUN.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace LINGYUN.Abp.Gdpr.Identity;

[DependsOn(
    typeof(AbpGdprDomainModule),
    typeof(AbpIdentityDomainModule))]
public class AbpGdprDomainIdentityModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpGdprDomainIdentityModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<GdprResource>()
                .AddVirtualJson("/LINGYUN/Abp/Gdpr/Identity/Localization/Resources");
        });

        Configure<AbpGdprOptions>(options =>
        {
            // User data provider
            options.GdprUserDataProviders.Add(new AbpGdprIdentityUserDataProvider());
            // User account provider
            options.GdprUserAccountProviders.Add(new AbpGdprIdentityUserAccountProvider());
        });
    }
}
