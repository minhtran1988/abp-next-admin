using System.Threading.Tasks;

namespace LINGYUN.Abp.WeChat.OpenId;

public interface IWeChatOpenIdFinder
{
    Task<WeChatOpenId?> FindAsync(string code, string appId, string appSecret);
    /// <summary>
    /// Get the current logged in user OpenId
    /// </summary>
    /// <param name="appId">Application ID</param>
    /// <returns></returns>
    /// <exception cref="Volo.Abp.Authorization.AbpAuthorizationException">When the user is not logged in</exception>
    /// <exception cref="Volo.Abp.AbpException">When WeChat sessionKey expires</exception>
    Task<WeChatOpenId?> FindAsync(string appId);
}
