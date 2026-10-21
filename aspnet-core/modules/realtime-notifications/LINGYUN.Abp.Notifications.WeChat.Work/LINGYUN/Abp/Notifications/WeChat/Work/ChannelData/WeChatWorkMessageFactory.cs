using LINGYUN.Abp.WeChat.Work.Messages;
using LINGYUN.Abp.WeChat.Work.Messages.Models;
using System;
using Volo.Abp;

namespace LINGYUN.Abp.Notifications.WeChat.Work;

/// <summary>
/// Enterprise WeChat default message construction factory
/// </summary>
/// <remarks>
/// <para>
/// The mapping between each channel data (payload) and the message carrier is explicit. To add a new message type, you only need to add one
/// Called by <c>Map</c>, there is no need to modify the provider's publishing logic.
/// </para>
/// <para>
/// The options supported by each message carrier of Enterprise WeChat are not consistent, so they are overloaded separately according to the specific carrier type.
/// The compiler guarantees that only fields truly supported by the vector are set, rather than indiscriminate assignment like the old implementation.
/// </para>
/// </remarks>
public static class WeChatWorkMessageFactoryExtensions
{
    /// <summary>
    /// Register the built-in message construction factory
    /// </summary>
    public static AbpWeChatWorkNotificationMessageOptions MapDefaultMessageFactories(
        this AbpWeChatWorkNotificationMessageOptions options)
    {
        Check.NotNull(options, nameof(options));

        options.Map(WeChatWorkMessageKind.MiniProgramNotice, context =>
        {
            var channelData = Cast<WeChatWorkMiniProgramNoticeChannelData>(context);
            var miniProgram = channelData.MiniProgram
                ?? throw new AbpNotificationChannelDataConfigurationException(
                    $"The WeChat Work mini program notice of notification '{context.Notification.Name}' is empty.");

            return WeChatWorkMessageFactoryResult.Create(
                Apply(
                    new WeChatWorkMiniProgramMessage(
                        miniProgram,
                        context.Contact.ToUser ?? string.Empty,
                        context.Contact.ToParty ?? string.Empty,
                        context.Contact.ToTag ?? string.Empty),
                    context),
                WeChatWorkMessageKind.MiniProgramNotice);
        });

        options.Map(WeChatWorkMessageKind.MpNews, context =>
        {
            var channelData = Cast<WeChatWorkMpNewChannelData>(context);
            var mpNewMessage = channelData.MpNewMessage
                ?? throw new AbpNotificationChannelDataConfigurationException(
                    $"The WeChat Work mp news of notification '{context.Notification.Name}' is empty.");

            return WeChatWorkMessageFactoryResult.Create(
                Apply(new WeChatWorkMpNewMessage(context.Options.AgentId, mpNewMessage), context),
                WeChatWorkMessageKind.MpNews);
        });

        options.Map(WeChatWorkMessageKind.TemplateCard, context =>
        {
            var channelData = Cast<WeChatWorkTemplateCardChannelData>(context);
            var templateCard = channelData.TemplateCard
                ?? throw new AbpNotificationChannelDataConfigurationException(
                    $"The WeChat Work template card of notification '{context.Notification.Name}' is empty.");

            return WeChatWorkMessageFactoryResult.Create(
                Apply(new WeChatWorkTemplateCardMessage(context.Options.AgentId, templateCard), context),
                WeChatWorkMessageKind.TemplateCard);
        });

        options.Map(WeChatWorkMessageKind.TextCard, context =>
        {
            var channelData = Cast<WeChatWorkTextCardChannelData>(context);
            var textCard = channelData.TextCard
                ?? throw new AbpNotificationChannelDataConfigurationException(
                    $"The WeChat Work text card of notification '{context.Notification.Name}' is empty.");

            return WeChatWorkMessageFactoryResult.Create(
                Apply(new WeChatWorkTextCardMessage(context.Options.AgentId, textCard), context),
                WeChatWorkMessageKind.TextCard);
        });

        options.Map(WeChatWorkMessageKind.Video, context =>
        {
            var channelData = Cast<WeChatWorkVideoChannelData>(context);
            var video = channelData.Video
                ?? throw new AbpNotificationChannelDataConfigurationException(
                    $"The WeChat Work video of notification '{context.Notification.Name}' is empty.");

            return WeChatWorkMessageFactoryResult.Create(
                Apply(new WeChatWorkVideoMessage(context.Options.AgentId, video), context),
                WeChatWorkMessageKind.Video);
        });

        options.Map(WeChatWorkMessageKind.Voice, context =>
        {
            var channelData = Cast<WeChatWorkVoiceChannelData>(context);
            var voice = channelData.Voice
                ?? throw new AbpNotificationChannelDataConfigurationException(
                    $"The WeChat Work voice of notification '{context.Notification.Name}' is empty.");

            return WeChatWorkMessageFactoryResult.Create(
                Apply(new WeChatWorkVoiceMessage(context.Options.AgentId, voice), context),
                WeChatWorkMessageKind.Voice);
        });

        options.Map(WeChatWorkMessageKind.File, context =>
        {
            var channelData = Cast<WeChatWorkFileChannelData>(context);
            var file = channelData.File
                ?? throw new AbpNotificationChannelDataConfigurationException(
                    $"The WeChat Work file of notification '{context.Notification.Name}' is empty.");

            return WeChatWorkMessageFactoryResult.Create(
                Apply(new WeChatWorkFileMessage(context.Options.AgentId, file), context),
                WeChatWorkMessageKind.File);
        });

        options.Map(WeChatWorkMessageKind.Text, context =>
        {
            return WeChatWorkMessageFactoryResult.Create(
                Apply(new WeChatWorkTextMessage(context.Options.AgentId, new TextMessage(GetContent(context))), context),
                WeChatWorkMessageKind.Text);
        });

        options.Map(WeChatWorkMessageKind.Markdown, context =>
        {
            return WeChatWorkMessageFactoryResult.Create(
                Apply(new WeChatWorkMarkdownMessage(context.Options.AgentId, new MarkdownMessage(GetContent(context))), context),
                WeChatWorkMessageKind.Markdown);
        });

        return options;
    }

