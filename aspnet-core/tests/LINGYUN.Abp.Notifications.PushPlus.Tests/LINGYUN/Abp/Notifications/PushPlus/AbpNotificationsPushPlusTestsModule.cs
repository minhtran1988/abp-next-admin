using LINGYUN.Abp.PushPlus.Message;
using LINGYUN.Abp.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.Modularity;

namespace LINGYUN.Abp.Notifications.PushPlus;

[DependsOn(
    typeof(AbpNotificationsPushPlusModule),
    typeof(AbpNotificationsTestsModule),
    typeof(AbpTestsBaseModule))]
public class AbpNotificationsPushPlusTestsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // Replace the real sending with a capturing fake implementation to avoid actually calling the PushPlus interface
        context.Services.Replace(
            ServiceDescriptor.Singleton<IPushPlusMessageSender, FakePushPlusMessageSender>());
    }
}
