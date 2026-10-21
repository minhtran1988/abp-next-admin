using LINGYUN.Abp.WeChat.Work.Messages;
using LINGYUN.Abp.WeChat.Work.Messages.Models;
using Volo.Abp;

namespace LINGYUN.Abp.Notifications.Webhook.WeChat.Work;

/// <summary>
/// Enterprise WeChat Webhook Message Factory
/// </summary>
public static class WeChatWorkWebhookMessageFactory
{
    /// <summary>
    /// Register the built-in message factory
    /// </summary>
    /// <remarks>
    /// Each payload corresponds to a <c>msgtype</c>, one-to-one mapping, without implicit degradation:
    /// If the producer selects a template card, the template card must be sent instead of silently sending it as text.
    /// The only exception is the choice between markdown and markdown_v2, which is determined by the channel option
    /// <see cref="AbpNotificationsWebhookWeChatWorkOptions.UseMarkdownV2"/> Decision.
    /// </remarks>
    public static AbpNotificationsWebhookWeChatWorkMessageOptions MapDefaultMessageFactories(
        this AbpNotificationsWebhookWeChatWorkMessageOptions options)
    {
        Check.NotNull(options, nameof(options));

        return options
            .Map(WeChatWorkWebhookMessageKind.Text, context =>
            {
                var payload = (WeChatWorkWebhookTextChannelData)context.ChannelData;

                return WeChatWorkWebhookMessageFactoryResult.Create(
                    new WeChatWorkWebhookTextMessage(new WebhookTextMessage(payload.Content!)),
                    WeChatWorkWebhookMessageKind.Text);
            })
            .Map(WeChatWorkWebhookMessageKind.Markdown, context =>
            {
                var payload = (WeChatWorkWebhookMarkdownChannelData)context.ChannelData;

                return WeChatWorkWebhookMessageFactoryResult.Create(
                    new WeChatWorkWebhookMarkdownMessage(new WebhookMarkdownMessage(payload.Content!)),
                    WeChatWorkWebhookMessageKind.Markdown);
            })
            .Map(WeChatWorkWebhookMessageKind.MarkdownV2, context =>
            {
                var payload = (WeChatWorkWebhookMarkdownV2ChannelData)context.ChannelData;

                return WeChatWorkWebhookMessageFactoryResult.Create(
                    new WeChatWorkWebhookMarkdownV2Message(new WebhookMarkdownV2Message(payload.Content!)),
                    WeChatWorkWebhookMessageKind.MarkdownV2);
            })
            .Map(WeChatWorkWebhookMessageKind.Image, context =>
            {
                var payload = (WeChatWorkWebhookImageChannelData)context.ChannelData;

                return WeChatWorkWebhookMessageFactoryResult.Create(
                    new WeChatWorkWebhookImageMessage(payload.Image!),
                    WeChatWorkWebhookMessageKind.Image);
            })
            .Map(WeChatWorkWebhookMessageKind.News, context =>
            {
                var payload = (WeChatWorkWebhookNewsChannelData)context.ChannelData;

                return WeChatWorkWebhookMessageFactoryResult.Create(
                    new WeChatWorkWebhookNewsMessage(payload.News!),
                    WeChatWorkWebhookMessageKind.News);
            })
            .Map(WeChatWorkWebhookMessageKind.File, context =>
            {
                var payload = (WeChatWorkWebhookFileChannelData)context.ChannelData;

                return WeChatWorkWebhookMessageFactoryResult.Create(
                    new WeChatWorkWebhookFileMessage(payload.File!),
                    WeChatWorkWebhookMessageKind.File);
            })
            .Map(WeChatWorkWebhookMessageKind.Voice, context =>
            {
                var payload = (WeChatWorkWebhookVoiceChannelData)context.ChannelData;

                return WeChatWorkWebhookMessageFactoryResult.Create(
                    new WeChatWorkWebhookVoiceMessage(payload.Voice!),
                    WeChatWorkWebhookMessageKind.Voice);
            })
            .Map(WeChatWorkWebhookMessageKind.TemplateCard, context =>
            {
                var payload = (WeChatWorkWebhookTemplateCardChannelData)context.ChannelData;

                return WeChatWorkWebhookMessageFactoryResult.Create(
                    new WeChatWorkWebhookTemplateCardMessage(payload.TemplateCard!),
                    WeChatWorkWebhookMessageKind.TemplateCard);
            });
    }
}
