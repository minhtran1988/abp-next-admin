using LINGYUN.Abp.AspNetCore.Mvc.Wrapper.Filters;
using LINGYUN.Abp.AspNetCore.Mvc.Wrapper.Localization;
using LINGYUN.Abp.AspNetCore.Wrapper;
using LINGYUN.Abp.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.ApiExploring;
using Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations;
using Volo.Abp.AspNetCore.Mvc.ProxyScripting;
using Volo.Abp.Content;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace LINGYUN.Abp.AspNetCore.Mvc.Wrapper;

[DependsOn(
    typeof(AbpWrapperModule),
    typeof(AbpAspNetCoreWrapperModule))]
public class AbpAspNetCoreMvcWrapperModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpAspNetCoreMvcWrapperModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<AbpMvcWrapperResource>("en")
                .AddVirtualJson("/LINGYUN/Abp/AspNetCore/Mvc/Wrapper/Localization/Resources");
        });

        Configure<MvcOptions>(mvcOptions =>
        {
            // Wrap Result Filter
            mvcOptions.Filters.AddService(typeof(AbpWrapResultFilter));
        });

        Configure<AbpWrapperOptions>(options =>
        {
            // Do not wrap the return result even if the endpoint is overridden
            // api/abp/api-definition
            // options.IgnoreReturnTypes.Add<ApplicationApiDescriptionModel>();
            // api/abp/application-configuration
            //  options.IgnoreReturnTypes.Add<ApplicationConfigurationDto>();
            // api/abp/application-localization
            // options.IgnoreReturnTypes.Add<ApplicationLocalizationDto>(); 
            // file stream
            options.IgnoreReturnTypes.Add<IRemoteStreamContent>();
            // options.IgnoreReturnTypes.Add<FileResult>();

            // options.IgnoreReturnTypes.Add<ViewResult>();
            // options.IgnoreReturnTypes.Add<ViewEngineResult>();
            // options.IgnoreReturnTypes.Add<ViewComponentResult>();

            // options.IgnoreReturnTypes.Add<RedirectToActionResult>();
            // options.IgnoreReturnTypes.Add<RedirectToPageResult>();
            // options.IgnoreReturnTypes.Add<RedirectToRouteResult>();

            // options.IgnoreReturnTypes.Add<SignInResult>();
            // options.IgnoreReturnTypes.Add<SignOutResult>();
            // options.IgnoreReturnTypes.Add<ForbidResult>();

            // options.IgnoreControllers.Add<AbpApplicationLocalizationController>();
            // options.IgnoreControllers.Add<AbpApplicationConfigurationController>();

            // Api Endpoints
            options.IgnoreControllers.Add<AbpApiDefinitionController>();
            // Abp/ServiceProxyScript
            options.IgnoreControllers.Add<AbpServiceProxyScriptController>();
            // Application Configuration Script
            options.IgnoreControllers.Add<AbpApplicationConfigurationScriptController>();

            // Official modules do not wrap results
            // options.IgnoreNamespaces.Add("Volo.Abp");

            // oidc endpoint does not wrap results
            options.IgnorePrefixUrls.Add("/connect");

            // Return localized 404 error message
            options.MessageWithEmptyResult = (serviceProvider) =>
            {
                var localizer = serviceProvider.GetRequiredService<IStringLocalizer<AbpMvcWrapperResource>>();
                return localizer["Wrapper:NotFound"];
            };
        });
    }
}
