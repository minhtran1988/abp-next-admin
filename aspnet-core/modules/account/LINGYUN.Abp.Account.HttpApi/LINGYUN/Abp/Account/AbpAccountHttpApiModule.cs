using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account.Localization;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.Modularity;

namespace LINGYUN.Abp.Account;

[DependsOn(
    typeof(AbpAccountApplicationContractsModule),
    typeof(Volo.Abp.Account.AbpAccountHttpApiModule))]
public class AbpAccountHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(AccountResource), typeof(AbpAccountApplicationContractsModule).Assembly);
            // Natively specified in the Web project, it is not required if there is no reference to the Web project
            options.AddAssemblyResource(typeof(AccountResource), typeof(Volo.Abp.Account.AbpAccountApplicationContractsModule).Assembly);
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(AbpAccountHttpApiModule).Assembly);
        });
    }
}
