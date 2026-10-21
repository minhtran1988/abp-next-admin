using LINGYUN.Abp.Tests;
using Volo.Abp.Modularity;

namespace LINGYUN.Abp.Notifications.Sms
{
    [DependsOn(
        typeof(AbpNotificationsSmsModule),
        typeof(AbpNotificationsTestsModule),
        typeof(AbpTestsBaseModule))]
    public class AbpNotificationsSmsTestsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            // Change default parameter prefix settings
            PreConfigure<AbpNotificationsSmsOptions>(options =>
            {
                options.TemplateParamsPrefix = "[sms-override]";
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            // Custom data processor settings
            Configure<AbpNotificationsPublishOptions>(options =>
            {
            });
        }
    }
}
