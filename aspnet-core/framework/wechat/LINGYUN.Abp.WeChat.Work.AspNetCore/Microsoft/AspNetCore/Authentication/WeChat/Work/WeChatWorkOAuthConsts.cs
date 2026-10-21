using LINGYUN.Abp.WeChat.Work;

namespace Microsoft.AspNetCore.Authentication.WeChat.Work;

public static class WeChatWorkOAuthConsts
{
    /// <summary>
    /// Enterprise WeChat authorized name
    /// </summary>
    public static string AuthenticationScheme => AbpWeChatWorkGlobalConsts.ProviderName;
    /// <summary>
    /// WeChat provider display name
    /// </summary>
    public static string DisplayName => AbpWeChatWorkGlobalConsts.DisplayName;
    /// <summary>
    /// Callback address
    /// </summary>
    public static string CallbackPath { get; set; } = "/signin-wxwork";
    /// <summary>
    /// Web page login in WeChat client
    /// </summary>
    public const string AuthorizationEndpoint = "https://open.weixin.qq.com/connect/oauth2/authorize";
    /// <summary>
    /// Web page login
    /// </summary>
    public const string AuthorizationSsoEndpoint = "https://login.work.weixin.qq.com/wwlogin/sso/login";
    /// <summary>
    /// After the user allows authorization, the returned code is exchanged for the access_token address.
    /// </summary>
    public const string TokenEndpoint = "https://qyapi.weixin.qq.com/cgi-bin/gettoken";

    /// <summary>
    /// Use access_token to obtain the user's personal information address
    /// </summary>
    public const string UserInfoEndpoint = "https://qyapi.weixin.qq.com/cgi-bin/auth/getuserinfo";
    /// <summary>
    /// Use access_token to obtain the user's sensitive information address
    /// </summary>
    public const string UserDetailEndpoint = "https://qyapi.weixin.qq.com/cgi-bin/auth/getuserdetail";

    public const string UserInfoScope = "snsapi_privateinfo";

    public const string LoginScope = "snsapi_base";
}
