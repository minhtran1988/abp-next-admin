using LINGYUN.Abp.AuditLogging.OpenObserve.Localization;
using LINGYUN.Abp.OpenObserve;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Json;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace LINGYUN.Abp.AuditLogging.OpenObserve;

/// <summary>
/// Audit log OpenObserve module
/// </summary>
/// <remarks>
/// The module does not do any processing on the data stream (no detection, no creation, no modification of settings):
/// OpenObserve will automatically create a data stream when writing for the first time. Please configure the retention policy and other settings on the OpenObserve side.
/// </remarks>
[DependsOn(
    typeof(AbpAuditLoggingModule),
    typeof(AbpOpenObserveModule),
    typeof(AbpJsonModule),
    typeof(AbpLocalizationModule))]
public class AbpAuditLoggingOpenObserveModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        Configure<AbpAuditLoggingOpenObserveOptions>(configuration.GetSection("AuditLogging:OpenObserve"));

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<AuditLoggingOpenObserveResource>()
                .AddVirtualJson("/LINGYUN/Abp/AuditLogging/OpenObserve/Localization/Resources");
        });

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpAuditLoggingOpenObserveModule>();
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace(AuditLoggingOpenObserveErrorCodes.Namespace, typeof(AuditLoggingOpenObserveResource));
        });
    }
}
