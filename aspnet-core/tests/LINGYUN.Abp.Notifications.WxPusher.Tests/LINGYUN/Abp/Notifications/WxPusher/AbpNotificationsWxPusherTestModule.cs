using LINGYUN.Abp.WxPusher.Messages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.Modularity;

namespace LINGYUN.Abp.Notifications.WxPusher;

[DependsOn(
    typeof(AbpNotificationsWxPusherModule),
    typeof(AbpNotificationsTestsModule))]
public class AbpNotificationsWxPusherTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // Replace the real sending with a capturing fake implementation to avoid actually calling the WxPusher interface
        context.Services.Replace(
            ServiceDescriptor.Singleton<IWxPusherMessageSender, FakeWxPusherMessageSender>());
    }
}
