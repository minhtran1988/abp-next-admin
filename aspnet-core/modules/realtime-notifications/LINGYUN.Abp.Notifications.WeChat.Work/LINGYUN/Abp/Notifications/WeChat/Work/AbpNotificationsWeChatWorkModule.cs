using LINGYUN.Abp.WeChat.Work;
using LINGYUN.Abp.WeChat.Work.Localization;
using LINGYUN.Abp.WeChat.Work.Messages.Models;
using Volo.Abp.Json.SystemTextJson;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace LINGYUN.Abp.Notifications.WeChat.Work;

[DependsOn(
    typeof(AbpWeChatWorkModule),
    typeof(AbpNotificationsModule))]
public class AbpNotificationsWeChatWorkModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpNotificationsWeChatWorkModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<WeChatWorkResource>()
                .AddVirtualJson("/LINGYUN/Abp/Notifications/WeChat/Work/Localization/Resources");
        });

        Configure<AbpNotificationsPublishOptions>(options =>
        {
            options.PublishProviders.Add<WeChatWorkNotificationPublishProvider>();
        });

        // The template card is an abstract type: register a type converter to enable it to and from channel data (JSON)
        Configure<AbpSystemTextJsonSerializerOptions>(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new TemplateCardSystemTextJsonConverter());
        });

        // Declare the payload formats supported by this platform (the registration period is to verify the existence contract)
        Configure<AbpNotificationChannelDataOptions>(options =>
        {
            options.Add<WeChatWorkMiniProgramNoticeChannelData>();
            options.Add<WeChatWorkMpNewChannelData>();
            options.Add<WeChatWorkTemplateCardChannelData>();
            options.Add<WeChatWorkTextCardChannelData>();
            options.Add<WeChatWorkVideoChannelData>();
            options.Add<WeChatWorkVoiceChannelData>();
            options.Add<WeChatWorkFileChannelData>();
        });

        // Message carrier factory dispatch
        Configure<AbpWeChatWorkNotificationMessageOptions>(options =>
        {
            options.MapDefaultMessageFactories();
        });
    }
}
