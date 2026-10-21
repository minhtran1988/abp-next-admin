using LINGYUN.Abp.WeChat.Common.Messages;
using System.Threading.Tasks;

namespace LINGYUN.Abp.WeChat.Work.Common.Messages;
/// <summary>
/// Enterprise WeChat message parser
/// </summary>
public interface IWeChatWorkMessageResolver
{
    /// <summary>
    /// Parse corporate WeChat messages
    /// </summary>
    /// <param name="messageData">Enterprise WeChat message</param>
    /// <returns></returns>
    Task<MessageResolveResult> ResolveAsync(MessageResolveData messageData);
}
