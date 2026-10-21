using LINGYUN.Abp.WeChat.Work.Authorize;
using LINGYUN.Abp.WeChat.Work.Features;
using LINGYUN.Abp.WeChat.Work.Messages;
using LINGYUN.Abp.WeChat.Work.Messages.Models;
using LINGYUN.Abp.WeChat.Work.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Features;
using Volo.Abp.Settings;

namespace LINGYUN.Abp.Notifications.WeChat.Work;

/// <summary>
/// Enterprise WeChat notification release channel
/// </summary>
public class WeChatWorkNotificationPublishProvider : NotificationPublishProvider
{
    public const string ProviderName = NotificationProviderNames.WechatWork;

    public override string Name => ProviderName;

    protected IFeatureChecker FeatureChecker => ServiceProvider.LazyGetRequiredService<IFeatureChecker>();
    protected ISettingProvider SettingProvider => ServiceProvider.LazyGetRequiredService<ISettingProvider>();
    protected IWeChatWorkMessageSender WeChatWorkMessageSender => ServiceProvider.LazyGetRequiredService<IWeChatWorkMessageSender>();
    protected INotificationDataSerializer NotificationDataSerializer => ServiceProvider.LazyGetRequiredService<INotificationDataSerializer>();
    protected IWeChatWorkUserClaimProvider WeChatWorkInternalUserFinder => ServiceProvider.LazyGetRequiredService<IWeChatWorkUserClaimProvider>();
    protected INotificationDefinitionManager NotificationDefinitionManager => ServiceProvider.LazyGetRequiredService<INotificationDefinitionManager>();
    protected INotificationChannelDataResolver ChannelDataResolver => ServiceProvider.LazyGetRequiredService<INotificationChannelDataResolver>();
    protected IOptions<AbpWeChatWorkNotificationMessageOptions> MessageOptions => ServiceProvider.LazyGetRequiredService<IOptions<AbpWeChatWorkNotificationMessageOptions>>();

    protected async override Task<bool> CanPublishAsync(NotificationInfo notification, CancellationToken cancellationToken = default)
    {
        if (!await FeatureChecker.IsEnabledAsync(true,
            WeChatWorkFeatureNames.Enable,
            WeChatWorkFeatureNames.Message.Enable))
        {
            Logger.LogWarning(
                "{0} cannot push messages because the feature {1} is not enabled",
                Name,
                WeChatWorkFeatureNames.Message.Enable);
            return false;
        }
        return true;
    }

    protected async override Task PublishAsync(
        NotificationPublishContext context,
        CancellationToken cancellationToken = default)
    {
        var agentId = await SettingProvider.GetOrNullAsync(WeChatWorkSettingNames.Connection.AgentId);
        if (agentId.IsNullOrWhiteSpace())
        {
            context.Cancel("Unable to send work weixin messages because agentId is not set.");
            Logger.LogWarning(context.Reason);
            return;
        }

        var notificationData = await NotificationDataSerializer.ToStandard(context.Notification.Data);
        if (notificationData.Title.IsNullOrWhiteSpace() && notificationData.Message.IsNullOrWhiteSpace())
        {
            context.Cancel("Unable to send work weixin messages because the title and content of the message must not be empty.");
            Logger.LogWarning(context.Reason);
            return;
        }

        var notificationDefine = await NotificationDefinitionManager.GetOrNullAsync(context.Notification.Name);

        var contact = await ResolveContactAsync(context, notificationDefine, cancellationToken);
        if (contact.IsEmpty())
        {
            // touser, toparty, and totag cannot be empty at the same time: https://developer.work.weixin.qq.com/document/path/90236
            context.Cancel("Unable to send work weixin messages because The recipient/department/label cannot be empty simultaneously.");
            Logger.LogWarning(context.Reason);
            return;
        }

        var options = WeChatWorkMessageOptions.From(ChannelDataResolver, context.Notification.Data);
        options.AgentId = agentId;

        var messages = ResolveMessages(context, notificationData, contact, options);
        if (messages.Count == 0)
        {
            context.Cancel("Unable to send work weixin messages because WeChatWorkMessage is null.");
            Logger.LogWarning(context.Reason);
            return;
        }
        var failures = new List<Exception>();

        foreach (var message in messages)
        {
            try
            {
                var response = await WeChatWorkMessageSender.SendAsync(message.Message, cancellationToken);

                if (!response.IsSuccessed)
                {
                    // Enterprise WeChat returns failure with a business error code, which must be perceived by the upper layer, otherwise the failure will be recorded as success.
                    context.Cancel(
                        $"Unable to send work weixin messages, error code: {response.ErrorCode}, error message: {response.ErrorMessage}");

                    Logger.LogWarning(
                        "The notification: {Notification} with provider: {Provider} and message kind: {Kind} failed, error code: {ErrorCode}, error message: {ErrorMessage}",
                        context.Notification.Name, Name, message.Kind, response.ErrorCode, response.ErrorMessage);
                    continue;
                }

                Logger.LogDebug(
                    "The notification: {Notification} with provider: {Provider} and message kind: {Kind} has successfully published!",
                    context.Notification.Name, Name, message.Kind);
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex,
                    "Send notification {Notification} with provider {Provider} and message kind {Kind} failed",
                    context.Notification.Name, Name, message.Kind);
                failures.Add(ex);
            }
        }

