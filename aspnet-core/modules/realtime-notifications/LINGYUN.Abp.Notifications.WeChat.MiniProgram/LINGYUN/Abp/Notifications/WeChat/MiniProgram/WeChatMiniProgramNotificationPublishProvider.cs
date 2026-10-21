using LINGYUN.Abp.WeChat.Common.Security.Claims;
using LINGYUN.Abp.WeChat.MiniProgram.Features;
using LINGYUN.Abp.WeChat.MiniProgram.Messages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Features;

namespace LINGYUN.Abp.Notifications.WeChat.MiniProgram;

/// <summary>
/// WeChat applet message push provider
/// </summary>
/// <remarks>
/// The template data of the applet's subscription message itself is a dynamic key-value pair, so the payload only declares scalar fields.
/// The template data is still taken directly from the property bag of the notification data.
/// </remarks>
public class WeChatMiniProgramNotificationPublishProvider : NotificationPublishProvider
{
    public const string ProviderName = NotificationProviderNames.WechatMiniProgram;
    public override string Name => ProviderName;
    protected IFeatureChecker FeatureChecker => ServiceProvider.LazyGetRequiredService<IFeatureChecker>();
    protected ISubscribeMessager SubscribeMessager => ServiceProvider.LazyGetRequiredService<ISubscribeMessager>();
    protected IOptions<AbpNotificationsWeChatMiniProgramOptions> Options => ServiceProvider.LazyGetRequiredService<IOptions<AbpNotificationsWeChatMiniProgramOptions>>();
    protected INotificationChannelDataResolver ChannelDataResolver => ServiceProvider.LazyGetRequiredService<INotificationChannelDataResolver>();

    protected async override Task<bool> CanPublishAsync(NotificationInfo notification, CancellationToken cancellationToken = default)
    {
        if (!await FeatureChecker.IsEnabledAsync(true,
            WeChatMiniProgramFeatures.Enable,
            WeChatMiniProgramFeatures.Messages.Enable))
        {
            Logger.LogWarning(
                "{0} cannot push messages because the feature {1} is not enabled",
                Name,
                WeChatMiniProgramFeatures.Messages.Enable);
            return false;
        }
        return true;
    }

    protected async override Task PublishAsync(NotificationPublishContext context, CancellationToken cancellationToken = default)
    {
        // WeChat does not support pushing to all users
        // After the user subscribes to the message in the mini program, he subscribes to the corresponding template message through the api/subscribes/subscribe interface.
        var channelData = ResolveChannelData(context.Notification.Data);

        foreach (var identifier in context.Users)
        {
            if (channelData.TemplateId.IsNullOrWhiteSpace())
            {
                context.Cancel("Wechat weapp template id be empty, can not send notification!");
                Logger.LogWarning(context.Reason);
                continue;
            }

            Logger.LogDebug($"Get wechat weapp template id: {channelData.TemplateId}");

            Logger.LogDebug($"Get wechat weapp redirect page: {channelData.RedirectPage ?? "null"}");
            Logger.LogDebug($"Get wechat weapp state: {channelData.WeAppState ?? null}");
            Logger.LogDebug($"Get wechat weapp language: {channelData.WeAppLanguage ?? null}");

            // TODO: If WeChat publishes a notification, please assemble the openid field in the notification data content.
            var openId = ChannelDataResolver.GetOrNull(context.Notification.Data, AbpWeChatClaimTypes.OpenId);

            if (openId.IsNullOrWhiteSpace())
            {
                // Send mini program subscription message
                await SubscribeMessager
                    .SendAsync(
                        identifier.UserId,
                        channelData.TemplateId!,
                        channelData.RedirectPage,
                        channelData.WeAppLanguage,
                        channelData.WeAppState,
                        context.Notification.Data.ExtraProperties,
                        cancellationToken);
            }
            else
            {
                var weChatWeAppNotificationData = new SubscribeMessage(
                    openId,
                    channelData.TemplateId!,
                    channelData.RedirectPage,
                    channelData.WeAppState,
                    channelData.WeAppLanguage);

                // Write template data
                weChatWeAppNotificationData.WriteData(context.Notification.Data.ExtraProperties);

                Logger.LogDebug($"Sending wechat weapp notification: {context.Notification.Name}");

                // Send mini program subscription message
                await SubscribeMessager.SendAsync(weChatWeAppNotificationData, cancellationToken);

                Logger.LogDebug("The notification: {0} with provider: {1} has successfully published!", context.Notification.Name, Name);
            }
        }
    }

