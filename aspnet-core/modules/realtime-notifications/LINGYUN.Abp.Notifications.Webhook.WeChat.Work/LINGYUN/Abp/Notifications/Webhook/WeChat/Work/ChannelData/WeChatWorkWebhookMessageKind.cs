namespace LINGYUN.Abp.Notifications.Webhook.WeChat.Work;

/// <summary>
/// Enterprise WeChat Webhook message carrier type
/// </summary>
/// <remarks>
/// The value is consistent with the framework <c>WeChatWorkWebhookMessage.MsgType</c>.
/// </remarks>
public enum WeChatWorkWebhookMessageKind
{
    /// <summary>
    /// not specified
    /// </summary>
    None = 0,
    /// <summary>
    /// text
    /// </summary>
    Text = 1,
    /// <summary>
    /// markdown
    /// </summary>
    Markdown = 2,
    /// <summary>
    /// markdown_v2
    /// </summary>
    MarkdownV2 = 3,
    /// <summary>
    /// Picture
    /// </summary>
    Image = 4,
    /// <summary>
    /// Graphics and text
    /// </summary>
    News = 5,
    /// <summary>
    /// File
    /// </summary>
    File = 6,
    /// <summary>
    /// Voice
    /// </summary>
    Voice = 7,
    /// <summary>
    /// Template card
    /// </summary>
    TemplateCard = 8
}
