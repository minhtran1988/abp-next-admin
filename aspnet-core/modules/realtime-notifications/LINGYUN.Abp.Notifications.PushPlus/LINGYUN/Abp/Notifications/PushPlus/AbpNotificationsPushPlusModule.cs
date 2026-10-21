using LINGYUN.Abp.PushPlus;
using Volo.Abp.Modularity;

namespace LINGYUN.Abp.Notifications.PushPlus;

[DependsOn(
    typeof(AbpNotificationsModule),
    typeof(AbpPushPlusModule))]
public class AbpNotificationsPushPlusModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpNotificationsPublishOptions>(options =>
        {
            options.PublishProviders.Add<PushPlusNotificationPublishProvider>();
        });

        // Declare the payload formats supported by this platform (the registration period is to verify the existence contract)
        Configure<AbpNotificationChannelDataOptions>(options =>
        {
            options.Add<PushPlusChannelData>();
        });
    }
}
