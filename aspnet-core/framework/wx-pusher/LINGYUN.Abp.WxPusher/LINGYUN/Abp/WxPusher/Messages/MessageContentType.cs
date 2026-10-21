namespace LINGYUN.Abp.WxPusher.Messages;
public enum MessageContentType
{
    /// <summary>
    /// text
    /// </summary>
    Text = 1,
    /// <summary>
    /// html (only send the data inside the body tag, excluding the body tag)
    /// </summary>
    Html = 2,
    /// <summary>
    /// markdown
    /// </summary>
    Markdown = 3
}
