using LINGYUN.Abp.Notifications.Webhook.WeChat.Work;
using LINGYUN.Abp.WeChat.Work.Messages.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;

namespace LINGYUN.Abp.Notifications.Webhook.WeChat.Work;

/// <summary>
/// Enterprise WeChat Webhook notifies contributors
/// </summary>
/// <remarks>
/// <para>
/// Prioritize parsing the **channel data (load)** written by the producer, and only fall back to the button if the load is missing.
/// <see cref="NotificationInfo.ContentType"/> derivation,
/// So historical usage (only writing standard data) behaves unchanged.
/// </para>
/// <para>
/// A Webhook can only carry one message, so when declaring multiple payloads
/// <see cref="NotificationChannelDataAttribute.Priority"/> The smallest one,
/// And construct the vector with a 1:1 mapping - no longer silently discarding unknown content types like the old implementation.
/// </para>
/// </remarks>
public class WeChatWorkWebhookNotificationContributor : IWebhookNotificationContributor
{
    /// <summary>
    /// Contributor name (for logging only)
    /// </summary>
    public string Name => WeChatWorkWebhookNotificationProvider.ProviderName;

    public async virtual Task ContributeAsync(IWebhookNotificationContext context)
    {
        Check.NotNull(context, nameof(context));

        var options = context.ServiceProvider
            .GetRequiredService<IOptions<AbpNotificationsWebhookWeChatWorkOptions>>().Value;
        var messageOptions = context.ServiceProvider
            .GetRequiredService<IOptions<AbpNotificationsWebhookWeChatWorkMessageOptions>>().Value;
        var resolver = context.ServiceProvider
            .GetRequiredService<INotificationChannelDataResolver>();
        var notificationDataSerializer = context.ServiceProvider
            .GetRequiredService<INotificationDataSerializer>();

        var standardData = await notificationDataSerializer.ToStandard(context.Notification.Data);

        try
        {
            var payloads = resolver.Resolve(
                WeChatWorkWebhookNotificationProvider.ProviderName,
                context.Notification.Data);

            if (payloads.Count == 0)
            {
                // Fallback: The producer did not write the payload, deduced by content type (maintain historical behavior)
                payloads = ResolveContentTypePayloads(
                    context.Notification.ContentType,
                    standardData.Message,
                    options);
            }

            if (payloads.Count == 0)
            {
                Logger(context).LogDebug(
                    "No wechat work webhook channel data was resolved for the notification: {name}, skip it.",
                    context.Notification.Name);

                return;
            }

            // A Webhook can only carry one message: take the first one according to Priority (the parsing results have been sorted)
            var channelData = (WeChatWorkWebhookChannelData)payloads[0];

            ApplyStandardDataFallback(channelData, standardData);

            var message = messageOptions.Create(
                new WeChatWorkWebhookMessageFactoryContext(
                    context.Notification,
                    standardData,
                    channelData,
                    options));

            context.Webhook = new WebhookNotificationData(
                context.Notification.Name,
                message.Message);
            context.Handled = true;
        }
        catch (Exception ex)
        {
            Logger(context).LogWarning(
                "Failed to parse the content of the wechat work webhook message: {message}",
                ex.Message);
        }
    }

    /// <summary>
    /// Deduced payload according to notification content type (compatible with historical producers that have not written channel data)
    /// </summary>
    protected virtual IReadOnlyList<NotificationChannelData> ResolveContentTypePayloads(
        NotificationContentType contentType,
        string? message,
        AbpNotificationsWebhookWeChatWorkOptions options)
    {
        var content = message ?? string.Empty;

        switch (contentType)
        {
            case NotificationContentType.Text:
                return new NotificationChannelData[]
                {
                    new WeChatWorkWebhookTextChannelData { Content = content }
                };

            case NotificationContentType.Markdown:
                return options.UseMarkdownV2
                    ? new NotificationChannelData[]
                    {
                        new WeChatWorkWebhookMarkdownV2ChannelData { Content = content }
                    }
                    : new NotificationChannelData[]
                    {
                        new WeChatWorkWebhookMarkdownChannelData { Content = content }
                    };

            case NotificationContentType.Html:
                // Convert HTML to markdown first and then deliver it
                var markdown = CommonMark.CommonMarkConverter.Convert(content);

                return options.UseMarkdownV2
                    ? new NotificationChannelData[]
                    {
                        new WeChatWorkWebhookMarkdownV2ChannelData { Content = markdown }
                    }
                    : new NotificationChannelData[]
                    {
                        new WeChatWorkWebhookMarkdownChannelData { Content = markdown }
                    };

            default:
                return Array.Empty<NotificationChannelData>();
        }
    }

    /// <summary>
    /// Use standard data to complete unspecified content in the payload
    /// </summary>
    /// <remarks>
    /// When the producer only declares "which carrier to use" without writing the content (for example, just <c>wx-work-hook:markdown</c>),
    /// Use localized/rendered notification body to avoid emitting empty content.
    /// </remarks>
    protected virtual void ApplyStandardDataFallback(
        WeChatWorkWebhookChannelData channelData,
        NotificationStandardData standardData)
    {
        if (standardData.Message.IsNullOrWhiteSpace())
        {
            return;
        }

        switch (channelData)
        {
            case WeChatWorkWebhookTextChannelData text when text.Content.IsNullOrWhiteSpace():
                text.Content = standardData.Message;
                break;

            case WeChatWorkWebhookMarkdownChannelData markdown when markdown.Content.IsNullOrWhiteSpace():
                markdown.Content = standardData.Message;
                break;

            case WeChatWorkWebhookMarkdownV2ChannelData markdownV2 when markdownV2.Content.IsNullOrWhiteSpace():
                markdownV2.Content = standardData.Message;
                break;
        }
    }

    private static ILogger Logger(IWebhookNotificationContext context)
    {
        return context.ServiceProvider.GetService<ILogger<WeChatWorkWebhookNotificationContributor>>()
            ?? NullLogger<WeChatWorkWebhookNotificationContributor>.Instance;
    }
}
