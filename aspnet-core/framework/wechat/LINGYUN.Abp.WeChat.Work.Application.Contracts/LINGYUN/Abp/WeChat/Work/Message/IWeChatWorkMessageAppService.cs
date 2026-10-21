using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace LINGYUN.Abp.WeChat.Work.Message;
/// <summary>
/// Enterprise WeChat messaging interface
/// </summary>
public interface IWeChatWorkMessageAppService : IApplicationService
{
    /// <summary>
    /// Verify enterprise WeChat messages
    /// </summary>
    /// <remarks>
    /// Reference document: <see cref="https://developer.work.weixin.qq.com/document/path/90238"/>
    /// </remarks>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<string> Handle(MessageValidationInput input);
    /// <summary>
    /// Process corporate WeChat messages
    /// </summary>
    /// <remarks>
    /// Reference document: <see cref="https://developer.work.weixin.qq.com/document/path/90238"/>
    /// </remarks>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<string> Handle(MessageHandleInput input);
}