    private static TChannelData Cast<TChannelData>(WeChatWorkMessageFactoryContext context)
        where TChannelData : WeChatWorkChannelData
    {
        if (context.ChannelData is not TChannelData channelData)
        {
            throw new AbpNotificationChannelDataConfigurationException(
                $"The channel data '{context.ChannelData.GetType().FullName}' of notification '{context.Notification.Name}' does not match the registered factory '{typeof(TChannelData).FullName}'.");
        }

        return channelData;
    }

    private static string GetContent(WeChatWorkMessageFactoryContext context)
    {
        if (context.ChannelData is WeChatWorkTextChannelData textChannelData &&
            !textChannelData.Content.IsNullOrWhiteSpace())
        {
            return textChannelData.Content!;
        }

        if (context.ChannelData is WeChatWorkMarkdownChannelData markdownChannelData &&
            !markdownChannelData.Content.IsNullOrWhiteSpace())
        {
            return markdownChannelData.Content!;
        }

        return context.StandardData.Message;
    }

    /// <summary>
    /// Apply receiver
    /// </summary>
    private static TMessage ApplyContact<TMessage>(TMessage message, WeChatWorkMessageFactoryContext context)
        where TMessage : WeChatWorkMessage
    {
        if (!context.Contact.IsEmpty())
        {
            message.ToUser = context.Contact.ToUser;
            message.ToParty = context.Contact.ToParty;
            message.ToTag = context.Contact.ToTag;
        }

        return message;
    }

    /// <summary>
    /// Text message: supports safe, enable_id_trans, enable_duplicate_check
    /// </summary>
    private static WeChatWorkTextMessage Apply(
        WeChatWorkTextMessage message,
        WeChatWorkMessageFactoryContext context)
    {
        ApplyContact(message, context);

        message.Safe = context.Options.SafeValue;
        message.EnableIdTrans = context.Options.EnableIdTransValue;
        message.EnableDuplicateCheck = context.Options.EnableDuplicateCheckValue;
        message.DuplicateCheckInterval = context.Options.DuplicateCheckIntervalValue;

        return message;
    }

