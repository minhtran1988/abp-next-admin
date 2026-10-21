using LINGYUN.Abp.WeChat.Work.Messages;
using System;
using System.Collections.Generic;
using Volo.Abp;

namespace LINGYUN.Abp.Notifications.WeChat.Work;

/// <summary>
/// Enterprise WeChat message construction options
/// </summary>
/// <remarks>
/// <para>
/// The conversion of channel data form to message carrier is centrally registered here. To add a new message type, you only need to
/// <c>options.Map(kind, factory)</c>, there is no need to modify the Provider's publishing logic.
/// </para>
/// <para>
/// Compared to the old <c>if/else</c> probe chain, dispatch here is O(1) and explicit:
/// When the payload type is inconsistent with the factory key, an exception will be thrown directly instead of silently sending an error message.
/// </para>
/// </remarks>
public class AbpWeChatWorkNotificationMessageOptions
{
    private readonly Dictionary<WeChatWorkMessageKind, Func<WeChatWorkMessageFactoryContext, WeChatWorkMessageFactoryResult>> _factories
        = new Dictionary<WeChatWorkMessageKind, Func<WeChatWorkMessageFactoryContext, WeChatWorkMessageFactoryResult>>();

    /// <summary>
    /// Registered message type
    /// </summary>
    public IReadOnlyCollection<WeChatWorkMessageKind> MessageKinds => _factories.Keys;

    /// <summary>
    /// Register message construction factory
    /// </summary>
    public AbpWeChatWorkNotificationMessageOptions Map(
        WeChatWorkMessageKind kind,
        Func<WeChatWorkMessageFactoryContext, WeChatWorkMessageFactoryResult> factory)
    {
        Check.NotNull(factory, nameof(factory));

        _factories[kind] = factory;

        return this;
    }

    /// <summary>
    /// Construct message carrier
    /// </summary>
    public WeChatWorkMessageFactoryResult Create(WeChatWorkMessageFactoryContext context)
    {
        Check.NotNull(context, nameof(context));

        var kind = context.ChannelData.Kind;

        if (!_factories.TryGetValue(kind, out var factory))
        {
            throw new AbpNotificationChannelDataConfigurationException(
                $"The WeChat Work message kind '{kind}' has not been mapped, please call '{nameof(Map)}' when configuring '{nameof(AbpWeChatWorkNotificationMessageOptions)}'.");
        }

        var result = factory(context);
        if (result == null || result.Message == null)
        {
            throw new AbpNotificationChannelDataConfigurationException(
                $"The factory of the WeChat Work message kind '{kind}' returned an empty message.");
        }

        return result;
    }

    /// <summary>
    /// Get the registered factory
    /// </summary>
    public bool TryGetFactory(
        WeChatWorkMessageKind kind,
        out Func<WeChatWorkMessageFactoryContext, WeChatWorkMessageFactoryResult>? factory)
    {
        return _factories.TryGetValue(kind, out factory);
    }
}
