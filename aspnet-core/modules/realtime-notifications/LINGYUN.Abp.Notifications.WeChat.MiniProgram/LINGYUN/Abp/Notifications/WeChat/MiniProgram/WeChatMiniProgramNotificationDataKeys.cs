using System;

namespace LINGYUN.Abp.Notifications.WeChat.MiniProgram;

/// <summary>
/// WeChat applet notification data key
/// </summary>
/// <remarks>
/// <para>
/// The key name should be unified with the channel prefix (<see cref="Prefix"/>), consistent with other channels.
/// Prevent keys from different channels from conflicting with each other in the same notification data property bag.
/// </para>
/// <para>
/// The reading process is case-insensitive (exact matching takes precedence), so the producer can hit with any case.
/// </para>
/// </remarks>
public static class WeChatMiniProgramNotificationDataKeys
{
    /// <summary>
    /// The data key prefix of this channel
    /// </summary>
    public const string Prefix = "wx-mp:";

    /// <summary>
    /// Mini program template identification (load identification key)
    /// </summary>
    public const string TemplateId = Prefix + "template_id";

    /// <summary>
    /// Click to jump to the mini program page
    /// </summary>
    public const string RedirectPage = Prefix + "redirect_page";

    /// <summary>
    /// Jump applet type
    /// </summary>
    public const string WeAppState = Prefix + "weapp_state";

    /// <summary>
    /// Mini program language
    /// </summary>
    public const string WeAppLanguage = Prefix + "weapp_language";

    /// <summary>
    /// Unprefixed keys left over from history
    /// </summary>
    /// <remarks>
    /// <para>
    /// Earlier versions used unprefixed keys, and installed producers may still be writing, thus retaining **read-only compatibility**.
    /// </para>
    /// <para>
    /// <b>Deprecated</b>: Please use prefixed constants (<see cref="TemplateId"/>, etc.) for new code.
    /// The only read point allowed is the historical rollback on the channel side
    /// (<c>WeChatMiniProgramNotificationPublishProvider.ApplyLegacyKeys</c>),
    /// It will log an alert when it hits a legacy key to promote producer migration.
    /// </para>
    /// </remarks>
    [Obsolete("The legacy non-prefixed keys are deprecated. Use the prefixed constants instead, for example WeChatMiniProgramNotificationDataKeys.TemplateId. They are kept for read-only fallback to remain compatible with already deployed producers.")]
    public static class Legacy
    {
#pragma warning disable CS0618 // Type or member is obsolete: the nested class references its own members

        /// <summary>
        /// History: Mini Program Template Identity
        /// </summary>
        [Obsolete("The legacy non-prefixed key is deprecated. Use WeChatMiniProgramNotificationDataKeys.TemplateId instead.")]
        public const string TemplateId = "TemplateId";

        /// <summary>
        /// History: Click to jump to the mini program page
        /// </summary>
        [Obsolete("The legacy non-prefixed key is deprecated. Use WeChatMiniProgramNotificationDataKeys.RedirectPage instead.")]
        public const string RedirectPage = "RedirectPage";

        /// <summary>
        /// History: Jump applet type
        /// </summary>
        [Obsolete("The legacy non-prefixed key is deprecated. Use WeChatMiniProgramNotificationDataKeys.WeAppState instead.")]
        public const string WeAppState = "WeAppState";

        /// <summary>
        /// History: Mini Program Language
        /// </summary>
        [Obsolete("The legacy non-prefixed key is deprecated. Use WeChatMiniProgramNotificationDataKeys.WeAppLanguage instead.")]
        public const string WeAppLanguage = "WeAppLanguage";

        /// <summary>
        /// All history keys
        /// </summary>
        public static readonly string[] All =
        {
            TemplateId,
            RedirectPage,
            WeAppState,
            WeAppLanguage
        };

#pragma warning restore CS0618
    }
}
