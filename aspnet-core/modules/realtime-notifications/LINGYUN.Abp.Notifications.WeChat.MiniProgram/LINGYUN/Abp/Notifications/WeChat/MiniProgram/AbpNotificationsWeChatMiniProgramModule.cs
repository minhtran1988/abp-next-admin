using LINGYUN.Abp.WeChat.MiniProgram;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace LINGYUN.Abp.Notifications.WeChat.MiniProgram;

[DependsOn(
    typeof(AbpWeChatMiniProgramModule), 
    typeof(AbpNotificationsModule))]
public class AbpNotificationsWeChatMiniProgramModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var preActions = context.Services.GetPreConfigureActions<AbpNotificationsWeChatMiniProgramOptions>();

        Configure<AbpNotificationsWeChatMiniProgramOptions>(options =>
        {
            preActions.Configure(options);
        });

        Configure<AbpNotificationsPublishOptions>(options =>
        {
            options.PublishProviders.Add<WeChatMiniProgramNotificationPublishProvider>();
        });

        // Declare the payload formats supported by this platform (the registration period is to verify the existence contract)
        Configure<AbpNotificationChannelDataOptions>(options =>
        {
            options.Add<WeChatMiniProgramSubscribeChannelData>();
        });
    }
}
