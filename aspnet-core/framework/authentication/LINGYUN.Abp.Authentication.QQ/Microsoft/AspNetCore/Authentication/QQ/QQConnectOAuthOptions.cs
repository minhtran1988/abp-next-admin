using LINGYUN.Abp.Authentication.QQ;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Microsoft.AspNetCore.Authentication.QQ;

public class QQConnectOAuthOptions : OAuthOptions
{
    /// <summary>
    /// Whether to use mobile style
    /// </summary>
    public bool IsMobile { get; set; }
    /// <summary>
    /// Get user OpenID_OAuth2.0
    /// </summary>
    public string OpenIdEndpoint { get; set; }

    public QQConnectOAuthOptions()
    {
        // Used to prevent initialization errors, it will be rewritten in OAuthHandler.InitializeHandlerAsync
        ClientId = "QQConnect";
        ClientSecret = "QQConnect";

        ClaimsIssuer = "connect.qq.com";
        CallbackPath = new PathString(AbpAuthenticationQQConsts.CallbackPath);

        AuthorizationEndpoint = "https://graph.qq.com/oauth2.0/authorize";
        TokenEndpoint = "https://graph.qq.com/oauth2.0/token";
        OpenIdEndpoint = "https://graph.qq.com/oauth2.0/me";
        UserInformationEndpoint = "https://graph.qq.com/user/get_user_info";

        Scope.Add("get_user_info");

        // This original attribute must be written in, and the framework association determines whether to bind QQ.
        ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "openid");
        ClaimActions.MapJsonKey(ClaimTypes.Name, "nickname");

        // Write the custom identity into the token
        ClaimActions.MapJsonKey(AbpQQClaimTypes.OpenId, "openid");
        ClaimActions.MapJsonKey(AbpQQClaimTypes.NickName, "nickname");
        ClaimActions.MapJsonKey(AbpQQClaimTypes.Gender, "gender");
        ClaimActions.MapJsonKey(AbpQQClaimTypes.AvatarUrl, "figureurl_qq_1");
    }
}
