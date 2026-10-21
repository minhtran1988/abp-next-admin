using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.WeChat.Work.Messages;
/// <summary>
/// Enterprise WeChat Message Manager
/// </summary>
public interface IWeChatWorkMessageManager
{
    /// <summary>
    /// Withdraw application message
    /// </summary>
    /// <remarks>
    /// Reference: <see href="https://developer.work.weixin.qq.com/document/path/94867"/>
    /// </remarks>
    /// <param name="messageId">Message ID. Obtained from the application sending message interface <see cref="IWeChatWorkMessageSender"/>. </param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> ReCallMessageAsync( string messageId, CancellationToken cancellationToken = default);

}