    /// <summary>
    /// Graphic message: supports safe, enable_id_trans, enable_duplicate_check
    /// </summary>
    private static WeChatWorkMpNewMessage Apply(
        WeChatWorkMpNewMessage message,
        WeChatWorkMessageFactoryContext context)
    {
        ApplyContact(message, context);

        message.Safe = context.Options.SafeValue;
        message.EnableIdTrans = context.Options.EnableIdTransValue;
        message.EnableDuplicateCheck = context.Options.EnableDuplicateCheckValue;
        message.DuplicateCheckInterval = context.Options.DuplicateCheckIntervalValue;

        return message;
    }

    /// <summary>
    /// File message: safe, enable_duplicate_check is supported, enable_id_trans is not supported
    /// </summary>
    private static WeChatWorkFileMessage Apply(
        WeChatWorkFileMessage message,
        WeChatWorkMessageFactoryContext context)
    {
        ApplyContact(message, context);

        message.Safe = context.Options.SafeValue;
        message.EnableDuplicateCheck = context.Options.EnableDuplicateCheckValue;
        message.DuplicateCheckInterval = context.Options.DuplicateCheckIntervalValue;

        return message;
    }

    /// <summary>
    /// Video message: supports safe, enable_duplicate_check, does not support enable_id_trans
    /// </summary>
    private static WeChatWorkVideoMessage Apply(
        WeChatWorkVideoMessage message,
        WeChatWorkMessageFactoryContext context)
    {
        ApplyContact(message, context);

        message.Safe = context.Options.SafeValue;
        message.EnableDuplicateCheck = context.Options.EnableDuplicateCheckValue;
        message.DuplicateCheckInterval = context.Options.DuplicateCheckIntervalValue;

        return message;
    }

    /// <summary>
    /// Voice message: does not support safe, nor enable_id_trans
    /// </summary>
    private static WeChatWorkVoiceMessage Apply(
        WeChatWorkVoiceMessage message,
        WeChatWorkMessageFactoryContext context)
    {
        ApplyContact(message, context);

        message.EnableDuplicateCheck = context.Options.EnableDuplicateCheckValue;
        message.DuplicateCheckInterval = context.Options.DuplicateCheckIntervalValue;

        return message;
    }

    /// <summary>
    /// markdown message: enable_duplicate_check is supported, safe is not supported
    /// </summary>
    private static WeChatWorkMarkdownMessage Apply(
        WeChatWorkMarkdownMessage message,
        WeChatWorkMessageFactoryContext context)
    {
        ApplyContact(message, context);

        message.EnableDuplicateCheck = context.Options.EnableDuplicateCheckValue;
        message.DuplicateCheckInterval = context.Options.DuplicateCheckIntervalValue;

        return message;
    }

    /// <summary>
    /// Text card message: safe is not supported
    /// </summary>
    private static WeChatWorkTextCardMessage Apply(
        WeChatWorkTextCardMessage message,
        WeChatWorkMessageFactoryContext context)
    {
        ApplyContact(message, context);

        message.EnableIdTrans = context.Options.EnableIdTransValue;
        message.EnableDuplicateCheck = context.Options.EnableDuplicateCheckValue;
        message.DuplicateCheckInterval = context.Options.DuplicateCheckIntervalValue;

        return message;
    }

    /// <summary>
    /// Template card message: safe is not supported
    /// </summary>
    private static WeChatWorkTemplateCardMessage Apply(
        WeChatWorkTemplateCardMessage message,
        WeChatWorkMessageFactoryContext context)
    {
        ApplyContact(message, context);

        message.EnableIdTrans = context.Options.EnableIdTransValue;
        message.EnableDuplicateCheck = context.Options.EnableDuplicateCheckValue;
        message.DuplicateCheckInterval = context.Options.DuplicateCheckIntervalValue;

        return message;
    }

    /// <summary>
    /// Mini program notification message: safe is not supported
    /// </summary>
    private static WeChatWorkMiniProgramMessage Apply(
        WeChatWorkMiniProgramMessage message,
        WeChatWorkMessageFactoryContext context)
    {
        ApplyContact(message, context);

        message.EnableIdTrans = context.Options.EnableIdTransValue;
        message.EnableDuplicateCheck = context.Options.EnableDuplicateCheckValue;
        message.DuplicateCheckInterval = context.Options.DuplicateCheckIntervalValue;

        return message;
    }
}
