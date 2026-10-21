using LINGYUN.Abp.WeChat.Work.Token.Models;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Token;
/// <summary>
/// Address book application token provider
/// </summary>
/// <remarks>
/// Some interfaces of Enterprise WeChat require the use of address book application token.
/// </remarks>
public interface IWeChatWorkContactTokenProvider
{
    /// <summary>
    /// Get the address book application token
    /// </summary>
    /// <remarks>
    /// Reference: https://developer.work.weixin.qq.com/document/path/91039
    /// </remarks>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkToken> GetTokenAsync(CancellationToken cancellationToken = default);
}