    /// <summary>
    /// Parse mini program channel data
    /// </summary>
    /// <remarks>
    /// The key name has the prefix of this channel, and the reading is not case-sensitive;
    /// In order to allow installed producers to upgrade smoothly, here is a read-only rollback for the historical **no prefix keys**;
    /// Default value comes from channel configuration.
    /// </remarks>
    protected virtual WeChatMiniProgramSubscribeChannelData ResolveChannelData(NotificationData notificationData)
    {
        var channelData = ChannelDataResolver.Resolve<WeChatMiniProgramSubscribeChannelData>(notificationData)
            ?? new WeChatMiniProgramSubscribeChannelData();

        // Historical unprefixed key read-only fallback
        ApplyLegacyKeys(channelData, notificationData);

        if (channelData.TemplateId.IsNullOrWhiteSpace())
        {
            channelData.TemplateId = Options.Value.DefaultTemplateId;
        }
        if (channelData.WeAppState.IsNullOrWhiteSpace())
        {
            channelData.WeAppState = Options.Value.DefaultState;
        }
        if (channelData.WeAppLanguage.IsNullOrWhiteSpace())
        {
            channelData.WeAppLanguage = Options.Value.DefaultLanguage;
        }

        return channelData;
    }

    /// <summary>
    /// Fall back to reading the unprefixed keys left over from the history
    /// </summary>
    /// <remarks>
    /// <para>
    /// Earlier versions used unprefixed keys such as <c>TemplateId</c>, which installed producers may still be writing to.
    /// Prefixed keys take precedence; log a warning when a legacy key is hit to facilitate migration.
    /// </para>
    /// </remarks>
#pragma warning disable CS0618 // Type or member is obsolete: the only sanctioned read site for legacy keys
    protected virtual void ApplyLegacyKeys(
        WeChatMiniProgramSubscribeChannelData channelData,
        NotificationData notificationData)
    {
        if (channelData.TemplateId.IsNullOrWhiteSpace())
        {
            channelData.TemplateId = ReadLegacy(notificationData, WeChatMiniProgramNotificationDataKeys.Legacy.TemplateId);
        }
        if (channelData.RedirectPage.IsNullOrWhiteSpace())
        {
            channelData.RedirectPage = ReadLegacy(notificationData, WeChatMiniProgramNotificationDataKeys.Legacy.RedirectPage);
        }
        if (channelData.WeAppState.IsNullOrWhiteSpace())
        {
            channelData.WeAppState = ReadLegacy(notificationData, WeChatMiniProgramNotificationDataKeys.Legacy.WeAppState);
        }
        if (channelData.WeAppLanguage.IsNullOrWhiteSpace())
        {
            channelData.WeAppLanguage = ReadLegacy(notificationData, WeChatMiniProgramNotificationDataKeys.Legacy.WeAppLanguage);
        }
    }
#pragma warning restore CS0618

    private string? ReadLegacy(NotificationData notificationData, string legacyKey)
    {
        var value = ChannelDataResolver.GetOrNull(notificationData, legacyKey);
        if (!value.IsNullOrWhiteSpace())
        {
            Logger.LogWarning(
                "The wechat mini program notification data is using the legacy key '{LegacyKey}' without channel prefix. " +
                "Please migrate to the prefixed key '{Prefix}...'.",
                legacyKey,
                WeChatMiniProgramNotificationDataKeys.Prefix);
        }

        return value;
    }
}
