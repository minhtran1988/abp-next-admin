using LINGYUN.Abp.PushPlus.Channel;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.PushPlus.Setting;
/// <summary>
/// Sending channel interface
/// </summary>
public interface IPushPlusChannelProvider
{
    /// <summary>
    /// Get the default sending channel
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PushPlusChannel> GetDefaultChannelAsync(
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Modify the default sending channel
    /// </summary>
    /// <param name="defaultChannel">Default channel; wechat-WeChat public account, mail-mail, cp-enterprise WeChat application, webhook-third-party webhook</param>
    /// <param name="defaultWebhook">Channel parameters; webhook and cp channels need to fill in the specific webhook number or custom code</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task UpdateDefaultChannelAsync(
        PushPlusChannelType defaultChannel,
        string defaultWebhook = "",
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Modify the message receiving limit
    /// </summary>
    /// <param name="recevieLimit">Receive message limit; 0-Receive all, 1-Do not receive messages</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task UpdateRecevieLimitAsync(
        PushPlusChannelRecevieLimit recevieLimit,
        CancellationToken cancellationToken = default);
}
