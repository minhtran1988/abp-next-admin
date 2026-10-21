using LINGYUN.Abp.WeChat.Work.Messages;
using System;
using System.Collections.Generic;
using Volo.Abp;

namespace LINGYUN.Abp.Notifications.Webhook.WeChat.Work;

/// <summary>
/// Enterprise WeChat Webhook message construction options
/// </summary>
/// <remarks>
/// <para>
/// The conversion of channel data form to message carrier is centrally registered here. To add a new message type, you only need to
/// <c>options.Map(kind, factory)</c>, there is no need to modify the contributor's publishing logic.
/// </para>
/// </remarks>
public class AbpNotificationsWebhookWeChatWorkMessageOptions
{
    private readonly Dictionary<WeChatWorkWebhookMessageKind, Func<WeChatWorkWebhookMessageFactoryContext, WeChatWorkWebhookMessageFactoryResult>> _factories
        = new Dictionary<WeChatWorkWebhookMessageKind, Func<WeChatWorkWebhookMessageFactoryContext, WeChatWorkWebhookMessageFactoryResult>>();

    /// <summary>
    /// Registered message type
    /// </summary>
    public IReadOnlyCollection<WeChatWorkWebhookMessageKind> MessageKinds => _factories.Keys;

    /// <summary>
    /// Register message construction factory
    /// </summary>
    public AbpNotificationsWebhookWeChatWorkMessageOptions Map(
        WeChatWorkWebhookMessageKind kind,
        Func<WeChatWorkWebhookMessageFactoryContext, WeChatWorkWebhookMessageFactoryResult> factory)
    {
        Check.NotNull(factory, nameof(factory));

        _factories[kind] = factory;

        return this;
    }

    /// <summary>
    /// Construct message carrier
    /// </summary>
    public WeChatWorkWebhookMessageFactoryResult Create(WeChatWorkWebhookMessageFactoryContext context)
    {
        Check.NotNull(context, nameof(context));

        var kind = context.ChannelData.Kind;

        if (!_factories.TryGetValue(kind, out var factory))
        {
            throw new AbpNotificationChannelDataConfigurationException(
                $"The WeChat Work webhook message kind '{kind}' has not been mapped, please call '{nameof(Map)}' when configuring '{nameof(AbpNotificationsWebhookWeChatWorkMessageOptions)}'.");
        }

        var result = factory(context);
        if (result == null || result.Message == null)
        {
            throw new AbpNotificationChannelDataConfigurationException(
                $"The factory of the WeChat Work webhook message kind '{kind}' returned an empty message.");
        }

        return result;
    }

    /// <summary>
    /// Get the registered factory
    /// </summary>
    public bool TryGetFactory(
        WeChatWorkWebhookMessageKind kind,
        out Func<WeChatWorkWebhookMessageFactoryContext, WeChatWorkWebhookMessageFactoryResult>? factory)
    {
        return _factories.TryGetValue(kind, out factory);
    }
}
