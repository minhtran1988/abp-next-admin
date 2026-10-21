using LINGYUN.Abp.MessageService.Localization;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.Modularity;

namespace LINGYUN.Abp.MessageService;

[DependsOn(
    typeof(AbpMessageServiceApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule)
    )]
public class AbpMessageServiceHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(AbpMessageServiceHttpApiModule).Assembly);
        });

        PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(MessageServiceResource), // Resources for localization
                typeof(AbpMessageServiceApplicationContractsModule).Assembly); // The assembly where Dto is located
        });
    }
}
