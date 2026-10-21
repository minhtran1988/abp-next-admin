using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.PushPlus.Channel.Webhook;
/// <summary>
/// Webhook configuration interface
/// </summary>
public interface IPushPlusWebhookProvider
{
    /// <summary>
    /// Get the webhook list
    /// </summary>
    /// <param name="current">Current page number</param>
    /// <param name="pageSize">The size of each page, the maximum value is 50</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PushPlusPagedResponse<PushPlusWebhook>> GetWebhookListAsync(
        int current = 1,
        int pageSize = 20,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// webhook details
    /// </summary>
    /// <param name="webhookId">webhook number</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PushPlusWebhook> GetWebhookAsync(
        int webhookId,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Add new webhook
    /// </summary>
    /// <param name="webhookCode">webhook encoding</param>
    /// <param name="webhookName">webhook name</param>
    /// <param name="webhookType">webhook type; 1-Enterprise WeChat, 2-DingTalk, 3-Feishu, 4-server Jiang</param>
    /// <param name="webhookUrl">URL address to call</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> CreateWebhookAsync(
        string webhookCode,
        string webhookName,
        PushPlusWebhookType webhookType,
        string webhookUrl,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Modify webhook configuration
    /// </summary>
    /// <param name="id">webhook number</param>
    /// <param name="webhookCode">webhook encoding</param>
    /// <param name="webhookName">webhook name</param>
    /// <param name="webhookType">webhook type; 1-Enterprise WeChat, 2-DingTalk, 3-Feishu, 4-server Jiang</param>
    /// <param name="webhookUrl">URL address to call</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<string> UpdateWebhookAsync(
        int id,
        string webhookCode,
        string webhookName,
        PushPlusWebhookType webhookType,
        string webhookUrl,
        CancellationToken cancellationToken = default);
}
