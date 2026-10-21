using LINGYUN.Abp.WeChat.Work.Messages.Response;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.WeChat.Work.Messages;
/// <summary>
/// Message sending interface
/// </summary>
public interface IWeChatWorkMessageSender
{
    /// <summary>
    /// Send application message
    /// </summary>
    /// <remarks>
    /// Reference: <see href="https://developer.work.weixin.qq.com/document/path/90236"/>
    /// </remarks>
    /// <param name="message">Enterprise WeChat message carrier inherited from <see cref="WeChatWorkMessage"/></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkMessageResponse> SendAsync(
        WeChatWorkMessage message,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Send group chat message
    /// </summary>
    /// <remarks>
    /// Reference: <see href="https://developer.work.weixin.qq.com/document/path/90248"/>
    /// </remarks>
    /// <param name="message">Enterprise WeChat group chat message carrier inherited from <see cref="WeChatWorkAppChatMessage"/></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkResponse> SendAsync(
        WeChatWorkAppChatMessage message,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Send Webhook message
    /// </summary>
    /// <remarks>
    /// Reference: <see href="https://developer.work.weixin.qq.com/document/path/99110"/>
    /// </remarks>
    /// <param name="webhookKey">webhook Key for message push</param>
    /// <param name="message">Enterprise WeChat Webhook message carrier inherited from <see cref="WeChatWorkWebhookMessage"/></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkResponse> SendAsync(
        string webhookKey,
        WeChatWorkWebhookMessage message,
        CancellationToken cancellationToken = default);
}
