using LINGYUN.Abp.WeChat.Work;
using LINGYUN.Abp.WeChat.Work.Messages.Models;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Json.SystemTextJson;
using Volo.Abp.Modularity;

namespace LINGYUN.Abp.Notifications.Webhook.WeChat.Work;

[DependsOn(
    typeof(AbpNotificationsWebhookModule),
    typeof(AbpWeChatWorkModule))]
public class AbpNotificationsWebhookWeChatWorkModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // Allow users to override in PreConfigure (such as turning off markdown v2)
        var preActions = context.Services.GetPreConfigureActions<AbpNotificationsWebhookWeChatWorkOptions>();

        Configure<AbpNotificationsWebhookWeChatWorkOptions>(options =>
        {
            options.UseMarkdownV2 = true;

            preActions.Configure(options);
        });

        Configure<AbpNotificationsWebhookOptions>(options =>
        {
            options.Contributors.Add(new WeChatWorkWebhookNotificationContributor());
        });

        // The template card is an abstract type: register a type converter to enable it to and from channel data (JSON)
        Configure<AbpSystemTextJsonSerializerOptions>(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new WebhookTemplateCardSystemTextJsonConverter());
        });

        // Declare the payload formats supported by this platform (the registration period is to verify the existence contract)
        Configure<AbpNotificationChannelDataOptions>(options =>
        {
            options.Add<WeChatWorkWebhookTextChannelData>();
            options.Add<WeChatWorkWebhookMarkdownChannelData>();
            options.Add<WeChatWorkWebhookMarkdownV2ChannelData>();
            options.Add<WeChatWorkWebhookImageChannelData>();
            options.Add<WeChatWorkWebhookNewsChannelData>();
            options.Add<WeChatWorkWebhookFileChannelData>();
            options.Add<WeChatWorkWebhookVoiceChannelData>();
            options.Add<WeChatWorkWebhookTemplateCardChannelData>();
        });

        // Message carrier factory dispatch
        Configure<AbpNotificationsWebhookWeChatWorkMessageOptions>(options =>
        {
            options.MapDefaultMessageFactories();
        });
    }
}
