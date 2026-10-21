namespace LINGYUN.Abp.Notifications.Webhook.WeChat.Work;
public class AbpNotificationsWebhookWeChatWorkOptions
{
    /// <summary>
    /// Whether to use MarkdownV2 format notification when sending Markdown type notification, default: true
    /// </summary>
    /// <remarks>
    /// For details, see: https://developer.work.weixin.qq.com/document/path/99110#markdown-v2%E7%B1%BB%E5%9E%8B
    /// </remarks>
    public bool UseMarkdownV2 {  get; set; }
    public AbpNotificationsWebhookWeChatWorkOptions()
    {
        UseMarkdownV2 = true;
    }
}