        if (failures.Count > 0)
        {
            context.Cancel(
                $"Failed to send work weixin messages of notification '{context.Notification.Name}'.",
                new AggregateException(
                    $"Failed to send work weixin messages of notification '{context.Notification.Name}'.",
                    failures));
        }
    }

    /// <summary>
    /// Parse the message receiver
    /// </summary>
    /// <remarks>
    /// The member identification is parsed by the enterprise WeChat identity binding relationship. Departments and labels get notification data first, and notification definitions second.
    /// </remarks>
    protected async virtual Task<WeChatWorkNotificationContact> ResolveContactAsync(
        NotificationPublishContext context,
        NotificationDefinition? notificationDefine,
        CancellationToken cancellationToken = default)
    {
        var toUser = await WeChatWorkInternalUserFinder.FindUserIdentifierListAsync(
            context.Users.Select(id => id.UserId),
            cancellationToken);

        var toTag = ChannelDataResolver.GetOrNull(context.Notification.Data, WeChatWorkNotificationDataKeys.ToTag)
            ?? notificationDefine?.GetTagOrNull();

        var toParty = ChannelDataResolver.GetOrNull(context.Notification.Data, WeChatWorkNotificationDataKeys.ToParty)
            ?? notificationDefine?.GetPartyOrNull();

        return WeChatWorkNotificationContact.From(
            toUser: toUser.JoinAsString("|"),
            toParty: toParty,
            toTag: toTag);
    }

    /// <summary>
    /// Parse the collection of message carriers to be sent
    /// </summary>
    /// <remarks>
    /// Prioritize using channel data (payload) explicitly declared by the producer;
    /// When not declared, the default carrier is deduced according to the notification content type to ensure that the behavior of existing producers remains unchanged.
    /// </remarks>
    protected virtual List<WeChatWorkMessageFactoryResult> ResolveMessages(
        NotificationPublishContext context,
        NotificationStandardData standardData,
        WeChatWorkNotificationContact contact,
        WeChatWorkMessageOptions options)
    {
        var messages = new List<WeChatWorkMessageFactoryResult>();

        if (TryResolveChannelData(context, out var invalidPayload, out var resolvedPayloads))
        {
            foreach (var channelData in resolvedPayloads)
            {
                messages.Add(CreateMessage(channelData, context.Notification, standardData, contact, options));
            }
        }
        else
        {
            context.Cancel(
                $"Unable to send work weixin messages because the work weixin payload '{invalidPayload}' can not be parsed, please check the notification data written by the producer.");
            Logger.LogWarning(context.Reason);
            return messages;
        }

        if (messages.Count == 0)
        {
            var defaultChannelData = CreateDefaultChannelData(context, standardData);
            if (defaultChannelData != null)
            {
                messages.Add(CreateMessage(defaultChannelData, context.Notification, standardData, contact, options));
            }
        }

        return messages;
    }

    /// <summary>
    /// Parse channel data (load)
    /// </summary>
    /// <remarks>
    /// Distinguish between "the producer did not write the payload" and "the payload was written but could not be parsed":
    /// The latter is a producer error and must be reported explicitly instead of silently falling back to the default carrier.
    /// </remarks>
    protected virtual bool TryResolveChannelData(
        NotificationPublishContext context,
        out string? invalidPayloadKey,
        out List<NotificationChannelData> resolvedPayloads)
    {
        invalidPayloadKey = null;
        resolvedPayloads = new List<NotificationChannelData>();

        foreach (var payloadType in ChannelDataResolver.GetPayloadTypes(ProviderName))
        {
            if (!ChannelDataResolver.IsDeclared(context.Notification.Data, payloadType))
            {
                continue;
            }

            var payload = ChannelDataResolver.Resolve(payloadType, context.Notification.Data);

            if (payload == null)
            {
                invalidPayloadKey = ChannelDataResolver.GetKey(context.Notification.Data, payloadType);
                return false;
            }

            resolvedPayloads.Add(payload);
        }

        return true;
    }

    /// <summary>
    /// Derivation of default message carrier based on notification content type
    /// </summary>
    protected virtual WeChatWorkChannelData? CreateDefaultChannelData(
        NotificationPublishContext context,
        NotificationStandardData standardData)
    {
        switch (context.Notification.ContentType)
        {
            case NotificationContentType.Text:
                return new WeChatWorkTextChannelData
                {
                    Content = standardData.Message
                };

            case NotificationContentType.Html:
                return new WeChatWorkTextCardChannelData
                {
                    TextCard = new TextCardMessage(
                        standardData.Title ?? string.Empty,
                        // The old implementation discarded description, here we complete the description of the text card
                        standardData.Description.IsNullOrWhiteSpace() ? standardData.Message : standardData.Description!,
                        // The jump address should be explicitly specified by the producer through the payload. It is not clickable by default.
                        "javascript:;")
                };

            case NotificationContentType.Markdown:
                return new WeChatWorkMarkdownChannelData
                {
                    Content = standardData.Message
                };

            default:
                Logger.LogWarning(
                    "The notification: {Notification} with provider: {Provider} can not resolve a work weixin message for content type: {ContentType}.",
                    context.Notification.Name, Name, context.Notification.ContentType);
                return null;
        }
    }

    protected virtual WeChatWorkMessageFactoryResult CreateMessage(
        NotificationChannelData channelData,
        NotificationInfo notification,
        NotificationStandardData standardData,
        WeChatWorkNotificationContact contact,
        WeChatWorkMessageOptions options)
    {
        if (channelData is not WeChatWorkChannelData weChatWorkChannelData)
        {
            throw new AbpNotificationChannelDataConfigurationException(
                $"The channel data '{channelData.GetType().FullName}' is not a work weixin channel data.");
        }

        var factoryContext = new WeChatWorkMessageFactoryContext(
            notification,
            standardData,
            weChatWorkChannelData,
            contact,
            options);

        return MessageOptions.Value.Create(factoryContext);
    }
}
