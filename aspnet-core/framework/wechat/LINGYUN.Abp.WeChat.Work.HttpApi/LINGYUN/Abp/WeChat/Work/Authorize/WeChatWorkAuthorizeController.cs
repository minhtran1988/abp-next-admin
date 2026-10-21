using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Auditing;

namespace LINGYUN.Abp.WeChat.Work.Authorize;

/// <summary>
/// Enterprise WeChat web page authorization
/// </summary>
/// <remarks>
/// For details, see the Enterprise WeChat document: <see cref="https://developer.work.weixin.qq.com/document/path/91335"/>
/// </remarks>
[Controller]
[DisableAuditing]
//[RemoteService(false)]
//[ApiExplorerSettings(IgnoreApi = true)]
[RemoteService(Name = AbpWeChatWorkRemoteServiceConsts.RemoteServiceName)]
[Area(AbpWeChatWorkRemoteServiceConsts.ModuleName)]
[Route("api/wechat/work/authorize")]
public class WeChatWorkAuthorizeController : AbpControllerBase, IWeChatWorkAuthorizeAppService
{
    private readonly IWeChatWorkAuthorizeAppService _service;

    public WeChatWorkAuthorizeController(IWeChatWorkAuthorizeAppService service)
    {
        _service = service;
    }

    /// <summary>
    /// Construct web page authorization link
    /// </summary>
    /// <remarks>
    /// For details, see the Enterprise WeChat document: <see cref="https://developer.work.weixin.qq.com/document/path/91022"/>
    /// </remarks>
    /// <param name="urlName">Authorization callback Url name</param>
    /// <param name="responseType">oauth response type</param>
    /// <param name="scope">oauth authorization scope</param>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    [Route("oauth2/generate")]
    public virtual Task<string> GenerateOAuth2AuthorizeAsync(
        [FromQuery] string urlName,
        [FromQuery(Name = "response_type")] string responseType = "code",
        [FromQuery] string scope = "snsapi_base")
    {
        return _service.GenerateOAuth2AuthorizeAsync(responseType, scope);
    }

    /// <summary>
    /// Construct web page authorization link
    /// </summary>
    /// <remarks>
    /// For details, see the Enterprise WeChat document: <see cref="https://developer.work.weixin.qq.com/document/path/91022"/>
    /// </remarks>
    /// <param name="urlName">Authorization callback Url name</param>
    /// <param name="responseType">oauth response type</param>
    /// <param name="scope">oauth authorization scope</param>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    [Route("oauth2")]
    public async virtual Task<IActionResult> OAuth2AuthorizeAsync(
        [FromQuery] string urlName,
        [FromQuery(Name = "response_type")] string responseType = "code",
        [FromQuery] string scope = "snsapi_base")
    {
        var url = await _service.GenerateOAuth2AuthorizeAsync(urlName, responseType, scope);

        return Redirect(url);
    }

    /// <summary>
    /// Construct the enterprise WeChat login link
    /// </summary>
    /// <remarks>
    /// For details, see the Enterprise WeChat document: <see cref="https://developer.work.weixin.qq.com/document/path/98152#api%E6%8E%A5%E5%8F%A3"/>
    /// </remarks>
    /// <param name="urlName">Authorization callback Url name</param>
    /// <param name="loginType">Login type, ServiceApp: service provider login; CorpApp: enterprise self-built / agent-developed application login</param>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    [Route("oauth2/login/generate")]
    public virtual Task<string> GenerateOAuth2LoginAsync(
        string urlName,
        string loginType = "CorpApp")
    {
        return _service.GenerateOAuth2LoginAsync(urlName, loginType);
    }

    /// <summary>
    /// Construct the enterprise WeChat login link
    /// </summary>
    /// <remarks>
    /// For details, see the Enterprise WeChat document: <see cref="https://developer.work.weixin.qq.com/document/path/98152#api%E6%8E%A5%E5%8F%A3"/>
    /// </remarks>
    /// <param name="urlName">Authorization callback Url name</param>
    /// <param name="loginType">Login type, ServiceApp: service provider login; CorpApp: enterprise self-built / agent-developed application login</param>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    [Route("oauth2/login")]
    public async virtual Task<IActionResult> OAuth2LoginAsync(
        string urlName,
        string loginType = "CorpApp")
    {
        var url = await _service.GenerateOAuth2LoginAsync(urlName, loginType);

        return Redirect(url);
    }
}
