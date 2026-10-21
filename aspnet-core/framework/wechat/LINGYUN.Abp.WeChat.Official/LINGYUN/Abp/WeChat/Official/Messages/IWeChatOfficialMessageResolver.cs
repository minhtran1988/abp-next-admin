using LINGYUN.Abp.WeChat.Common.Messages;
using System.Threading.Tasks;

namespace LINGYUN.Abp.WeChat.Official.Messages;
/// <summary>
/// WeChat public account message parser
/// </summary>
public interface IWeChatOfficialMessageResolver
{
    /// <summary>
    /// Parse WeChat public account messages
    /// </summary>
    /// <param name="messageData">Official account message</param>
    /// <returns></returns>
    Task<MessageResolveResult> ResolveAsync(MessageResolveData messageData);
}
