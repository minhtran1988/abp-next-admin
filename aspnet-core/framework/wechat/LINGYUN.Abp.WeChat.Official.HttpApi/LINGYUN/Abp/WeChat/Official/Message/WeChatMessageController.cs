using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace LINGYUN.Abp.WeChat.Official.Message;

/// <summary>
/// WeChat public account message processing interface
/// </summary>
/// <remarks>
/// For details, please see the WeChat official account document: <seealso cref="https://developers.weixin.qq.com/doc/offiaccount/Basic_Information/Access_Overview.html"/>
/// </remarks>
[Controller]
[RemoteService(Name = AbpWeChatOfficialRemoteServiceConsts.RemoteServiceName)]
[Area(AbpWeChatOfficialRemoteServiceConsts.ModuleName)]
[Route("api/wechat/official/messages")]
public class WeChatMessageController : AbpControllerBase, IWeChatMessageAppService
{
    private readonly IWeChatMessageAppService _service;

    public WeChatMessageController(IWeChatMessageAppService service)
    {
        _service = service;
    }
    /// <summary>
    /// WeChat public account server message verification callback interface<br />
    /// Verify the validity of the message sent by the WeChat official account server
    /// </summary>
    /// <param name="input">Verification message parameters passed by the WeChat official account server</param>
    /// <returns></returns>
    [HttpGet]
    public virtual Task<string> Handle([FromQuery] MessageValidationInput input)
    {
        return _service.Handle(input);
    }

    /// <summary>
    /// WeChat public account server business message callback interface<br />
    /// Process business messages sent by the WeChat public account server
    /// </summary>
    /// <param name="input">Message parameters passed by the WeChat official account server</param>
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
