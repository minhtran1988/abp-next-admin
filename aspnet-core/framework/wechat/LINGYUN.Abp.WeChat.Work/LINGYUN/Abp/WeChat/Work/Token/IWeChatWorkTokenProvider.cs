using LINGYUN.Abp.WeChat.Work.Token.Models;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.WeChat.Work.Token;
/// <summary>
/// Enterprise WeChat AccessToken interface
/// </summary>
public interface IWeChatWorkTokenProvider
{
    /// <summary>
    /// Get application token
    /// </summary>
    /// <remarks>
    /// Reference: https://developer.work.weixin.qq.com/document/path/91039
    /// </remarks>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkToken> GetTokenAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Get application token
    /// </summary>
    /// <remarks>
    /// Reference: https://developer.work.weixin.qq.com/document/path/91039
    /// </remarks>
    /// <param name="corpId">Corporate identity</param>
    /// <param name="agentId">Application ID</param>
    /// <param name="secret">Application secret</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkToken> GetTokenAsync(string corpId, string agentId, string secret, CancellationToken cancellationToken = default);
}
