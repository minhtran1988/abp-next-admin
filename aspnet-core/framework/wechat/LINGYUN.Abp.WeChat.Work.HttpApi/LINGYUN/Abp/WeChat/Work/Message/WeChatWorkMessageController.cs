using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Auditing;

namespace LINGYUN.Abp.WeChat.Work.Message;

/// <summary>
/// Enterprise WeChat message processing interface
/// </summary>
/// <remarks>
/// For details, see the Enterprise WeChat document: <seealso cref="https://developer.work.weixin.qq.com/document/path/90930"/>
/// </remarks>
[Controller]
[DisableAuditing]
[RemoteService(Name = AbpWeChatWorkRemoteServiceConsts.RemoteServiceName)]
[Area(AbpWeChatWorkRemoteServiceConsts.ModuleName)]
[Route("api/wechat/work/messages")]
public class WeChatWorkMessageController : AbpControllerBase, IWeChatWorkMessageAppService
{
    private readonly IWeChatWorkMessageAppService _service;

    public WeChatWorkMessageController(IWeChatWorkMessageAppService service)
    {
        _service = service;
    }

    /// <summary>
    /// Enterprise WeChat server message verification callback interface<br />
    /// Verify the validity of messages sent by Enterprise WeChat
    /// </summary>
    /// <param name="agentId">Enterprise internal application identification</param>
    /// <param name="input">Verification message parameters passed by the enterprise WeChat server</param>
    /// <returns></returns>
    [HttpGet]
    public virtual Task<string> Handle([FromQuery] MessageValidationInput input)
    {
        return _service.Handle(input);
    }

    /// <summary>
    /// Enterprise WeChat server business message callback interface<br />
    /// Process business messages sent by the enterprise WeChat server
    /// </summary>
    /// <param name="agentId">Enterprise internal application identification</param>
    /// <param name="input">Message parameters passed by the enterprise WeChat server</param>
    /// <returns></returns>
    [HttpPost]
    public async virtual Task<string> Handle([FromQuery] MessageHandleInput input)
    {
        using var reader = new StreamReader(Request.Body, Encoding.UTF8);
        var content = await reader.ReadToEndAsync();

        input.Data = content;

        return await _service.Handle(input);
    }
}
