namespace LINGYUN.Abp.Notifications.Webhook.WeChat.Work;

/// <summary>
/// Enterprise WeChat Webhook notification data key
/// </summary>
/// <remarks>
/// <para>
/// The prefix <c>wx-work-hook:</c> belongs to the Webhook channel itself.
/// Enterprise WeChat **Application Message** channel uses <c>wx-work:</c> prefix - the two are different transmission methods,
/// The key spaces must be separated, otherwise when the same notification is subscribed by two channels at the same time,
/// The payload will be parsed by both sides and one will be sent each.
/// </para>
/// <para>
/// The reading process is case-insensitive (exact matching takes precedence).
/// </para>
/// </remarks>
public static class WeChatWorkWebhookNotificationDataKeys
{
    /// <summary>
    /// The data key prefix of this channel
    /// </summary>
    public const string Prefix = "wx-work-hook:";

    /// <summary>
    /// Text message
    /// </summary>
    public const string Text = Prefix + "text";
    /// <summary>
    /// markdown message
    /// </summary>
    public const string Markdown = Prefix + "markdown";
    /// <summary>
    /// markdown_v2 message
    /// </summary>
    public const string MarkdownV2 = Prefix + "markdown_v2";
    /// <summary>
    /// Picture message (base64 + md5)
    /// </summary>
    public const string Image = Prefix + "image";
    /// <summary>
    /// Graphic message
    /// </summary>
    public const string News = Prefix + "news";
    /// <summary>
    /// File message
    /// </summary>
    public const string File = Prefix + "file";
    /// <summary>
    /// Voice message
    /// </summary>
    public const string Voice = Prefix + "voice";
    /// <summary>
    /// Template card message
    /// </summary>
    public const string TemplateCard = Prefix + "template_card";
}
