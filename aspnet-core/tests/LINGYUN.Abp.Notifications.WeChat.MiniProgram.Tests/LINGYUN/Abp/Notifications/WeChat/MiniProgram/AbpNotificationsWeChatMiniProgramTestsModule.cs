using LINGYUN.Abp.Tests;
using LINGYUN.Abp.WeChat.MiniProgram.Messages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.Modularity;

namespace LINGYUN.Abp.Notifications.WeChat.MiniProgram
{
    [DependsOn(
        typeof(AbpNotificationsWeChatMiniProgramModule),
        typeof(AbpNotificationsTestsModule),
        typeof(AbpTestsBaseModule))]
    public class AbpNotificationsWeChatMiniProgramTestsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<AbpNotificationsWeChatMiniProgramOptions>(options =>
            {
                // Default value, convenient for asserting "the bottom line when channel data is not written"
                options.DefaultTemplateId = "DEFAULT_TEMPLATE_ID";
                options.DefaultState = "formal";
                options.DefaultLanguage = "zh_CN";
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            // Replace the real sending with a capturing fake implementation to avoid actually calling the WeChat interface
            context.Services.Replace(
                ServiceDescriptor.Singleton<ISubscribeMessager, FakeSubscribeMessager>());
        }
    }
}
