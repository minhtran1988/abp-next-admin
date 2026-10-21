using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace LINGYUN.Abp.WeChat.Official.Message;
/// <summary>
/// WeChat message interface
/// </summary>
public interface IWeChatMessageAppService : IApplicationService
{
    /// <summary>
    /// Verify WeChat messages
    /// </summary>
    /// <remarks>
    /// Reference document: <see cref="https://developers.weixin.qq.com/doc/offiaccount/Basic_Information/Access_Overview.html"/>
    /// </remarks>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<string> Handle(MessageValidationInput input);
    /// <summary>
    /// Process WeChat messages
    /// </summary>
    /// <remarks>
    /// Reference document: <see cref="https://developers.weixin.qq.com/doc/offiaccount/Basic_Information/Access_Overview.html"/>
    /// </remarks>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<string> Handle(MessageHandleInput input);
}
