using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace LINGYUN.Abp.WeChat.Work.Authorize;
public interface IWeChatWorkAuthorizeAppService : IApplicationService
{
    /// <summary>
    /// Generate authorization link
    /// </summary>
    /// <param name="urlName">Authorization callback Url name</param>
    /// <param name="responseType">Response type</param>
    /// <param name="scope">Authorization scope</param>
    /// <returns></returns>
    Task<string> GenerateOAuth2AuthorizeAsync(
        string urlName,
        string responseType = "code",
        string scope = "snsapi_base");
    /// <summary>
    /// Generate login link
    /// </summary>
    /// <param name="urlName">Authorization callback Url name</param>
    /// <param name="loginType">Login type</param>
    /// <returns></returns>
    Task<string> GenerateOAuth2LoginAsync(
        string urlName,
        string loginType = "CorpApp");
}
