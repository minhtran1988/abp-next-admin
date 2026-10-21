using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.WeChat.MiniProgram.Messages;

/// <summary>
/// Mini program template message
/// Details: https://developers.weixin.qq.com/miniprogram/dev/api-backend/open-api/subscribe-message/subscribeMessage.send.html
/// </summary>
/// <remarks>
/// Only sending subscription messages is implemented for the time being.
/// </remarks>
public interface ISubscribeMessager
{
    /// <summary>
    /// Send subscription message
    /// </summary>
    /// <param name="message"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task SendAsync(SubscribeMessage message, CancellationToken cancellation = default);
    /// <summary>
    /// Send subscription message
    /// </summary>
    /// <param name="toUser">User</param>
    /// <param name="templateId">Template</param>
    /// <param name="page">Jump page</param>
    /// <param name="lang">Language</param>
    /// <param name="state">Type</param>
    /// <param name="data">data</param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task SendAsync(
        Guid toUser, 
        string templateId,
        string? page = "",
        string? lang = "zh_CN",
        string? state = "formal",
        Dictionary<string, object?>? data = null,
        CancellationToken cancellation = default);
}
