using LINGYUN.Abp.WeChat.Work.Token.Models;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Token;
/// <summary>
/// Customer contacts application token provider
/// </summary>
/// <remarks>
/// Some interfaces of Enterprise WeChat require the use of customer contact application tokens
/// </remarks>
public interface IWeChatWorkExternalContactTokenProvider
{
    /// <summary>
    /// Get the customer contact application token
    /// </summary>
    /// <remarks>
    /// Reference: https://developer.work.weixin.qq.com/document/path/91039
    /// </remarks>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkToken> GetTokenAsync(CancellationToken cancellationToken = default);
}
