namespace LINGYUN.Abp.Notifications.WeChat.Work;

/// <summary>
/// Enterprise WeChat message carrier type
/// </summary>
/// <remarks>
/// Explicitly declare the form of channel data, replacing the implicit priority of "probe one by field by field" in the old implementation.
/// Producers can therefore clearly express their intentions.
/// </remarks>
public enum WeChatWorkMessageKind
{
    /// <summary>
    /// Unspecified, inferred by channel based on content type
    /// </summary>
    None = 0,
    /// <summary>
    /// Mini program notification message
    /// </summary>
    MiniProgramNotice = 1,
    /// <summary>
    /// Graphic news (mpnews)
    /// </summary>
    MpNews = 2,
    /// <summary>
    /// Template card message
    /// </summary>
    TemplateCard = 3,
    /// <summary>
    /// Text card message
    /// </summary>
    TextCard = 4,
    /// <summary>
    /// Video message
    /// </summary>
    Video = 5,
    /// <summary>
    /// Voice message
    /// </summary>
    Voice = 6,
    /// <summary>
    /// File message
    /// </summary>
    File = 7,
    /// <summary>
    /// Text message
    /// </summary>
    Text = 8,
    /// <summary>
    /// markdown message
    /// </summary>
    Markdown = 9
}
