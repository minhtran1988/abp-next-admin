using System.Threading.Tasks;

namespace LINGYUN.Abp.WeChat.Work.Authorize;
public interface IWeChatWorkAuthorizeGenerator
{
    /// <summary>
    /// Construct web page authorization link
    /// </summary>
    /// <remarks>
    /// Reference: <see href="https://developer.work.weixin.qq.com/document/path/91022"/>
    /// </remarks>
    /// <param name="redirectUri"></param>
    /// <param name="state"></param>
    /// <param name="responseType"></param>
    /// <param name="scope"></param>
    /// <returns></returns>
    Task<string> GenerateOAuth2AuthorizeAsync(
        string redirectUri,
        string state,
        string? responseType = "code",
        string? scope = "snsapi_base");
    /// <summary>
    /// Build web page login link
    /// </summary>
    /// <param name="redirectUri"></param>
    /// <param name="state"></param>
    /// <param name="loginType"></param>
    /// <param name="agentId"></param>
    /// <param name="lang"></param>
    /// <returns></returns>
    Task<string> GenerateOAuth2LoginAsync(
        string redirectUri,
        string state,
        string? loginType = "ServiceApp",
        string? agentId = "",
        string? lang = "zh");
}
