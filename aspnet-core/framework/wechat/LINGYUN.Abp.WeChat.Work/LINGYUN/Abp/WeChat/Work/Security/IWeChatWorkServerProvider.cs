using LINGYUN.Abp.WeChat.Work.Security.Models;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.WeChat.Work.Security;
public interface IWeChatWorkServerProvider
{
    /// <summary>
    /// Get enterprise WeChat domain name IP information
    /// </summary>
    /// <remarks>
    /// Reference: https://developer.work.weixin.qq.com/document/path/100079
    /// </remarks>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatServerDomainResponse> GetWeChatServerAsync(CancellationToken cancellationToken = default);
}
